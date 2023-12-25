using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class TUserSiteMapping
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? SiteCode { get; set; }

    public string? CountryCode { get; set; }

    public string? StateCode { get; set; }

    public string? AreaCode { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public string? EmployeeCode { get; set; }
}
