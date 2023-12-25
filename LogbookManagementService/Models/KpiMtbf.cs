using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class KpiMtbf
{
    public int Id { get; set; }

    public string? Model { get; set; }

    public int? Value { get; set; }

    public DateTime? LogDate { get; set; }
}
