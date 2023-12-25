using Microsoft.AspNetCore.Mvc;
using UsersManagementService.CommonFiles;
using UsersManagementService.ViewModels;

namespace UsersManagementService.Repositories
{
    public interface ITaskRepository
    {
        ResponseModel GetAllTaskMng();
        ResponseModel GetAllTasks(string? userName,string? searchQuery = null, string? view = null, string? status = null, string? taskType = null,
            string? sprint = null, string? priority = null, string? natureOfTask = null);
        ResponseModel GetAllTaskById(int? id);
        ResponseModel AddorUpdateTasks(ResponseTaskMain responseTaskMain, string? username);
        ResponseModel UpdateTaskList(List<ResponseTaskMain> allTasks, string? username);
        ResponseModel UpdateTaskBoard(List<SequenceTaskMng> allTasks, string? username);
        ResponseModel DeleteTaskById(int id);
        ResponseModel GetTicketNo();
        ResponseModel GetHistoryTask(int? task_id = null);
        ResponseModel GetAllTaskDocuments(int taskId);
        ResponseModel DeleteImageaskById(int? id);
        ResponseModel GetDocumentMaster();
        ResponseModel GenerateRecordId();
        ResponseModel FileUpload(DocUploadHistoryViewModel model);
        ResponseModel UploadAndSaveExcelData(UploadAndSaveExcelFormView uploadAndSaveExcelForm);
        ResponseModel GetDocUploadHistory(int employee_ID, string entityname, int? RecordId, string? documentType, string Status);
        ResponseModel GetUploadedFile(int recordId, int employee_ID, string DocumentType);
    }
}
