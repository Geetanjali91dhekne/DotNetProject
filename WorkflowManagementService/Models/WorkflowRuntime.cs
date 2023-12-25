using System;
using System.Collections.Generic;

namespace WorkflowManagementService.Models;

public partial class WorkflowRuntime
{
    public int Id { get; set; }

    public string? WorkItem { get; set; }

    public string? TableName { get; set; }

    public int TableId { get; set; }

    public string? StepNo { get; set; }

    public string? RoleName { get; set; }

    public string? UserName { get; set; }

    public DateTime? Submittedon { get; set; }

    public string? SubmittedBy { get; set; }

    public string? Stdmsg { get; set; }
}
