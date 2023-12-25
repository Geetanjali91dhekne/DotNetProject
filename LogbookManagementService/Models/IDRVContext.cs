using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LogbookManagementService.Models;

public partial class IDRVContext : DbContext
{
    public IDRVContext()
    {
    }

    public IDRVContext(DbContextOptions<IDRVContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ViewIdrvInspectionDetailsOmsPbi> ViewIdrvInspectionDetailsOmsPbis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyIDRVConnectionString"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ViewIdrvInspectionDetailsOmsPbi>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_IDRV_INSPECTION_DETAILS_OMS_PBI");

            entity.Property(e => e.AgeingNc).HasColumnName("AgeingNC");
            entity.Property(e => e.AgeingRud)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("AgeingRUD");
            entity.Property(e => e.AgeingRus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("AgeingRUS");
            entity.Property(e => e.AgingDate).HasColumnType("datetime");
            entity.Property(e => e.AllowDispoNc).HasColumnName("AllowDispoNC");
            entity.Property(e => e.AppVersion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AttendNc).HasColumnName("attendNC");
            entity.Property(e => e.AuditYear).HasMaxLength(20);
            entity.Property(e => e.CheckListId).HasColumnName("CheckListID");
            entity.Property(e => e.Comment).HasMaxLength(350);
            entity.Property(e => e.CorrectiveActionRootCauseOfNc)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("CorrectiveActionRootCauseOfNC");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ExpectedClouserDate).HasColumnType("datetime");
            entity.Property(e => e.FunctionalLocNo).HasMaxLength(50);
            entity.Property(e => e.IdrvactionId).HasColumnName("IDRVActionID");
            entity.Property(e => e.Idrvremark)
                .HasMaxLength(500)
                .HasColumnName("IDRVRemark");
            entity.Property(e => e.Idrvstatus).HasColumnName("IDRVStatus");
            entity.Property(e => e.Idrvtype)
                .HasMaxLength(50)
                .HasColumnName("IDRVType");
            entity.Property(e => e.ImageDate).HasColumnType("date");
            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.ImageName).HasMaxLength(100);
            entity.Property(e => e.InspectionComment).HasMaxLength(250);
            entity.Property(e => e.InspectionDateTime).HasColumnType("datetime");
            entity.Property(e => e.InspectionId).HasColumnName("InspectionID");
            entity.Property(e => e.InspectionRemark).HasMaxLength(50);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Nccategory)
                .HasMaxLength(10)
                .HasColumnName("NCCategory");
            entity.Property(e => e.NcdetailsId).HasColumnName("NCDetailsID");
            entity.Property(e => e.NcopenReason)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("NCOpenReason");
            entity.Property(e => e.Ncstatus).HasColumnName("NCStatus");
            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.QuickInspectionPoints).HasMaxLength(200);
            entity.Property(e => e.ReasonId).HasColumnName("ReasonID");
            entity.Property(e => e.RootCauseOfNc)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("RootCauseOfNC");
            entity.Property(e => e.Wtgcategory)
                .HasMaxLength(10)
                .HasColumnName("WTGCategory");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
