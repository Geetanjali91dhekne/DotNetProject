using System;
using System.Collections.Generic;
using AuthenticationService.Models;
using LogbookManagementService.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LogbookManagementService.Models;

public partial class FleetManagerContext : DbContext
{
    public FleetManagerContext()
    {
    }

    public FleetManagerContext(DbContextOptions<FleetManagerContext> options)
        : base(options)
    {
    }
  
    public virtual DbSet<SiteId> SiteIds { get; set; }

    public virtual DbSet<SiteKpiDetail> SiteKpiDetails { get; set; }
    public virtual DbSet<TUserSiteMapping> TUserSiteMappings { get; set; }
    public virtual DbSet<TKpiDetail> TKpiDetails { get; set; }
    public virtual DbSet<TLogbookCommonMaster> TLogbookCommonMasters { get; set; }
    public virtual DbSet<ErrorMaster> ErrorMasters { get; set; }
    public virtual DbSet<TLogbookEmployeeDetail> TLogbookEmployeeDetails { get; set; }

    public virtual DbSet<TaskMain> TaskMains { get; set; }
    public virtual DbSet<TLogbookGridBreakdownDetail> TLogbookGridBreakdownDetails { get; set; }

    public virtual DbSet<TLogbookHoto> TLogbookHotos { get; set; }

    public virtual DbSet<TLogbookRemark> TLogbookRemarks { get; set; }

    public virtual DbSet<WhyReasonMaster> WhyReasonMasters { get; set; }
    public virtual DbSet<TLogbookScadaDetail> TLogbookScadaDetails { get; set; }

    public virtual DbSet<TLogbookScheduleMaintenanceActivity> TLogbookScheduleMaintenanceActivities { get; set; }

    public virtual DbSet<TLogbookWtgBreakdownDetail> TLogbookWtgBreakdownDetails { get; set; }


    public virtual DbSet<KpiIdrv> KpiIdrvs { get; set; }

    public virtual DbSet<KpiLs> KpiLs { get; set; }

    public virtual DbSet<KpiPm> KpiPms { get; set; }

    public virtual DbSet<KpiTci> KpiTcis { get; set; }
    public virtual DbSet<KpiMtbf> KpiMtbfs { get; set; }

    public virtual DbSet<KpiMa> KpiMas { get; set; }

 
    public virtual DbSet<KpiMttr> KpiMttrs { get; set; }
    public virtual DbSet<KpiMttrMtbf> KpiMttrMtbfs { get; set; }

    public virtual DbSet<PKProjectsInspection> PKProjectsInspections { get; set; }
    public virtual DbSet<PKWorkOrderMangement> PKWorkOrderMangements { get; set; }
    public virtual DbSet<PKBDPlanning> PKBDPlannings { get; set; }
    public virtual DbSet<PKLubricationPlanning> PKLubricationPlannings { get; set; }
    public virtual DbSet<PKOilProcessPlanning> PKOilProcessPlannings { get; set; }
    public virtual DbSet<PKPMPlanning> PKPMPlannings { get; set; }
    public virtual DbSet<PKScadaInfraPM> PKScadaInfraPMs { get; set; }
    public virtual DbSet<PKTCIPlanning> PKTCIPlannings { get; set; }
    public virtual DbSet<PKTrainingPlanning> PKTrainingPlannings { get; set; }
    public virtual DbSet<PKResourcePlanning> PKResourcePlannings { get; set; }
    public virtual DbSet<PKInventoryPlanning> PKInventoryPlannings { get; set; }
    public virtual DbSet<PKVehiclePlanning> PKVehiclePlannings { get; set; }
    public virtual DbSet<TWhyAnalysis> WhyAnalyses { get; set; }
    
    public virtual DbSet<TWhyAnalysisDetail> TWhyAnalysisDetails { get; set; }
    
    public virtual DbSet<PkSiteToDoList> PkSiteToDoLists { get; set; }
    public virtual DbSet<TLogbookProductImage> TLogbookProductImages { get; set; }

    public virtual DbSet<TLogbookStatus> TLogbookStatuses { get; set; }

    public virtual DbSet<Permission> TPermissions { get; set; }

    public virtual DbSet<RoleMaster> TRoleMasters { get; set; }

    public virtual DbSet<RolePermissionMapping> TRolePermissionMappings { get; set; }

