using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class KpiLs
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public string? Wtg { get; set; }

    public string? Status { get; set; }

    public DateTime? LogDate { get; set; }
}
