﻿using System;
using System.Collections.Generic;

namespace UsersManagementService.Models;

public partial class ScArea
{
    public string StateCode { get; set; } = null!;

    public string AreaCode { get; set; } = null!;

    public string? Area { get; set; }

    public string? Enteredby { get; set; }

    public DateTime? Enteredon { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifiedon { get; set; }

    public decimal Isactive { get; set; }
}
