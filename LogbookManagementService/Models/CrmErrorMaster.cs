using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class CrmErrorMaster
{
    public string CrmErrorCode { get; set; } = null!;

    public decimal? InstCapacity { get; set; }

    public string? ControllerType { get; set; }

    public string? ErrorCode { get; set; }

    public string? Description { get; set; }

    public string? Severity { get; set; }

    public string? ErrRelatedTo { get; set; }

    public string? Classification { get; set; }

    public string? Enteredby { get; set; }

    public DateTime? Enteredon { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedon { get; set; }

    public decimal Isactive { get; set; }
}
