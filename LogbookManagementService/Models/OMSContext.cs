using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LogbookManagementService.Models;

public partial class OMSContext : DbContext
{
    public OMSContext()
    {
    }

    public OMSContext(DbContextOptions<OMSContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MstPmSchedule> MstPmSchedules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=216.48.180.83;database=omsmobileapp;Integrated Security=False;MultipleActiveResultSets=true;user id=sa;password=#Qwer123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MstPmSchedule>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MST_PM_SCHEDULE");

            entity.Property(e => e.ChecklistTypeId).HasColumnName("checklist_type_id");
            entity.Property(e => e.CheklistId).HasColumnName("cheklist_id");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.FunctionalLocation)
                .HasMaxLength(255)
                .HasColumnName("functional_location");
            entity.Property(e => e.ModifiedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Modified_On");
            entity.Property(e => e.OrderId)
                .HasMaxLength(255)
                .HasColumnName("order_id");
            entity.Property(e => e.OrderNo)
                .HasMaxLength(255)
                .HasColumnName("order_no");
            entity.Property(e => e.Pmsapsentdate)
                .HasColumnType("datetime")
                .HasColumnName("PMSAPSENTDATE");
            entity.Property(e => e.Pmsapstatus).HasColumnName("PMSAPSTATUS");
            entity.Property(e => e.ScheduleType)
                .HasMaxLength(255)
                .HasColumnName("schedule_type");
            entity.Property(e => e.ScheduledId)
                .ValueGeneratedOnAdd()
                .HasColumnName("scheduled_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
