using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class VScSiteStateMasterOmsPbi
{
    public string MainSiteCode { get; set; } = null!;

    public string SiteCode { get; set; } = null!;

    public string? Site { get; set; }

    public string? UseForSelection { get; set; }

    public string? DelKey { get; set; }

    public string? SiteShortName { get; set; }

    public string? SbuOnm { get; set; }

    public string? Enteredby { get; set; }

    public DateTime? Enteredon { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedon { get; set; }

    public decimal Isactive { get; set; }

    public DateTime? Dgrstartdate { get; set; }
}
