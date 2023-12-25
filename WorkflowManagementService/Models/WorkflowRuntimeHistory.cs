using System;
using System.Collections.Generic;

namespace WorkflowManagementService.Models;

public partial class WorkflowRuntimeHistory
{
    public int Id { get; set; }

    public string? WorkItem { get; set; }

    public string? TableName { get; set; }

    public int TableId { get; set; }

    public string? StepNo { get; set; }

    public string? RoleName { get; set; }

    public string? UserName { get; set; }

    public DateTime? Createdon { get; set; }

    public string? CreatedBy { get; set; }

    public string? Status { get; set; }

    public string? Remarks { get; set; }

    public DateTime? TaskCreatedOn { get; set; }
}
