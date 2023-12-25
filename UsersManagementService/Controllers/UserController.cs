using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersManagementService.Models;
using UsersManagementService.Services;
using UsersManagementService.ViewModels;

namespace UsersManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private readonly IUserService _context;
        private readonly ILogger<RoleController> _logger;

        public UserController(IUserService userService, ILogger<RoleController> logger)
        {
            _context = userService;
            _logger = logger;

        }

        [HttpPost("AddGroup")]
        public IActionResult AddGroup(string groupCode, List<RoleMaster> roleList)
        {
            _logger.LogInformation("AddGroup Method Started");
            try
            {
                var user = _context.AddGroup(groupCode, roleList);
                if (user.data == null)
                {
                    _logger.LogError("Group Not Added");
                }
                return Ok(user);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while Adding group");
                return StatusCode(500, "An error occurred while Adding group");
            }
        }

        [HttpPost("DeleteGroupByGroupCode")]
        public IActionResult DeleteGroupByGroupCode(string groupCode)
        {
            _logger.LogInformation("Delete Group Method Started");
            try
            {
                var response = _context.DeleteGroupByGroupCode(groupCode);
                if (response.data == null)
                {
                    _logger.LogError("Group Not found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while deleting group");
                return StatusCode(500, "An error occurred while deleting group");
            }
        }

        [HttpGet("GetGroupDetailsByGroupCode")]
        
        public IActionResult GetGroupDetailsByGroupCode(string groupCode)
        {
            _logger.LogInformation("Get Group Details Method Started");
            try
            {
                var response = _context.GetGroupDetailsByGroupCode(groupCode);
                if (response.data == null)
                {
                    _logger.LogError("Group Not found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting group");
                return StatusCode(500, "An error occurred while getting group");
            }
        }

        [HttpGet("GetUserRoleMapping")]
        public IActionResult GetUserRoleMapping()
        {
            _logger.LogInformation("Getting all User List");

            try
            {
                var response = _context.GetUserRoleMapping();
                if (response.data == null)
                {
                    _logger.LogError("User Not found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting User");
                return StatusCode(500, "An error occurred while getting User");
            }
        }


        [HttpGet("GetGroupMasterList")]
        public IActionResult GetGroupList()
        {
			_logger.LogInformation("Getting all Group List");

			try
			{
				var response = _context.GetGroupList();
				if (response.data == null)
				{
					_logger.LogError("Group Not found");
				}
				return Ok(response);
			}
			catch (Exception)
			{
				_logger.LogError("An error occurred while getting group");
				return StatusCode(500, "An error occurred while getting group");
			}
		}

        [HttpGet("GetAllUserSiteMapping")]
        public IActionResult GetAllUserSiteMapping()
        {
            _logger.LogInformation("Getting all Group List");

            try
            {
                var response = _context.GetAllUserSiteMapping();
                if (response.data == null)
                {
                    _logger.LogError("Group Not found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting group");
                return StatusCode(500, "An error occurred while getting group");
            }
        }

        [HttpGet("GetSiteByUser")]
        public IActionResult GetSiteByUser(string? userName, string? employeeCode, string? countryName, string? stateName, string? areaName, string? siteName)
        {
            try
            {
                _logger.LogInformation("GetSiteByUser Method Started");
                var siteByUserList = _context.GetSiteByUser(userName, employeeCode, countryName, stateName, areaName, siteName);

                if (siteByUserList.data == null)
                {
                    _logger.LogInformation("Site Does Not Exists");
                }

                return Ok(siteByUserList);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving Permissions By Roles.");
                return StatusCode(500, "An error occurred while retrieving Permissions By Roles.");
            }
        }

        [HttpGet("GetScCountryOmsPbi")]
        public IActionResult GetScCountryOmsPbi()
        {
            try
            {
                _logger.LogInformation("GetScCountryOmsPbi Method Started");
                var response = _context.GetScCountryOmsPbi();
                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }

        [HttpGet("GetStateByCountryCode")]
        public IActionResult GetStateByCountryCode(string? countryCode)
        {
            try
            {
                _logger.LogInformation("GetStateByCountryCode Method Started");
                var response = _context.GetStateByCountryCode(countryCode);
                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }

        [HttpGet("GetAreaByStateCode")]
        public IActionResult GetAreaByStateCode(string? stateCode)
        {
            try
            {
                _logger.LogInformation("GetAreaByStateCode Method Started");
                var response = _context.GetAreaByStateCode(stateCode);
                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }

        [HttpGet("GetSiteByAreaCode")]
        public IActionResult GetSiteByAreaCode(string? areaCode)
        {
            try
            {
                _logger.LogInformation("GetSiteByAreaCode Method Started");
                var response = _context.GetSiteByAreaCode(areaCode);
                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }

        [HttpPost("UpdateUserSite")]
        public IActionResult UpdateUserSite(List<UserSiteResponse> userSiteResponseList, string? userName)
        {
            try
            {
                _logger.LogInformation("UpdateUserSite Method Started");
                var responseList = _context.UpdateUserSite(userSiteResponseList, userName);

                if (responseList.data == null)
                {
                    _logger.LogInformation("Site Does Not Exists");
                }
                return Ok(responseList);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving Permissions By Roles.");
                return StatusCode(500, "An error occurred while retrieving Permissions By Roles.");
            }
        }


    }
}
