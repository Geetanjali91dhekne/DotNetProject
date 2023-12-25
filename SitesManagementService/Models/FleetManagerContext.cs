using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SitesManagementService.Models;

public partial class FleetManagerContext : DbContext
{
    public FleetManagerContext()
    {
    }

    public FleetManagerContext(DbContextOptions<FleetManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<SiteId> SiteIds { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<TUserSiteMapping> TUserSiteMappings { get; set; }

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
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Area__3213E83FA6A2DC91");

            entity.ToTable("Area");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Areaname).HasMaxLength(20);
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(20);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Country__3213E83FCF2807DD");

            entity.ToTable("Country");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Countryname).HasMaxLength(20);
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(20);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        

        modelBuilder.Entity<SiteId>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SiteId__3213E83FEAE612A2");

            entity.ToTable("SiteId");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(20);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(20);
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__State__3213E83F2B124CC7");

            entity.ToTable("State");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(20);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Statename).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        

        modelBuilder.Entity<TUserSiteMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_User_S__3213E83F51A069C0");

            entity.ToTable("T_User_Site_Mapping");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FkArea).HasColumnName("FK_Area");
            entity.Property(e => e.FkCountry).HasColumnName("FK_Country");
            entity.Property(e => e.FkSiteId).HasColumnName("FK_SiteId");
            entity.Property(e => e.FkState).HasColumnName("FK_State");
            entity.Property(e => e.ModifiedBy).HasMaxLength(20);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(20);

            entity.HasOne(d => d.FkAreaNavigation).WithMany(p => p.TUserSiteMappings)
                .HasForeignKey(d => d.FkArea)
                .HasConstraintName("FK__T_User_Si__FK_Ar__0F624AF8");

            entity.HasOne(d => d.FkCountryNavigation).WithMany(p => p.TUserSiteMappings)
                .HasForeignKey(d => d.FkCountry)
                .HasConstraintName("FK__T_User_Si__FK_Co__0D7A0286");

            entity.HasOne(d => d.FkSite).WithMany(p => p.TUserSiteMappings)
                .HasForeignKey(d => d.FkSiteId)
                .HasConstraintName("FK__T_User_Si__FK_Si__0C85DE4D");

            entity.HasOne(d => d.FkStateNavigation).WithMany(p => p.TUserSiteMappings)
                .HasForeignKey(d => d.FkState)
                .HasConstraintName("FK__T_User_Si__FK_St__0E6E26BF");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
