using System;
using System.Collections.Generic;

namespace LogbookManagementService.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Email { get; set; }

    public string? Contact { get; set; }

    public int? Country { get; set; }

    public string? Fcmtoken { get; set; }

    public string? Apitoken { get; set; }

    public string? DeviceId { get; set; }

    public bool IsActive { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public bool? IsExternal { get; set; }
}
