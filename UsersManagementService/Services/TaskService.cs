using Microsoft.AspNetCore.Mvc;
using UsersManagementService.CommonFiles;
using UsersManagementService.Repositories;
using UsersManagementService.ViewModels;

namespace UsersManagementService.Services
{
    public class TaskService: ITaskService
    {
        private readonly ITaskRepository taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        public ResponseModel GetAllTaskMng()
        {
            return taskRepository.GetAllTaskMng();
        }
		public ResponseModel GetAllTasks(string? userName,string? searchQuery = null, string? view = null, string? status = null, string? taskType = null,
            string? sprint = null, string? priority = null, string? natureOfTask = null)
        {
            return taskRepository.GetAllTasks(userName,searchQuery, view, status, taskType, sprint, priority, natureOfTask);
        }
        public ResponseModel GetAllTaskById(int? id)
        {
            return taskRepository.GetAllTaskById(id);
        }
        public ResponseModel AddorUpdateTasks(ResponseTaskMain responseTaskMain, string? username)
        {
            return taskRepository.AddorUpdateTasks(responseTaskMain, username);
        }
        public ResponseModel UpdateTaskList(List<ResponseTaskMain> allTasks, string? username)
        {
            return taskRepository.UpdateTaskList(allTasks, username);
        }
        public ResponseModel UpdateTaskBoard(List<SequenceTaskMng> allTasks, string? username)
        {
            return taskRepository.UpdateTaskBoard(allTasks, username);
        }
        public ResponseModel DeleteTaskById(int id)
        {
            return taskRepository.DeleteTaskById(id);
        }        
        public ResponseModel GetTicketNo()
        {
            return taskRepository.GetTicketNo();
        }

        public ResponseModel GetHistoryTask(int? task_id = null)
        {
            return taskRepository.GetHistoryTask(task_id);
        }
        public ResponseModel GetAllTaskDocuments(int taskId)
        {
            return taskRepository.GetAllTaskDocuments(taskId);
        }
        public ResponseModel DeleteImageaskById(int? id)
        {
            return taskRepository.DeleteImageaskById(id);
        }
        public ResponseModel GetDocumentMaster()
        {
            return taskRepository.GetDocumentMaster();
        }
        public ResponseModel GenerateRecordId()
        {
            return taskRepository.GenerateRecordId();
        }
        public ResponseModel FileUpload(DocUploadHistoryViewModel model)
        {
            return taskRepository.FileUpload(model);
        }
        public ResponseModel UploadAndSaveExcelData(UploadAndSaveExcelFormView uploadAndSaveExcelForm)
        {
            return taskRepository.UploadAndSaveExcelData(uploadAndSaveExcelForm);
        }
        public ResponseModel GetDocUploadHistory(int employee_ID, string entityname, int? RecordId, string? documentType, string Status)
        {
            return taskRepository.GetDocUploadHistory(employee_ID, entityname, RecordId, documentType, Status);
        }
        public ResponseModel GetUploadedFile(int recordId, int employee_ID, string DocumentType)
        {
            return taskRepository.GetUploadedFile(recordId, employee_ID, DocumentType);
        }
    }
}
