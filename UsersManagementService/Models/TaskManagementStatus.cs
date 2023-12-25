using System;
using System.Collections.Generic;

namespace UsersManagementService.Models;

public partial class TaskManagementStatus
{
    public int Id { get; set; }

    public string? Status { get; set; }

    public int? Sequence { get; set; }

    public string? Colorcode { get; set; }

    public string? Projectname { get; set; }

    public string? AllowedDays { get; set; }
}
