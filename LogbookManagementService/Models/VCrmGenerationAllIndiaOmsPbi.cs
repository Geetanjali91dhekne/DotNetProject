using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class VCrmGenerationAllIndiaOmsPbi
{
    public decimal? Srno { get; set; }

    public string GenerationNo { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string? MeteringPoint { get; set; }

    public string? FCode { get; set; }

    public string? CustomerCode { get; set; }

    public string? MainSiteCode { get; set; }

    public string SiteCode { get; set; } = null!;

    public decimal? InstCapacity { get; set; }

    public string? FnYear { get; set; }

    public string? Yyyymm { get; set; }

    public DateTime? DateOfGen { get; set; }

    public decimal? DailySmallG { get; set; }

    public decimal? DailyBigG { get; set; }

    public decimal? TotalDaily { get; set; }

    public decimal? Monthly { get; set; }

    public decimal? Yearly { get; set; }

    public decimal? GenHrSmallG { get; set; }

    public decimal? GenHrBigG { get; set; }

    public decimal? TotalGenHr { get; set; }

    public decimal? DailyPlf { get; set; }

    public decimal? MonthlyPlf { get; set; }

    public decimal? YearlyPlf { get; set; }

    public decimal? Z1 { get; set; }

    public decimal? Z4 { get; set; }

    public decimal? Z5 { get; set; }

    public decimal? MachineAvailabilty { get; set; }

    public decimal? GridAvailabilty { get; set; }

    public decimal? InadequateWindSpeed { get; set; }

    public decimal? PreventiveBreakDownHours { get; set; }

    public decimal? BreakDownHours { get; set; }

    public string? BreakDownDetailRemark { get; set; }

    public decimal? GridDownHrIntPrev11 { get; set; }

    public decimal? GridDownHrExtPrev11 { get; set; }

    public decimal? GridDownHrIntBreak11 { get; set; }

    public decimal? GridDownHrExtBreak11 { get; set; }

    public decimal? GridDownHrIntPrev33 { get; set; }

    public decimal? GridDownHrExtPrev33 { get; set; }

    public decimal? GridDownHrIntBreak33 { get; set; }

    public decimal? GridDownHrExtBreak33 { get; set; }

    public decimal? GridDownHrIntPrev66 { get; set; }

    public decimal? GridDownHrExtPrev66 { get; set; }

    public decimal? GridDownHrIntBreak66 { get; set; }

    public decimal? GridDownHrExtBreak66 { get; set; }

    public decimal? ExpKwh { get; set; }

    public decimal? ExpKvarh { get; set; }

    public decimal? ImpKwh { get; set; }

    public decimal? ImpKvarh { get; set; }

    public decimal? EbMainKwhExp { get; set; }

    public decimal? EbMainKwhImp { get; set; }

    public decimal? EbMainNetKwhExp { get; set; }

    public decimal? EbMainKvarhExp { get; set; }

    public decimal? EbMainKvarhImp { get; set; }

    public decimal? EbMainNetKvarhExp { get; set; }

    public decimal? EbMainLineLoss { get; set; }

    public decimal? EbBackKwhExp { get; set; }

    public decimal? EbBackKwhImp { get; set; }

    public decimal? EbBackNetKwhExp { get; set; }

    public decimal? EbBackKvarhExp { get; set; }

    public decimal? EbBackKvarhImp { get; set; }

    public decimal? EbBackNetKvarhExp { get; set; }

    public decimal? EbBackLineLoss { get; set; }

    public string? SectionNo { get; set; }

    public string? Phase { get; set; }

    public decimal? KvahImport { get; set; }

    public decimal? KvahExport { get; set; }

    public string? AddUser { get; set; }

    public DateTime? AddDate { get; set; }

    public string? DelKey { get; set; }

    public decimal? ScheduleMaintenance { get; set; }

    public string? SapExpFlag { get; set; }

    public DateTime? SapExpDate { get; set; }

    public string? SapDataFlag { get; set; }

    public string? G1g2Swap { get; set; }

    public string? GridBreakdownRemarks { get; set; }

    public double? Issubmitted { get; set; }

    public string? Submittedby { get; set; }

    public DateTime? Submittedon { get; set; }

    public decimal? MaxZ5 { get; set; }

    public decimal? Isprocess { get; set; }

    public decimal? Ismonprocess { get; set; }

    public string? SapFuncLocCode { get; set; }

    public string? ExtGridBreakdownRemarks { get; set; }

    public decimal? LoadShedding { get; set; }

    public string? GroupCode { get; set; }

    public decimal? OldMaxZ5 { get; set; }

    public decimal? Maxz5Ma { get; set; }

    public decimal? ContMa { get; set; }

    public decimal? Gf { get; set; }

    public decimal? Fm { get; set; }

    public decimal? S { get; set; }

    public decimal? U { get; set; }

    public decimal? C { get; set; }

    public decimal? T { get; set; }

    public decimal? A { get; set; }

    public decimal? B { get; set; }

    public decimal? Lw { get; set; }

    public decimal? Hw { get; set; }

    public decimal? Dailygenhrs { get; set; }

    public decimal? Il { get; set; }

    public decimal? Ls { get; set; }

    public decimal? Trun { get; set; }

    public decimal? Tmo { get; set; }

    public decimal? Ttotal { get; set; }

    public decimal? Tstop { get; set; }

    public decimal? Tgrid { get; set; }

    public decimal? Nor { get; set; }

    public decimal? Gfgf { get; set; }

    public decimal? Gffm { get; set; }

    public decimal? Gfs { get; set; }

    public decimal? Gfu { get; set; }

    public decimal? CmaDenominator { get; set; }

    public decimal? CmaNumerator { get; set; }

    public decimal? CgaDenominator { get; set; }

    public decimal? CgaNumerator { get; set; }

    public decimal? Rna { get; set; }

    public decimal? No { get; set; }

    public decimal? Od { get; set; }

    public decimal? Uad { get; set; }

    public decimal? OdEhv { get; set; }

    public decimal? Md { get; set; }

    public decimal? MdEhv { get; set; }

    public decimal? MdHv { get; set; }

    public decimal? IntlGfIntr { get; set; }

    public decimal? IntlGfPss { get; set; }

    public decimal? IntlGfExtr { get; set; }

    public decimal? IntlFm { get; set; }

    public decimal? IntlU { get; set; }

    public decimal? IntlS { get; set; }

    public decimal? IntlRna { get; set; }

    public decimal? SmaNum { get; set; }

    public decimal? SmaDen { get; set; }

    public decimal? IgaNum { get; set; }

    public decimal? PssGaNum { get; set; }

    public decimal? EgaNum { get; set; }

    public decimal? Sma { get; set; }

    public decimal? Iga { get; set; }

    public decimal? PssGa { get; set; }

    public decimal? Ega { get; set; }

    public decimal? GfI { get; set; }

    public decimal? GfIi { get; set; }

    public decimal? GfPss { get; set; }

    public decimal? Ra1Num { get; set; }

    public decimal? Ra1Den { get; set; }

    public decimal? Ra1 { get; set; }

    public decimal? Ra2Num { get; set; }

    public decimal? Ra2Den { get; set; }

    public decimal? Ra2 { get; set; }
}
