using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class CrmBreakdownDetail
{
    public string? BdType { get; set; }

    public string? ErrorCode { get; set; }

    public DateTime? DateOfGen { get; set; }

    public string? State { get; set; }

    public string? AreaCode { get; set; }

    public string? MainSiteCode { get; set; }

    public string? SiteCode { get; set; }

    public string? LocNo { get; set; }

    public string? FCode { get; set; }

    public string? BdRemark { get; set; }

    public DateTime? BdStartTime { get; set; }

    public DateTime? BdEndTime { get; set; }

    public double? TotalDuration { get; set; }

    public string? TravelTime { get; set; }

    public string? AttendedBy { get; set; }

    public string? WorkingAt { get; set; }

    public string? ActionTaken { get; set; }

    public string? IsDataCompleted { get; set; }

    public double? IsSubmitted { get; set; }

    public decimal Detailid { get; set; }

    public string? Enteredby { get; set; }

    public DateTime? Enteredon { get; set; }

    public string? SapFuncLocCode { get; set; }

    public string? Classification { get; set; }

    public string? Commercial { get; set; }

    public string? SystemComponent { get; set; }

    public string? DowntimeType { get; set; }

    public string? FormulaParameter { get; set; }

    public Guid Rowid { get; set; }
}
