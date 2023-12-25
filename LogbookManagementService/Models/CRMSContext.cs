using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LogbookManagementService.Models;

public partial class CRMSContext : DbContext
{
    public CRMSContext()
    {
    }

    public CRMSContext(DbContextOptions<CRMSContext> options)
        : base(options)
    {
    }

    public virtual DbSet<VScAreaOmsPbi> VScAreaOmsPbis { get; set; }
    public virtual DbSet<ScCountry> ScCountries { get; set; }
    public virtual DbSet<CmrEmployeeMaster> CmrEmployeeMasters { get; set; }
    public virtual DbSet<CrmBreakdownDetail> CrmBreakdownDetails { get; set; }
    public virtual DbSet<ScAllMachineStaticDetail> ScAllMachineStaticDetails { get; set; }
    public virtual DbSet<CrmErrorMaster> CrmErrorMasters { get; set; }
    public virtual DbSet<ScArea> ScAreas { get; set; }
    public virtual DbSet<ScMainSite> ScMainSites { get; set; }
    public virtual DbSet<ScState> ScStates { get; set; }
    public virtual DbSet<VScSiteStateMasterOmsPbi> VScSiteStateMasterOmsPbis { get; set; }
    public virtual DbSet<VScCustMktGroupMstOmsPbi> VScCustMktGroupMstOmsPbis { get; set; }
    public virtual DbSet<VAllMachineStaticDetailSc> VAllMachineStaticDetailScs { get; set; }
    public virtual DbSet<VScAllMachineStaticDetailOmsPbi> VScAllMachineStaticDetailOmsPbis { get; set; }
    public virtual DbSet<VScCustSubGroupMstOmsPbi> VScCustSubGroupMstOmsPbis { get; set; }
    public virtual DbSet<VScWtgModelMasterOmsPbi> VScWtgModelMasterOmsPbis { get; set; }
    public virtual DbSet<VCrmGenerationAllIndiaOmsPbi> VCrmGenerationAllIndiaOmsPbis { get; set; }
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
        modelBuilder.Entity<VScAreaOmsPbi>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_SC_AREA_OMS_PBI");

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
        modelBuilder.Entity<VScSiteStateMasterOmsPbi>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_SC_SITE_STATE_MASTER_OMS_PBI");

