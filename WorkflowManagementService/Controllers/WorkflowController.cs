using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkflowManagementService.Services;

namespace WorkflowManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly IWorkflowService workflowService;
        private readonly ILogger<WorkflowController> _logger;

        public WorkflowController(IWorkflowService workflowService, ILogger<WorkflowController> logger)
        {
            this.workflowService = workflowService;
            _logger = logger;
        }
       
        [HttpGet("GetDetailsRuntime")]
        public IActionResult GetDetailsRuntime(int tableId, string tableName, string roleName, string userName)
        {
            _logger.LogInformation("GetDetails Method Started");
            try
            {
                var response = workflowService.GetDetailsRuntime(tableId, tableName, roleName, userName);
                if (response.data == null)
                {
                    _logger.LogError("Details of workflow runtime are not there");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving detials of workflow runtime.");
                return StatusCode(500, "An error occurred while retrieving detials of workflow runtime.");
            }
        }

        [HttpGet("GetDetailsRuntimeHistory")]
        public IActionResult GetDetailsRuntimeHistory(int tableId, string tableName)
        {
            _logger.LogInformation("GetDetailsHistory Method Started");
            try
            {
                var response = workflowService.GetDetailsRuntimeHistory(tableId, tableName);
                if (response.data == null)
                {
                    _logger.LogError("Details of workflow runtime history are not there");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving detials of workflow runtime history.");
                return StatusCode(500, "An error occurred while retrieving detials of workflow runtime history.");
            }
        }
    }
}
