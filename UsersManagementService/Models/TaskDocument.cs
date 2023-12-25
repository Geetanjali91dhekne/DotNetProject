using System;
using System.Collections.Generic;

namespace UsersManagementService.Models;

public partial class TaskDocument
{
    public int Id { get; set; }

    public int? TaskId { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Guid { get; set; }
}
