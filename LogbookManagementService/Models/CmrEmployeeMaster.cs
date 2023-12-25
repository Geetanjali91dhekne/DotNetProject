using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class CmrEmployeeMaster
{
    public string? EmpCode { get; set; }

    public string? EmpName { get; set; }

    public string EmpDomainId { get; set; } = null!;

    public string? EmpEmailId { get; set; }

    public string? EmpDesignation { get; set; }

    public string? EmpMobileNo { get; set; }

    public string RoleId { get; set; } = null!;

    public string? SiteId { get; set; }

    public string? Addedby { get; set; }

    public DateTime? Addeddt { get; set; }

    public string? Modifiedby { get; set; }

    public DateTime? Modifieddt { get; set; }
}
