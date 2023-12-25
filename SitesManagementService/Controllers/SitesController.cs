using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SitesManagementService.Services;
using SitesManagementService.ViewModels;

namespace SitesManagementService.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private readonly ISitesService _sitesService;
        private readonly ILogger<SitesController> _logger;

        public SitesController(ISitesService sitesService, ILogger<SitesController> logger)
        {
            _sitesService = sitesService;
            _logger = logger;

        }

        [HttpGet("GetDetails")]
        public IActionResult GetDetails(UserSiteMapping userSiteMapping)
        {
            _logger.LogInformation("GetDetails Method Started");
            try
            {
                var response = _sitesService.GetDetails(userSiteMapping);
                if (response.data == null)
                {
                    _logger.LogError("User Details not exist.");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving detials of sites.");
                return StatusCode(500, "An error occurred while retrieving detials of sites.");
            }
        }

        [HttpPost("DeleteByUserId")]
        public IActionResult DeleteByUserId(List<UserSiteMapping> userSiteMapping)
        {
            _logger.LogInformation("DeleteBySite Method Started");
            try
            {
                var response = _sitesService.DeleteByUserId(userSiteMapping);
                if (response.data == null)
                {
                    _logger.LogError("User Details not exist.");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while deleting detials of sites.");
                return StatusCode(500, "An error occurred while deleting detials of sites.");
            }
        }

        [HttpPost("AddOrUpdateSites")]
        public IActionResult AddOrUpdateSites(List<UserSiteMapping> userSiteMapping)
        {
            _logger.LogInformation("Add or Update Site Method Started");
            try
            {
                var response = _sitesService.AddOrUpdateSites(userSiteMapping);
                if (response.data == null)
                {
                    _logger.LogError("Details not updated");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while Add or Update Site.");
                return StatusCode(500, "An error occurred while Add or Update Site.");
            }
        }
    }
}
