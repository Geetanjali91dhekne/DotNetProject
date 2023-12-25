using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class TWhyAnalysis
{
    public int Id { get; set; }

    public DateTime? AnalysisDate { get; set; }

    public string? State { get; set; }

    public string? Site { get; set; }

    public string? Section { get; set; }

    public string? ModelName { get; set; }

    public string? SapCode { get; set; }

    public string? TowerType { get; set; }

    public int? Week { get; set; }

    public bool? CheckMark { get; set; }

    public string? Remarks1 { get; set; }

    public string? Remarks2 { get; set; }

    public string? StandardRemarks { get; set; }

    public string? OverallActionItem { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? MainBucket { get; set; }

    public string? SubBucket { get; set; }

    public string? GrandTotal { get; set; }
    public string? Status { get; set; }
}
