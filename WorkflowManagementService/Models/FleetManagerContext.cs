using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WorkflowManagementService.Models;

public partial class FleetManagerContext : DbContext
{
    public FleetManagerContext()
    {
    }

    public FleetManagerContext(DbContextOptions<FleetManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<WorkFlowConfiguration> WorkFlowConfigurations { get; set; }

    public virtual DbSet<WorkflowRuntime> WorkflowRuntimes { get; set; }

    public virtual DbSet<WorkflowRuntimeHistory> WorkflowRuntimeHistories { get; set; }

    public virtual DbSet<WorkflowRuntimeSubTask> WorkflowRuntimeSubTasks { get; set; }

    public virtual DbSet<Workitemdefination> Workitemdefinations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyConnectionString"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WorkFlowConfiguration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WorkFlow__3214EC07418E9F13");

            entity.ToTable("WorkFlowConfiguration");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Conditions)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("conditions");
            entity.Property(e => e.OnApprovCcemailList)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("OnApprovCCEmailList");
            entity.Property(e => e.OnApprovEmailTemplate)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.OnApprovalNextStep)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.OnApprovalStatus)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.OnRejectCcemailList)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("OnRejectCCEmailList");
            entity.Property(e => e.OnRejectEmailTemplate)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.OnRejectNextStep)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.OnRejectStatus)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.OwnerByRole)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.RejectedCondition)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ReminderDays)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ReminderTemplate)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ReminderTime)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SentBackCondition)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SentBackStatus)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SentBackSteps)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SentBackTemplate)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Stdmsg)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("stdmsg");
            entity.Property(e => e.Step)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TaskDeleteCondition)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TaskDeleteConditionType)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.WorkItem)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.WorkflowOn)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<WorkflowRuntime>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Workflow__3214EC279FD900B5");

            entity.ToTable("WorkflowRuntime");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Stdmsg)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("stdmsg");
            entity.Property(e => e.StepNo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SubmittedBy)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Submittedon).HasColumnType("datetime");
            entity.Property(e => e.TableName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.WorkItem)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<WorkflowRuntimeHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Workflow__3214EC07B821504D");

            entity.ToTable("WorkflowRuntimeHistory");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Createdon).HasColumnType("datetime");
            entity.Property(e => e.Remarks)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.RoleName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.StepNo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TableName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TaskCreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.WorkItem)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<WorkflowRuntimeSubTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Workflow__3214EC07620BB039");

            entity.ToTable("WorkflowRuntime_SubTasks");

            entity.Property(e => e.RoleName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.RuntimeId).HasColumnName("runtimeId");
            entity.Property(e => e.Status)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Stepno)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TableName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.WorkItem)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Workitemdefination>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Workitem__3214EC07117E3B1E");

            entity.ToTable("Workitemdefination");

            entity.Property(e => e.Action)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TableName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("URL");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
