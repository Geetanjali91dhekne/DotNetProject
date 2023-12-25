using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace UsersManagementService.Models;

public partial class FleetManagerContext : DbContext
{
    public FleetManagerContext()
    {
    }

    public FleetManagerContext(DbContextOptions<FleetManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TPermission> TPermissions { get; set; }

    public virtual DbSet<TRoleMaster> TRoleMasters { get; set; }

    public virtual DbSet<TRolePermissionMapping> TRolePermissionMappings { get; set; }

    public virtual DbSet<TUserRoleMapping> TUserRoleMappings { get; set; }
    public virtual DbSet<TUserSiteMapping> TUserSiteMappings { get; set; }
    public virtual DbSet<RoleMenuConfiguration> RoleMenuConfigurations { get; set; }

    public virtual DbSet<TGroupRoleMapping> TGroupRoleMappings { get; set; }

	public virtual DbSet<GroupMaster> GroupMasters { get; set; }

    public virtual DbSet<TaskManagementStatus> TaskManagementStatuses { get; set; }

    public virtual DbSet<TLogbookCommonMaster> TLogbookCommonMasters { get; set; }
    public virtual DbSet<TaskMain> TaskMains { get; set; }
    public virtual DbSet<AuditTaskMain> AuditTaskMains { get; set; }
    public virtual DbSet<TaskDocument> TaskDocuments { get; set; }
    public virtual DbSet<PlanningUploadDetail> PlanningUploadDetails { get; set; }
    public virtual DbSet<TempPlanningUploadDetail> TempPlanningUploadDetails { get; set; }
    public virtual DbSet<BulkUploadConfiguration> BulkUploadConfigurations { get; set; }

    public virtual DbSet<DocUploadHistory> DocUploadHistories { get; set; }
    public virtual DbSet<DocumentMaster> DocumentMasters { get; set; }
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
        modelBuilder.Entity<TUserSiteMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_User_S__3213E83F6E861C0E");

            entity.ToTable("T_User_Site_Mapping");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AreaCode).HasMaxLength(20);
            entity.Property(e => e.CountryCode).HasMaxLength(20);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.FkRoleId).HasColumnName("Fk_Role_Id");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SiteCode).HasMaxLength(20);
            entity.Property(e => e.StateCode).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(80);
        });

        modelBuilder.Entity<TPermission>(entity =>
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

        modelBuilder.Entity<TRoleMaster>(entity =>
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

        modelBuilder.Entity<TRolePermissionMapping>(entity =>
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

        modelBuilder.Entity<TGroupRoleMapping>(entity =>
        {
            entity.ToTable("T_Group_Role_Mapping");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FkRoleId).HasColumnName("FK_Role_Id");
            entity.Property(e => e.GroupCode).HasMaxLength(20);
            entity.Property(e => e.ModifiedBy).HasMaxLength(20);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.FkRole).WithMany(p => p.TGroupRoleMappings)
                .HasForeignKey(d => d.FkRoleId)
                .HasConstraintName("FK_T_Group_Role_Mapping_T_RoleMaster");
        });

        modelBuilder.Entity<RoleMenuConfiguration>(entity =>
        {
            entity.ToTable("Role_Menu_Configuration");

            entity.Property(e => e.Action)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("action");
            entity.Property(e => e.ActionMessage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("action_message");
            entity.Property(e => e.ActionRoute)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("action_route");
            entity.Property(e => e.ActionableItem)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("actionable_item");
            entity.Property(e => e.ApiType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("api_type");
            entity.Property(e => e.ComponantList)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("componant_list");
            entity.Property(e => e.Icons)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("icons");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Isparent).HasColumnName("isparent");
            entity.Property(e => e.MenuName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("menu_name");
            entity.Property(e => e.MenuRoute)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("menu_route");
            entity.Property(e => e.ParentName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("parent_name");
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("role_name");
            entity.Property(e => e.ScreenMessage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("screen_message");
            entity.Property(e => e.Sequence).HasColumnName("sequence");
            entity.Property(e => e.TabList)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tab_list");
            entity.Property(e => e.ValidationMessage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("validation_message");
        });
		modelBuilder.Entity<GroupMaster>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PK__GroupMas__3214EC077E16892F");

			entity.ToTable("GroupMaster");

			entity.Property(e => e.GroupCode)
				.HasMaxLength(600)
				.IsUnicode(false);
		});
        modelBuilder.Entity<TaskManagementStatus>(entity =>
        {
            entity.ToTable("Task_Management_Status");

            entity.Property(e => e.AllowedDays).HasMaxLength(50);
            entity.Property(e => e.Colorcode).HasMaxLength(100);
            entity.Property(e => e.Projectname).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(100);
        });

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
        modelBuilder.Entity<TaskMain>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Task_Mai__3214EC076195C1F2");

            entity.ToTable("Task_Main");

            entity.Property(e => e.AssignedToGroup).HasMaxLength(100);
            entity.Property(e => e.AssignedToUser).HasMaxLength(255);
            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.DueDate).HasColumnType("date");
            entity.Property(e => e.Extractfrom).HasMaxLength(200);
            entity.Property(e => e.ExtractfromId)
                .HasMaxLength(100)
                .HasColumnName("Extractfrom_Id");
            entity.Property(e => e.Label)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.NatureOfTaskId).HasColumnName("NatureOfTask_ID");
            entity.Property(e => e.PriorityId).HasColumnName("Priority_ID");
            entity.Property(e => e.Reviewer).HasMaxLength(255);
            entity.Property(e => e.SiteCode).HasMaxLength(100);
            entity.Property(e => e.SiteIncharge).HasMaxLength(100);
            entity.Property(e => e.SiteName).HasMaxLength(100);
            entity.Property(e => e.SprintId).HasColumnName("Sprint_ID");
            entity.Property(e => e.StatusChangedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatusChangedDate).HasColumnType("datetime");
            entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            entity.Property(e => e.Tablename).HasMaxLength(100);
            entity.Property(e => e.TaskTypeId).HasColumnName("TaskType_ID");
            entity.Property(e => e.TicketNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AuditTaskMain>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Audit_Task_Main");

            entity.Property(e => e.Action)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AssignedToGroup).HasMaxLength(100);
            entity.Property(e => e.AssignedToUser).HasMaxLength(300);
            entity.Property(e => e.ChangedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ChangedDate).HasColumnType("datetime");
            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.DueDate).HasColumnType("date");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Label)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.NatureOfTaskId).HasColumnName("NatureOfTask_ID");
            entity.Property(e => e.PriorityId).HasColumnName("Priority_ID");
            entity.Property(e => e.Reviewer)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SprintId).HasColumnName("Sprint_ID");
            entity.Property(e => e.StatusChangedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatusChangedDate).HasColumnType("datetime");
            entity.Property(e => e.StatusId).HasColumnName("Status_ID");
            entity.Property(e => e.TaskId).HasColumnName("Task_Id");
            entity.Property(e => e.TaskTypeId).HasColumnName("TaskType_ID");
            entity.Property(e => e.TicketNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });
        modelBuilder.Entity<TaskDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Task_Doc__3214EC072C4A09D3");

            entity.ToTable("Task_Documents");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.FileName)
                .HasMaxLength(50)
                .HasColumnName("File_Name");
            entity.Property(e => e.FilePath)
                .HasMaxLength(255)
                .HasColumnName("File_Path");
            entity.Property(e => e.Guid).HasMaxLength(255);
            entity.Property(e => e.TaskId).HasColumnName("Task_Id");
        });
        modelBuilder.Entity<PlanningUploadDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Planning__3214EC07077A6BAF");

            entity.ToTable("Planning_Upload_Details");

            entity.Property(e => e.Area)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AreaCode)
                .HasMaxLength(50)
                .HasColumnName("Area_Code");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.CategoryDescription)
                .HasColumnType("text")
                .HasColumnName("Category_Description");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("End_Date");
            entity.Property(e => e.FunctionLocation)
                .HasMaxLength(50)
                .HasColumnName("Function_Location");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PlanDate)
                .HasColumnType("date")
                .HasColumnName("Plan_Date");
            entity.Property(e => e.RecordId).HasColumnName("Record_Id");
            entity.Property(e => e.Site)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SiteCode)
                .HasMaxLength(50)
                .HasColumnName("Site_Code");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("Start_Date");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StateCode)
                .HasMaxLength(50)
                .HasColumnName("State_Code");
            entity.Property(e => e.Status).HasMaxLength(50);
        });
        modelBuilder.Entity<TempPlanningUploadDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Temp_Pla__3214EC070D0E03DC");

            entity.ToTable("Temp_Planning_Upload_Details");

            entity.Property(e => e.Area)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AreaCode)
                .HasMaxLength(50)
                .HasColumnName("Area_Code");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.CategoryDescription)
                .HasColumnType("text")
                .HasColumnName("Category_Description");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FunctionLocation)
                .HasMaxLength(50)
                .HasColumnName("Function_Location");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PlanDate)
                .HasColumnType("date")
                .HasColumnName("Plan_Date");
            entity.Property(e => e.RecordId).HasColumnName("Record_Id");
            entity.Property(e => e.Site)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SiteCode)
                .HasMaxLength(50)
                .HasColumnName("Site_Code");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StateCode)
                .HasMaxLength(50)
                .HasColumnName("State_Code");
            entity.Property(e => e.Status).HasMaxLength(50);
        });
        modelBuilder.Entity<BulkUploadConfiguration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bulk_Upl__3214EC072872DE08");

            entity.ToTable("Bulk_Upload_Configuration");

            entity.Property(e => e.CreatedDateName).HasMaxLength(50);
            entity.Property(e => e.CreatedUserName).HasMaxLength(50);
            entity.Property(e => e.ExcelColumnName).HasMaxLength(50);
            entity.Property(e => e.FeildDataType).HasMaxLength(50);
            entity.Property(e => e.FieldName).HasMaxLength(128);
            entity.Property(e => e.Mandatory).HasMaxLength(50);
            entity.Property(e => e.MasterName).HasMaxLength(128);
            entity.Property(e => e.ModifiedDateName).HasMaxLength(50);
            entity.Property(e => e.ModifiedUserName).HasMaxLength(50);
            entity.Property(e => e.RegularExpression).HasMaxLength(50);
            entity.Property(e => e.TableName).HasMaxLength(50);
            entity.Property(e => e.UploadTable)
                .HasMaxLength(50)
                .HasColumnName("Upload_Table");
        });
        modelBuilder.Entity<DocUploadHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DocUploa__3214EC077767F13E");

            entity.ToTable("DocUploadHistory");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Documenttype)
                .HasMaxLength(50)
                .HasColumnName("documenttype");
            entity.Property(e => e.Entityname)
                .HasMaxLength(100)
                .HasColumnName("entityname");
            entity.Property(e => e.Filename)
                .HasMaxLength(255)
                .HasColumnName("filename");
            entity.Property(e => e.Filepath)
                .HasMaxLength(255)
                .HasColumnName("filepath");
            entity.Property(e => e.MandateId).HasColumnName("mandate_id");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Remarks)
                .HasColumnType("text")
                .HasColumnName("remarks");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.VersionNumber).HasMaxLength(50);
        });
        modelBuilder.Entity<DocumentMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC07AB12D97E");

            entity.ToTable("DocumentMaster");

            entity.Property(e => e.AttachmentLink).HasMaxLength(255);
            entity.Property(e => e.CreatedBy).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DocumentContent).HasMaxLength(50);
            entity.Property(e => e.DocumentDescription).HasColumnType("text");
            entity.Property(e => e.DocumentId).HasMaxLength(50);
            entity.Property(e => e.DocumentName).HasMaxLength(100);
            entity.Property(e => e.DocumentType).HasMaxLength(100);
            entity.Property(e => e.FileName).HasMaxLength(100);
            entity.Property(e => e.ModifiedBy).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TemplatePath).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
