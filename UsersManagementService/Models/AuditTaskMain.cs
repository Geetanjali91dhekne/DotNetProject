using System;
using System.Collections.Generic;

namespace UsersManagementService.Models;

public partial class AuditTaskMain
{
    public int Id { get; set; }

    public string? Action { get; set; }

    public int? TaskId { get; set; }

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

    public string? AssignedToUser { get; set; }

    public string? AssignedToGroup { get; set; }

    public DateTime? DueDate { get; set; }

    public int TaskTypeId { get; set; }

    public int TaskParentId { get; set; }

    public string? Label { get; set; }

    public int SprintId { get; set; }

    public int PriorityId { get; set; }

    public string? Location { get; set; }

    public int NatureOfTaskId { get; set; }

    public string? Comment { get; set; }

    public string? Reviewer { get; set; }

    public string? ChangedBy { get; set; }

    public DateTime? ChangedDate { get; set; }
}
