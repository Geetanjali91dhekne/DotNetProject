using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using UsersManagementService.CommonFiles;
using UsersManagementService.Models;
using UsersManagementService.ViewModels;

namespace UsersManagementService.Repositories
{
    public class UserRepository : IUserRepository   
    {
        private readonly FleetManagerContext _context;
        private readonly CrmsContext crmsContext;
        private readonly ResponseModel responseModel;

        public UserRepository(FleetManagerContext mntFleetContext, CrmsContext crmsContext)
        {
            _context = mntFleetContext;
            this.responseModel = new ResponseModel();
            this.crmsContext = crmsContext;
        }

        public ResponseModel AddGroup(string groupCode, List<RoleMaster> roleList, string username)
        {
            try
            {
                //check if all the roles are avialble or not --> NOT required I think
                var groupList = _context.TGroupRoleMappings
                    .Where(u => u.GroupCode == groupCode).ToList();
                if (groupList.Count != 0)
                {
                    _context.TGroupRoleMappings.RemoveRange(groupList);
                    _context.SaveChanges();
                }

                
                var newGroupList = roleList.Select(role => new TGroupRoleMapping
                {
                    GroupCode = groupCode,
                    FkRoleId = role.Id,
                    CreatedBy = username,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = username,
                    ModifiedDate = DateTime.Now,
                    Status = role.Status,
                }).ToList();

                _context.TGroupRoleMappings.AddRange(newGroupList);
                _context.SaveChanges();

                var createNewGroupList = CreateTGroupRoleMappingDTO(newGroupList);

                return GetResponseModel(Constants.httpCodeSuccess, createNewGroupList,
                    "Group Added Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel DeleteGroupByGroupCode(string groupCode)
        {
            try
            {
                var groupList = _context.TGroupRoleMappings.Where(u => u.GroupCode == groupCode).ToList();
                if (groupList.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null,
                        "Group Not found", false);
                }
                else
                {
                    _context.TGroupRoleMappings.RemoveRange(groupList);
                    _context.SaveChanges();
                }

                var deleteGroup = CreateTGroupRoleMappingDTO(groupList);

                return GetResponseModel(Constants.httpCodeSuccess, deleteGroup,
                        "Group Deleted Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetGroupDetailsByGroupCode(string groupCode)
        {
            try
            {
                var groupList = _context.TGroupRoleMappings.Where(u => u.GroupCode == groupCode).ToList();
                if (groupList.Count == 0)
                {
                    GetResponseModel(Constants.httpCodeSuccess, null, 
                        "Group Not found", false);
                }

                var roleList = _context.TRoleMasters.ToList();
                var roleListAssigned = CreateTRoleMasterAssigned(roleList);

                foreach (var group in groupList)
                {
                    var roleDTO = roleListAssigned.FirstOrDefault(dto => dto.Id == group.FkRoleId);
                    if (roleDTO != null)
                    {
                        roleDTO.IsAssigned = true;
                    }
                }

                return GetResponseModel(Constants.httpCodeSuccess, roleListAssigned, 
                    "data", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetGroupList()
        {
            try
            {
                var groupList = _context.GroupMasters.Select(p=>p.GroupCode).ToList();
                if (groupList.Count == 0)
				{
					return GetResponseModel(Constants.httpCodeSuccess, null, 
                        "Group Not found", false);
				}
               
                return GetResponseModel(Constants.httpCodeSuccess, groupList,
                        "All groups Listed", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetUserRoleMapping()
        {
            try
            {
                var userRoles = _context.TUserRoleMappings
                    .GroupBy(mapping => mapping.Username)
                    .Select(group => new
                    {
                        Username = group.Key,
                        Roles = group.Join(
                            _context.TRoleMasters,
                            mapping => mapping.FkRoleId, 
                            role => role.Id,
                            (mapping, role) => role.RoleName 
                        ).ToList()
                    })
                    .ToList();

                if (userRoles.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "User Roles Not Available", false);
                }

                return GetResponseModel(Constants.httpCodeSuccess, userRoles, "User Roles Data", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetAllUserSiteMapping()
        {
            try
            {
                var UserSiteList = _context.TUserSiteMappings
                     .Select(d => new
                     {
                         Username = d.Username,
                         EmployeeCode = d.EmployeeCode,
                         EmployeeEmail = d.EmployeeEmail,
                         RoleName = (_context.TRoleMasters.FirstOrDefault(e => e.Id == d.FkRoleId) != null)
                             ? _context.TRoleMasters.First(e => e.Id == d.FkRoleId).RoleName
                             : ""
                     })
                     .GroupBy(d => d.Username)
                     .Select(group => group.First())
                     .ToList();




                if (UserSiteList.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "User Not Available", false);
                }

                return GetResponseModel(Constants.httpCodeSuccess, UserSiteList, "UserSite data retrieved successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetSiteByUser(string? userName, string? employeeCode, string? countryName = null, string? stateName = null, string? areaName = null, string? siteName = null)
        {
            try
            {
                ListUserSiteResponse response = new ListUserSiteResponse();
                List<UserSiteResponse> allUserSites = new List<UserSiteResponse>();

                if (string.IsNullOrEmpty(userName))
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "User name is required", false);
                }

                var getUser=userName;
                var getEmployeeCode = employeeCode;
                var getSiteList = crmsContext.ScMainSites.ToList();
                var existingData = _context.TUserSiteMappings?.FirstOrDefault(d => d.Username == getUser);
                var userSitesDictionary = new Dictionary<string, UserSiteResponse>();
                
                if(existingData != null)
                {
                    response.roleObj = _context.TRoleMasters?.FirstOrDefault(d => d.Id == existingData.FkRoleId);
                }

                foreach (var site in getSiteList)
                {
                    UserSiteResponse userSiteResponse = new UserSiteResponse();
                   
                    var getAreaObject = crmsContext.ScAreas.SingleOrDefault(i => i.AreaCode == site.AreaCode);
                    var getStateObject = crmsContext.ScStates.SingleOrDefault(i => i.StateCode == getAreaObject.StateCode);
                    var getCountryObject = crmsContext.ScCountries.SingleOrDefault(i => i.CountryCode == getStateObject.CountryCode);
                    userSiteResponse.UserName = userName;
                    userSiteResponse.RoleId = existingData?.FkRoleId;
                    userSiteResponse.SiteName = site.MainSite;
                    userSiteResponse.CountryName = getCountryObject.Country;
                    userSiteResponse.StateName = getStateObject.State;
                    userSiteResponse.AreaName = getAreaObject.Area;
                    userSiteResponse.Status = "InActive";
                    userSiteResponse.EmployeeCode = employeeCode;

                    
                    if (countryName != null && !string.IsNullOrEmpty(countryName) && getCountryObject.Country != countryName)
                        continue;

                    if (stateName != null && !string.IsNullOrEmpty(stateName) && getStateObject.State != stateName)
                        continue;

                    if (areaName != null && !string.IsNullOrEmpty(areaName) && getAreaObject.Area != areaName)
                        continue;

                    if (siteName != null && !string.IsNullOrEmpty(siteName) && site.MainSite != siteName)
                        continue;


                    userSitesDictionary[site.MainSiteCode] = userSiteResponse;
                }

                var allSiteCode = _context.TUserSiteMappings
                    .Where(d => d.Username == userName && d.EmployeeCode == employeeCode && d.SiteCode != null)
                    .ToList();

                foreach (var siteMapping in allSiteCode)
                {
                    if (userSitesDictionary.TryGetValue(siteMapping.SiteCode, out var userSiteResponse))
                    {
                        userSiteResponse.Status = "Active";
                    }
                }

                //var convertToResponse = ResponseGetSiteUser(userSitesDictionary.Values.ToList(),existingData.FkRoleId);

                allUserSites.AddRange(userSitesDictionary.Values);

                response.UserSiteResponses = allUserSites;

                return GetResponseModel(Constants.httpCodeSuccess, response, "data", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        //private List<GetUserSiteResponse> ResponseGetSiteUser(List<UserSiteResponse> existingData, int? roleId)
        //{
        //    List<GetUserSiteResponse> updateAllData = new List<GetUserSiteResponse>();
        //    foreach (var list in existingData)
        //    {
        //        GetUserSiteResponse updateData = new GetUserSiteResponse();
        //        updateData.UserName = list.UserName;
        //        updateData.AreaName = list.AreaName;
        //        updateData.StateName = list.StateName;
        //        updateData.SiteName = list.SiteName;
        //        updateData.CountryName = list.CountryName;
        //        updateData.Status = list.Status;
        //        updateData.EmployeeCode = list.EmployeeCode;
        //        updateData.Role
        //        updateAllData.Add(updateData);
        //    };
        //    return updateAllData;
        //}
        public ResponseModel UpdateUserSite(List<UserSiteResponse> userSiteResponseList, string? userName)
        {
            try
            {
                if (userName == null)
                {
                    userName = userSiteResponseList.Count > 0 ? userSiteResponseList[0].UserName : string.Empty;
                }

                List<TUserSiteMapping> userSiteMappingList = new List<TUserSiteMapping>();

                var existingUserSiteList = _context.TUserSiteMappings
                    .Where(d => d.Username == userName).ToList();

                foreach (var list in userSiteResponseList)
                {
                    var roleId = _context.TRoleMasters?.FirstOrDefault(d => d.Id == list.RoleId);
                    var countryCode = crmsContext.ScCountries?.SingleOrDefault(d => d.Country == list.CountryName);
                    var stateCode = crmsContext.ScStates?.SingleOrDefault(d => d.State == list.StateName);
                    var areaCode = crmsContext.ScAreas?.SingleOrDefault(d => d.Area == list.AreaName && d.StateCode == stateCode.StateCode);
                    var siteCode = crmsContext.ScMainSites?.SingleOrDefault(d => d.MainSite == list.SiteName);

                    if (countryCode == null || stateCode == null || areaCode == null || siteCode == null)
                    {
                        continue; // Skip this iteration if any code is null
                    }

                    var checkUserSite = existingUserSiteList.FirstOrDefault(d =>
                        d.Username == userName &&
                        d.CountryCode == countryCode.CountryCode &&
                        d.StateCode == stateCode.StateCode &&
                        d.AreaCode == areaCode.AreaCode &&
                        d.SiteCode == siteCode.MainSiteCode &&
                        d.FkRoleId==roleId.Id);

                    if (checkUserSite != null)
                    {
                        var updateUserSite = UpdateUserSites(checkUserSite, list, countryCode, stateCode, areaCode, siteCode,roleId);
                        updateUserSite.ModifiedBy = userName;
                        updateUserSite.ModifiedDate = DateTime.Now;
                        userSiteMappingList.Add(updateUserSite);
                    }
                    else
                    {
                        var createUserSite = CreateUserSite(list, countryCode, stateCode, areaCode, siteCode,roleId);
                        createUserSite.CreatedBy = userName;
                        createUserSite.ModifiedBy = userName;
                        createUserSite.CreatedDate = DateTime.Now;
                        createUserSite.ModifiedDate = DateTime.Now;
                        userSiteMappingList.Add(createUserSite);
                        _context.TUserSiteMappings.Add(createUserSite);
                    }
                }

                var itemsToKeep = new List<TUserSiteMapping>();

                foreach (var responseList in userSiteResponseList)
                {
                    var roleId = _context.TRoleMasters?.FirstOrDefault(d => d.Id == responseList.RoleId);
                    var countryCode = crmsContext.ScCountries?.SingleOrDefault(d => d.Country == responseList.CountryName);
                    var stateCode = crmsContext.ScStates?.SingleOrDefault(d => d.State == responseList.StateName);
                    var areaCode = crmsContext.ScAreas?.SingleOrDefault(d => d.Area == responseList.AreaName && d.StateCode == stateCode.StateCode);
                    var siteCode = crmsContext.ScMainSites?.SingleOrDefault(d => d.MainSite == responseList.SiteName);

                   var matchingItem = existingUserSiteList.FirstOrDefault(existingList =>
                        existingList.Username == userName &&
                        existingList.CountryCode == countryCode?.CountryCode &&
                        existingList.StateCode == stateCode?.StateCode &&
                        existingList.AreaCode == areaCode?.AreaCode &&
                        existingList.SiteCode == siteCode?.MainSiteCode &&
                        existingList.Status == responseList?.Status &&
                        existingList.FkRoleId==responseList?.RoleId);

                    if (matchingItem != null)
                    {
                        itemsToKeep.Add(matchingItem);
                    }
                }

                // Remove items that are not in the list of items to keep
                var itemsToRemove = existingUserSiteList.Except(itemsToKeep).ToList();

                foreach (var itemToRemove in itemsToRemove)
                {
                    _context.TUserSiteMappings.Remove(itemToRemove);
                }

                _context.SaveChanges();
                return GetResponseModel(Constants.httpCodeSuccess, userSiteMappingList, "Data Received Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetScCountryOmsPbi()
        {
            try
            {
                var allDataList = crmsContext.ScCountries.ToList();
                if (allDataList.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "No Data Found.", false);
                }
                return GetResponseModel(Constants.httpCodeSuccess, allDataList, "All Data Retreive Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetStateByCountryCode(string? countryCode)
        {
            try
            {
                var state = crmsContext.ScStates.Where(a => a.CountryCode == countryCode).ToList();
                if (state == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null,
                        " No States Available", false);
                }
                var stateData = state.Select(state => new
                {
                    state.StateCode,
                    state.State
                }).ToList();
                return GetResponseModel(Constants.httpCodeSuccess, stateData,
                        "data", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetAreaByStateCode(string? stateCode)
        {
            try
            {
                var area = crmsContext.ScAreas.Where(a => a.StateCode == stateCode).ToList();
                if (area == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null,
                        " No Areas Available", false);
                }
                var areaData = area.Select(area => new
                {
                    area.AreaCode,
                    area.Area
                }).ToList();
                return GetResponseModel(Constants.httpCodeSuccess, areaData,
                        "data", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetSiteByAreaCode(string? areaCode)
        {
            try
            {
                var site = crmsContext.ScMainSites.Where(a => a.AreaCode == areaCode).ToList();
                if (site == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null,
                        " No Sites Available", false);
                }
                var siteData = site.Select(site => new
                {
                    site.MainSiteCode,
                    site.MainSite
                }).ToList();
                return GetResponseModel(Constants.httpCodeSuccess, siteData,
                        "data", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        private TUserSiteMapping UpdateUserSites(TUserSiteMapping existingUser, UserSiteResponse updateUser, ScCountry scCountry,
            ScState scState, ScArea scArea, ScMainSite scMainSite, TRoleMaster roleId)
        {
            existingUser.Username = updateUser.UserName;
            existingUser.CountryCode = scCountry.CountryCode;
            existingUser.AreaCode = scArea.AreaCode;
            existingUser.StateCode = scState.StateCode;
            existingUser.SiteCode = scMainSite.MainSiteCode;
            existingUser.Status = updateUser.Status;
            existingUser.Id = existingUser.Id;
            existingUser.CreatedBy = existingUser.CreatedBy;
            existingUser.CreatedDate = existingUser.CreatedDate;
            existingUser.EmployeeCode = updateUser.EmployeeCode;
            existingUser.FkRoleId = roleId.Id;
            existingUser.EmployeeEmail = updateUser.EmployeeEmail;
            return existingUser;
        }

        private TUserSiteMapping CreateUserSite(UserSiteResponse createUserSite, ScCountry scCountry,
            ScState scState,ScArea scArea, ScMainSite scMainSite,TRoleMaster roleId)
        {
                TUserSiteMapping userSiteMapping = new TUserSiteMapping();
                userSiteMapping.Username = createUserSite.UserName;
                userSiteMapping.SiteCode = scMainSite.MainSiteCode;
                userSiteMapping.StateCode = scState.StateCode;
                userSiteMapping.AreaCode = scArea.AreaCode;
                userSiteMapping.CountryCode = scCountry.CountryCode;
                userSiteMapping.Status = createUserSite.Status;
                userSiteMapping.EmployeeCode = createUserSite.EmployeeCode;
                userSiteMapping.FkRoleId = createUserSite.RoleId;
                userSiteMapping.EmployeeEmail = createUserSite.EmployeeEmail;
            return userSiteMapping;
        }

        private TUserRoleMapping CreateTUserRoleMapping(UserRoleMapping userRoleMapping)
        {
            return new TUserRoleMapping
            {
                Username = userRoleMapping.Username,
                FkRoleId = userRoleMapping.FkRoleId,
                CreatedBy = userRoleMapping.CreatedBy,
                CreatedDate = userRoleMapping.CreatedDate,
                ModifiedBy = userRoleMapping.ModifiedBy,
                ModifiedDate = userRoleMapping.ModifiedDate,
                Status = userRoleMapping.Status,
                GroupCode = userRoleMapping.GroupCode
            };
        }
        private List<UserRoleMapping> CreateTUserRoleMapping(List<TUserRoleMapping> userList)
        {
            List<UserRoleMapping> newUserList = userList.Select(user => new UserRoleMapping
            {
                Id = user.Id,
                Username = user.Username,
                FkRoleId = user.FkRoleId,
                CreatedBy = user.CreatedBy,
                CreatedDate = user.CreatedDate,
                ModifiedBy = user.ModifiedBy,
                ModifiedDate = user.ModifiedDate,
                Status = user.Status,
                GroupCode = user.GroupCode,
            }).ToList();

            return newUserList;
        }
        private List<GroupRoleMapping> CreateTGroupRoleMappingDTO(List<TGroupRoleMapping> groupList)
        {
            List<GroupRoleMapping> newGroupList = groupList.Select(user => new GroupRoleMapping
            {
                Id = user.Id,
                GroupCode = user.GroupCode,
                FkRoleId = user.FkRoleId,
                CreatedBy = user.CreatedBy,
                CreatedDate = user.CreatedDate,
                ModifiedBy = user.ModifiedBy,
                ModifiedDate = user.ModifiedDate,
                Status = user.Status,
                
            }).ToList();

            return newGroupList;
        }
        private List<RoleMasterAssigned> CreateTRoleMasterAssigned(List<TRoleMaster> roleList)
        {
            List<RoleMasterAssigned> newRoleList = roleList.Select(role => new RoleMasterAssigned
            {
                Id = role.Id,
                RoleName = role.RoleName,
                RoleDescription = role.RoleDescription,
                CreatedBy = role.CreatedBy,
                CreatedDate = role.CreatedDate,
                ModifiedBy = role.ModifiedBy,
                ModifiedDate = role.ModifiedDate,
                Status = role.Status,
                GroupCode = role.GroupCode,
                IsAssigned = false
            }).ToList();

            return newRoleList;
        }

        private ResponseModel GetResponseModel
            (int code, object? data, string message, bool status)
        {
            responseModel.code = code;
            responseModel.data = data;
            responseModel.message = message;
            responseModel.status = status;

            return responseModel;
        }

        public ResponseModel SendEmail(string? templatename ,int tableid ,int taskid)
        {
            try
            {
                string[] Names = templatename.Split(",");
                List<TemplateConfigurationViewModel> templateConfiguration = new List<TemplateConfigurationViewModel>();
                DataSet ds = new DataSet();

                var conn = _context.Database.GetDbConnection();
                conn.Open();

                foreach (var Temp in Names)
                {
                    TemplateConfigurationViewModel templates = new TemplateConfigurationViewModel();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "sp_getemail";
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add(new SqlParameter("@templatename", templatename));
                        cmd.Parameters.Add(new SqlParameter("@taskid", taskid));
                        cmd.Parameters.Add(new SqlParameter("@tableid", tableid));

                        DbProviderFactory factory = DbProviderFactories.GetFactory(conn);
                        DbDataAdapter adapter = factory.CreateDataAdapter();
                        adapter.SelectCommand = cmd;
                        adapter.Fill(ds);
                    }

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            templates.TemplateName = dt.Rows[i]["TemplateName"].ToString();
                            templates.Subject = dt.Rows[i]["Subject"].ToString();
                            templates.To = dt.Rows[i]["To"].ToString();
                            templates.CC = dt.Rows[i]["cc"].ToString();
                            templates.BCC = dt.Rows[i]["Bcc"].ToString();
                            templates.MainBody = dt.Rows[i]["emailBody"].ToString();
                            templates.Keys = dt.Rows[i]["Keys"].ToString();
                            templates.List1 = dt.Rows[i]["List1"].ToString();
                            templates.List2 = dt.Rows[i]["List2"].ToString();
                            templates.List3 = dt.Rows[i]["List3"].ToString();
                            templates.List4 = dt.Rows[i]["List4"].ToString();
                            templates.List5 = dt.Rows[i]["List5"].ToString();
                            templates.url = dt.Rows[i]["url"].ToString();
                            templates.pairs = new Dictionary<string, string>();
                            for (var j = 1; j < ds.Tables.Count; j++)
                            {
                                var list = ConvertDataTableToHTML(ds.Tables[j]);
                                var listName = "[list" + j + "]";
                                templates.pairs.Add(listName, list);
                            }
                        }
                        SendMailOneByOne(templates);
                    }

                }
                    return GetResponseModel(Constants.httpCodeSuccess, "Email sent successfully",
                        "data", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public async void SendMailOneByOne(TemplateConfigurationViewModel templateConfiguration)
        {
            try
            {
                TemplateConfigurationViewModel MailTemplate = new TemplateConfigurationViewModel();
                MailTemplate.TemplateName = templateConfiguration.TemplateName;
                MailTemplate.Subject = templateConfiguration.Subject;
                MailTemplate.CC = templateConfiguration.CC;
                MailTemplate.BCC = templateConfiguration.BCC;
                MailTemplate.To = templateConfiguration.To;
                MailTemplate.MainBody = templateConfiguration.MainBody;
                if (templateConfiguration.pairs != null)
                {
                    foreach (KeyValuePair<string, string> entry in templateConfiguration.pairs)
                    {
                        MailTemplate.MainBody = MailTemplate.MainBody.Replace(entry.Key, entry.Value);
                        templateConfiguration.MainBody = templateConfiguration.MainBody.Replace(entry.Key, entry.Value);
                    }
                }


                EmailConfigurations configurations = _context.EmailConfigurations.Where(x => x.IsActive == true && x.From == "application_alert@suzlon.com").FirstOrDefault();

                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(configurations.From, configurations.FromName);

                mm.To.Add(MailTemplate.To);

                if (!string.IsNullOrEmpty(MailTemplate.CC))
                {
                    mm.CC.Add(MailTemplate.CC);
                }

                if (!string.IsNullOrEmpty(MailTemplate.BCC))
                {
                    mm.Bcc.Add(MailTemplate.BCC);
                }

                // Example: Remove special characters
                mm.Subject = CleanSpecialCharacters(MailTemplate.Subject);

                //mm.Subject = MailTemplate.Subject;
                mm.Body = MailTemplate.MainBody;
                mm.IsBodyHtml = true;

                var smtpClient = new SmtpClient(configurations.HostName)
                {
                    //Port = configurations.Port,
                    //Credentials = new NetworkCredential(configurations.UserName, configurations.Password),
                    UseDefaultCredentials = false,
                    EnableSsl = Convert.ToBoolean(configurations.EnableSsl),
                };
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtpClient.Send(mm);
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                EventLog.WriteEntry("Application", $"Error sending email: {ex.Message} + {ex.InnerException}", EventLogEntryType.Error);
                // You might want to throw the exception or handle it according to your application's needs
                // throw;
            }
        }
        public static string ConvertDataTableToHTML(DataTable dt)
        {
            string html = "<table style='padding: 5px;border: 1px solid black;border-collapse: collapse'>";
            //add header row
            html += "<tr style='padding: 5px;color: white; background: #6e6262'>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<td style ='padding: 5px;border: 1px solid black;border-collapse: collapse'>" + dt.Columns[i].ColumnName + "</td>";
            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td style='padding: 5px;border: 1px solid black;border-collapse: collapse'>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }
        public static string CleanSpecialCharacters(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input; // If the input is empty or null, return as is
            }

            // Replace or remove special characters, excluding spaces
            string cleanedString = Regex.Replace(input, "[^a-zA-Z0-9 ]", "");

            return cleanedString;
        }


    }
}
