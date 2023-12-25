using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class MstPmSchedule
{
    public int ScheduledId { get; set; }

    public string? OrderNo { get; set; }

    public string? OrderId { get; set; }

    public string? ScheduleType { get; set; }

    public int? CheklistId { get; set; }

    public string? FunctionalLocation { get; set; }

    public int? ChecklistTypeId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public int? IsActive { get; set; }

    public int? Pmsapstatus { get; set; }

    public DateTime? Pmsapsentdate { get; set; }
}
