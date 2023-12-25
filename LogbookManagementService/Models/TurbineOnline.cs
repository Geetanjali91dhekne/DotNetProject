using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class TurbineOnline
{
    public int PlantUnitId { get; set; }

    public string? ControllerTimestamp { get; set; }

    public DateTime? ServerTimestamp { get; set; }

    public byte? TurbineStateCode { get; set; }

    public short? EventCode { get; set; }

    public decimal? WindSpeed { get; set; }

    public decimal? Production { get; set; }

    public decimal? AccumulatedProduction { get; set; }

    public byte? ProductionStateCode { get; set; }
}
