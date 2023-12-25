using System;
using System.Collections.Generic;

namespace UsersManagementService.Models;

public partial class RoleMenuConfiguration
{
    public int Id { get; set; }

    public string? RoleName { get; set; }

    public string? MenuName { get; set; }

    public string? ParentName { get; set; }

    public int? Isparent { get; set; }

    public string? Action { get; set; }

    public string? MenuRoute { get; set; }

    public string? ActionRoute { get; set; }

    public string? ScreenMessage { get; set; }

    public string? ComponantList { get; set; }

    public string? ValidationMessage { get; set; }

    public string? ActionMessage { get; set; }

    public string? Icons { get; set; }

    public string? TabList { get; set; }

    public string? ActionableItem { get; set; }

    public string? ApiType { get; set; }

    public bool? IsDeleted { get; set; }

    public int? Sequence { get; set; }
}
