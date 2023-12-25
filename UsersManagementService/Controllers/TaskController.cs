using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersManagementService.Models;
using UsersManagementService.Services;
using UsersManagementService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using static ClosedXML.Excel.XLPredefinedFormat;
using UsersManagementService.CommonFiles;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Security.Claims;

namespace UsersManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ITaskService context;
        private readonly ILogger<RoleController> _logger;
        private readonly FleetManagerContext fleetManagerContext;
        private readonly CrmsContext crmsContext;
        private readonly ResponseModel responseModel;

        public TaskController(ITaskService context, ILogger<RoleController> logger, FleetManagerContext fleetManagerContext, CrmsContext crmsContext)
        {
            this.context = context;
            _logger = logger;
            this.fleetManagerContext = fleetManagerContext;
            this.crmsContext = crmsContext;
            this.responseModel = new ResponseModel();
        }

        [HttpPost("UploadFile")]
        public IActionResult UploadFile([FromForm] UploadFileData uploadFileData)
        {
            _logger.LogInformation("UploadFile Function Started");

            try
            {
                if (uploadFileData == null || uploadFileData.file == null || uploadFileData.file.Count == 0 || uploadFileData.TaskId == null)
                {
                    return BadRequest("No files uploaded");
                }

                var uploadedFiles = new List<TaskDocument>();

                foreach (var file in uploadFileData.file)
                {
                    if (file.Length > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        //var folderPath = @"D:\Git Development\Suzlon\fleetmanager-bk\UsersManagementService\FileUpload\";
                        var folderPath = "/home/ubuntu/FleetManager/fleetmanager-bk/UsersManagementService/FileUpload";
                        //var folderPath = @"D:\FleetManager\upload\bulkUploadFile\";
                        //var folderPath = @"D:\FleetManager - Live\upload\bulkUploadFile\";
                        var filePath = Path.Combine(folderPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        var taskDocument = new TaskDocument
                        {
                            TaskId = uploadFileData.TaskId,
                            FileName = fileName,
                            FilePath = filePath,
                            CreatedBy = "API User",
                            CreatedDate = System.DateTime.Now,
                            Guid = Guid.NewGuid().ToString()
                        };

                        uploadedFiles.Add(taskDocument);
                    }
                }

                fleetManagerContext.TaskDocuments.AddRange(uploadedFiles);
                fleetManagerContext.SaveChanges();

                return Ok(uploadedFiles);
            }
            catch (Exception)
            {
                _logger.LogError($"An error occurred while uploading files");
                return StatusCode(500, "An error occurred while uploading files");
            }
        }

        [HttpGet("DownloadFile")]
        public IActionResult DownloadFile(int fileId)
        {
            _logger.LogInformation("DownloadFile Function Started");

            try
            {
                var taskDocument = fleetManagerContext.TaskDocuments.FirstOrDefault(t => t.Id == fileId);
                if (taskDocument == null)
                {
                    _logger.LogError($"File not found for fileId: {fileId}");
                    return NotFound("File not found");
                }

                var filePath = taskDocument.FilePath;

                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                var base64String = Convert.ToBase64String(fileBytes);

                // Prepare the response model with the Base64 string
                var responseModel = new ResponseModel
                {
                    code = 200,
                    data = base64String,
                    message = "Task Report Downloaded Successfully",
                    status = true
                };

                return Ok(responseModel);
            }
            catch (Exception)
            {
                _logger.LogError($"An error occurred while Downloading files");
                return StatusCode(500, "An error occurred while Downloading files");
            }
        }

        [HttpGet("GetAllTaskDocumnets")]
        public IActionResult GetAllTaskDocuments(int taskId)
        {
            _logger.LogInformation("GetAllTaskDocuments Fuction Started");
            try
            {
                var response = context.GetAllTaskDocuments(taskId);
                if (response != null)
                {
                    _logger.LogError("task found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting Task");
                return StatusCode(500, "An error occurred while getting task");
            }
        }

        [HttpGet("GetAllTaskMng")]
        public IActionResult GetAllTaskMng()
        {
            _logger.LogInformation("GetAllTaskMng Fuction Started");
            try
            {
                var response = context.GetAllTaskMng();
                if (response.data == null)
                {
                    _logger.LogError("task Not found");
                }
                return Ok(response);

            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting Task");
                return StatusCode(500, "An error occurred while getting task");

            }
        }

		[HttpGet("GetAllTasks")]
        [Authorize]
        public IActionResult GetAllTasks(string? searchQuery = null, string? view = null, string? status = null, string? taskType = null,
            string? sprint = null, string? priority = null, string? natureOfTask = null, int pageNo = 1)
        {
            _logger.LogInformation("GetAllTaskas Fuction Started");
            var nameClaim = User.FindFirst(ClaimTypes.Name);
            string userName = "";
            if (nameClaim != null)
            {
                userName = nameClaim.Value;
            }
            try
            {
                var response = context.GetAllTasks(userName,searchQuery, view, status, taskType, sprint, priority, natureOfTask, pageNo);
                if (response.data == null)
                {
                    _logger.LogError("task Not found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting Task");
                return StatusCode(500, "An error occurred while getting task");

            }
        }
        /*[HttpGet("GetAllTasks")]
        [Authorize]
        public IActionResult GetAllTasks(string? searchQuery = null, string? view = null, string? status = null, string? taskType = null,
            string? sprint = null, string? priority = null, string? natureOfTask = null)
        {
            _logger.LogInformation("GetAllTaskas Fuction Started");
            var nameClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userName = "";
            if (nameClaim != null)
            {
                userName = nameClaim.Value;
            }
            try
            {
                var response = context.GetAllTasks(userName,searchQuery, view, status, taskType, sprint, priority, natureOfTask);
                if (response.data == null)
                {
                    _logger.LogError("task Not found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting Task");
                return StatusCode(500, "An error occurred while getting task");

            }
        }
		*/

        [HttpGet("GetTaskById")]
        public IActionResult GetAllTaskById(int? id)
        {
            _logger.LogInformation("GetAllTaskById Fuction Started");
            try
            {
                var response = context.GetAllTaskById(id);
                if (response != null)
                {
                    _logger.LogError("task found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting Task");
                return StatusCode(500, "An error occurred while getting task");
            }
        }

        [HttpPost("AddOrUpdateTaks")]
        [Authorize]
        public async Task<IActionResult> AddorUpdateTasks(ResponseTaskMain responseTaskMain)
        {
            _logger.LogInformation("AddorUpdateTasks Function Started");
            try
            {
                var nameClaim = User.FindFirst(ClaimTypes.Name);
                string userName = "";
                if (nameClaim != null)
                {
                    userName = nameClaim?.Value;
                }
                var response = await context.AddorUpdateTasks(responseTaskMain, userName);
                
                if (response != null)
                {
                    _logger.LogError("task found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting Task");
                return StatusCode(500, "An error occurred while getting task");
            }
        }

        [HttpPost("UpdateTaskList")]
        [Authorize]
        public IActionResult UpdateTaskList(List<ResponseTaskMain> allTasks)
        {
            _logger.LogInformation("UpdateTaskList Fuction Started");
            var nameClaim = User.FindFirst(ClaimTypes.Name);
            string userName = "";
            if (nameClaim != null)
            {
                userName = nameClaim?.Value;
            }
            try
            {
                var response = context.UpdateTaskList(allTasks, userName);
                if (response != null)
                {
                    _logger.LogError("task found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting Task");
                return StatusCode(500, "An error occurred while getting task");
            }
        }

        [HttpPost("UpdateTaskBoard")]
        [Authorize]
        public IActionResult UpdateTaskBoard(List<SequenceTaskMng> allTasks)
        {
            _logger.LogInformation("AddorUpdateTasks Fuction Started");
            var nameClaim = User.FindFirst(ClaimTypes.Name);
            string userName = "";
            if (nameClaim != null)
            {
                userName = nameClaim?.Value;
            }
            try
            {
                var response = context.UpdateTaskBoard(allTasks, userName);
                if (response != null)
                {
                    _logger.LogError("task found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting Task");
                return StatusCode(500, "An error occurred while getting task");
            }
        }

        [HttpPost("UpdateTaskBoardNew")]
        [Authorize]
        public async Task<IActionResult> UpdateTaskBoardNew(updateTaskMngrModel task)
        {
            _logger.LogInformation("UpdateTaskBoardNew Fuction Started");
            var nameClaim = User.FindFirst(ClaimTypes.Name);
            string userName = "";
            if (nameClaim != null)
            {
                userName = nameClaim?.Value;
            }
            try
            {
                var response = await context.UpdateTaskBoardNew(task, userName);
                if (response != null)
                {
                    _logger.LogError("task found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting Task");
                return StatusCode(500, "An error occurred while getting task");
            }
        }


        [HttpDelete("DeleteTaskById")]
        public IActionResult DeleteTaskById(int id)
        {
            _logger.LogInformation("GetAllTaskId Fuction Started");
            try
            {
                var response = context.DeleteTaskById(id);
                if (response != null)
                {
                    _logger.LogError("task found");
                }
                return Ok(response);

            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting Task");
                return StatusCode(500, "An error occurred while getting task");

            }
        }
        [HttpGet("GetTicketNo")]
        public IActionResult GetTicketNo()
        {
            _logger.LogInformation("GetTicketNo Fuction Started");
            try
            {
                var response = context.GetTicketNo();
                if (response == null)
                {
                    _logger.LogError("Ticket Not found");
                }
                return Ok(response);

            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting Ticket Number");
                return StatusCode(500, "An error occurred while getting Ticket Number");

            }
        }
        [HttpGet("GetHistoryTask")]
        public IActionResult GetHistoryTask(int task_id)
        {
            _logger.LogInformation("GetHistoryTask Fuction Started");
            try
            {
                var response = context.GetHistoryTask(task_id);
                if (response.data == null)
                {
                    _logger.LogError("History Not found");
                }
                return Ok(response);

            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting History");
                return StatusCode(500, "An error occurred while getting History");

            }

        }

        [HttpDelete("DeleteImageaskById")]
        public IActionResult DeleteImageaskById(int id)
        {
            _logger.LogInformation("DeleteImageaskById Fuction Started");
            try
            {
                var response = context.DeleteImageaskById(id);
                if (response != null)
                {
                    _logger.LogError("task found");
                }
                return Ok(response);

            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while Deleting Image");
                return StatusCode(500, "An error occurred while Deleting Image");

            }
        }

        [HttpGet("GetDocumentMaster")]
        public IActionResult GetDocumentMaster()
        {
            _logger.LogInformation("GetDocumentMaster Fuction Started");
            try
            {
                var response = context.GetDocumentMaster();
                if (response != null)
                {
                    _logger.LogError("task found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting Data");
                return StatusCode(500, "An error occurred while getting Data");
            }
        }
        [HttpGet("GenerateRecordId")]
        public IActionResult GenerateRecordId()
        {
            _logger.LogInformation("GenerateRecordId Fuction Started");
            try
            {
                var response = context.GenerateRecordId();
                if (response != null)
                {
                    _logger.LogError("task found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting Data");
                return StatusCode(500, "An error occurred while getting Data");
            }
        }

        [HttpPost("FileUpload")]
        public IActionResult FileUpload([FromForm]DocUploadHistoryViewModel model)
        {
            _logger.LogInformation("FileUpload Fuction Started");
            try
            {
                var response = context.FileUpload(model);
                if (response != null)
                {
                    _logger.LogError("excel found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting excel data");
                return StatusCode(500, "An error occurred while getting excel data");
            }
        }

        [HttpPost("UploadAndSaveExcelData")]
        public IActionResult UploadAndSaveExcelData([FromForm] UploadAndSaveExcelFormView uploadAndSaveExcelForm)
        {
            _logger.LogInformation("UpdateTaskList Fuction Started");
            try
            {
                var response = context.UploadAndSaveExcelData(uploadAndSaveExcelForm);
                if (response != null)
                {
                    _logger.LogError("excel found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting excel data");
                return StatusCode(500, "An error occurred while getting excel data");
            }
        }

        [HttpGet("GetDocUploadHistory")]
        public IActionResult GetDocUploadHistory(int employee_ID, string entityname, int? RecordId, string? documentType, string Status)
        {
            _logger.LogInformation("GetDocUploadHistory Fuction Started");
            try
            {
                var response = context.GetDocUploadHistory(employee_ID, entityname, RecordId, documentType, Status);
                if (response != null)
                {
                    _logger.LogError("excel found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting excel data");
                return StatusCode(500, "An error occurred while getting excel data");
            }
        }

        [HttpGet("GetUploadedFile")]
        public IActionResult GetUploadedFile(int recordId, int employee_ID, string DocumentType)
        {
            _logger.LogInformation("GetUploadedFile Fuction Started");
            try
            {
                var response = context.GetUploadedFile(recordId, employee_ID, DocumentType);
                if (response != null)
                {
                    _logger.LogError("excel found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting excel data");
                return StatusCode(500, "An error occurred while getting excel data");
            }
        }

        [HttpGet("DownloadTaskExcel")]
        public IActionResult DounloadTaskExcel(string? searchQuery = null)
        {
          
                var taskData = GetAllTasksList(searchQuery);

            //return Ok(taskData);


            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Task Details");
                ws.Cell(3, 1).InsertTable(taskData);

                //using (MemoryStream stream = new MemoryStream())
                //{
                //    wb.SaveAs(stream);
                //    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TaskReport.xlsx");
                //}

                using (var memoryStream = new System.IO.MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                    var excelBytes = memoryStream.ToArray();
                    var base64String = Convert.ToBase64String(excelBytes);

                    ResponseModel responseModel = new ResponseModel();
                    responseModel.code = 200;
                    responseModel.data = base64String;
                    responseModel.message = "Task Report Downloaded Successfully";
                    responseModel.status = true;
                    return Ok(responseModel);
                }
            }
        }

        private List<ExcelTaskMain> GetAllTasksList(string? searchQuery = null)

        {
            //var id = ToString.Parse(searchQuery);
            //if (searchQuery != null)
            //{
            //    var getTaskDetails = fleetManagerContext.TaskMains.Where(d => d.TicketNo == searchQuery || d.Title == searchQuery).ToList();
            //    return getTaskDetails;
            //}

            //return fleetManagerContext.TaskMains.ToList();

            var getTaskDetails = fleetManagerContext.TaskMains.ToList();
            var responseList = getTaskDetails.Select(list => ConvertToResponseTaskMain(list)).OrderBy(d => d.Sequence).ToList();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                responseList = responseList.Where(task =>
                    task.Id.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.TicketNo.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.Title.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.Description.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.CreatedBy.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.CreatedDate.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.ModifiedBy.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.ModifiedDate.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.StatusId.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (task.StatusChangedBy != null && task.StatusChangedBy.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    task.StatusChangedDate.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.AssignedToUser.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (task.AssignedToGroup != null && task.AssignedToGroup.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    task.DueDate.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.TaskTypeId.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.TaskParentId.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.Label.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.SprintId.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.PriorityId.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.Location.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.NatureOfTaskId.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (task.Comment != null && task.Comment.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    task.Reviewer.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0

                ).ToList();

            }
            if (responseList.Count == 0)
                {
                    return null;
                }
                return responseList;
            }

        private ExcelTaskMain ConvertToResponseTaskMain(TaskMain task)

        {
            //string[] assignedToUserParts = task.AssignedToUser.Split(',').Select(s => s.Trim()).ToArray();
            //string[] reviewerToUserParts = task.Reviewer.Split(',').Select(s => s.Trim()).ToArray();

            //List<CmrEmployeeMaster> assigned = new List<CmrEmployeeMaster>();
            //List<CmrEmployeeMaster> reviewer = new List<CmrEmployeeMaster>();

            //foreach (string assignpart in assignedToUserParts)
            //{
            //    List<CmrEmployeeMaster> assigndata = crmsContext.CmrEmployeeMasters.Where(d => d.EmpCode == assignpart).ToList();
            //    assigned.AddRange(assigndata);
            //}
            //foreach (string reviewpart in reviewerToUserParts)
            //{
            //    List<CmrEmployeeMaster> reviewerdata = crmsContext.CmrEmployeeMasters.Where(d => d.EmpCode == reviewpart).ToList();
            //    reviewer.AddRange(reviewerdata);
            //}
            return new ExcelTaskMain
{
    Id = task.Id,
    TicketNo = task.TicketNo,
    Title = task.Title,
    CreatedBy = task.CreatedBy,
    CreatedDate = task.CreatedDate,
    ModifiedBy = task.ModifiedBy,
    Description = task.Description,
    Comment = task.Comment,
    ModifiedDate = task.ModifiedDate,
    StatusId = fleetManagerContext.TaskManagementStatuses.FirstOrDefault(d => d.Id == task.StatusId)?.Status,
    StatusChangedBy = task.StatusChangedBy,
    StatusChangedDate = task.StatusChangedDate,
    AssignedToGroup = task.AssignedToGroup,
    AssignedToUser = crmsContext.CmrEmployeeMasters.FirstOrDefault(d => d.EmpCode == task.AssignedToUser)?.EmpName,
    DueDate = task.DueDate,
    Label = task.Label,
    Location = task.Location,
    Reviewer = crmsContext.CmrEmployeeMasters.FirstOrDefault(d => d.EmpCode == task.Reviewer)?.EmpName,
    TaskTypeId = fleetManagerContext.TLogbookCommonMasters.FirstOrDefault(d => d.Id == task.TaskTypeId)?.MasterName,
    TaskParentId = task.TaskParentId,
    SprintId = fleetManagerContext.TLogbookCommonMasters.FirstOrDefault(d => d.Id == task.SprintId)?.MasterName,
    PriorityId = fleetManagerContext.TLogbookCommonMasters.FirstOrDefault(d => d.Id == task.PriorityId)?.MasterName,
    NatureOfTaskId = fleetManagerContext.TLogbookCommonMasters.FirstOrDefault(d => d.Id == task.NatureOfTaskId)?.MasterName,
    Sequence = task.Sequence
};
            
        }

        [HttpGet("GetExcelErrorLog")]
        public IActionResult GetExcelErrorLog(int recordId,string type)
        {
            
            List<PlanningUploadDetail> data = new List<PlanningUploadDetail>();

            if (type == "Completed")
            {
               data = fleetManagerContext.PlanningUploadDetails.Where(x => x.RecordId ==  recordId && x.Status == "SUCCESS").ToList();
            }
            else if(type != "Error")
            {
                data = fleetManagerContext.PlanningUploadDetails.Where(x => x.RecordId == recordId && x.Status != "SUCCESS").ToList();
            }
            else
            {
                data = fleetManagerContext.PlanningUploadDetails.Where(x => x.RecordId == recordId).ToList();
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Error Details");
                ws.Cell(3, 1).InsertTable(data);

                //using (MemoryStream stream = new MemoryStream())
                //{
                //    wb.SaveAs(stream);
                //    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ErrorReport.xlsx");
                //}

                using (var memoryStream = new System.IO.MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                    var excelBytes = memoryStream.ToArray();
                    var base64String = Convert.ToBase64String(excelBytes);

                    ResponseModel responseModel = new ResponseModel();
                    responseModel.code = 200;
                    responseModel.data = base64String;
                    responseModel.message = "Error Report Downloaded Successfully";
                    responseModel.status = true;
                    return Ok(responseModel);
                }
            }
           
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
    }
}