            entity.Property(e => e.DelKey)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DEL_KEY");
            entity.Property(e => e.Dgrstartdate)
                .HasPrecision(0)
                .HasColumnName("DGRSTARTDATE");
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
            entity.Property(e => e.SbuOnm)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("SBU_ONM");
            entity.Property(e => e.Site)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SITE");
            entity.Property(e => e.SiteCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("SITE_CODE");
            entity.Property(e => e.SiteShortName)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("SITE_SHORT_NAME");
            entity.Property(e => e.UseForSelection)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USE_FOR_SELECTION");
        });

        modelBuilder.Entity<VScWtgModelMasterOmsPbi>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_SC_WTG_MODEL_MASTER_OMS_PBI");

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
            entity.Property(e => e.WtgModelCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("WTG_MODEL_CODE");
            entity.Property(e => e.WtgModelDesc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WTG_MODEL_DESC");
            entity.Property(e => e.WtgModelName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WTG_MODEL_NAME");
        });

        modelBuilder.Entity<VScCustSubGroupMstOmsPbi>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_SC_CUST_SUB_GROUP_MST_OMS_PBI");

            entity.Property(e => e.CompletionFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("COMPLETION_FLAG");
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("CONTACT_PERSON");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("COUNTRY_CODE");
            entity.Property(e => e.CrmsAccMgr)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CRMS_ACC_MGR");
            entity.Property(e => e.CustAdd1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("CUST_ADD1");
            entity.Property(e => e.CustAdd2)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("CUST_ADD2");
            entity.Property(e => e.CustAdd3)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("CUST_ADD3");
            entity.Property(e => e.CustCity)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("CUST_CITY");
            entity.Property(e => e.CustCountry)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("CUST_COUNTRY");
            entity.Property(e => e.CustCstDate)
                .HasPrecision(0)
                .HasColumnName("CUST_CST_DATE");
            entity.Property(e => e.CustCstNo)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("CUST_CST_NO");
            entity.Property(e => e.CustDocGroup)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CUST_DOC_GROUP");
            entity.Property(e => e.CustDocSname)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("CUST_DOC_SNAME");
            entity.Property(e => e.CustEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CUST_EMAIL");
            entity.Property(e => e.CustFax)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CUST_FAX");
            entity.Property(e => e.CustPanNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CUST_PAN_NO");
            entity.Property(e => e.CustPhone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CUST_PHONE");
            entity.Property(e => e.CustPin)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CUST_PIN");
            entity.Property(e => e.CustStDate)
                .HasPrecision(0)
                .HasColumnName("CUST_ST_DATE");
            entity.Property(e => e.CustStNo)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("CUST_ST_NO");
            entity.Property(e => e.CustState)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("CUST_STATE");
            entity.Property(e => e.CustTinNo)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("CUST_TIN_NO");
            entity.Property(e => e.CustWebSite)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CUST_WEB_SITE");
            entity.Property(e => e.CustomerCode)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_CODE");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_NAME");
            entity.Property(e => e.DeliverReport).HasColumnName("DELIVER_REPORT");
            entity.Property(e => e.Enteredby)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ENTEREDBY");
            entity.Property(e => e.Enteredon)
                .HasPrecision(0)
                .HasColumnName("ENTEREDON");
            entity.Property(e => e.GroupCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("GROUP_CODE");
            entity.Property(e => e.GroupName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("GROUP_NAME");
            entity.Property(e => e.Industrysegmentid)
                .HasColumnType("numeric(38, 0)")
                .HasColumnName("INDUSTRYSEGMENTID");
            entity.Property(e => e.Isactive)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Isaddressfromcustomer)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISADDRESSFROMCUSTOMER");
            entity.Property(e => e.Isaddressfrommktgroup)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISADDRESSFROMMKTGROUP");
            entity.Property(e => e.MainCustomerCode)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("MAIN_CUSTOMER_CODE");
            entity.Property(e => e.MktEmailAdd)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("MKT_EMAIL_ADD");
            entity.Property(e => e.MktGroupCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MKT_GROUP_CODE");
            entity.Property(e => e.Modifiedby)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MODIFIEDBY");
            entity.Property(e => e.Modifiedon)
                .HasPrecision(0)
                .HasColumnName("MODIFIEDON");
            entity.Property(e => e.OmsInvBccMailid)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("OMS_INV_BCC_MAILID");
            entity.Property(e => e.OmsInvCcMailid)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("OMS_INV_CC_MAILID");
            entity.Property(e => e.OmsInvToMailid)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("OMS_INV_TO_MAILID");
            entity.Property(e => e.SapCustomerCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SAP_CUSTOMER_CODE");
            entity.Property(e => e.SapExpDate)
                .HasPrecision(0)
                .HasColumnName("SAP_EXP_DATE");
            entity.Property(e => e.SapExpFlag).HasColumnName("SAP_EXP_FLAG");
            entity.Property(e => e.SapModiDate)
                .HasPrecision(0)
                .HasColumnName("SAP_MODI_DATE");
            entity.Property(e => e.SapModiFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("SAP_MODI_FLAG");
        });

        modelBuilder.Entity<VScCustMktGroupMstOmsPbi>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_SC_CUST_MKT_GROUP_MST_OMS_PBI");

            entity.Property(e => e.Add1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ADD1");
            entity.Property(e => e.Add2)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ADD2");
            entity.Property(e => e.Add3)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ADD3");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CITY");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COUNTRY");
            entity.Property(e => e.CustomerSegment)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_SEGMENT");
            entity.Property(e => e.DeliverReport).HasColumnName("DELIVER_REPORT");
            entity.Property(e => e.Emailid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAILID");
            entity.Property(e => e.Enteredby)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ENTEREDBY");
            entity.Property(e => e.Enteredon)
                .HasPrecision(0)
                .HasColumnName("ENTEREDON");
            entity.Property(e => e.Fax)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("FAX");
            entity.Property(e => e.Isactive)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.MktGroupCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MKT_GROUP_CODE");
            entity.Property(e => e.MktGroupName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MKT_GROUP_NAME");
            entity.Property(e => e.Modifiedby)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MODIFIEDBY");
            entity.Property(e => e.Modifiedon)
                .HasPrecision(0)
                .HasColumnName("MODIFIEDON");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PHONE");
            entity.Property(e => e.PinCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PIN_CODE");
            entity.Property(e => e.Remarks)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("REMARKS");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STATE");
            entity.Property(e => e.StdCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("STD_CODE");
            entity.Property(e => e.WebSite)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("WEB_SITE");
        });

        modelBuilder.Entity<VAllMachineStaticDetailSc>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_ALL_MACHINE_STATIC_DETAIL_SC");

            entity.Property(e => e.Area)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AREA");
            entity.Property(e => e.AreaCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("AREA_CODE");
            entity.Property(e => e.Bladetype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BLADETYPE");
            entity.Property(e => e.Capitive)
                .HasColumnType("numeric(10, 3)")
                .HasColumnName("CAPITIVE");
            entity.Property(e => e.CirCode)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("CIR_CODE");
            entity.Property(e => e.Client)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("CLIENT");
            entity.Property(e => e.CommDate)
                .HasPrecision(0)
                .HasColumnName("COMM_DATE");
            entity.Property(e => e.CommOpDate)
                .HasPrecision(0)
                .HasColumnName("COMM_OP_DATE");
            entity.Property(e => e.ControlPanel)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("CONTROL_PANEL");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COUNTRY");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("COUNTRY_CODE");
            entity.Property(e => e.CrmsAccMgr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CRMS_ACC_MGR");
            entity.Property(e => e.CustomerCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_CODE");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_NAME");
            entity.Property(e => e.DelKey)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("DEL_KEY");
            entity.Property(e => e.DgrIsActive)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("DGR_IS_ACTIVE");
            entity.Property(e => e.District)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DISTRICT");
            entity.Property(e => e.Eb)
                .HasColumnType("numeric(10, 3)")
                .HasColumnName("EB");
            entity.Property(e => e.FeederCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FEEDER_CODE");
            entity.Property(e => e.FeederName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FEEDER_NAME");
            entity.Property(e => e.GridCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("GRID_CODE");
            entity.Property(e => e.GridName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GRID_NAME");
            entity.Property(e => e.GroupCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("GROUP_CODE");
            entity.Property(e => e.GroupName)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("GROUP_NAME");
            entity.Property(e => e.GurantedUnits)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("GURANTED_UNITS");
            entity.Property(e => e.HubHeight)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("HUB_HEIGHT");
            entity.Property(e => e.Hubtype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HUBTYPE");
            entity.Property(e => e.InstCapacity)
                .HasColumnType("numeric(10, 3)")
                .HasColumnName("INST_CAPACITY");
            entity.Property(e => e.InstalledCapacityMw)
                .HasColumnType("numeric(10, 3)")
                .HasColumnName("INSTALLED_CAPACITY_MW");
            entity.Property(e => e.Latitude)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LATITUDE");
            entity.Property(e => e.LocNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("LOC_NO");
            entity.Property(e => e.Longitude)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LONGITUDE");
            entity.Property(e => e.MF)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("M_F");
            entity.Property(e => e.MainCustomerCode)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("MAIN_CUSTOMER_CODE");
            entity.Property(e => e.MainSite)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MAIN_SITE");
            entity.Property(e => e.MainSiteCode)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("MAIN_SITE_CODE");
            entity.Property(e => e.MapId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MAP_ID");
            entity.Property(e => e.Meansealevel)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("MEANSEALEVEL");
            entity.Property(e => e.MeterNo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("METER_NO");
            entity.Property(e => e.MeteringPoint)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("METERING_POINT");
            entity.Property(e => e.MktgAccMgr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MKTG_ACC_MGR");
            entity.Property(e => e.MmCode)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("MM_CODE");
            entity.Property(e => e.MmName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MM_NAME");
            entity.Property(e => e.MtrPointFuncLocCode)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("MTR_POINT_FUNC_LOC_CODE");
            entity.Property(e => e.PhaseNum)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHASE_NUM");
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("REGION");
            entity.Property(e => e.Reportonmaxz5)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("REPORTONMAXZ5");
            entity.Property(e => e.RotarDia)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("ROTAR_DIA");
            entity.Property(e => e.SCNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("S_C_NO");
            entity.Property(e => e.SapFuncLocCode)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("SAP_FUNC_LOC_CODE");
            entity.Property(e => e.SbuOnm)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("SBU_ONM");
            entity.Property(e => e.Section)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SECTION");
            entity.Property(e => e.Site)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SITE");
            entity.Property(e => e.SiteCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SITE_CODE");
            entity.Property(e => e.SiteShortName)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("SITE_SHORT_NAME");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STATE");
            entity.Property(e => e.StateCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("STATE_CODE");
            entity.Property(e => e.Substation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SUBSTATION");
            entity.Property(e => e.Surveyno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SURVEYNO");
            entity.Property(e => e.Taluka)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TALUKA");
            entity.Property(e => e.Thirdpartty)
                .HasColumnType("numeric(10, 3)")
                .HasColumnName("THIRDPARTTY");
            entity.Property(e => e.Towertype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TOWERTYPE");
            entity.Property(e => e.UseForSelection)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USE_FOR_SELECTION");
            entity.Property(e => e.Village)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("VILLAGE");
            entity.Property(e => e.WtgId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("WTG_ID");
            entity.Property(e => e.WtgMake)
                .HasMaxLength(50)
                .HasColumnName("WTG_MAKE");
            entity.Property(e => e.WtgModelCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("WTG_MODEL_CODE");
            entity.Property(e => e.WtgModelDesc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WTG_MODEL_DESC");
            entity.Property(e => e.WtgModelName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WTG_MODEL_NAME");
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
        modelBuilder.Entity<VScAllMachineStaticDetailOmsPbi>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_SC_ALL_MACHINE_STATIC_DETAIL_OMS_PBI");

            entity.Property(e => e.Area)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AREA");
            entity.Property(e => e.AreaCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("AREA_CODE");
            entity.Property(e => e.Bladetype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BLADETYPE");
            entity.Property(e => e.Cod)
                .HasPrecision(0)
                .HasColumnName("COD");
            entity.Property(e => e.CommDate)
                .HasPrecision(0)
                .HasColumnName("COMM_DATE");
            entity.Property(e => e.ControlPanel)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("CONTROL_PANEL");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COUNTRY");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("COUNTRY_CODE");
            entity.Property(e => e.CustomerCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_CODE");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_NAME");
            entity.Property(e => e.District)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DISTRICT");
            entity.Property(e => e.FeederCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FEEDER_CODE");
            entity.Property(e => e.FeederName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FEEDER_NAME");
            entity.Property(e => e.FormulaType)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("FORMULA_TYPE");
            entity.Property(e => e.GenEstimateBd)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("GEN_ESTIMATE_BD");
            entity.Property(e => e.GenEstimateWrd)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("GEN_ESTIMATE_WRD");
            entity.Property(e => e.GridCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("GRID_CODE");
            entity.Property(e => e.GridName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GRID_NAME");
            entity.Property(e => e.GridNode)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("GRID_NODE");
            entity.Property(e => e.HubHeight)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("HUB_HEIGHT");
            entity.Property(e => e.Hubtype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HUBTYPE");
            entity.Property(e => e.InstCapacity)
                .HasColumnType("numeric(10, 3)")
                .HasColumnName("INST_CAPACITY");
            entity.Property(e => e.KamName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("KAM_NAME");
            entity.Property(e => e.KamState)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("KAM_STATE");
            entity.Property(e => e.Latitude)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LATITUDE");
            entity.Property(e => e.LocNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("LOC_NO");
            entity.Property(e => e.Longitude)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LONGITUDE");
            entity.Property(e => e.MF)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("M_F");
            entity.Property(e => e.MainSite)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MAIN_SITE");
            entity.Property(e => e.MainSiteCode)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("MAIN_SITE_CODE");
            entity.Property(e => e.Meansealevel)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("MEANSEALEVEL");
            entity.Property(e => e.MeterNo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("METER_NO");
            entity.Property(e => e.MeteringPoint)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("METERING_POINT");
            entity.Property(e => e.MktGroupCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MKT_GROUP_CODE");
            entity.Property(e => e.MktGroupName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MKT_GROUP_NAME");
            entity.Property(e => e.MktgGroupCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MKTG_GROUP_CODE");
            entity.Property(e => e.MmCode)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("MM_CODE");
            entity.Property(e => e.MtrPointFuncLocCode)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("MTR_POINT_FUNC_LOC_CODE");
            entity.Property(e => e.PhaseNum)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHASE_NUM");
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("REGION");
            entity.Property(e => e.Reportonmaxz5)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("REPORTONMAXZ5");
            entity.Property(e => e.RotarDia)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("ROTAR_DIA");
            entity.Property(e => e.SapFuncLocCode)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("SAP_FUNC_LOC_CODE");
            entity.Property(e => e.Site)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SITE");
            entity.Property(e => e.SiteCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("SITE_CODE");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STATE");
            entity.Property(e => e.StateCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("STATE_CODE");
            entity.Property(e => e.Substation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SUBSTATION");
            entity.Property(e => e.Surveyno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SURVEYNO");
            entity.Property(e => e.Taluka)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TALUKA");
            entity.Property(e => e.Towertype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TOWERTYPE");
            entity.Property(e => e.Village)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("VILLAGE");
            entity.Property(e => e.WtgMake)
                .HasMaxLength(50)
                .HasColumnName("WTG_MAKE");
            entity.Property(e => e.WtgModelCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("WTG_MODEL_CODE");
            entity.Property(e => e.WtgModelName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WTG_MODEL_NAME");
        });
        modelBuilder.Entity<ScAllMachineStaticDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SC_ALL_MACHINE_STATIC_DETAIL");

            entity.Property(e => e.AdjSapFuncLocCode1)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ADJ_SAP_FUNC_LOC_CODE_1");
            entity.Property(e => e.AdjSapFuncLocCode2)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("ADJ_SAP_FUNC_LOC_CODE_2");
            entity.Property(e => e.Bladetype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BLADETYPE");
            entity.Property(e => e.Capitive)
                .HasColumnType("numeric(10, 3)")
                .HasColumnName("CAPITIVE");
            entity.Property(e => e.CirCode)
                .HasDefaultValueSql("((0))")
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("CIR_CODE");
            entity.Property(e => e.Cod)
                .HasPrecision(0)
                .HasColumnName("COD");
            entity.Property(e => e.CommDate)
                .HasPrecision(0)
                .HasColumnName("COMM_DATE");
            entity.Property(e => e.CommOpDate)
                .HasPrecision(0)
                .HasColumnName("COMM_OP_DATE");
            entity.Property(e => e.ControlPanel)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("CONTROL_PANEL");
            entity.Property(e => e.CrmsAccMgr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CRMS_ACC_MGR");
            entity.Property(e => e.CustomerCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_CODE");
            entity.Property(e => e.DgrIsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("DGR_IS_ACTIVE");
            entity.Property(e => e.District)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DISTRICT");
            entity.Property(e => e.Eb)
                .HasColumnType("numeric(10, 3)")
                .HasColumnName("EB");
            entity.Property(e => e.Enteredby)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ENTEREDBY");
            entity.Property(e => e.Enteredon)
                .HasPrecision(0)
                .HasColumnName("ENTEREDON");
            entity.Property(e => e.FeederCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FEEDER_CODE");
            entity.Property(e => e.GenEstimateBd)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("GEN_ESTIMATE_BD");
            entity.Property(e => e.GenEstimateWrd)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("GEN_ESTIMATE_WRD");
            entity.Property(e => e.GridCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("GRID_CODE");
            entity.Property(e => e.GridNode)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("GRID_NODE");
            entity.Property(e => e.GroupCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("GROUP_CODE");
            entity.Property(e => e.GurantedUnits)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("GURANTED_UNITS");
            entity.Property(e => e.HubHeight)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("HUB_HEIGHT");
            entity.Property(e => e.Hubtype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HUBTYPE");
            entity.Property(e => e.InstCapacity)
                .HasColumnType("numeric(10, 3)")
                .HasColumnName("INST_CAPACITY");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IPADDRESS");
            entity.Property(e => e.Isactive)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISACTIVE");
            entity.Property(e => e.Latitude)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LATITUDE");
            entity.Property(e => e.LocNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("LOC_NO");
            entity.Property(e => e.Longitude)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LONGITUDE");
            entity.Property(e => e.MF)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("M_F");
            entity.Property(e => e.MaFormula)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("MA_FORMULA");
            entity.Property(e => e.MapId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MAP_ID");
            entity.Property(e => e.Meansealevel)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("MEANSEALEVEL");
            entity.Property(e => e.MeterNo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("METER_NO");
            entity.Property(e => e.MeteringPoint)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("METERING_POINT");
            entity.Property(e => e.MktGroupCode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MKT_GROUP_CODE");
            entity.Property(e => e.MktgAccMgr)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MKTG_ACC_MGR");
            entity.Property(e => e.MmCode)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("MM_CODE");
            entity.Property(e => e.Modifiedby)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MODIFIEDBY");
            entity.Property(e => e.Modifiedon)
                .HasPrecision(0)
                .HasColumnName("MODIFIEDON");
            entity.Property(e => e.MtrPointFuncLocCode)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("MTR_POINT_FUNC_LOC_CODE");
            entity.Property(e => e.PhaseNum)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHASE_NUM");
            entity.Property(e => e.Plantid)
                .HasColumnType("numeric(38, 0)")
                .HasColumnName("PLANTID");
            entity.Property(e => e.Plantname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PLANTNAME");
            entity.Property(e => e.Plantunitid)
                .HasColumnType("numeric(38, 0)")
                .HasColumnName("PLANTUNITID");
            entity.Property(e => e.RefMetMast)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("REF_MET_MAST");
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("REGION");
            entity.Property(e => e.Reportonmaxz5)
                .HasDefaultValueSql("((0))")
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("REPORTONMAXZ5");
            entity.Property(e => e.RotarDia)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("ROTAR_DIA");
            entity.Property(e => e.SCNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("S_C_NO");
            entity.Property(e => e.SapExpDate)
                .HasPrecision(0)
                .HasColumnName("SAP_EXP_DATE");
            entity.Property(e => e.SapExpFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SAP_EXP_FLAG");
            entity.Property(e => e.SapFuncLocCode)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("SAP_FUNC_LOC_CODE");
            entity.Property(e => e.Section)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SECTION");
            entity.Property(e => e.SiteCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SITE_CODE");
            entity.Property(e => e.Substation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SUBSTATION");
            entity.Property(e => e.Surveyno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SURVEYNO");
            entity.Property(e => e.Taluka)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TALUKA");
            entity.Property(e => e.Thirdpartty)
                .HasColumnType("numeric(10, 3)")
                .HasColumnName("THIRDPARTTY");
            entity.Property(e => e.Towertype)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TOWERTYPE");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Village)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("VILLAGE");
            entity.Property(e => e.WtgId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("WTG_ID");
            entity.Property(e => e.WtgMake)
                .HasMaxLength(50)
                .HasColumnName("WTG_MAKE");
            entity.Property(e => e.WtgModelCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("WTG_MODEL_CODE");
        });
        modelBuilder.Entity<CrmErrorMaster>(entity =>
        {
            entity.HasKey(e => e.CrmErrorCode).HasName("PKERRCODE");

            entity.ToTable("CRM_ERROR_MASTER");

            entity.Property(e => e.CrmErrorCode)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("CRM_ERROR_CODE");
            entity.Property(e => e.Classification)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CLASSIFICATION");
            entity.Property(e => e.ControllerType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CONTROLLER_TYPE");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Enteredby)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ENTEREDBY");
            entity.Property(e => e.Enteredon)
                .HasPrecision(0)
                .HasColumnName("ENTEREDON");
            entity.Property(e => e.ErrRelatedTo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ERR_RELATED_TO");
            entity.Property(e => e.ErrorCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ERROR_CODE");
            entity.Property(e => e.InstCapacity)
                .HasColumnType("numeric(10, 3)")
                .HasColumnName("INST_CAPACITY");
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
            entity.Property(e => e.Severity)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SEVERITY");
        });
        modelBuilder.Entity<CrmBreakdownDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CRM_BREAKDOWN_DETAIL");

            entity.Property(e => e.ActionTaken)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("ACTION_TAKEN");
            entity.Property(e => e.AreaCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("AREA_CODE");
            entity.Property(e => e.AttendedBy)
                .HasMaxLength(4000)
                .IsUnicode(false)
                .HasColumnName("ATTENDED_BY");
            entity.Property(e => e.BdEndTime)
                .HasPrecision(0)
                .HasColumnName("BD_END_TIME");
            entity.Property(e => e.BdRemark)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("BD_REMARK");
            entity.Property(e => e.BdStartTime)
                .HasPrecision(0)
                .HasColumnName("BD_START_TIME");
            entity.Property(e => e.BdType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BD_TYPE");
            entity.Property(e => e.Classification)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CLASSIFICATION");
            entity.Property(e => e.Commercial)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("COMMERCIAL");
            entity.Property(e => e.DateOfGen)
                .HasPrecision(0)
                .HasColumnName("DATE_OF_GEN");
            entity.Property(e => e.Detailid)
                .ValueGeneratedOnAdd()
                .HasColumnType("numeric(38, 0)")
                .HasColumnName("DETAILID");
            entity.Property(e => e.DowntimeType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DOWNTIME_TYPE");
            entity.Property(e => e.Enteredby)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ENTEREDBY");
            entity.Property(e => e.Enteredon)
                .HasPrecision(0)
                .HasColumnName("ENTEREDON");
            entity.Property(e => e.ErrorCode)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ERROR_CODE");
            entity.Property(e => e.FCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("F_CODE");
            entity.Property(e => e.FormulaParameter)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FORMULA_PARAMETER");
            entity.Property(e => e.IsDataCompleted)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('F')")
                .HasColumnName("IS_DATA_COMPLETED");
            entity.Property(e => e.IsSubmitted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("IS_SUBMITTED");
            entity.Property(e => e.LocNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LOC_NO");
            entity.Property(e => e.MainSiteCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MAIN_SITE_CODE");
            entity.Property(e => e.Rowid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ROWID");
            entity.Property(e => e.SapFuncLocCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SAP_FUNC_LOC_CODE");
            entity.Property(e => e.SiteCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SITE_CODE");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STATE");
            entity.Property(e => e.SystemComponent)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SYSTEM_COMPONENT");
            entity.Property(e => e.TotalDuration).HasColumnName("TOTAL_DURATION");
            entity.Property(e => e.TravelTime)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TRAVEL_TIME");
            entity.Property(e => e.WorkingAt)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("WORKING_AT");
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

        modelBuilder.Entity<VCrmGenerationAllIndiaOmsPbi>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_CRM_GENERATION_ALL_INDIA_OMS_PBI");

            entity.Property(e => e.A).HasColumnType("numeric(6, 2)");
            entity.Property(e => e.AddDate)
                .HasPrecision(0)
                .HasColumnName("ADD_DATE");
            entity.Property(e => e.AddUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ADD_USER");
            entity.Property(e => e.B).HasColumnType("numeric(6, 2)");
            entity.Property(e => e.BreakDownDetailRemark)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("BREAK_DOWN_DETAIL_REMARK");
            entity.Property(e => e.BreakDownHours)
                .HasColumnType("numeric(7, 2)")
                .HasColumnName("BREAK_DOWN_HOURS");
            entity.Property(e => e.C).HasColumnType("numeric(6, 2)");
            entity.Property(e => e.CgaDenominator)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("CGA_DENOMINATOR");
            entity.Property(e => e.CgaNumerator)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("CGA_NUMERATOR");
            entity.Property(e => e.CmaDenominator)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("CMA_DENOMINATOR");
            entity.Property(e => e.CmaNumerator)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("CMA_NUMERATOR");
            entity.Property(e => e.ContMa)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("CONT_MA");
            entity.Property(e => e.CustomerCode)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_CODE");
            entity.Property(e => e.DailyBigG)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("DAILY_BIG_G");
            entity.Property(e => e.DailyPlf)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("DAILY_PLF");
            entity.Property(e => e.DailySmallG)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("DAILY_SMALL_G");
            entity.Property(e => e.Dailygenhrs)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("DAILYGENHRS");
            entity.Property(e => e.DateOfGen)
                .HasPrecision(0)
                .HasColumnName("DATE_OF_GEN");
            entity.Property(e => e.DelKey)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("DEL_KEY");
            entity.Property(e => e.EbBackKvarhExp)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("EB_BACK_KVARH_EXP");
            entity.Property(e => e.EbBackKvarhImp)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("EB_BACK_KVARH_IMP");
            entity.Property(e => e.EbBackKwhExp)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("EB_BACK_KWH_EXP");
            entity.Property(e => e.EbBackKwhImp)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("EB_BACK_KWH_IMP");
            entity.Property(e => e.EbBackLineLoss)
                .HasColumnType("numeric(7, 2)")
                .HasColumnName("EB_BACK_LINE_LOSS");
            entity.Property(e => e.EbBackNetKvarhExp)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("EB_BACK_NET_KVARH_EXP");
            entity.Property(e => e.EbBackNetKwhExp)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("EB_BACK_NET_KWH_EXP");
            entity.Property(e => e.EbMainKvarhExp)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("EB_MAIN_KVARH_EXP");
            entity.Property(e => e.EbMainKvarhImp)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("EB_MAIN_KVARH_IMP");
            entity.Property(e => e.EbMainKwhExp)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("EB_MAIN_KWH_EXP");
            entity.Property(e => e.EbMainKwhImp)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("EB_MAIN_KWH_IMP");
            entity.Property(e => e.EbMainLineLoss)
                .HasColumnType("numeric(7, 2)")
                .HasColumnName("EB_MAIN_LINE_LOSS");
            entity.Property(e => e.EbMainNetKvarhExp)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("EB_MAIN_NET_KVARH_EXP");
            entity.Property(e => e.EbMainNetKwhExp)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("EB_MAIN_NET_KWH_EXP");
            entity.Property(e => e.Ega)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("EGA");
            entity.Property(e => e.EgaNum)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("EGA_NUM");
            entity.Property(e => e.ExpKvarh)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("EXP_KVARH");
            entity.Property(e => e.ExpKwh)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("EXP_KWH");
            entity.Property(e => e.ExtGridBreakdownRemarks)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("EXT_GRID_BREAKDOWN_REMARKS");
            entity.Property(e => e.FCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("F_CODE");
            entity.Property(e => e.Fm)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("FM");
            entity.Property(e => e.FnYear)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("FN_YEAR");
            entity.Property(e => e.G1g2Swap)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("G1G2_SWAP");
            entity.Property(e => e.GenHrBigG)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("GEN_HR_BIG_G");
            entity.Property(e => e.GenHrSmallG)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("GEN_HR_SMALL_G");
            entity.Property(e => e.GenerationNo)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("GENERATION_NO");
            entity.Property(e => e.Gf)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("GF");
            entity.Property(e => e.GfI)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("GF_I");
            entity.Property(e => e.GfIi)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("GF_II");
            entity.Property(e => e.GfPss)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("GF_PSS");
            entity.Property(e => e.Gffm)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("GFFM");
            entity.Property(e => e.Gfgf)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("GFGF");
            entity.Property(e => e.Gfs)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("GFS");
            entity.Property(e => e.Gfu)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("GFU");
            entity.Property(e => e.GridAvailabilty)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("GRID_AVAILABILTY");
            entity.Property(e => e.GridBreakdownRemarks)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("GRID_BREAKDOWN_REMARKS");
            entity.Property(e => e.GridDownHrExtBreak11)
                .HasColumnType("numeric(7, 2)")
                .HasColumnName("GRID_DOWN_HR_EXT_BREAK_11");
            entity.Property(e => e.GridDownHrExtBreak33)
                .HasColumnType("numeric(7, 2)")
                .HasColumnName("GRID_DOWN_HR_EXT_BREAK_33");
            entity.Property(e => e.GridDownHrExtBreak66)
                .HasColumnType("numeric(7, 2)")
                .HasColumnName("GRID_DOWN_HR_EXT_BREAK_66");
            entity.Property(e => e.GridDownHrExtPrev11)
                .HasColumnType("numeric(7, 2)")
                .HasColumnName("GRID_DOWN_HR_EXT_PREV_11");
            entity.Property(e => e.GridDownHrExtPrev33)
                .HasColumnType("numeric(7, 2)")
                .HasColumnName("GRID_DOWN_HR_EXT_PREV_33");
            entity.Property(e => e.GridDownHrExtPrev66)
                .HasColumnType("numeric(7, 2)")
                .HasColumnName("GRID_DOWN_HR_EXT_PREV_66");
            entity.Property(e => e.GridDownHrIntBreak11)
                .HasColumnType("numeric(7, 2)")
                .HasColumnName("GRID_DOWN_HR_INT_BREAK_11");
            entity.Property(e => e.GridDownHrIntBreak33)
                .HasColumnType("numeric(7, 2)")
                .HasColumnName("GRID_DOWN_HR_INT_BREAK_33");
            entity.Property(e => e.GridDownHrIntBreak66)
                .HasColumnType("numeric(7, 2)")
                .HasColumnName("GRID_DOWN_HR_INT_BREAK_66");
            entity.Property(e => e.GridDownHrIntPrev11)
                .HasColumnType("numeric(7, 2)")
                .HasColumnName("GRID_DOWN_HR_INT_PREV_11");
            entity.Property(e => e.GridDownHrIntPrev33)
                .HasColumnType("numeric(7, 2)")
                .HasColumnName("GRID_DOWN_HR_INT_PREV_33");
            entity.Property(e => e.GridDownHrIntPrev66)
                .HasColumnType("numeric(7, 2)")
                .HasColumnName("GRID_DOWN_HR_INT_PREV_66");
            entity.Property(e => e.GroupCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("GROUP_CODE");
            entity.Property(e => e.Hw)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("HW");
            entity.Property(e => e.Iga)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("IGA");
            entity.Property(e => e.IgaNum)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("IGA_NUM");
            entity.Property(e => e.Il)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("IL");
            entity.Property(e => e.ImpKvarh)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("IMP_KVARH");
            entity.Property(e => e.ImpKwh)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("IMP_KWH");
            entity.Property(e => e.InadequateWindSpeed)
                .HasColumnType("numeric(7, 2)")
                .HasColumnName("INADEQUATE_WIND_SPEED");
            entity.Property(e => e.InstCapacity)
                .HasColumnType("numeric(10, 3)")
                .HasColumnName("INST_CAPACITY");
            entity.Property(e => e.IntlFm)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("INTL_FM");
            entity.Property(e => e.IntlGfExtr)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("INTL_GF_EXTR");
            entity.Property(e => e.IntlGfIntr)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("INTL_GF_INTR");
            entity.Property(e => e.IntlGfPss)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("INTL_GF_PSS");
            entity.Property(e => e.IntlRna)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("INTL_RNA");
            entity.Property(e => e.IntlS)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("INTL_S");
            entity.Property(e => e.IntlU)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("INTL_U");
            entity.Property(e => e.Ismonprocess)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISMONPROCESS");
            entity.Property(e => e.Isprocess)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("ISPROCESS");
            entity.Property(e => e.Issubmitted).HasColumnName("ISSUBMITTED");
            entity.Property(e => e.KvahExport)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("KVAH_EXPORT");
            entity.Property(e => e.KvahImport)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("KVAH_IMPORT");
            entity.Property(e => e.LoadShedding)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("LOAD_SHEDDING");
            entity.Property(e => e.Location)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LOCATION");
            entity.Property(e => e.Ls)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("LS");
            entity.Property(e => e.Lw)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("LW");
            entity.Property(e => e.MachineAvailabilty)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("MACHINE_AVAILABILTY");
            entity.Property(e => e.MainSiteCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("MAIN_SITE_CODE");
            entity.Property(e => e.MaxZ5)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("MAX_Z5");
            entity.Property(e => e.Maxz5Ma)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("MAXZ5_MA");
            entity.Property(e => e.Md)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("MD");
            entity.Property(e => e.MdEhv)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("MD_EHV");
            entity.Property(e => e.MdHv)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("MD_HV");
            entity.Property(e => e.MeteringPoint)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("METERING_POINT");
            entity.Property(e => e.Monthly)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("MONTHLY");
            entity.Property(e => e.MonthlyPlf)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("MONTHLY_PLF");
            entity.Property(e => e.No)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("NO");
            entity.Property(e => e.Nor)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("NOR");
            entity.Property(e => e.Od)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("OD");
            entity.Property(e => e.OdEhv)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("OD_EHV");
            entity.Property(e => e.OldMaxZ5)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("OLD_MAX_Z5");
            entity.Property(e => e.Phase)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PHASE");
            entity.Property(e => e.PreventiveBreakDownHours)
                .HasColumnType("numeric(7, 2)")
                .HasColumnName("PREVENTIVE_BREAK_DOWN_HOURS");
            entity.Property(e => e.PssGa)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("PSS_GA");
            entity.Property(e => e.PssGaNum)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("PSS_GA_NUM");
            entity.Property(e => e.Ra1)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("RA1");
            entity.Property(e => e.Ra1Den)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("RA1_DEN");
            entity.Property(e => e.Ra1Num)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("RA1_NUM");
            entity.Property(e => e.Ra2)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("RA2");
            entity.Property(e => e.Ra2Den)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("RA2_DEN");
            entity.Property(e => e.Ra2Num)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("RA2_NUM");
            entity.Property(e => e.Rna)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("RNA");
            entity.Property(e => e.S).HasColumnType("numeric(6, 2)");
            entity.Property(e => e.SapDataFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SAP_DATA_FLAG");
            entity.Property(e => e.SapExpDate)
                .HasPrecision(0)
                .HasColumnName("SAP_EXP_DATE");
            entity.Property(e => e.SapExpFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SAP_EXP_FLAG");
            entity.Property(e => e.SapFuncLocCode)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("SAP_FUNC_LOC_CODE");
            entity.Property(e => e.ScheduleMaintenance)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("SCHEDULE_MAINTENANCE");
            entity.Property(e => e.SectionNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SECTION_NO");
            entity.Property(e => e.SiteCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("SITE_CODE");
            entity.Property(e => e.Sma)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("SMA");
            entity.Property(e => e.SmaDen)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("SMA_DEN");
            entity.Property(e => e.SmaNum)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("SMA_NUM");
            entity.Property(e => e.Srno)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("SRNO");
            entity.Property(e => e.Submittedby)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("SUBMITTEDBY");
            entity.Property(e => e.Submittedon)
                .HasPrecision(0)
                .HasColumnName("SUBMITTEDON");
            entity.Property(e => e.T).HasColumnType("numeric(6, 2)");
            entity.Property(e => e.Tgrid)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("TGRID");
            entity.Property(e => e.Tmo)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("TMO");
            entity.Property(e => e.TotalDaily)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("TOTAL_DAILY");
            entity.Property(e => e.TotalGenHr)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("TOTAL_GEN_HR");
            entity.Property(e => e.Trun)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("TRUN");
            entity.Property(e => e.Tstop)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("TSTOP");
            entity.Property(e => e.Ttotal)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("TTOTAL");
            entity.Property(e => e.U).HasColumnType("numeric(6, 2)");
            entity.Property(e => e.Uad)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("UAD");
            entity.Property(e => e.Yearly)
                .HasColumnType("numeric(15, 2)")
                .HasColumnName("YEARLY");
            entity.Property(e => e.YearlyPlf)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("YEARLY_PLF");
            entity.Property(e => e.Yyyymm)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("YYYYMM");
            entity.Property(e => e.Z1).HasColumnType("numeric(6, 2)");
            entity.Property(e => e.Z4).HasColumnType("numeric(6, 2)");
            entity.Property(e => e.Z5).HasColumnType("numeric(6, 2)");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
