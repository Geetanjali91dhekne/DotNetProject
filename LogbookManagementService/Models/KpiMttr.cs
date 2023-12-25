using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class KpiMttr
{
    public int Id { get; set; }

    public string? Model { get; set; }

    public DateTime? LogDate { get; set; }

    public decimal? Value { get; set; }
}
