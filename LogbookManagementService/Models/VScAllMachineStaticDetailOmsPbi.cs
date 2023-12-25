using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class VScAllMachineStaticDetailOmsPbi
{
    public string? WtgMake { get; set; }

    public string? LocNo { get; set; }

    public string? SapFuncLocCode { get; set; }

    public string? CustomerCode { get; set; }

    public string? CustomerName { get; set; }

    public string? SiteCode { get; set; }

    public string? Site { get; set; }

    public string MainSiteCode { get; set; } = null!;

    public string? MainSite { get; set; }

    public string AreaCode { get; set; } = null!;

    public string? Area { get; set; }

    public string StateCode { get; set; } = null!;

    public string? State { get; set; }

    public string CountryCode { get; set; } = null!;

    public string? Country { get; set; }

    public DateTime? CommDate { get; set; }

    public string? MeteringPoint { get; set; }

    public decimal? InstCapacity { get; set; }

    public string? FeederCode { get; set; }

    public string? FeederName { get; set; }

    public string? GridCode { get; set; }

    public string? GridName { get; set; }

    public string? WtgModelCode { get; set; }

    public string? WtgModelName { get; set; }

    public string? MmCode { get; set; }

    public string? MeterNo { get; set; }

    public string? PhaseNum { get; set; }

    public string? ControlPanel { get; set; }

    public decimal? HubHeight { get; set; }

    public decimal? RotarDia { get; set; }

    public decimal? MF { get; set; }

    public string? Region { get; set; }

    public string? District { get; set; }

    public string? Taluka { get; set; }

    public string? Village { get; set; }

    public string? Surveyno { get; set; }

    public string? Longitude { get; set; }

    public string? Latitude { get; set; }

    public decimal? Meansealevel { get; set; }

    public string? Substation { get; set; }

    public string? Bladetype { get; set; }

    public string? Hubtype { get; set; }

    public string? Towertype { get; set; }

    public decimal? Reportonmaxz5 { get; set; }

    public DateTime? Cod { get; set; }

    public decimal? GenEstimateBd { get; set; }

    public decimal? GenEstimateWrd { get; set; }

    public string? MktGroupCode { get; set; }

    public string? FormulaType { get; set; }

    public string? GridNode { get; set; }

    public string? MtrPointFuncLocCode { get; set; }

    public string? MktgGroupCode { get; set; }

    public string? MktGroupName { get; set; }

    public string? KamName { get; set; }

    public string? KamState { get; set; }
}
