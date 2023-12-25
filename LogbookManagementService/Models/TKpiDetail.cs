using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class TKpiDetail
{
    public int Id { get; set; }

    public string? Parameter { get; set; }

    public string? Filters { get; set; }

    public string? Columnname { get; set; }

    public decimal? Value { get; set; }

    public string? Years { get; set; }

    public string? Unit { get; set; }

    public string? ReplaceFilter { get; set; }

    public string? ReplaceColumns { get; set; }

    public string? Total { get; set; }

    public string? Topen { get; set; }
}
