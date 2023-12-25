using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class SiteKpiDetail
{
    public int Id { get; set; }

    public string? SiteName { get; set; }

    public string? TurbineNumber { get; set; }

    public string? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
