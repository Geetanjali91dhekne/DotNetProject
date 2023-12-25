using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class ManifestTurbineModel
{
    public int TurbineModelId { get; set; }

    public string TurbineModelName { get; set; } = null!;

    public int TurbineFamilyId { get; set; }

    public int? NominalPower { get; set; }

    public decimal RotorDiameter { get; set; }
}
