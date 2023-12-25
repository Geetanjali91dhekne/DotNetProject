using AuthenticationService.Models;
using AuthenticationService.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Runtime;
using ConfigurationSettings = System.Configuration.ConfigurationManager;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using System.Data;
using System;

namespace AuthenticationService.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        
        private readonly IConfiguration _config;
        private readonly RespondModel respondModel;
        private readonly FleetManagerContext _context;



        public LoginRepository( IConfiguration config, FleetManagerContext context)
        {
           
            _config = config;
            this.respondModel = new RespondModel();
            _context = context;

        }

        public async Task<RespondModel> GetLoginUser(string? userId, string? password)
        {
           

  try
 {
     var login = new Login();
     var settings = _config.GetSection("Settings").Get<Settings>();
     
     

     if (string.IsNullOrEmpty(userId))
     {
         var windowsIdentity = WindowsIdentity.GetCurrent();
         userId = windowsIdentity.Name?.Split('\\')[1];
     }


                if (!string.IsNullOrEmpty(userId))
                {
                    login.domainId = userId;
                    login.password = string.IsNullOrEmpty(password) ? "" : password;
                    login.appType = settings.AppType!;
                    login.url = settings.Url!;
                }
                else
                {
                    respondModel.message = "Invalid Credentials!";
                    return respondModel;
                }

                var user = await GetUserDetails(login);
                 
                if (user == null)
                {
                    respondModel.message = "Invalid Credentials!";
                    return respondModel;
                }

                var permissions = GetUserPermissions(user);

                if (permissions == null)
                {
                    respondModel.message = "User has no permissions!";
                    return respondModel;
                }
               
                var token = CreateToken(login, user);

                if (token == null)
                {
                    respondModel.message = "Cannot create token!";
                    return respondModel;
                }

                respondModel.code = Constants.httpCodeSuccess;
                respondModel.data = new
                {
                    empCode = login.domainId,
                    userName = login.userName,
                    authenticationToken = token,
                    permissions
                };
                respondModel.status = true;
                respondModel.message = "Login Successful!";
            }
            catch (Exception ex)
            {
                respondModel.message = ex.Message;
            }

            return respondModel;
        }

        private List<object> GetUserPermissions(List<RolePermissionList> rolePermissionLists)
        {
            var permissions = new List<object>();

            foreach (var rolePermission in rolePermissionLists)
            {
                var permissionObj = new
                {
                    roleId = rolePermission.RoleId,
                    role = rolePermission.Role,
                    permission = rolePermission.Permission.Select(permission => new
                    {
                        id = permission.Id,
                        entityCategory = permission.EntityCategory,
                        entityName = permission.EntityName,
                        actions = permission.Actions
                    }).ToList()
                };

                permissions.Add(permissionObj);
            }

            return permissions;
        }
        public async Task<List<RolePermissionList>> GetUserDetails(Login _login)
        {
            try
            {
               

               string domainId = Base64Encode(_login.domainId);
                
                string password = _login.password;
                string appType = _login.appType;
                string URI = _login.url!;

                //string myParameters = $"domainId={domainId}&password={password}&appType={appType}";
                string myParameters = $"domainId={domainId}&password={password}";


                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol =
                      SecurityProtocolType.Tls
                    | SecurityProtocolType.Tls11
                    | SecurityProtocolType.Tls12;

                

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");


                    var content = new StringContent(myParameters, Encoding.UTF8, "application/x-www-form-urlencoded");

                    HttpResponseMessage response =await httpClient.PostAsync(URI, content);



                        if (response.IsSuccessStatusCode)
                        {
                            string htmlResult = await response.Content.ReadAsStringAsync();

                            //    using (WebClient wc = new WebClient())
                            //{
                            //    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                            //    string htmlResult = wc.UploadString(URI, myParameters);

                            var userResult = ConvertUserDetailsToJson(htmlResult);

                            if (userResult != null && userResult.IsActive)
                            {
                                var groupRoleList = _context.TGroupRoleMappings.ToList();
                                var roleLists = new List<RoleList>();
                                _login.userName = userResult.Name;
                                foreach (string group in userResult.Groups)
                                {
                                    var matchingRoles = groupRoleList
                                        .Where(groupid => group.Contains(groupid.GroupCode))
                                        .Select(groupid => _context.TRoleMasters.FirstOrDefault(d => d.Id == groupid.FkRoleId))
                                        .Where(role => role != null);

                                    roleLists.AddRange(matchingRoles.Select(role => new RoleList
                                    {
                                        RoleName = role.RoleName,
                                        RoleId = role.Id
                                    }));
                                }

                                var distinctRoles = roleLists.DistinctBy(role => role.RoleName).ToList();

                                if (distinctRoles == null || distinctRoles.Count == 0)
                                {
                                distinctRoles = new List<RoleList>
                                    {
                                        new RoleList
                                        {
                                            RoleName = "Default Login",
                                            RoleId = _context.TRoleMasters.Where(d => d.RoleName == "Default Login").Select(i => i.Id).FirstOrDefault()
                                        }
                                    };
                                }

                                var rolePermissionLists = new List<RolePermissionList>();

                                foreach (var role in distinctRoles)
                                {
                                    var permissionList = _context.TRolePermissionMappings
                                        .Where(c => c.FkRoleId == role.RoleId)
                                        .ToList();

                                    var permissionObjList = permissionList
                                        .Select(permission => _context.TPermissions
                                            .Where(p => p.Id == permission.FkPermissionId)
                                            .Select(permissionDetails => new Permissions
                                            {
                                                Id = permissionDetails.Id,
                                                EntityCategory = permissionDetails.EntityCategory,
                                                EntityName = permissionDetails.EntityName,
                                                Actions = permissionDetails.Actions,
                                            })
                                            .FirstOrDefault())
                                        .ToList();

                                    var roleNameList = _context.TRoleMasters
                                        .Where(p => p.Id == role.RoleId)
                                        .ToList();

                                    var rolePermission = new RolePermissionList
                                    {

                                        RoleId = role.RoleId,
                                        Role = roleNameList.FirstOrDefault()?.RoleName,
                                        Permission = permissionObjList
                                    };

                                    rolePermissionLists.Add(rolePermission);
                                }

                                return rolePermissionLists;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    return null;
                
            }
            catch (Exception)
            {
                return null;
            }
        }

        // For Converting to Base64 string
        #region encoding domainid
        private static string Base64Encode(string text)
        {
            try
            {
                var textBytes = System.Text.Encoding.UTF8.GetBytes(text);
                return System.Convert.ToBase64String(textBytes);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion encoding domainid

        // Conerting JSON result
        #region converting string to json

        private User ConvertUserDetailsToJson(string text)
        {
           
            
                return JsonConvert.DeserializeObject<User>(text);
            
            
        }

        #endregion converting string to json

        #region create token


        private string CreateToken(Login login, List<RolePermissionList> rolePermissionLists)
        {

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("G3VF4C6KFV43JH6GKCDFGJH45V36JHGV3H4C6F3GJC63HG45GH6V345GHHJ4623FJL3HCVMO1P23PZ07W8");
                var issuer = "arbems.com";
                var audience = "Public";
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login.domainId),
                new Claim(ClaimTypes.NameIdentifier,login.userName)
            };

                foreach (var rolePermission in rolePermissionLists)
                {

                    claims.Add(new Claim("Role", rolePermission.Role));

                }

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = issuer,
                    Audience = audience,
                    Subject = new ClaimsIdentity(claims.ToArray()),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return (tokenHandler.WriteToken(token));
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
        }
        #endregion create token

    }
}
