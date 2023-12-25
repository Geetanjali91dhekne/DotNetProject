using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class KpiMttrMtbf
{
    public int Id { get; set; }

    public string? PlantRole { get; set; }

    public string? Event { get; set; }

    public string? EventDescription { get; set; }

    public string? SystemComponent { get; set; }

    public string? Duration { get; set; }

    public string? Instance { get; set; }

    public string? LostProdKwh { get; set; }

    public decimal? MttrHours { get; set; }

    public decimal? MtbfHours { get; set; }

    public decimal? AvailDistPer { get; set; }

    public decimal? AvailImpactPer { get; set; }

    public string? IsType { get; set; }

    public bool? IsModel { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