    public virtual DbSet<TUserRoleMapping> TUserRoleMappings { get; set; }
    public virtual DbSet<LogbookConfiguration> LogbookConfigurations { get; set; }
    public virtual DbSet<TurbineOnline> TurbineOnlines { get; set; }
    public virtual DbSet<VwWindFarm> VwWindFarms { get; set; }
    public virtual DbSet<VwTurbineOnline> VwTurbineOnlines { get; set; }
    public virtual DbSet<CatalogTurbine> CatalogTurbines { get; set; }
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
        modelBuilder.Entity<WhyReasonMaster>(entity =>
        {
            entity.ToTable("Why_Reason_Master");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<TUserSiteMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_User_S__3213E83F6E861C0E");

            entity.ToTable("T_User_Site_Mapping");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AreaCode).HasMaxLength(20);
            entity.Property(e => e.CountryCode).HasMaxLength(20);
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmployeeCode).HasMaxLength(20);
            entity.Property(e => e.ModifiedBy).HasMaxLength(20);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SiteCode).HasMaxLength(20);
            entity.Property(e => e.StateCode).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<LogbookConfiguration>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Logbook_Configuration");

            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .IsUnicode(false);
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

        modelBuilder.Entity<ErrorMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Error_Master");

            entity.Property(e => e.BrakeProgram).HasColumnName("Brake Program");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.ProposedLevelResetAlarm).HasColumnName("Proposed_Level Reset Alarm");
            entity.Property(e => e.ProposedRemoteReset)
                .HasMaxLength(255)
                .HasColumnName("Proposed Remote Reset");
            entity.Property(e => e.TurbineControlSWVersion)
                .HasMaxLength(255)
                .HasColumnName("Turbine control S/w version");
            entity.Property(e => e.WtgType)
                .HasMaxLength(255)
                .HasColumnName("WTG Type");
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
        modelBuilder.Entity<KpiMttr>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Kpi_MTTR");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.Model).HasMaxLength(55);
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<KpiMttrMtbf>(entity =>
        {
            entity.ToTable("Kpi_MTTR_MTBF");

            entity.Property(e => e.AvailDistPer)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Avail_Dist_per");
            entity.Property(e => e.AvailImpactPer)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Avail_Impact_per");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.Duration).HasMaxLength(50);
            entity.Property(e => e.Event).HasMaxLength(50);
            entity.Property(e => e.EventDescription)
                .HasMaxLength(100)
                .HasColumnName("Event_Description");
            entity.Property(e => e.Instance).HasMaxLength(20);
            entity.Property(e => e.IsModel).HasColumnName("Is_Model");
            entity.Property(e => e.IsType)
                .HasMaxLength(50)
                .HasColumnName("Is_Type");
            entity.Property(e => e.LostProdKwh)
                .HasMaxLength(20)
                .HasColumnName("Lost_Prod_kwh");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .HasColumnName("Modified_By");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("Modified_Date");
            entity.Property(e => e.MtbfHours)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("MTBF_hours");
            entity.Property(e => e.MttrHours)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("MTTR_hours");
            entity.Property(e => e.PlantRole)
                .HasMaxLength(60)
                .HasColumnName("Plant_Role");
            entity.Property(e => e.SystemComponent)
                .HasMaxLength(100)
                .HasColumnName("System_Component");
        });
        modelBuilder.Entity<SiteKpiDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Site_Kpi_Details");

            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("createdDate");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.ModifiedBy).HasMaxLength(20);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SiteName)
                .HasMaxLength(20)
                .HasColumnName("Site_Name");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.TurbineNumber)
                .HasMaxLength(20)
                .HasColumnName("Turbine_Number");
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
        modelBuilder.Entity<TKpiDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("T_KPI_Details");

            entity.Property(e => e.Columnname)
                .HasMaxLength(100)
                .HasColumnName("columnname");
            entity.Property(e => e.Filters).HasMaxLength(100);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Parameter).HasMaxLength(100);
            entity.Property(e => e.ReplaceColumns).HasMaxLength(100);
            entity.Property(e => e.ReplaceFilter).HasMaxLength(100);
            entity.Property(e => e.Topen)
                .HasMaxLength(20)
                .HasColumnName("TOPEN");
            entity.Property(e => e.Total)
                .HasMaxLength(20)
                .HasColumnName("TOTAL");
            entity.Property(e => e.Unit).HasMaxLength(100);
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Years).HasMaxLength(20);
        });

        modelBuilder.Entity<TLogbookEmployeeDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_Logboo__3214EC07488A2D5F");

            entity.ToTable("T_Logbook_EmployeeDetails");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmployeeCode)
                .HasMaxLength(20)
                .HasColumnName("Employee_Code");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(50)
                .HasColumnName("Employee_Name");
            entity.Property(e => e.FkSiteId).HasColumnName("Fk_Site_Id");
            entity.Property(e => e.LogDate)
                .HasColumnType("datetime")
                .HasColumnName("Log_Date");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.ShiftCycle).HasMaxLength(20);
            entity.Property(e => e.SiteName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Site_Name");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.WorkDoneShift)
                .HasMaxLength(50)
                .HasColumnName("WorkDone_Shift");

            entity.HasOne(d => d.FkSite).WithMany(p => p.TLogbookEmployeeDetails)
                .HasForeignKey(d => d.FkSiteId)
                .HasConstraintName("FK__T_Logbook__Fk_Si__40F9A68C");
        });


        modelBuilder.Entity<TLogbookGridBreakdownDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_Logboo__3214EC071BE440AB");

            entity.ToTable("T_Logbook_Grid_Breakdown_Details");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EptwNumber)
                .HasMaxLength(50)
                .HasColumnName("EPTW_Number");
            entity.Property(e => e.FeederName)
                .HasMaxLength(20)
                .HasColumnName("Feeder_Name");
            entity.Property(e => e.FkSiteId).HasColumnName("Fk_Site_Id");
            entity.Property(e => e.FkTaskId).HasColumnName("Fk_Task_Id");
            entity.Property(e => e.GridDropReason)
                .HasMaxLength(50)
                .HasColumnName("Grid_Drop_Reason");
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RemarkAction)
                .HasMaxLength(100)
                .HasColumnName("Remark_Action");
            entity.Property(e => e.ShiftCycle).HasMaxLength(20);
            entity.Property(e => e.SiteName)
                .HasMaxLength(20)
                .HasColumnName("Site_Name");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.TimeFrom)
                .HasMaxLength(100)
                .HasColumnName("Time_From");
            entity.Property(e => e.TimeTo)
                .HasMaxLength(100)
                .HasColumnName("Time_To");
            entity.Property(e => e.TotalTime)
                .HasMaxLength(20)
                .HasColumnName("Total_Time");

            entity.HasOne(d => d.FkSite).WithMany(p => p.TLogbookGridBreakdownDetails)
                .HasForeignKey(d => d.FkSiteId)
                .HasConstraintName("FK_Logbook_Grid_Breakdown_Details_SiteId");
        });

        modelBuilder.Entity<TLogbookHoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_Logboo__3214EC07C64E0D1D");

            entity.ToTable("T_Logbook_HOTO");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FkSiteId).HasColumnName("Fk_Site_Id");
            entity.Property(e => e.HandedOverDateTime)
                .HasMaxLength(50)
                .HasColumnName("Handed_Over_Date_Time");
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ShiftCycle).HasMaxLength(20);
            entity.Property(e => e.ShiftHandedOverBy)
                .HasMaxLength(50)
                .HasColumnName("Shift_Handed_Over_By");
            entity.Property(e => e.ShiftHours)
                .HasMaxLength(20)
                .HasColumnName("Shift_Hours");
            entity.Property(e => e.ShiftTakenOverBy)
                .HasMaxLength(50)
                .HasColumnName("Shift_Taken_Over_By");
            entity.Property(e => e.SiteName)
                .HasMaxLength(20)
                .HasColumnName("Site_Name");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.TakenOverDateTime)
                .HasMaxLength(50)
                .HasColumnName("Taken_Over_Date_Time");

            entity.HasOne(d => d.FkSite).WithMany(p => p.TLogbookHotos)
                .HasForeignKey(d => d.FkSiteId)
                .HasConstraintName("FK_Logbook_HOTO_SiteId");
        });

        modelBuilder.Entity<TLogbookRemark>(entity =>
        {
            entity.ToTable("T_Logbook_Remarks");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FkSiteId).HasColumnName("Fk_Site_Id");
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Remarks).HasMaxLength(200);
            entity.Property(e => e.ShiftCycle).HasMaxLength(20);
            entity.Property(e => e.SiteName).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.FkSite).WithMany(p => p.TLogbookRemarks)
                .HasForeignKey(d => d.FkSiteId)
                .HasConstraintName("FK_T_Logbook_Remarks_SiteId");
        });

        modelBuilder.Entity<TLogbookScadaDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_Logboo__3214EC07777F89ED");

            entity.ToTable("T_Logbook_Scada_Details");

            entity.Property(e => e.ActionTaken)
                .HasMaxLength(100)
                .HasColumnName("Action_Taken");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FkSiteId).HasColumnName("Fk_Site_Id");
            entity.Property(e => e.FkTaskId).HasColumnName("Fk_Task_Id");
            entity.Property(e => e.IssueDesc)
                .HasMaxLength(50)
                .HasColumnName("Issue_Desc");
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ShiftCycle).HasMaxLength(20);
            entity.Property(e => e.SiteName)
                .HasMaxLength(20)
                .HasColumnName("Site_Name");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.FkSite).WithMany(p => p.TLogbookScadaDetails)
                .HasForeignKey(d => d.FkSiteId)
                .HasConstraintName("FK_Logbook_Scada_Details_SiteId");
        });

        modelBuilder.Entity<TLogbookScheduleMaintenanceActivity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_Logboo__3214EC07B718991F");

            entity.ToTable("T_Logbook_Schedule_Maintenance_Activity");

            entity.Property(e => e.Activity).HasMaxLength(20);
            entity.Property(e => e.Closure).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EptwNumber)
                .HasMaxLength(50)
                .HasColumnName("EPTW_Number");
            entity.Property(e => e.FkSiteId).HasColumnName("Fk_Site_Id");
            entity.Property(e => e.FkTaskId).HasColumnName("Fk_Task_Id");
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Observation).HasMaxLength(50);
            entity.Property(e => e.RescheduleDate).HasColumnType("datetime");
            entity.Property(e => e.ShiftCycle).HasMaxLength(20);
            entity.Property(e => e.SiteName)
                .HasMaxLength(20)
                .HasColumnName("Site_Name");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.TurbineNumber)
                .HasMaxLength(50)
                .HasColumnName("Turbine_Number");
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

        modelBuilder.Entity<TLogbookWtgBreakdownDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_Logboo__3214EC07B156E470");

            entity.ToTable("T_Logbook_WTG_Breakdown_Details");

            entity.Property(e => e.ActionTaken)
                .HasMaxLength(500)
                .HasColumnName("Action_Taken");
            entity.Property(e => e.BreakdownCategory).HasMaxLength(100);
            entity.Property(e => e.Closure).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EptwNumber)
                .HasMaxLength(50)
                .HasColumnName("EPTW_Number");
            entity.Property(e => e.Error).HasMaxLength(100);
            entity.Property(e => e.FkSiteId).HasColumnName("Fk_Site_Id");
            entity.Property(e => e.FkTaskId).HasColumnName("Fk_Task_Id");
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PasswordUsage)
                .HasMaxLength(20)
                .HasColumnName("Password_Usage");
            entity.Property(e => e.PasswordUsageBy)
                .HasMaxLength(50)
                .HasColumnName("Password_Usage_By");
            entity.Property(e => e.RowId).HasMaxLength(100);
            entity.Property(e => e.ShiftCycle).HasMaxLength(20);
            entity.Property(e => e.SiteName)
                .HasMaxLength(20)
                .HasColumnName("Site_Name");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.TimeFrom)
                .HasMaxLength(100)
                .HasColumnName("Time_From");
            entity.Property(e => e.TimeTo)
                .HasMaxLength(100)
                .HasColumnName("Time_To");
            entity.Property(e => e.TotalTime)
                .HasMaxLength(20)
                .HasColumnName("Total_Time");
            entity.Property(e => e.TurbineNumber)
                .HasMaxLength(50)
                .HasColumnName("Turbine_Number");
        });

        modelBuilder.Entity<KpiIdrv>(entity =>
        {
            entity.ToTable("Kpi_IDRV");

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Wtg).HasMaxLength(50);
        });

        modelBuilder.Entity<KpiLs>(entity =>
        {
            entity.ToTable("Kpi_LS");

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Wtg).HasMaxLength(50);
        });

        modelBuilder.Entity<KpiPm>(entity =>
        {
            entity.ToTable("Kpi_PM");

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Wtg).HasMaxLength(50);
        });

        modelBuilder.Entity<KpiTci>(entity =>
        {
            entity.ToTable("Kpi_TCI");

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Wtg).HasMaxLength(50);
        });
        modelBuilder.Entity<KpiMtbf>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Kpi_MTBF");


            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.Model).HasMaxLength(20);
            entity.Property(e => e.Value).HasColumnName("VALUE");
        });


        modelBuilder.Entity<TWhyAnalysis>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_WhyAna__3214EC0738A020EB");

            entity.ToTable("T_Why_Analysis");

            entity.Property(e => e.AnalysisDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(20);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.GrandTotal).HasMaxLength(20);
            entity.Property(e => e.MainBucket)
                .HasMaxLength(200)
                .HasColumnName("Main_Bucket");
            entity.Property(e => e.ModelName).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.ModifiedBy).HasMaxLength(20);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OverallActionItem).HasMaxLength(255);
            entity.Property(e => e.Remarks1).HasMaxLength(255);
            entity.Property(e => e.Remarks2).HasMaxLength(255);
            entity.Property(e => e.SapCode).HasMaxLength(50);
            entity.Property(e => e.Section).HasMaxLength(20);
            entity.Property(e => e.Site)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.StandardRemarks).HasMaxLength(255);
            entity.Property(e => e.State).HasMaxLength(20);
            entity.Property(e => e.SubBucket)
                .HasMaxLength(200)
                .HasColumnName("Sub_Bucket");
            entity.Property(e => e.TowerType).HasMaxLength(50);
        });
        modelBuilder.Entity<TWhyAnalysisDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_Why_An__3214EC07AA95C526");

            entity.ToTable("T_Why_Analysis_Details");

            entity.Property(e => e.Ai)
                .HasMaxLength(55)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FkAnalysisId).HasColumnName("Fk_Analysis_Id");
            entity.Property(e => e.FkTypeId).HasColumnName("Fk_Type_Id");
            entity.Property(e => e.Hrs)
                .HasMaxLength(55)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Why1).HasMaxLength(255);
            entity.Property(e => e.Why2).HasMaxLength(255);
            entity.Property(e => e.Why3).HasMaxLength(255);
            entity.Property(e => e.Why4).HasMaxLength(255);
            entity.Property(e => e.Why5).HasMaxLength(255);
            entity.Property(e => e.Why6).HasMaxLength(255);
        });

        modelBuilder.Entity<TLogbookProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_Logboo__3214EC07A8EC6B14");

            entity.ToTable("T_Logbook_Product_Image");

            entity.Property(e => e.ProductImage).HasMaxLength(255);
            entity.Property(e => e.ProductName).HasMaxLength(50);
        });

        modelBuilder.Entity<TLogbookStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_Logboo__3214EC27DA4B9A9E");

            entity.ToTable("T_Logbook_Status");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.LogDate)
                .HasColumnType("date")
                .HasColumnName("Log_Date");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ShiftCycle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SiteName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });
        modelBuilder.Entity<TurbineOnline>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Turbine_Online");

            entity.Property(e => e.AccumulatedProduction).HasColumnType("numeric(19, 1)");
            entity.Property(e => e.ControllerTimestamp)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Production).HasColumnType("numeric(9, 4)");
            entity.Property(e => e.ServerTimestamp).HasPrecision(3);
            entity.Property(e => e.WindSpeed).HasColumnType("numeric(9, 4)");
        });
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
        modelBuilder.Entity<VwTurbineOnline>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vw_Turbine_Online");

            entity.Property(e => e.AccumulatedProduction).HasColumnType("numeric(19, 1)");
            entity.Property(e => e.ControllerTimestamp)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Production).HasColumnType("numeric(9, 4)");
            entity.Property(e => e.ServerTimestamp).HasPrecision(3);
            entity.Property(e => e.WindSpeed).HasColumnType("numeric(9, 4)");
        });
        modelBuilder.Entity<CatalogTurbine>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Catalog_Turbine");

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
