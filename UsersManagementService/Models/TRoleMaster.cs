using System;
using System.Collections.Generic;

namespace UsersManagementService.Models;

public partial class TRoleMaster
{
    public int Id { get; set; }

    public string? RoleName { get; set; }

    public string? RoleDescription { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public string? GroupCode { get; set; }

    public virtual ICollection<TGroupRoleMapping> TGroupRoleMappings { get; set; } = new List<TGroupRoleMapping>();

    public virtual ICollection<TRolePermissionMapping> TRolePermissionMappings { get; set; } = new List<TRolePermissionMapping>();

    public virtual ICollection<TUserRoleMapping> TUserRoleMappings { get; set; } = new List<TUserRoleMapping>();
}
