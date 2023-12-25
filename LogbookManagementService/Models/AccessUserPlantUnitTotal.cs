using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class AccessUserPlantUnitTotal
{
    public Guid UserId { get; set; }

    public int PlantUnitId { get; set; }
}
