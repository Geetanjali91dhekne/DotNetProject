
using System;

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UsersManagementService.Models;

public partial class CrmsContext : DbContext
{
    public CrmsContext()
    {
    }

    public CrmsContext(DbContextOptions<CrmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ScCountry> ScCountries { get; set; }
    public virtual DbSet<ScState> ScStates { get; set; }
    public virtual DbSet<ScArea> ScAreas { get; set; }
    public virtual DbSet<ScMainSite> ScMainSites { get; set; }

    public virtual DbSet<CmrEmployeeMaster> CmrEmployeeMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyCRMSConnectionString"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ScCountry>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SC_COUNTRY");

            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COUNTRY");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("COUNTRY_CODE");
            entity.Property(e => e.Enteredby)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ENTEREDBY");
            entity.Property(e => e.Enteredon)
                .HasPrecision(0)
                .HasColumnName("ENTEREDON");
            entity.Property(e => e.Isactive)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Modifiedby)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MODIFIEDBY");
            entity.Property(e => e.Modifiedon)
                .HasPrecision(0)
                .HasColumnName("MODIFIEDON");
        });
        modelBuilder.Entity<ScState>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SC_STATE");

            entity.Property(e => e.CountryCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("COUNTRY_CODE");
            entity.Property(e => e.Enteredby)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ENTEREDBY");
            entity.Property(e => e.Enteredon)
                .HasPrecision(0)
                .HasColumnName("ENTEREDON");
            entity.Property(e => e.Isactive)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Modifiedby)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MODIFIEDBY");
            entity.Property(e => e.Modifiedon)
                .HasPrecision(0)
                .HasColumnName("MODIFIEDON");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STATE");
            entity.Property(e => e.StateCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("STATE_CODE");
        });
        modelBuilder.Entity<ScArea>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SC_AREA");

            entity.Property(e => e.Area)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AREA");
            entity.Property(e => e.AreaCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("AREA_CODE");
            entity.Property(e => e.Enteredby)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ENTEREDBY");
            entity.Property(e => e.Enteredon)
                .HasPrecision(0)
                .HasColumnName("ENTEREDON");
            entity.Property(e => e.Isactive)
                .HasDefaultValueSql("((1))")
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Modifiedby)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MODIFIEDBY");
            entity.Property(e => e.Modifiedon)
                .HasPrecision(0)
                .HasColumnName("MODIFIEDON");
            entity.Property(e => e.StateCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("STATE_CODE");
        });
        modelBuilder.Entity<ScMainSite>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SC_MAIN_SITE");

            entity.Property(e => e.AreaCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("AREA_CODE");
            entity.Property(e => e.Enteredby)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ENTEREDBY");
            entity.Property(e => e.Enteredon)
                .HasPrecision(0)
                .HasColumnName("ENTEREDON");
            entity.Property(e => e.Isactive)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.MainSite)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MAIN_SITE");
            entity.Property(e => e.MainSiteCode)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("MAIN_SITE_CODE");
            entity.Property(e => e.Modifiedby)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MODIFIEDBY");
            entity.Property(e => e.Modifiedon)
                .HasPrecision(0)
                .HasColumnName("MODIFIEDON");
        });
        modelBuilder.Entity<CmrEmployeeMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CMR_EMPLOYEE_MASTER");

            entity.Property(e => e.Addedby)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ADDEDBY");
            entity.Property(e => e.Addeddt)
                .HasPrecision(0)
                .HasColumnName("ADDEDDT");
            entity.Property(e => e.EmpCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMP_CODE");
            entity.Property(e => e.EmpDesignation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMP_DESIGNATION");
            entity.Property(e => e.EmpDomainId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMP_DOMAIN_ID");
            entity.Property(e => e.EmpEmailId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMP_EMAIL_ID");
            entity.Property(e => e.EmpMobileNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMP_MOBILE_NO");
            entity.Property(e => e.EmpName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMP_NAME");
            entity.Property(e => e.Modifiedby)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MODIFIEDBY");
            entity.Property(e => e.Modifieddt)
                .HasPrecision(0)
                .HasColumnName("MODIFIEDDT");
            entity.Property(e => e.RoleId)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.SiteId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SITE_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
