using System;
using System.Collections.Generic;

namespace KpiAnalyzerService.Models;

public partial class DataFilterCondition
{
    public int Id { get; set; }

    public string? TableName { get; set; }

    public string? ColumnName { get; set; }

    public string? Condition { get; set; }

    public string? Value1 { get; set; }

    public string? Value2 { get; set; }
}
