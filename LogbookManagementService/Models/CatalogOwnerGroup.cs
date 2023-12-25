using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class CatalogOwnerGroup
{
    public int OwnerGroupId { get; set; }

    public string OwnerGroupName { get; set; } = null!;

    public int? CustomerSegmentId { get; set; }

    public string? AlternateKey { get; set; }
}
