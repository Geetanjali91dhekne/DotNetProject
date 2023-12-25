using System;
using System.Collections.Generic;

namespace UsersManagementService.Models;

public partial class DocumentMaster
{
    public int Id { get; set; }

    public string? DocumentId { get; set; }

    public string? DocumentName { get; set; }

    public string? DocumentDescription { get; set; }

    public string? DocumentContent { get; set; }

    public string? AttachmentLink { get; set; }

    public string? TemplatePath { get; set; }

    public string? DocumentType { get; set; }

    public string? Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? FileName { get; set; }
}
