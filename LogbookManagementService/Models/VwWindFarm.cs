using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class VwWindFarm
{
    public Guid? UserId { get; set; }

    public int PlantId { get; set; }

    public string PlantName { get; set; } = null!;

    public int PlantUnitId { get; set; }

    public string PlantUnitName { get; set; } = null!;

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public int? NominelPower { get; set; }

    public short? OwnerId { get; set; }

    public byte? ProductionStateCode { get; set; }

    public decimal? AccumulatedProduction { get; set; }

    public decimal? Production { get; set; }

    public decimal? WindSpeed { get; set; }

    public string? ControllerTimestamp { get; set; }

    public string? ServiceOrganizationName { get; set; }
}
