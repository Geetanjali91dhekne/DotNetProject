using UsersManagementService.Models;

namespace UsersManagementService.ViewModels
{
    public class ResponseTaskMain
    {
        public int Id { get; set; }

        public string? TicketNo { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int StatusId { get; set; }

        public string? StatusChangedBy { get; set; }

        public DateTime? StatusChangedDate { get; set; }

        public string[]? AssignedToUser { get; set; }

        public string? AssignedToGroup { get; set; }

        public DateTime? DueDate { get; set; }

        public int TaskTypeId { get; set; }

        public int? TaskParentId { get; set; }

        public string? Label { get; set; }

        public int SprintId { get; set; }

        public int PriorityId { get; set; }

        public string? Location { get; set; }

        public int NatureOfTaskId { get; set; }

        public string? Comment { get; set; }

        public string[]? Reviewer { get; set; }

        public int? Sequence { get; set; }
        public string? SiteCode { get; set; }

        public string? SiteName { get; set; }
        public string? SiteIncharge { get; set; }
        
    }
    public class GetTaskMain
    {
        public int Id { get; set; }

        public string? TicketNo { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public TaskManagementStatus? StatusId { get; set; }

        public string? StatusChangedBy { get; set; }

        public DateTime? StatusChangedDate { get; set; }

        public List<CmrEmployeeMaster>? AssignedToUser { get; set; }

        public string? AssignedToGroup { get; set; }

        public DateTime? DueDate { get; set; }

        public TLogbookCommonMaster? TaskTypeId { get; set; }

        public int? TaskParentId { get; set; }

        public string? Label { get; set; }

        public TLogbookCommonMaster? SprintId { get; set; }

        public TLogbookCommonMaster? PriorityId { get; set; }

        public string? Location { get; set; }

        public TLogbookCommonMaster? NatureOfTaskId { get; set; }

        public string? Comment { get; set; }

        public List<CmrEmployeeMaster>? Reviewer { get; set; }

        public int? Sequence { get; set; }
        public string? SiteIncharge { get; set; }
        public string? SiteName { get; set; }
    }
    public class GetTaskMainId
    {
        public int Id { get; set; }

        public string? TicketNo { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public TaskManagementStatus? StatusId { get; set; }

        public string? StatusChangedBy { get; set; }

        public DateTime? StatusChangedDate { get; set; }

        public List<CmrEmployeeMaster>? AssignedToUser { get; set; }

        public string? AssignedToGroup { get; set; }

        public DateTime? DueDate { get; set; }

        public TLogbookCommonMaster? TaskTypeId { get; set; }

        public int? TaskParentId { get; set; }

        public string? Label { get; set; }

        public TLogbookCommonMaster? SprintId { get; set; }

        public TLogbookCommonMaster? PriorityId { get; set; }

        public string? Location { get; set; }

        public TLogbookCommonMaster? NatureOfTaskId { get; set; }

        public string? Comment { get; set; }

        public List<CmrEmployeeMaster>? Reviewer { get; set; }

        public int? Sequence { get; set; }
        public string? SiteIncharge { get; set; }
        public ScMainSite? SiteCode { get; set; }
    }
    public class ExcelTaskMain

    {
        public int Id { get; set; }

        public string? TicketNo { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? StatusId { get; set; }


        public string? StatusChangedBy { get; set; }

        public DateTime? StatusChangedDate { get; set; }


        public string? AssignedToUser { get; set; }

        public string? AssignedToGroup { get; set; }

        public DateTime? DueDate { get; set; }


        public string? TaskTypeId { get; set; }

        public int? TaskParentId { get; set; }

        public string? Label { get; set; }


        public string? SprintId { get; set; }

        public string? PriorityId { get; set; }

        public string? Location { get; set; }

        public string? NatureOfTaskId { get; set; }

        public string? Comment { get; set; }

        public string? Reviewer { get; set; }

        public int? Sequence { get; set; }
    }
    public class SequenceTaskMng
    {
        public int Id { get; set; }

        public string? TicketNo { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public TaskManagementStatus? StatusId { get; set; }

        public string? StatusChangedBy { get; set; }

        public DateTime? StatusChangedDate { get; set; }

        public List<string>? AssignedToUser { get; set; }
        //public List<CmrEmployeeMaster>? AssignedToUser { get; set; }

        public string? AssignedToGroup { get; set; }

        public DateTime? DueDate { get; set; }

        public TLogbookCommonMaster? TaskTypeId { get; set; }

        public int? TaskParentId { get; set; }

        public string? Label { get; set; }

        public TLogbookCommonMaster? SprintId { get; set; }

        public TLogbookCommonMaster? PriorityId { get; set; }

        public string? Location { get; set; }

        public TLogbookCommonMaster? NatureOfTaskId { get; set; }

        public string? Comment { get; set; }

        public List<string>? Reviewer { get; set; }
        //public List<CmrEmployeeMaster>? Reviewer { get; set; }

        public int? Sequence { get; set; }
        public string? SiteIncharge { get; set; }
    }


    public class updateTaskMngrModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
