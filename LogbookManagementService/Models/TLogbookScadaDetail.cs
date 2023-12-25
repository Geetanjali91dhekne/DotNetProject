using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class TLogbookScadaDetail
{
    public int Id { get; set; }

    public string? SiteName { get; set; }

    public int? FkSiteId { get; set; }

    public string? IssueDesc { get; set; }

    public string? ActionTaken { get; set; }

    public DateTime? LogDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ShiftCycle { get; set; }

    public string? Status { get; set; }

    public virtual SiteId? FkSite { get; set; }
    public int? FkTaskId { get; set; }
}
