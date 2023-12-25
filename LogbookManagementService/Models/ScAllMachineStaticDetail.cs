using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class ScAllMachineStaticDetail
{
    public string LocNo { get; set; } = null!;

    public string? GroupCode { get; set; }

    public string? CustomerCode { get; set; }

    public string SiteCode { get; set; } = null!;

    public string? Section { get; set; }

    public decimal? CirCode { get; set; }

    public string? PhaseNum { get; set; }

    public string SapFuncLocCode { get; set; } = null!;

    public DateTime? CommDate { get; set; }

    public string? WtgModelCode { get; set; }

    public decimal? InstCapacity { get; set; }

    public string? FeederCode { get; set; }

    public string? GridCode { get; set; }

    public string? MeteringPoint { get; set; }

    public string? ControlPanel { get; set; }

    public decimal? HubHeight { get; set; }

    public decimal? RotarDia { get; set; }

    public string? MmCode { get; set; }

    public string? MeterNo { get; set; }

    public decimal? MF { get; set; }

    public decimal? GurantedUnits { get; set; }

    public string? SCNo { get; set; }

    public string? MapId { get; set; }

    public string? WtgId { get; set; }

    public DateTime? CommOpDate { get; set; }

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

    public DateTime? SapExpDate { get; set; }

    public string? SapExpFlag { get; set; }

    public decimal? Eb { get; set; }

    public decimal? Capitive { get; set; }

    public decimal? Thirdpartty { get; set; }

    public string? CrmsAccMgr { get; set; }

    public string? MktgAccMgr { get; set; }

    public string? Enteredby { get; set; }

    public DateTime? Enteredon { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedon { get; set; }

    public decimal Isactive { get; set; }

    public decimal? Reportonmaxz5 { get; set; }

    public string? Ipaddress { get; set; }

    public decimal? Plantid { get; set; }

    public string? Plantname { get; set; }

    public decimal? Plantunitid { get; set; }

    public DateTime? Cod { get; set; }

    public string? MaFormula { get; set; }

    public string? AdjSapFuncLocCode1 { get; set; }

    public string? AdjSapFuncLocCode2 { get; set; }

    public decimal? GenEstimateBd { get; set; }

    public decimal? GenEstimateWrd { get; set; }

    public string? RefMetMast { get; set; }

    public string? MktGroupCode { get; set; }

    public string? GridNode { get; set; }

    public string? MtrPointFuncLocCode { get; set; }

    public decimal? DgrIsActive { get; set; }

    public string? WtgMake { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
