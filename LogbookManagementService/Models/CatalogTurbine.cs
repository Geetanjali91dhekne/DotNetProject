using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class CatalogTurbine
{
    public int PlantUnitId { get; set; }

    public string PlantUnitName { get; set; } = null!;

    public int PlantId { get; set; }

    public int? SectionId { get; set; }

    public short? OwnerId { get; set; }

    public int TurbineModelId { get; set; }

    public int TurbineFamilyId { get; set; }

    public string FunctionalLocation { get; set; } = null!;

    public DateTime? CommercialOperationDate { get; set; }

    public DateTime? CommissioningDate { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public int? HubHeight { get; set; }

    public int ManufacturerId { get; set; }

    public int BladeModelId { get; set; }

    public int TowerTypeId { get; set; }

    public int HubTypeId { get; set; }

    public int? FeederId { get; set; }

    public int ControlPanelId { get; set; }

    public int PowerMeterId { get; set; }

    public string? Comment { get; set; }

    public int? DistrictId { get; set; }

    public int? TalukaId { get; set; }

    public int? VillageId { get; set; }

    public bool BusinessReportEnabled { get; set; }
}
