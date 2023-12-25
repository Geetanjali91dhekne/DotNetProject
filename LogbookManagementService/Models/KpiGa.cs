using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class KpiGa
{
    public int Id { get; set; }

    public int? Value { get; set; }

    public DateTime? LogDate { get; set; }
}
