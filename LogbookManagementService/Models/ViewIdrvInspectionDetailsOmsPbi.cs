using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class ViewIdrvInspectionDetailsOmsPbi
{
    public int PlanId { get; set; }

    public string? FunctionalLocNo { get; set; }

    public int? CheckListId { get; set; }

    public string? AuditYear { get; set; }

    public string? Idrvtype { get; set; }

    public int? Idrvstatus { get; set; }

    public string? Wtgcategory { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDone { get; set; }

    public string? DeleteRemark { get; set; }

    public string? Idrvremark { get; set; }

    public DateTime? AgingDate { get; set; }

    public string? AgeingRus { get; set; }

    public string? AgeingRud { get; set; }

    public int? ChecklistDetailsId { get; set; }

    public string? InspectionRemark { get; set; }

    public string? InspectionComment { get; set; }

    public string? Nccategory { get; set; }

    public int? Ncstatus { get; set; }

    public string? QuickInspectionPoints { get; set; }

    public int? AttendNc { get; set; }

    public int? AllowDispoNc { get; set; }

    public int? DeviationCorrected { get; set; }

    public string? AppVersion { get; set; }

    public int? AgeingNc { get; set; }

    public long? ImageId { get; set; }

    public string? ImageName { get; set; }

    public DateTime? ImageDate { get; set; }

    public long? NcdetailsId { get; set; }

    public DateTime? CreatedOn { get; set; }

    public bool? Status { get; set; }

    public string? Guid { get; set; }

    public long InspectionId { get; set; }

    public DateTime? InspectionDateTime { get; set; }

    public int? IdrvactionId { get; set; }

    public string? Comment { get; set; }

    public int? ReasonId { get; set; }

    public int? VerificationStaus { get; set; }

    public string? RootCauseOfNc { get; set; }

    public string? CorrectiveActionRootCauseOfNc { get; set; }

    public string? NcopenReason { get; set; }

    public DateTime? ExpectedClouserDate { get; set; }
}
