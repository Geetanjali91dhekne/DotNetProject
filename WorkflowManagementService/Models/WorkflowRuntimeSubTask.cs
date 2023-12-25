using System;
using System.Collections.Generic;

namespace WorkflowManagementService.Models;

public partial class WorkflowRuntimeSubTask
{
    public int Id { get; set; }

    public string? TableName { get; set; }

    public int TableId { get; set; }

    public int RuntimeId { get; set; }

    public string? RoleName { get; set; }

    public string? UserName { get; set; }

    public string? Status { get; set; }

    public string? WorkItem { get; set; }

    public string? Stepno { get; set; }
}
