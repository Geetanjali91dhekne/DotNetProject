using System;
using System.Collections.Generic;

namespace UsersManagementService.Models;

public partial class PlanningUploadDetail
{
    public int Id { get; set; }

    public string? State { get; set; }

    public string? Area { get; set; }

    public string? Site { get; set; }

    public string? StateCode { get; set; }

    public string? AreaCode { get; set; }

    public string? SiteCode { get; set; }

    public string? FunctionLocation { get; set; }

    public string? Category { get; set; }

    public string? CategoryDescription { get; set; }

    public DateTime? PlanDate { get; set; }

    public int? RecordId { get; set; }

    public string? Status { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
