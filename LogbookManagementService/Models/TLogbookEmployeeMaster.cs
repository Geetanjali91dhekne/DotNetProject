using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class TLogbookEmployeeMaster
{
    public int Id { get; set; }

    public string? EmpCode { get; set; }

    public string? EmpName { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
