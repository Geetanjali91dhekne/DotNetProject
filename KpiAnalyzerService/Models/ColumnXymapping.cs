using System;
using System.Collections.Generic;

namespace KpiAnalyzerService.Models;

public partial class ColumnXymapping
{
    public int Id { get; set; }

    public string? TableName { get; set; }

    public string? ColumnName { get; set; }

    public string? Axis { get; set; }
}
