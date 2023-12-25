using System;
using System.Collections.Generic;

namespace SitesManagementService.Models;

public partial class State
{
    public int Id { get; set; }

    public string? Statename { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<TUserSiteMapping> TUserSiteMappings { get; set; } = new List<TUserSiteMapping>();
}
