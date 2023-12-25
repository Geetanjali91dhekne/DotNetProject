using KpiAnalyzerService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;
using KpiAnalyzerService.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using KpiAnalyzerService.ViewModels;

namespace KpiAnalyzerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyzerController : Controller
    {
        private readonly IAnalyzerService context;
        private readonly FleetManagerContext fleetManagerContext;
        
        public AnalyzerController(IAnalyzerService context, FleetManagerContext fleetManagerContext)
        {
            this.context = context;
            this.fleetManagerContext = fleetManagerContext;
        }

        [HttpGet("GetTablesandViewNames")]
        public IActionResult GetTablesandViewNames()
        {
            //_logger.LogInformation("GetAllTaskDocuments Fuction Started");
            try
            {
                var response = context.GetTableAndViewNames();
                if (response != null)
                {
                    //_logger.LogError("task found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                //_logger.LogError("An error occurred while getting Task");
                return StatusCode(500, "An error occurred while getting task");
            }
        }

        [HttpGet("GetTableOrViewColumns")]
        public IActionResult GetTableOrViewColumns(string tableName)
        {
            //_logger.LogInformation("GetAllTaskDocuments Fuction Started");
            try
            {
                var response = context.GetTableOrViewColumns(tableName);
                if (response != null)
                {
                    //_logger.LogError("task found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                //_logger.LogError("An error occurred while getting Task");
                return StatusCode(500, "An error occurred while getting task");
            }
        }

        [HttpGet("GetDataFilterConditions")]
        public IActionResult GetDataFilterConditions(string tableName)
        {
            try
            {
                var response = context.GetDataFilterConditions(tableName);
                if (response != null)
                {
                    //_logger.LogError("task found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                //_logger.LogError("An error occurred while getting Task");
                return StatusCode(500, "An error occurred while getting task");
            }
        }

        [HttpPost("AddorUpdateData")]
        public IActionResult AddOrUpdateDataFilterCondition(ResponseDataFilterCondition conditions)
        {
            try
            {
                var response = context.AddOrUpdateDataFilterCondition(conditions);
                if (response != null)
                {
                    //_logger.LogError("task found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                //_logger.LogError("An error occurred while getting Task");
                return StatusCode(500, "An error occurred while getting task");
            }
        }

        [HttpGet("GetColumnXYMapping")]
        public IActionResult GetColumnXYMapping(string tableName)
        {
            try
            {
                var response = context.GetColumnXYMapping(tableName);
                if (response != null)
                {
                    //_logger.LogError("task found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                //_logger.LogError("An error occurred while getting Task");
                return StatusCode(500, "An error occurred while getting task");
            }
        }

        [HttpPost("AddOrUpdateColumnXYMapping")]
        public IActionResult AddOrUpdateColumnXYMapping(ResponseColumnXYMapping columns)
        {
            try
            {
                var response = context.AddOrUpdateColumnXYMapping(columns);
                if (response != null)
                {
                    //_logger.LogError("task found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                //_logger.LogError("An error occurred while getting Task");
                return StatusCode(500, "An error occurred while getting task");
            }
        }
    }
}
