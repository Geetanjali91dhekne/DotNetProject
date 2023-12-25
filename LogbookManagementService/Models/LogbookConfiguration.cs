﻿using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class LogbookConfiguration
{
    public int Id { get; set; }

    public string? Category { get; set; }

    public string? Code { get; set; }

    public string? Value { get; set; }
}
