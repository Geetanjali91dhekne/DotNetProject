using System;
using System.Collections.Generic;

namespace UsersManagementService.Models;

public partial class DocUploadHistory
{
    public int Id { get; set; }

    public string? Filename { get; set; }

    public int? MandateId { get; set; }

    public string? Entityname { get; set; }

    public int? RecordId { get; set; }

    public string? Status { get; set; }

    public string? Filepath { get; set; }

    public string? Documenttype { get; set; }

    public string? Remarks { get; set; }

    public string? VersionNumber { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
