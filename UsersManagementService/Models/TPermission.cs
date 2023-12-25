﻿using System;
using System.Collections.Generic;

namespace UsersManagementService.Models;

public partial class TPermission
{
    public int Id { get; set; }

    public string? EntityCategory { get; set; }

    public string? EntityName { get; set; }

    public string? Actions { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<TRolePermissionMapping> TRolePermissionMappings { get; set; } = new List<TRolePermissionMapping>();
}
