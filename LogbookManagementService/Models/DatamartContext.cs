using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LogbookManagementService.Models;

public partial class DatamartMobileContext : DbContext
{
    public DatamartMobileContext()
    {
    }

    public DatamartMobileContext(DbContextOptions<DatamartMobileContext> options)
        : base(options)
    {
    }
    public virtual DbSet<AccessUserPlantUnitTotal> AccessUserPlantUnitTotals { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<TurbineGridPerformanceMonth> TurbineGridPerformanceMonths { get; set; }
    public virtual DbSet<TurbinePerformanceMonth> TurbinePerformanceMonths { get; set; }
    public virtual DbSet<TurbineOnline> TurbineOnlines { get; set; }
    public virtual DbSet<CatalogTurbine> CatalogTurbines { get; set; }
    public virtual DbSet<CatalogOwner> CatalogOwners { get; set; }
    
    public virtual DbSet<CatalogOwnerGroup> CatalogOwnerGroups { get; set; }
    public virtual DbSet<ManifestProductionState> ManifestProductionStates { get; set; }

    public virtual DbSet<ManifestTurbineModel> ManifestTurbineModels { get; set; }
    public virtual DbSet<VwWindFarm> VwWindFarms { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyDatamartConnectionString"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VwWindFarm>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_WindFarm");

            entity.Property(e => e.AccumulatedProduction).HasColumnType("numeric(19, 1)");
            entity.Property(e => e.ControllerTimestamp)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Latitude)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Longitude)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.PlantName).HasMaxLength(50);
            entity.Property(e => e.PlantUnitName).HasMaxLength(50);
            entity.Property(e => e.Production).HasColumnType("numeric(9, 4)");
            entity.Property(e => e.ServiceOrganizationName).HasMaxLength(50);
            entity.Property(e => e.WindSpeed).HasColumnType("numeric(9, 4)");
        });
        modelBuilder.Entity<ManifestTurbineModel>(entity =>
        {
            entity.HasKey(e => e.TurbineModelId);

            entity.ToTable("Manifest_TurbineModel");

            entity.Property(e => e.TurbineModelId).ValueGeneratedNever();
            entity.Property(e => e.RotorDiameter).HasColumnType("numeric(5, 2)");
            entity.Property(e => e.TurbineModelName).HasMaxLength(50);
        });
        modelBuilder.Entity<ManifestProductionState>(entity =>
        {
            entity.HasKey(e => e.ProductionStateCode);

            entity.ToTable("Manifest_ProductionState");

            entity.Property(e => e.ProductionStateName).HasMaxLength(100);
        });
        modelBuilder.Entity<CatalogOwnerGroup>(entity =>
        {
            entity.HasKey(e => e.OwnerGroupId);

            entity.ToTable("Catalog_OwnerGroup");

            entity.Property(e => e.OwnerGroupId).ValueGeneratedNever();
            entity.Property(e => e.AlternateKey)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.OwnerGroupName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });
        modelBuilder.Entity<CatalogOwner>(entity =>
        {
            entity.HasKey(e => e.OwnerId);

            entity.ToTable("Catalog_Owner");

            entity.Property(e => e.OwnerId).ValueGeneratedNever();
            entity.Property(e => e.AlternateKey)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.OwnerName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OwnerShortname).HasMaxLength(20);
        });
        modelBuilder.Entity<CatalogTurbine>(entity =>
        {
            entity.HasKey(e => e.PlantUnitId);

            entity.ToTable("Catalog_Turbine");

            entity.Property(e => e.PlantUnitId).ValueGeneratedNever();
            entity.Property(e => e.Comment).HasMaxLength(25);
            entity.Property(e => e.CommercialOperationDate).HasColumnType("date");
            entity.Property(e => e.CommissioningDate).HasColumnType("date");
            entity.Property(e => e.FunctionalLocation)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Latitude)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Longitude)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.PlantUnitName).HasMaxLength(50);
        });
        modelBuilder.Entity<AccessUserPlantUnitTotal>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.PlantUnitId });

            entity.ToTable("Access_UserPlantUnitTotal");
        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Apitoken)
                .HasMaxLength(200)
                .HasColumnName("APIToken");
            entity.Property(e => e.Contact).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeviceId).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Fcmtoken)
                .HasMaxLength(200)
                .HasColumnName("FCMToken");
            entity.Property(e => e.Firstname).HasMaxLength(50);
            entity.Property(e => e.Lastname).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });
        modelBuilder.Entity<TurbineGridPerformanceMonth>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Turbine_GridPerformance_Month");

            entity.Property(e => e.ControllerTimestamp).HasColumnType("smalldatetime");
            entity.Property(e => e.CreateTimestamp).HasColumnType("smalldatetime");
            entity.Property(e => e.ServerTimestamp).HasColumnType("smalldatetime");
        });
        modelBuilder.Entity<TurbineOnline>(entity =>
        {
            entity.HasKey(e => e.PlantUnitId);

            entity.ToTable("Turbine_Online");

            entity.Property(e => e.PlantUnitId).ValueGeneratedNever();
            entity.Property(e => e.AccumulatedProduction).HasColumnType("numeric(19, 1)");
            entity.Property(e => e.ControllerTimestamp)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Production).HasColumnType("numeric(9, 4)");
            entity.Property(e => e.ServerTimestamp).HasPrecision(3);
            entity.Property(e => e.WindSpeed).HasColumnType("numeric(9, 4)");
        });
        modelBuilder.Entity<TurbinePerformanceMonth>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Turbine_Performance_Month");

            entity.Property(e => e.ControllerTimestamp).HasColumnType("datetime");
            entity.Property(e => e.CreateTimestamp).HasColumnType("datetime");
            entity.Property(e => e.EnergyInkWh).HasColumnType("numeric(12, 4)");
            entity.Property(e => e.EnergyOutkWh).HasColumnType("numeric(12, 4)");
            entity.Property(e => e.ProductionkWh).HasColumnType("numeric(12, 4)");
            entity.Property(e => e.ServerTimestamp).HasColumnType("datetime");
            entity.Property(e => e.WindSpeed).HasColumnType("numeric(9, 1)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
