using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class TLogbookCommonMaster
{
    public int Id { get; set; }

    public string? MasterCategory { get; set; }

    public string? MasterName { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public string? ColorCode { get; set; }

    public string? Icon { get; set; }
}
