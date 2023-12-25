using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class CatalogOwner
{
    public short OwnerId { get; set; }

    public string OwnerName { get; set; } = null!;

    public string? OwnerShortname { get; set; }

    public Guid? AccountManagerId { get; set; }

    public int? OwnerGroupId { get; set; }

    public string? AlternateKey { get; set; }

    public int? IndustrySectorId { get; set; }

    public int? IndustryGroupId { get; set; }

    public int? IndustrySegmentId { get; set; }

    public int? IndustrySubSegmentId { get; set; }
}
