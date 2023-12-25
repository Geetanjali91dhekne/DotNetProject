using Azure.Identity;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UsersManagementService.CommonFiles;
using UsersManagementService.Models;
using UsersManagementService.ViewModels;

namespace UsersManagementService.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly FleetManagerContext fleetManagerContext;
        private readonly CrmsContext crmsContext;
        private readonly ResponseModel responseModel;

        public TaskRepository(FleetManagerContext fleetManagerContext, CrmsContext crmsContext)
        {
            this.fleetManagerContext = fleetManagerContext;
            this.crmsContext = crmsContext;
            this.responseModel = new ResponseModel();
        }

        public ResponseModel GetAllTaskMng()
        {
            try
            {
                var TaskList = fleetManagerContext.TaskManagementStatuses.OrderBy(d => d.Sequence).ToList();
                if (TaskList.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "Task Not Found", false);
                }
                return GetResponseModel(Constants.httpCodeSuccess, TaskList, "All Task Listed", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }

        }

        public ResponseModel GetAllTasks(string? userName,string? searchQuery = null, string? view = null, string? status = null, string? taskType = null,
            string? sprint = null, string? priority = null, string? natureOfTask = null)
        {            
            var siteList = fleetManagerContext.TUserSiteMappings.Where(d => d.Username == userName).Select(d => d.SiteCode).ToList();
            if (string.IsNullOrEmpty(view))
            {
                view = "Kanban";
            }
            try
            {
                var taskList = fleetManagerContext.TaskMains.Where(d => siteList.Contains(d.SiteCode)).ToList();
                var responseList = taskList.Select(list => ConvertToResponseTaskMain(list)).OrderBy(d => d.Sequence).ToList();

                if (view.Equals("Kanban"))
                {
                    var currentDate = DateTime.Now;
                    responseList = responseList
                        .Where(task =>
                            (task.StatusId.Status != "TO DO" && task.StatusId.Status != "DONE") ||
                            (task.StatusId.Status == "TO DO" && task.DueDate <= CalculateDueDate(currentDate, task.StatusId.AllowedDays, "TO DO")) ||
                            (task.StatusId.Status == "DONE" && task.StatusChangedDate >= CalculateDueDate(currentDate, task.StatusId.AllowedDays, "DONE")))
                        .ToList();
                }

                List<string> statusList = !string.IsNullOrEmpty(status) ? status.Split(',').Select(s => s.Trim()).ToList() : new List<string>();
                List<string> taskTypeList = !string.IsNullOrEmpty(taskType) ? taskType.Split(',').Select(s => s.Trim()).ToList() : new List<string>();
                List<string> sprintList = !string.IsNullOrEmpty(sprint) ? sprint.Split(',').Select(s => s.Trim()).ToList() : new List<string>();
                List<string> priorityList = !string.IsNullOrEmpty(priority) ? priority.Split(',').Select(s => s.Trim()).ToList() : new List<string>();
                List<string> natureOfTaskList = !string.IsNullOrEmpty(natureOfTask) ? natureOfTask.Split(',').Select(s => s.Trim()).ToList() : new List<string>();

                if (statusList.Any())
                {
                    responseList = responseList.Where(d => statusList.Contains(d.StatusId.Status, StringComparer.OrdinalIgnoreCase)).ToList();
                }
                if (taskTypeList.Any())
                {
                    responseList = responseList.Where(d => taskTypeList.Contains(d.TaskTypeId.MasterName, StringComparer.OrdinalIgnoreCase)).ToList();
                }
                if (sprintList.Any())
                {
                    responseList = responseList.Where(d => sprintList.Contains(d.SprintId.MasterName, StringComparer.OrdinalIgnoreCase)).ToList();
                }
                if (priorityList.Any())
                {
                    responseList = responseList.Where(d => priorityList.Contains(d.PriorityId.MasterName, StringComparer.OrdinalIgnoreCase)).ToList();
                }
                if (natureOfTaskList.Any())
                {
                    responseList = responseList.Where(d => natureOfTaskList.Contains(d.NatureOfTaskId.MasterName, StringComparer.OrdinalIgnoreCase)).ToList();
                }

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
                    task.StatusId.Status.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (task.StatusChangedBy != null && task.StatusChangedBy.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    task.StatusChangedDate.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.AssignedToUser.Any(employee => employee.EmpName.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (task.AssignedToGroup != null && task.AssignedToGroup.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    task.DueDate.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.TaskTypeId.MasterName.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.TaskParentId.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.Label.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.SprintId.MasterName.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.PriorityId.MasterName.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.Location.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    task.NatureOfTaskId.MasterName.ToString().IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    (task.Comment != null && task.Comment.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    task.Reviewer.Any(employee => employee.EmpName.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    task.SiteIncharge.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) >= 0
                ).ToList();

                }

                if (responseList.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "Task Not Found", false);
                }

                return GetResponseModel(Constants.httpCodeSuccess, responseList, "All Tasks Listed", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetAllTaskById(int? id)
        {
            try
            {
                var task = fleetManagerContext.TaskMains.Where(i => i.Id == id).FirstOrDefault();
                if (task == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "Task Not Available", false);
                }
                var taskData = ConvertToResponseTaskMain(task);
                return GetResponseModel(Constants.httpCodeSuccess, taskData, "Task is Available", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        private GetTaskMain ConvertToResponseTaskMain(TaskMain task)
        {
            string[] assignedToUserParts = task.AssignedToUser?.Split(',')?.Select(s => s.Trim()).ToArray() ?? Array.Empty<string>();
            string[] reviewerToUserParts = task.Reviewer?.Split(',')?.Select(s => s.Trim()).ToArray() ?? Array.Empty<string>();

            List<CmrEmployeeMaster> assigned = new List<CmrEmployeeMaster>();
            List<CmrEmployeeMaster> reviewer = new List<CmrEmployeeMaster>();

            foreach (string assignpart in assignedToUserParts)
            {
                CmrEmployeeMaster userAssigned = new CmrEmployeeMaster();
                var empDetails = GetUserDetails(assignpart);
                userAssigned.EmpCode = empDetails?.Code;
                userAssigned.EmpDomainId = empDetails?.Code;
                userAssigned.EmpName = empDetails?.Name;
                assigned.Add(userAssigned);

            }
            foreach (string reviewpart in reviewerToUserParts)
            {
                CmrEmployeeMaster revAssigned = new CmrEmployeeMaster();
                var empDetails = GetUserDetails(reviewpart);
                revAssigned.EmpCode = empDetails?.Code;
                revAssigned.EmpDomainId = empDetails?.Code;
                revAssigned.EmpName = empDetails?.Name;
                assigned.Add(revAssigned);
            }

            return new GetTaskMain
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
                StatusId = fleetManagerContext.TaskManagementStatuses.FirstOrDefault(d => d.Id == task.StatusId),
                StatusChangedBy = task.StatusChangedBy,
                StatusChangedDate = task.StatusChangedDate,
                AssignedToGroup = task.AssignedToGroup,
                AssignedToUser = assigned,
                DueDate = task.DueDate,
                Label = task.Label,
                Location = task.Location,
                Reviewer = reviewer,
                TaskTypeId = fleetManagerContext.TLogbookCommonMasters.FirstOrDefault(d => d.Id == task.TaskTypeId),
                TaskParentId = task.TaskParentId,
                SprintId = fleetManagerContext.TLogbookCommonMasters.FirstOrDefault(d => d.Id == task.SprintId),
                PriorityId = fleetManagerContext.TLogbookCommonMasters.FirstOrDefault(d => d.Id == task.PriorityId),
                NatureOfTaskId = fleetManagerContext.TLogbookCommonMasters.FirstOrDefault(d => d.Id == task.NatureOfTaskId),
                Sequence = task.Sequence,
                SiteIncharge = task.SiteIncharge,
            };
        }

        public ResponseModel AddorUpdateTasks(ResponseTaskMain responseTaskMain, string? username)
        {
            try
            {
                string lastTicketNo = fleetManagerContext.TaskMains.OrderByDescending(e => e.Id).Select(e => e.TicketNo).FirstOrDefault();
                int nextNumber = 1;

                if (!string.IsNullOrEmpty(lastTicketNo))
                {
                    string numericPart = new string(lastTicketNo.Where(char.IsDigit).ToArray());
                    if (int.TryParse(numericPart, out int parsedNumber))
                    {
                        nextNumber = parsedNumber + 1;
                    }
                }

                string ticketNo = "FM-" + nextNumber;

                var existingTask = fleetManagerContext.TaskMains.FirstOrDefault(t => t.Id == responseTaskMain.Id);
                
                if (existingTask == null)
                {
                    var newTask = CreateTasks(responseTaskMain);
                  
                    newTask.TicketNo = ticketNo;
                    newTask.CreatedBy = username;
                    newTask.CreatedDate = DateTime.Now;
                    newTask.ModifiedBy = username;
                    newTask.ModifiedDate = DateTime.Now;
                    newTask.StatusChangedBy = username;
                    newTask.StatusChangedDate = DateTime.Now;

                    fleetManagerContext.TaskMains.Add(newTask);
                    fleetManagerContext.SaveChanges();

                    return GetResponseModel(Constants.httpCodeSuccess, newTask, "Task Added Successfully", true);
                }
                else
                {
                    var updatedTask = MapTasks(existingTask, responseTaskMain);
                    updatedTask.ModifiedBy = username;
                    updatedTask.ModifiedDate = DateTime.Now;
                    updatedTask.StatusChangedBy = username;
                    updatedTask.StatusChangedDate = DateTime.Now;

                    fleetManagerContext.TaskMains.Update(updatedTask);
                    fleetManagerContext.SaveChanges();
                    return GetResponseModel(Constants.httpCodeSuccess, updatedTask, "Task Updated Successfully", true);
                }
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel UpdateTaskList(List<ResponseTaskMain> allTasks, string? username)
        {
            

            try
            {
                List<TaskMain> tasks = new List<TaskMain>();
                if (allTasks.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "Task Not Updated", true);
                }

                foreach (var task in allTasks)
                {
                    var existingTask = fleetManagerContext.TaskMains.FirstOrDefault(t => t.Id == task.Id);

                    if (existingTask == null)
                    {

                        var newTask = CreateTasks(task);
                        
                        newTask.CreatedBy = username;
                        newTask.CreatedDate = DateTime.Now;
                        newTask.ModifiedBy = username;
                        newTask.ModifiedDate = DateTime.Now;
                        newTask.StatusChangedBy = username;
                        newTask.StatusChangedDate = DateTime.Now;

                        fleetManagerContext.TaskMains.Add(newTask);
                        fleetManagerContext.SaveChanges();

                        return GetResponseModel(Constants.httpCodeSuccess, newTask, "Task Added Successfully", true);
                    }

                    else
                    {

                        var updatedTask = MapTasks(existingTask, task);
                        updatedTask.ModifiedBy = username;
                        updatedTask.ModifiedDate = DateTime.Now;
                        updatedTask.StatusChangedBy = username;
                        updatedTask.StatusChangedDate = DateTime.Now;

                        fleetManagerContext.TaskMains.Update(updatedTask);
                        fleetManagerContext.SaveChanges();

                        tasks.Add(updatedTask);
                    }


                }
                return GetResponseModel(Constants.httpCodeSuccess, tasks, "Task Updated Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel UpdateTaskBoard(List<SequenceTaskMng> allTasks, string? username)
        {
            

            try
            {
                List<TaskMain> tasks = new List<TaskMain>();
                if (allTasks.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "Task Not Updated", true);
                }

                foreach (var task in allTasks)
                {
                    var existingTask = fleetManagerContext.TaskMains.FirstOrDefault(t => t.Id == task.Id);

                    if (existingTask == null)
                    {

                        var newTask = CreateTaskOrder(task);
                     
                        newTask.CreatedBy = username;
                        newTask.CreatedDate = DateTime.Now;
                        newTask.ModifiedBy = username;
                        newTask.ModifiedDate = DateTime.Now;
                        newTask.StatusChangedBy = username;
                        newTask.StatusChangedDate = DateTime.Now;


                        fleetManagerContext.TaskMains.Add(newTask);
                        fleetManagerContext.SaveChanges();

                        return GetResponseModel(Constants.httpCodeSuccess, newTask, "Task Added Successfully", true);
                    }

                    else
                    {
                        var updatedTask = MapTaskOrder(existingTask, task);
                        updatedTask.ModifiedBy = username;
                        updatedTask.ModifiedDate = DateTime.Now;
                        updatedTask.StatusChangedBy = username;
                        updatedTask.StatusChangedDate = DateTime.Now;

                        fleetManagerContext.TaskMains.Update(updatedTask);
                        fleetManagerContext.SaveChanges();

                        tasks.Add(updatedTask);
                    }


                }
                return GetResponseModel(Constants.httpCodeSuccess, tasks, "Task Updated Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        private TaskMain CreateTasks(ResponseTaskMain responseTaskMain)
        {
            TaskMain newTaskMain = new TaskMain();

            newTaskMain.Title = responseTaskMain.Title;
            newTaskMain.Description = responseTaskMain.Description;
            newTaskMain.AssignedToUser = string.Join(",", responseTaskMain.AssignedToUser);
            newTaskMain.AssignedToGroup = responseTaskMain.AssignedToGroup;
            newTaskMain.DueDate = responseTaskMain.DueDate.Value.Date;
            newTaskMain.StatusId = responseTaskMain.StatusId;
            newTaskMain.TaskTypeId = responseTaskMain.TaskTypeId;
            newTaskMain.TaskParentId = responseTaskMain.TaskParentId;
            newTaskMain.Label = responseTaskMain.Label;
            newTaskMain.SprintId = responseTaskMain.SprintId;
            newTaskMain.PriorityId = responseTaskMain.PriorityId;
            newTaskMain.Location = responseTaskMain.Location;
            newTaskMain.NatureOfTaskId = responseTaskMain.NatureOfTaskId;
            newTaskMain.Comment = responseTaskMain.Comment;
            newTaskMain.Reviewer = string.Join(",", responseTaskMain.Reviewer);
            newTaskMain.Sequence = responseTaskMain.Sequence;
            newTaskMain.SiteCode = responseTaskMain.SiteCode;
            newTaskMain.SiteName = responseTaskMain.SiteName;
            newTaskMain.SiteIncharge = responseTaskMain.SiteIncharge;
            return newTaskMain;
        }
        private TaskMain MapTasks(TaskMain existingTask, ResponseTaskMain updateTask)
        {

            existingTask.Id = updateTask.Id;
            existingTask.TicketNo = updateTask.TicketNo;
            existingTask.Title = updateTask.Title;
            existingTask.Description = updateTask.Description;
            existingTask.StatusId = updateTask.StatusId;
            existingTask.AssignedToUser = string.Join(",", updateTask.AssignedToUser);
            existingTask.AssignedToGroup = updateTask.AssignedToGroup;
            existingTask.DueDate = updateTask.DueDate.Value.Date;
            existingTask.TaskTypeId = updateTask.TaskTypeId;
            existingTask.TaskParentId = updateTask.TaskParentId;
            existingTask.Label = updateTask.Label;
            existingTask.SprintId = updateTask.SprintId;
            existingTask.PriorityId = updateTask.PriorityId;
            existingTask.Location = updateTask.Location;
            existingTask.NatureOfTaskId = updateTask.NatureOfTaskId;
            existingTask.Comment = updateTask.Comment;
            existingTask.Reviewer = string.Join(",", updateTask.Reviewer);
            existingTask.Sequence = updateTask.Sequence;
            existingTask.SiteCode = updateTask.SiteCode;
            existingTask.SiteName = updateTask.SiteName;
            existingTask.SiteIncharge = updateTask.SiteIncharge;
            return existingTask;
        }

        private TaskMain CreateTaskOrder(SequenceTaskMng responseTaskMain)
        {
            List<string> EmpCodes = new List<string>();
            List<string> ReviewerCodes = new List<string>();
            foreach (var codeList in responseTaskMain.AssignedToUser)
            {
                string empCode = codeList.EmpCode;
                EmpCodes.Add(empCode);
            }
            foreach (var list in responseTaskMain.Reviewer)
            {
                string reviewerList = list.EmpCode;
                ReviewerCodes.Add(reviewerList);
            }

            TaskMain newTaskMain = new TaskMain();
            newTaskMain.Id = responseTaskMain.Id;
            newTaskMain.TicketNo = responseTaskMain.TicketNo;
            newTaskMain.Title = responseTaskMain.Title;
            newTaskMain.Description = responseTaskMain.Description;
            newTaskMain.AssignedToUser = string.Join(",", EmpCodes);
            newTaskMain.AssignedToGroup = responseTaskMain.AssignedToGroup;
            newTaskMain.DueDate = responseTaskMain.DueDate;
            newTaskMain.StatusId = responseTaskMain.StatusId.Id;
            newTaskMain.TaskTypeId = responseTaskMain.TaskTypeId.Id;
            newTaskMain.TaskParentId = responseTaskMain.TaskParentId;
            newTaskMain.Label = responseTaskMain.Label;
            newTaskMain.SprintId = responseTaskMain.SprintId.Id;
            newTaskMain.PriorityId = responseTaskMain.PriorityId.Id;
            newTaskMain.Location = responseTaskMain.Location;
            newTaskMain.NatureOfTaskId = responseTaskMain.NatureOfTaskId.Id;
            newTaskMain.Comment = responseTaskMain.Comment;
            newTaskMain.Reviewer = string.Join(",", ReviewerCodes);
            newTaskMain.Sequence = responseTaskMain.Sequence;
            newTaskMain.SiteIncharge = responseTaskMain.SiteIncharge;
            return newTaskMain;
        }
        private TaskMain MapTaskOrder(TaskMain existingTask, SequenceTaskMng updateTask)
        {
            List<string> EmpCodes = new List<string>();
            List<string> ReviewerCodes = new List<string>();
            foreach (var codeList in updateTask.AssignedToUser)
            {
                string empCode = codeList.EmpCode;
                EmpCodes.Add(empCode);
            }
            foreach (var list in updateTask.Reviewer)
            {
                string reviewerList = list.EmpCode;
                ReviewerCodes.Add(reviewerList);
            }

            existingTask.TicketNo = updateTask.TicketNo;
            existingTask.Title = updateTask.Title;
            existingTask.Description = updateTask.Description;
            existingTask.CreatedBy = updateTask.CreatedBy;
            existingTask.CreatedDate = updateTask.CreatedDate;
            existingTask.StatusId = updateTask.StatusId.Id;
            existingTask.AssignedToUser = string.Join(",", EmpCodes);
            existingTask.AssignedToGroup = updateTask.AssignedToGroup;
            existingTask.DueDate = updateTask.DueDate;
            existingTask.TaskTypeId = updateTask.TaskTypeId.Id;
            existingTask.TaskParentId = updateTask.TaskParentId;
            existingTask.Label = updateTask.Label;
            existingTask.SprintId = updateTask.SprintId.Id;
            existingTask.PriorityId = updateTask.PriorityId.Id;
            existingTask.Location = updateTask.Location;
            existingTask.NatureOfTaskId = updateTask.NatureOfTaskId.Id;
            existingTask.Comment = updateTask.Comment;
            existingTask.Reviewer = string.Join(",", ReviewerCodes);
            existingTask.Sequence = updateTask.Sequence;
            existingTask.SiteIncharge = updateTask.SiteIncharge;
            return existingTask;
        }

        public ResponseModel GetAllTaskDocuments(int taskId)
        {
            try
            {
                var task = fleetManagerContext.TaskDocuments.Where(i => i.TaskId == taskId).ToList();
                if (task == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "Task Not Available", false);
                }
                List<DocumentHistoryView> DocumentList = new List<DocumentHistoryView>();
                foreach (var item in task)
                {
                    DocumentHistoryView Document = new DocumentHistoryView();
                    Document.Id = item.Id;
                    Document.TaskId = item.TaskId;
                    Document.FileName = item.FileName;
                    Document.FilePath = item.FilePath;
                    Document.CreatedBy = item.CreatedBy;
                    Document.CreatedDate = item.CreatedDate;
                    Document.Guid = item.Guid;

                    var fileBytes = System.IO.File.ReadAllBytes(item.FilePath);
                    var base64String = Convert.ToBase64String(fileBytes);

                    Document.Base64Data = base64String;

                    DocumentList.Add(Document);
                }

                return GetResponseModel(Constants.httpCodeSuccess, DocumentList, "Task is Available", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel DeleteTaskById(int id)
        {
            try
            {
                var taskList = fleetManagerContext.TaskMains.Find(id);
                if (taskList == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null,
                        "Task Not found", false);
                }
                else
                {
                    fleetManagerContext.TaskMains.Remove(taskList);
                    fleetManagerContext.SaveChanges();
                }

                return GetResponseModel(Constants.httpCodeSuccess, null,
                        "Task Deleted Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        /* public ResponseModel GetTicketNo()
         {
             try
             {
                 var maxTicketNo = fleetManagerContext.TaskMains.Max(e => e.TicketNo);

                 if (maxTicketNo == null || !maxTicketNo.StartsWith("FM-"))
                 {
                     return GetResponseModel(Constants.httpCodeSuccess, null, "Ticket Number Not found", false);
                 }
                 string numericPart = maxTicketNo.Substring(3);
                 if (int.TryParse(numericPart, out int number))
                 {
                     int nextNumber = number + 1;

                     return GetResponseModel(Constants.httpCodeSuccess, "FM-" + nextNumber, "Get Ticket Number Successfully", true);
                 }
                 return GetResponseModel(Constants.httpCodeFailure, null, "Failed to parse numeric part of the ticket number", false);
             }
             catch (Exception ex)
             {
                 return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
             }
         } */

        public ResponseModel GetTicketNo()
        {
            try
            {
                string numericPart = string.Empty;
                int number = 0;
                var ticketNo = fleetManagerContext.TaskMains.OrderByDescending(e => e.Id).Select(e => e.TicketNo).FirstOrDefault();
                if (ticketNo == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null,
                        "Ticket Number Not found", false);
                }
                foreach (char c in ticketNo)
                {
                    if (char.IsDigit(c))
                    {
                        numericPart += c;
                    }
                }
                if (!string.IsNullOrEmpty(numericPart))
                {
                    number = Convert.ToInt32(numericPart);
                }

                int nextNumber = number + 1;

                return GetResponseModel(Constants.httpCodeSuccess, "FM-" + nextNumber,
                        "Get Ticket Number Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetHistoryTask(int? task_id = null)
        {
            try
            {
                var TaskList = fleetManagerContext.AuditTaskMains.Where(d => d.TaskId == task_id).ToList();
                if (TaskList.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "History Not Found", false);
                }
                return GetResponseModel(Constants.httpCodeSuccess, TaskList, "All History Listed", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }

        }
        public ResponseModel DeleteImageaskById(int? id = null)
        {
            try
            {
                var imageId = fleetManagerContext.TaskDocuments.Find(id);
                if (imageId == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null,
                        "Document Not found", false);
                }
                else
                {
                    fleetManagerContext.TaskDocuments.Remove(imageId);
                    fleetManagerContext.SaveChanges();
                }

                return GetResponseModel(Constants.httpCodeSuccess, null,
                        "Document Deleted Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GenerateRecordId()
        {
            try
            {
                int recordId = 0;
                List<DocUploadHistory> list = fleetManagerContext.DocUploadHistories.Where(d => d.Entityname == "MasterUpload").ToList();
                if (list.Count > 0)
                {
                    var record = list.Count;
                    recordId = record + 1;
                }
                else
                {
                    recordId = 1;
                }
                return GetResponseModel(Constants.httpCodeSuccess, recordId, "New RecordId", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetDocumentMaster()
        {
            try
            {
                var task = fleetManagerContext.DocumentMasters.ToList();

                List<DocumentMasterView> documentMasterList = new List<DocumentMasterView>();
                foreach (var item in task)
                {
                    DocumentMasterView document = new DocumentMasterView();
                    document.Id = item.Id;
                    document.DocumentId = item.DocumentId;
                    document.DocumentName = item.DocumentName;
                    document.DocumentDescription = item.DocumentDescription;
                    document.DocumentContent = item.DocumentContent;
                    document.AttachmentLink = item.AttachmentLink;
                    document.TemplatePath = item.TemplatePath;
                    document.DocumentType = item.DocumentType;
                    document.Status = item.Status;
                    document.CreatedBy = item.CreatedBy;
                    document.CreatedDate = item.CreatedDate;
                    document.ModifiedBy = item.ModifiedBy;
                    document.ModifiedDate = item.ModifiedDate;
                    document.FileName = item.FileName;

                    var fileBytes = System.IO.File.ReadAllBytes(item.TemplatePath);
                    var base64String = Convert.ToBase64String(fileBytes);

                    document.Base64Data = base64String;

                    documentMasterList.Add(document);
                }
                return GetResponseModel(Constants.httpCodeSuccess, documentMasterList, "Document is Available", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel UploadAndSaveExcelData(UploadAndSaveExcelFormView uploadAndSaveExcelForm)
        {
            uploadAndSaveExcelForm.UserName = "Admin";
            try
            {

                int numberOfFiles = uploadAndSaveExcelForm.files.Count;
                bool checkFileFormat = true;

                foreach (var check in uploadAndSaveExcelForm.files)
                {
                    if (check.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" ||
                           !Path.GetExtension(check.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                    {
                        checkFileFormat = false;
                        return GetResponseModel(Constants.httpCodeFailure, null, "Invalid file format. Please upload Excel files with .xlsx extension.", false);
                    }
                    else
                    {
                        checkFileFormat = true;
                    }
                }
                foreach (var file in uploadAndSaveExcelForm.files)
                {
                
                var fileName = Path.GetFileName(file.FileName);
                    //var folderPath = @"D:\Git Development\Suzlon\fleetmanager-bk\UsersManagementService\FileUpload\";
                    var folderPath = "/home/ubuntu/FleetManager/fleetmanager-bk/UsersManagementService/FileUpload";
                    var filePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    FileInfo fileInfo = new FileInfo(filePath);
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    ExcelPackage package = new ExcelPackage(fileInfo);
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    int rows = worksheet.Dimension.Rows;

                    List<BulkUploadConfiguration> bulkData = new List<BulkUploadConfiguration>();
                    bulkData = fleetManagerContext.BulkUploadConfigurations.Where(x => x.MasterName == uploadAndSaveExcelForm.MasterName).ToList();
                    List<Dictionary<string, dynamic>> bulkD = new List<Dictionary<string, dynamic>>();
                    for (int i = 2; i <= rows; i++)
                    {
                        int excount = (int)bulkData.Max(x => x.ExcelSequence);
                        Dictionary<string, dynamic> bulkDt = new Dictionary<string, dynamic>();
                        //var excount = worksheet.Columns.Count();
                        for (int j = 1; j <= excount; j++)
                        {
                            var data = bulkData.Where(x => x.ExcelSequence == j).FirstOrDefault();
                            var key = data.FieldName;

                            //This should be datatype check

                            if (data.RegularExpression.ToLower() == "special")
                            {
                                var regex = new Regex(@"[^a-zA-Z0-9\s]");
                                if (regex.IsMatch(Convert.ToString(worksheet.Cells[i, j].Value)))
                                {
                                    //_msg.Data = $"Special Charactors are not allowed ={data.FieldName} at row {i}";
                                    return GetResponseModel(Constants.httpCodeFailure, null, $"Special Charactors are not allowed ={data.FieldName} at row {i}", false);
                                }
                            }
                            else if (data.RegularExpression.ToLower() == "mobile")
                            {
                                var regex = new Regex(@"^[1-9]\d{9}$");
                                if (!regex.IsMatch(Convert.ToString(worksheet.Cells[i, j].Value)))
                                {
                                    //_msg.Data = $"Mobile No. not Valid at row {i}";
                                    return GetResponseModel(Constants.httpCodeFailure, null, $"Mobile No. not Valid at row {i}", false);
                                }

                            }
                            else if (data.RegularExpression.ToLower() == "email")
                            {
                                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                                if (!regex.IsMatch(Convert.ToString(worksheet.Cells[i, j].Value)))
                                {
                                    //_msg.Data = $"E-mail Id is not valid at row {i}";
                                    return GetResponseModel(Constants.httpCodeFailure, null, $"E-mail Id is not valid at row {i}", false);
                                }
                            }

                            var mandatory = Convert.ToString(worksheet.Cells[i, j].Value) != "" && data.Mandatory == "1";
                            var nonMandatory = Convert.ToString(worksheet.Cells[i, j].Value) == "" && data.Mandatory == "0";
                            var nMandatory = Convert.ToString(worksheet.Cells[i, j].Value) != "" && data.Mandatory == "0";
                            if (mandatory == true || nonMandatory == true || nMandatory == true)
                            {
                                
                                if (data.FeildDataType.ToLower() == "string")
                                {
                                    var value = Convert.ToString(worksheet.Cells[i, j].Value);
                                    bulkDt.Add(key, "'" + value + "'");
                                }
                                else if (data.FeildDataType.ToLower() == "int")
                                {
                                    var value = Convert.ToInt32(worksheet.Cells[i, j].Value);
                                    bulkDt.Add(key, value);
                                }
                                else if (data.FeildDataType.ToLower() == "bool")
                                {
                                    var value = Convert.ToBoolean(worksheet.Cells[i, j].Value);
                                    bulkDt.Add(key, value);
                                }
                                else if (data.FeildDataType.ToLower() == "long")
                                {
                                    var value = Convert.ToInt64(worksheet.Cells[i, j].Value);
                                    bulkDt.Add(key, value);
                                }
                                else if (data.FeildDataType.ToLower() == "datetime")
                                {
                                    var value = Convert.ToDateTime(worksheet.Cells[i, j].Value);
                                    bulkDt.Add(key, "'" + value + "'");
                                }
                            }
                            else
                            {
                                var man = data.FieldName;
                                //_msg.Data = $"Please fill Mandatory field = {man} at row = {i}";
                                return GetResponseModel(Constants.httpCodeFailure, null, $"Please Fill Mandatory Field = {man} at row = {i}", false);
                            }
                        }
                        var creatTIme = DateTime.Now;
                        var modifyTIme = DateTime.Now;
                        string modifyTime = modifyTIme.ToString("yyyy-MM-dd HH:mm:ss");
                        string creatTime = creatTIme.ToString("yyyy-MM-dd HH:mm:ss");
                        bulkDt.Add(bulkData[0].CreatedDateName, "'" + creatTime + "'");
                        bulkDt.Add(bulkData[0].ModifiedDateName, "'" + modifyTime + "'");
                        bulkDt.Add(bulkData[0].CreatedUserName, "'" + uploadAndSaveExcelForm.UserName + "'");
                        bulkDt.Add(bulkData[0].ModifiedUserName, "'" + uploadAndSaveExcelForm.UserName + "'");
                        
                        bulkDt.Add("Record_Id", "'" + uploadAndSaveExcelForm.recordId + "'");
                        bulkD.Add(bulkDt);
                    }
                    var columns = String.Join(",", bulkD[0].Keys.ToArray());
                    List<string> values = new List<string>();
                    foreach (var item in bulkD)
                    {
                        var str = String.Join(",", item.Values.ToArray());
                        values.Add("(" + str + ")");
                    }
                    var finalval = String.Join(",", values);
                    var query = "insert into " + bulkData[0].UploadTable + " (" + columns + ") values " + finalval;
                    query = query.Replace("'null'", "null");
                    DataSet ds = new DataSet();
                    var conn = fleetManagerContext.Database.GetDbConnection();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.Text;
                        string name_space = conn.GetType().FullName;
                        DbProviderFactory factory = DbProviderFactories.GetFactory(conn);
                        DbDataAdapter adapter = factory.CreateDataAdapter();
                        adapter.SelectCommand = cmd;
                        adapter.Fill(ds);
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            //_msg.Data = Newtonsoft.Json.JsonConvert.SerializeObject(dt);

                        }
                        else
                        {
                            
                        }

                    }
                    int Id = 0;
                    if (uploadAndSaveExcelForm.recordId != null)
                    {
                        Id = (int)uploadAndSaveExcelForm.recordId;
                    }
                    bulkUploadData(uploadAndSaveExcelForm.recordId);
                    return GetResponseModel(Constants.httpCodeSuccess, bulkD, "Data uploaded and saved successfully", true);

                }

                return GetResponseModel(Constants.httpCodeSuccess, null, "Data uploaded and saved successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel FileUpload(DocUploadHistoryViewModel model)
        {
            try
            {
                List<string> files = new List<string>();
                string vnum = "";
                if (model.file != null)
                {
                    for (int i = 0; i < model.file.Count; i++)
                    {
                        bool Check = IsFileAllowed(model.file[i].FileName, "");
                        if (!Check)
                        {
                            return GetResponseModel(Constants.httpCodeFailure, null, "Invalid File", false);
                        }
                    }
                }
                if (model.file != null)
                {
                    for (int i = 0; i < model.file.Count; i++)
                    {
                        if (model.mandate_id != null && model.entityname != null && model.file != null && model.RecordId != null)
                        {
                            FileInfo fileInfo = new FileInfo(model.file[i].FileName);
                            var filename = model.file[i].FileName;

                            if (model.entityname == "MasterUpload")
                            {
                                string folderpath = "/home/ubuntu/FleetManager/fleetmanager-bk/UsersManagementService/FileUpload";
                                //string folderpath = @"D:\Git Development\Suzlon\fleetmanager-bk\UsersManagementService\FileUpload\";
                                var fullpath = Path.Combine(folderpath, filename);

                                DocUploadHistory history = new DocUploadHistory();
                                history.MandateId = model.mandate_id;
                                history.Entityname = model.entityname;
                                history.RecordId = model.RecordId;
                                history.Documenttype = model.documenttype;
                                history.Remarks = model.remarks;
                                history.Status = "Active";
                                history.Filename = filename;
                                history.Filepath = fullpath;
                                history.CreatedBy = "Admin";
                                history.CreatedDate = DateTime.Now;
                                history.ModifiedDate = DateTime.Now;
                                history.ModifiedBy = "Admin";
                                fleetManagerContext.Add(history);
                                fleetManagerContext.SaveChanges();
                            }
                            files.Add(filename);
                        }
                    }
                }
                else
                {
                    return GetResponseModel(Constants.httpCodeFailure, null, "Files Not Found", false);
                }
                
                return GetResponseModel(Constants.httpCodeSuccess, files, "Files Uploaded Successfully", true);
                 
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetDocUploadHistory(int employee_ID, string entityname, int? RecordId, string? documentType, string Status)
        {
            try
            {
                var dochistory = fleetManagerContext.DocUploadHistories.Where(d => d.MandateId == employee_ID && d.Entityname == entityname).ToList();
                if (Status != null)
                {
                    if (Status == "Completed")
                    {
                        dochistory = dochistory.Where(x => (x.Status == "Completed" || x.Status == "Partially Completed")).ToList();
                    }
                    else if (Status == "Error")
                    {
                        dochistory = dochistory.Where(x => (x.Status == "Failed" || x.Status == "Partially Completed")).ToList();
                    }
                    else
                    {
                        dochistory = dochistory.Where(x => (x.Status != "Completed" && x.Status != "Failed" && x.Status != "Partially Completed")).ToList();
                    }
                }
                if (RecordId != null)
                {
                    dochistory = dochistory.Where(x => x.RecordId == RecordId).ToList();
                }
                if (documentType != null)
                {
                    dochistory = dochistory.Where(x => x.Documenttype == documentType).ToList();
                }
                if (dochistory != null)
                {
                    var history = dochistory.Where(s => entityname.Contains(s.Entityname)).OrderByDescending(s => s.CreatedDate).ToList();
                    if (history != null)
                    {
                        return GetResponseModel(Constants.httpCodeSuccess, history, "Data", true);
                    }
                    return GetResponseModel(Constants.httpCodeSuccess, null, "Nothing", true);
                }
                return GetResponseModel(Constants.httpCodeSuccess, null, "Nothing", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetUploadedFile(int recordId, int employee_ID, string DocumentType)
        {
            try
            {
                var File = fleetManagerContext.DocUploadHistories.Where(d => d.RecordId == recordId && d.MandateId == employee_ID && 
                d.Documenttype == DocumentType).FirstOrDefault();
                if (File == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "File Not Available", false);
                }
                DownloadUploadedFile file = new DownloadUploadedFile();
                
                var fileBytes = System.IO.File.ReadAllBytes(File.Filepath);
                var base64String = Convert.ToBase64String(fileBytes);

                file.base64String = base64String;
                file.filename = File.Filename;

                return GetResponseModel(Constants.httpCodeSuccess, file, "Data Retrived Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel bulkUploadData(int? RecordId)
        {
            try
            {
                DataSet ds = new DataSet();
                var conn = fleetManagerContext.Database.GetDbConnection();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "sp_upload_planning_sheet";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@UploadNo", RecordId));
                   
                    string name_space = conn.GetType().FullName;
                    DbProviderFactory factory = DbProviderFactories.GetFactory(conn);
                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    List<dynamic> bulkUpload = new List<dynamic>();
                    foreach (DataRow row in dt.Rows)
                    {
                        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>();
                        for (int k = 0; k < dt.Columns.Count; k++)
                        {
                            data.Add(dt.Columns[k].ColumnName, row.ItemArray[k]);

                        }
                        bulkUpload.Add(data);
                    }
                    return GetResponseModel(Constants.httpCodeSuccess, bulkUpload, "Record Updated Successfully", true);
                }

            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel EmployeeDetails(string? EmployeeCode)
        {
            try
            {
                var getEmployeeDetail = GetUserDetails(EmployeeCode);
                return GetResponseModel(Constants.httpCodeSuccess, getEmployeeDetail, "Data Retrieve Successfully", true);

            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeSuccess, null, ex.Message, false);
            }
        }
        private ResponseEmployeeDetail GetUserDetails(string? EmployeeCode)
        {

            try
            {

                string domainId = Base64Encode(EmployeeCode);
                string password = "a";
                string appType = "windowsAuth";
                //string URI = "http://172.16.11.74:8090/api/SuzlonActiveUser/IsUserActive";
                string URI = "https://uat-mob.suzlon.com/SuzlonActiveUser/api/SuzlonActiveUser/IsUserActive";
                string myParameters = $"domainId={domainId}&password={password}&appType={appType}";


                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                // Checking data from api
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string htmlResult = wc.UploadString(URI, myParameters);
                    var userResult = ConvertUserDetailsToJson(htmlResult);

                    if (userResult != null && userResult.IsActive)
                    {
                        ResponseEmployeeDetail response = new ResponseEmployeeDetail();
                        response.Code = userResult.DomainId;
                        response.Name = userResult.Name;
                        return response;
                    }
                    else
                    {

                        return null;
                    }

                }


            }
            catch (Exception)
            {

                return null;

            }

        }

        private static string Base64Encode(string text)
        {
            var textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(textBytes);
        }
        private static UserLogin ConvertUserDetailsToJson(string text)
        {
            return JsonConvert.DeserializeObject<UserLogin>(text)!;
        }
        private bool IsFileAllowed(string FileName, string fileType)
        {
            bool ISallowed = false;
            if (fileType == "Image")
            {
                ISallowed = true;
            }
            else
            {
                string pattern = @"[^\w\d\s\(\)]";
                var Name = Path.GetFileNameWithoutExtension(FileName);
                ISallowed = !Regex.IsMatch(Name, pattern);
            }
            if (ISallowed)
            {
                var Extension = Path.GetExtension(FileName).ToLower();
                if (fileType == "Image")
                {
                    string[] allowedExtension = { ".jpg", ".jpeg", ".png" };
                    ISallowed = allowedExtension.Contains(Extension);
                }
                else
                {
                    string[] allowedExtension = { ".jpg", ".jpeg", ".png" , ".rtf", ".email", ".eml", ".xls", ".xlm", ".xlsx", ".ods", ".csv",
                                                  ".doc", ".docx", ".pdf", ".ppt", ".pptx", ".pps", ".mp4", ".zip",".7z"};
                    ISallowed = allowedExtension.Contains(Extension);
                }
            }
            return ISallowed;
        }

        private DateTime CalculateDueDate(DateTime currentDate, string? allowedDays, string status)
        {
            if (string.IsNullOrEmpty(allowedDays) || !int.TryParse(allowedDays, out int days))
            {
                // Handle invalid or missing allowedDays
                return DateTime.MinValue;
            }

            if (status == "TO DO")
            {
                return currentDate.AddDays(days);
            }
            else if (status == "DONE")
            {
                return currentDate.AddDays(-days);
            }
            else
            {
                // Handle other statuses if necessary
                return DateTime.MinValue;
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
