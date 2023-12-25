using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class ErrorMaster
{
    public double? EventCode { get; set; }

    public string? Name { get; set; }

    public double? BrakeProgram { get; set; }

    public double? ProposedLevelResetAlarm { get; set; }

    public string? ProposedRemoteReset { get; set; }

    public string? WtgType { get; set; }

    public string? TurbineControlSWVersion { get; set; }
}
