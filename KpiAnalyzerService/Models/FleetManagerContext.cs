using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KpiAnalyzerService.Models;

public partial class FleetManagerContext : DbContext
{
    public FleetManagerContext()
    {
    }

    public FleetManagerContext(DbContextOptions<FleetManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TLogbookCommonMaster> TLogbookCommonMasters { get; set; }
    public virtual DbSet<DataFilterCondition> DataFilterConditions { get; set; }
    public virtual DbSet<ColumnXymapping> ColumnXymappings { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=216.48.180.83;database=SZ_FLEET_MGR;Integrated Security=False;MultipleActiveResultSets=true;user id=sa;password=#Qwer123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TLogbookCommonMaster>(entity =>
        {
            entity.ToTable("T_Logbook_Common_Master");

            entity.Property(e => e.ColorCode).HasMaxLength(20);
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Icon).HasMaxLength(100);
            entity.Property(e => e.MasterCategory).HasMaxLength(40);
            entity.Property(e => e.MasterName).HasMaxLength(40);
            entity.Property(e => e.ModifiedBy).HasMaxLength(20);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
        });
        modelBuilder.Entity<DataFilterCondition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Data_Fil__3214EC07C0A66FDA");

            entity.ToTable("Data_Filter_Conditions");

            entity.Property(e => e.ColumnName).HasMaxLength(50);
            entity.Property(e => e.Condition).HasMaxLength(50);
            entity.Property(e => e.TableName).HasMaxLength(100);
            entity.Property(e => e.Value1).HasMaxLength(50);
            entity.Property(e => e.Value2).HasMaxLength(50);
        });
        modelBuilder.Entity<ColumnXymapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ColumnXY__3214EC0735F48475");

            entity.ToTable("ColumnXYMapping");

            entity.Property(e => e.Axis)
                .HasMaxLength(50)
                .HasColumnName("axis");
            entity.Property(e => e.ColumnName)
                .HasMaxLength(100)
                .HasColumnName("column_name");
            entity.Property(e => e.TableName)
                .HasMaxLength(100)
                .HasColumnName("table_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
