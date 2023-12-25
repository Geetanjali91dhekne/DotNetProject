using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class TurbinePerformanceMonth
{
    public int? PlantUnitId { get; set; }

    public DateTime? ControllerTimestamp { get; set; }

    public DateTime? ServerTimestamp { get; set; }

    public int? Numerator { get; set; }

    public int? Denominator { get; set; }

    public decimal? ProductionkWh { get; set; }

    public decimal? WindSpeed { get; set; }

    public int? BinCount { get; set; }

    public DateTime? CreateTimestamp { get; set; }

    public decimal? EnergyOutkWh { get; set; }

    public decimal? EnergyInkWh { get; set; }
}
