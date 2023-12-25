using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Models;

public partial class FleetManagerContext : DbContext
{
    public FleetManagerContext()
    {
    }

    public FleetManagerContext(DbContextOptions<FleetManagerContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Permission> TPermissions { get; set; }

    public virtual DbSet<RoleMaster> TRoleMasters { get; set; }

    public virtual DbSet<RolePermissionMapping> TRolePermissionMappings { get; set; }

    public virtual DbSet<TUserRoleMapping> TUserRoleMappings { get; set; }
    public virtual DbSet<TGroupRoleMapping> TGroupRoleMappings { get; set; }

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
        modelBuilder.Entity<TGroupRoleMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_T_Group_Role_Mapping1");

            entity.ToTable("T_Group_Role_Mapping");

            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FkRoleId).HasColumnName("FK_Role_Id");
            entity.Property(e => e.GroupCode).HasMaxLength(200);
            entity.Property(e => e.ModifiedBy).HasMaxLength(20);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
        });


        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_Permis__3213E83F6CD1B3D4");

            entity.ToTable("T_Permissions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Actions).HasMaxLength(20);
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EntityCategory).HasMaxLength(20);
            entity.Property(e => e.EntityName).HasMaxLength(20);
            entity.Property(e => e.ModifiedBy).HasMaxLength(20);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<RoleMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_RoleMa__3214EC073F1ACF01");

            entity.ToTable("T_RoleMaster");

            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.GroupCode).HasMaxLength(20);
            entity.Property(e => e.ModifiedBy).HasMaxLength(20);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RoleDescription).HasMaxLength(100);
            entity.Property(e => e.RoleName).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(20);
        });

        modelBuilder.Entity<RolePermissionMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_Role_P__3213E83FAEFA4CAB");

            entity.ToTable("T_Role_Permission_Mapping");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FkPermissionId).HasColumnName("Fk_PermissionId");
            entity.Property(e => e.FkRoleId).HasColumnName("Fk_RoleId");
            entity.Property(e => e.IsAccess)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ModifiedBy).HasMaxLength(20);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.FkPermission).WithMany(p => p.TRolePermissionMappings)
                .HasForeignKey(d => d.FkPermissionId)
                .HasConstraintName("FK__T_Role_Pe__Fk_Pe__02084FDA");

            entity.HasOne(d => d.FkRole).WithMany(p => p.TRolePermissionMappings)
                .HasForeignKey(d => d.FkRoleId)
                .HasConstraintName("FK__T_Role_Pe__Fk_Ro__01142BA1");
        });

        modelBuilder.Entity<TUserRoleMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_User_R__3213E83FB8CAAD47");

            entity.ToTable("T_User_Role_Mapping");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FkRoleId).HasColumnName("FK_Role_Id");
            entity.Property(e => e.GroupCode).HasMaxLength(20);
            entity.Property(e => e.ModifiedBy).HasMaxLength(20);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(20);

            entity.HasOne(d => d.FkRole).WithMany(p => p.TUserRoleMappings)
                .HasForeignKey(d => d.FkRoleId)
                .HasConstraintName("FK__T_User_Ro__FK_Ro__7C4F7684");
        });

       
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
