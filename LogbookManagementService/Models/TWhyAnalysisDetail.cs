using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class TWhyAnalysisDetail
{
    public int Id { get; set; }

    public int? FkAnalysisId { get; set; }

    public int? FkTypeId { get; set; }

    public string? Hrs { get; set; }

    public string? Ai { get; set; }

    public string? Why1 { get; set; }

    public string? Why2 { get; set; }

    public string? Why3 { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Why4 { get; set; }

    public string? Why5 { get; set; }

    public string? Why6 { get; set; }

   

}
