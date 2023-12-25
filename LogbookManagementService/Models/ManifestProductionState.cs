using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class ManifestProductionState
{
    public byte ProductionStateCode { get; set; }

    public string ProductionStateName { get; set; } = null!;
}
