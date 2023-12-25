﻿using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class ScMainSite
{
    public string AreaCode { get; set; } = null!;

    public string MainSiteCode { get; set; } = null!;

    public string? MainSite { get; set; }

    public string? Enteredby { get; set; }

    public DateTime? Enteredon { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedon { get; set; }

    public decimal Isactive { get; set; }
}
