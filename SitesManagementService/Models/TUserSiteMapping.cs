using System;
using System.Collections.Generic;

namespace SitesManagementService.Models;

public partial class TUserSiteMapping
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public int? FkSiteId { get; set; }

    public int? FkCountry { get; set; }

    public int? FkState { get; set; }

    public int? FkArea { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public virtual Area? FkAreaNavigation { get; set; }

    public virtual Country? FkCountryNavigation { get; set; }

    public virtual SiteId? FkSite { get; set; }

    public virtual State? FkStateNavigation { get; set; }
}
