using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class KpiMa
{
    public int Id { get; set; }

    public decimal? Value { get; set; }

    public DateTime? LogDate { get; set; }
}
