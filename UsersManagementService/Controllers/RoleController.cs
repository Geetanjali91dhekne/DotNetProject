using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Security.Claims;
using UsersManagementService.Models;
using UsersManagementService.Services;
using UsersManagementService.ViewModels;

namespace UsersManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _context;
		private readonly ILogger<RoleController> _logger;

		public RoleController(IRoleService roleService, ILogger<RoleController> logger)
        {
            _context = roleService;
			_logger = logger;
        }

        [HttpGet("GetAllRoles")]
       
        public IActionResult GetAllRoles()
        {
            try
            {
                _logger.LogInformation("GetAllRoles Method Started");

                
                var roleList = _context.GetAllRoles();

                if (roleList.data == null)
                {
                    _logger.LogInformation("Roles Does Not Exists");
                }
                return Ok(roleList);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving Roles.");
                return StatusCode(500, "An error occurred while retrieving Roles.");
            }
        }

        [HttpGet("GetRoleById/{id}")]
       
        public IActionResult GetRoleById(int id)
        {
            try
            {
                _logger.LogInformation("GetRolesById Method Started");
                
                var role = _context.GetRoleById(id);

                if (role.data == null)
                {
                    _logger.LogInformation("Role Does Not Exists");
                }
                return Ok(role);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving Role.");
                return StatusCode(500, "An error occurred while retrieving Role.");
            }
        }

        [HttpPost("AddRole")]
        [Authorize]
        public IActionResult AddRole(RoleMaster roleMaster)
        {
            try
            {
                _logger.LogInformation("Add Role");
               
                var nameClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                string userName = "";
                if (nameClaim != null)
                {
                    userName = nameClaim.Value;
                }
                var response = _context.AddRole(roleMaster,userName);
                if (response.data == null)
                {
                    _logger.LogInformation("Roles Does Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Role.");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }

        [HttpPost("UpdateRole")]
        public IActionResult UpdateRole(RoleMaster roleMaster)
        {
            try
            {
                _logger.LogInformation("Update Role");
                var nameClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                string userName = "";
                if (nameClaim != null)
                {
                    userName = nameClaim.Value;
                }
                var response = _context.UpdateRole(roleMaster,userName);

                if (response.data == null)
                {
                    _logger.LogInformation("Roles Does Not updated");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while updating Role.");
                return StatusCode(500, "An error occurred while updating Role.");
            }
        }

        [HttpGet("GetDetailsByRoleName")]
       
        public IActionResult GetDetailsByRoleName()
        {
            try
            {
                _logger.LogInformation("GetDetailsByRoleName Method Started");

                // Get the roles from the decoded token
                var rolesClaim = User.Claims.Where(c => c.Type == "Role").ToList();
                if (rolesClaim == null)
                {
                    _logger.LogInformation("No Roles found in the token.");
                    return StatusCode(403, "No Roles found in the token.");
                }
                var roleValues = rolesClaim.Select(c => c.Value).ToList();


                // Filter the results based on the roles in the token
                var roleDetails = _context.GetDetailsByRoleName(roleValues);

                if (roleDetails.data == null)
                {
                    _logger.LogInformation("No matching Role Details found.");
                }

                return Ok(roleDetails);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving Role Details.");
                return StatusCode(500, "An error occurred while retrieving Role Details.");
            }
        }

        //public IActionResult GetDetailsByRoleName(string roleName)
        //{
        //    try
        //    {
        //        _logger.LogInformation("GetDetailsByRoleName Method Started");
        //        var roleDetails = _context.GetDetailsByRoleName(roleName);

        //        if (roleDetails.data == null)
        //        {
        //            _logger.LogInformation("RoleName Does Not Exists");
        //        }
        //        return Ok(roleDetails);
        //    }
        //    catch (Exception)
        //    {
        //        _logger.LogError("An error occurred while retrieving Role Details.");
        //        return StatusCode(500, "An error occurred while retrieving Role Details.");
        //    }
        //}
    }
}
