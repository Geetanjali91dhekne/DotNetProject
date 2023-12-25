using System;
using System.Collections.Generic;

namespace AuthenticationService.Models;

public partial class RolePermissionMapping
{
    public int Id { get; set; }

    public int? FkRoleId { get; set; }

    public int? FkPermissionId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public string? IsAccess { get; set; }

    public virtual Permission? FkPermission { get; set; }

    public virtual RoleMaster? FkRole { get; set; }
}
