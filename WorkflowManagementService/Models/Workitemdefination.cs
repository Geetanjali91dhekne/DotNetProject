using System;
using System.Collections.Generic;

namespace WorkflowManagementService.Models;

public partial class Workitemdefination
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? TableName { get; set; }

    public string? Action { get; set; }

    public string? Url { get; set; }
}
