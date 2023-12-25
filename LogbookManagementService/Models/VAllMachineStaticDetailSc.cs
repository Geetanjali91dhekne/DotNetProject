using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class VAllMachineStaticDetailSc
{
    public string LocNo { get; set; } = null!;

    public string SiteCode { get; set; } = null!;

    public string? GridCode { get; set; }

    public string? FeederCode { get; set; }

    public string? MeteringPoint { get; set; }

    public string? WtgModelCode { get; set; }

    public decimal? InstCapacity { get; set; }

    public string? GroupCode { get; set; }

    public string? CustomerCode { get; set; }

    public string? MmCode { get; set; }

    public decimal? CirCode { get; set; }

    public DateTime? CommDate { get; set; }

    public string? PhaseNum { get; set; }

    public string? ControlPanel { get; set; }

    public string? Section { get; set; }

    public decimal? HubHeight { get; set; }

    public string SapFuncLocCode { get; set; } = null!;

    public decimal? RotarDia { get; set; }

    public decimal? MF { get; set; }

    public string? MeterNo { get; set; }

    public string? SCNo { get; set; }

    public decimal? GurantedUnits { get; set; }

    public string? MapId { get; set; }

    public string? WtgId { get; set; }

    public DateTime? CommOpDate { get; set; }

    public decimal? Eb { get; set; }

    public decimal? Capitive { get; set; }

    public decimal? Thirdpartty { get; set; }

    public string? CrmsAccMgr { get; set; }

    public string? MktgAccMgr { get; set; }

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

    public string GroupName { get; set; } = null!;

    public string? MainCustomerCode { get; set; }

    public string? CustomerName { get; set; }

    public string? Client { get; set; }

    public string? CountryCode { get; set; }

    public string? Country { get; set; }

    public string? StateCode { get; set; }

    public string? State { get; set; }

    public string? AreaCode { get; set; }

    public string? Area { get; set; }

    public string? MainSiteCode { get; set; }

    public string? MainSite { get; set; }

    public string? Site { get; set; }

    public string? UseForSelection { get; set; }

    public string? DelKey { get; set; }

    public string? SiteShortName { get; set; }

    public string? SbuOnm { get; set; }

    public string? WtgModelName { get; set; }

    public string? WtgModelDesc { get; set; }

    public string? FeederName { get; set; }

    public decimal? InstalledCapacityMw { get; set; }

    public string? GridName { get; set; }

    public string? MmName { get; set; }

    public decimal? Reportonmaxz5 { get; set; }

    public string? MtrPointFuncLocCode { get; set; }

    public decimal? DgrIsActive { get; set; }

    public string? WtgMake { get; set; }
}
