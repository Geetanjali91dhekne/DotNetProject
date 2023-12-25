using System;
using System.Collections.Generic;

namespace WorkflowManagementService.Models;

public partial class WorkFlowConfiguration
{
    public int Id { get; set; }

    public string? WorkItem { get; set; }

    public string? OwnerByRole { get; set; }

    public string? Step { get; set; }

    public string? OnApprovalNextStep { get; set; }

    public string? OnRejectNextStep { get; set; }

    public string? OnApprovalStatus { get; set; }

    public string? OnRejectStatus { get; set; }

    public string? OnApprovEmailTemplate { get; set; }

    public string? OnRejectEmailTemplate { get; set; }

    public string? OnApprovCcemailList { get; set; }

    public string? OnRejectCcemailList { get; set; }

    public string? WorkflowOn { get; set; }

    public string? Conditions { get; set; }

    public string? Stdmsg { get; set; }

    public string? RejectedCondition { get; set; }

    public string? SentBackCondition { get; set; }

    public string? SentBackStatus { get; set; }

    public string? SentBackSteps { get; set; }

    public string? SentBackTemplate { get; set; }

    public string? TaskDeleteCondition { get; set; }

    public string? TaskDeleteConditionType { get; set; }

    public string? ReminderDays { get; set; }

    public string? ReminderTemplate { get; set; }

    public string? ReminderTime { get; set; }
}
