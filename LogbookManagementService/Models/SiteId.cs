using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class SiteId
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<TLogbookEmployeeDetail> TLogbookEmployeeDetails { get; set; } = new List<TLogbookEmployeeDetail>();

    public virtual ICollection<TLogbookGridBreakdownDetail> TLogbookGridBreakdownDetails { get; set; } = new List<TLogbookGridBreakdownDetail>();

    public virtual ICollection<TLogbookHoto> TLogbookHotos { get; set; } = new List<TLogbookHoto>();

    public virtual ICollection<TLogbookRemark> TLogbookRemarks { get; set; } = new List<TLogbookRemark>();

    public virtual ICollection<TLogbookScadaDetail> TLogbookScadaDetails { get; set; } = new List<TLogbookScadaDetail>();

    public virtual ICollection<TLogbookScheduleMaintenanceActivity> TLogbookScheduleMaintenanceActivities { get; set; } = new List<TLogbookScheduleMaintenanceActivity>();

    public virtual ICollection<TLogbookWtgBreakdownDetail> TLogbookWtgBreakdownDetails { get; set; } = new List<TLogbookWtgBreakdownDetail>();

}
