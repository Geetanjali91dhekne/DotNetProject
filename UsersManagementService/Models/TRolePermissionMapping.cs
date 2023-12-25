using System;
using System.Collections.Generic;

namespace UsersManagementService.Models;

public partial class TRolePermissionMapping
{
    public int Id { get; set; }

    public int? FkRoleId { get; set; }

    public int? FkPermissionId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public char? IsAccess { get; set; }

    public virtual TPermission? FkPermission { get; set; }

    public virtual TRoleMaster? FkRole { get; set; }
}
