using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersManagementService.Models;
using UsersManagementService.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using UsersManagementService.ViewModels;
using UsersManagementService.CommonFiles;

namespace UsersManagementService.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
    
    public class PermissionController : ControllerBase
	{
		private readonly IPermissionService _permissionService;
		private readonly ILogger<PermissionController> _logger;

		public PermissionController(IPermissionService permissionService, ILogger<PermissionController> logger)
		{
			_permissionService = permissionService;
			_logger = logger;
		}

        [HttpGet("GetAllPermissions")]
        public IActionResult GetAllPermission()
        {
			_logger.LogInformation("GetAllPermission Method Started");
            try
            {
                var permissionList = _permissionService.GetAllPermission();
                if (permissionList.data == null)
                {
					_logger.LogError("Permission List are not there");
                    //return Ok("Permission Doesn't Exsist");
                }
                return Ok(permissionList);
            }
            catch (Exception) 
            {
				_logger.LogError("An error occurred while retrieving Permissions.");
                return StatusCode(500, "An error occurred while retrieving Permissions.");
            }
        }

        [HttpGet("GetPermissionByRole/{id}")]
        public IActionResult GetPermission(int id)
        {
            try
            {
				_logger.LogInformation("GetAllPermissionByRole Method Started");
                var permissionByRoleList = _permissionService.GetPermissionByRole(id);
 
                if (permissionByRoleList.data == null)
                {   
					_logger.LogInformation("Role Does Not Exists");
                    //return Ok("Role Does Not Exists");
                }
                if (permissionByRoleList.code == 200 && permissionByRoleList.status == false) 
                {
                    _logger.LogInformation("Role Doesn't Have Permssions");
                    //return Ok("Role Doesn't Have Permssions");
                }
                return Ok(permissionByRoleList);
            }
            catch (Exception)
            {
				_logger.LogError("An error occurred while retrieving Permissions By Roles.");
                return StatusCode(500, "An error occurred while retrieving Permissions By Roles.");
            }
            
        }

        [HttpPost("Add_Role_Permission_Mapping")]
        public IActionResult AddRolePermissionMapping(RolePermissionMapping rolePermissionMap)
        {
            try
            {
                _logger.LogInformation("Adding role permission mapping table");
                var response = _permissionService.AddRolePermissionMapping(rolePermissionMap);
                if (response.data == null)
                {
                    _logger.LogInformation("Role or Permission not avialable");
                    //return Ok("role or permission not avialable");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while Adding role permission mapping table.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Update_Role_Permission_Mapping")]
        public IActionResult UpdateRolePermissionMapping(List<RolePermissionMapping> rolePermissionMap)
        {
            try
            {
                _logger.LogInformation("Updating role permission mapping table");
                var response = _permissionService.UpdateRolePermissionMapping(rolePermissionMap);
                if(response.data == null)
                {
                    _logger.LogInformation("Role or Permission not avialable");
                    //return Ok("role or permission not avialable");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while Updating role permission mapping table.");
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpPost("UpdatePermissionByRoleId")]
        //public IActionResult UpdatePermissionByRoleId(int? id)
        //{
        //    try
        //    {
        //        _logger.LogInformation("UpdatePermissionByRoleId Method Started");
        //        var response = _permissionService.UpdatePermissionByRoleId(id);
        //        if (response.data == null)
        //        {
        //            _logger.LogInformation("Role or Permission not avialable");
                   
        //        }
        //        return Ok(response);
        //    }
        //    catch (Exception)
        //    {
        //        _logger.LogError("An error occurred while retrieving detials of logbook employee.");
        //        return StatusCode(500, "An error occurred while retrieving detials of logbook employee.");
        //    }
        //}
    }
}
