using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsersManagementService.CommonFiles;
using UsersManagementService.Services;
using UsersManagementService.ViewModels;

namespace UsersManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
       
        
       // [AllowAnonymous]
       // [HttpPost("AuthenticateUser")]
        //public IActionResult Authorisation(UserRoleMapping userRoleMap)
        //{
        //    string token = _jwtAuthentcationManager.AuthenticateUser(userRoleMap);
        //    if (token.Equals("UnAuthorised"))
        //    {
        //        responseModel.code = 200;
        //        responseModel.data = token;
        //        responseModel.message = token.ToString();
        //        responseModel.status = false;

        //        return Ok(responseModel);
        //    }

        //    IDictionary<string, Object> resultData = new Dictionary<string, Object>
        //    {
        //        { "authhenticationToken", token },
        //        {"UserName", userRoleMap.Username }
        //    };


        //    responseModel.code = 200;
        //    responseModel.data = resultData;
        //    responseModel.message = "Login Successfully";
        //    responseModel.status = true;

        //    return Ok(responseModel);
        //}
    }
}
