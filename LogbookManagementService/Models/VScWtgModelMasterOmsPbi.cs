using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class VScWtgModelMasterOmsPbi
{
    public string WtgModelCode { get; set; } = null!;

    public string WtgModelName { get; set; } = null!;

    public string? WtgModelDesc { get; set; }

    public string? Enteredby { get; set; }

    public DateTime? Enteredon { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedon { get; set; }

    public decimal Isactive { get; set; }
}
