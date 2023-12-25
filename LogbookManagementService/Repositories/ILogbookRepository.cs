using LogbookManagementService.CommonFiles;
using LogbookManagementService.ViewModels;
using LogbookManagementService.Models;
namespace LogbookManagementService.Repositories
{
    public interface ILogbookRepository
    {

        dynamic GeneratePDF(DateTime logDate, int? fksiteId, string siteName, string shiftCycle);
        ResponseModel EmployeeDetails(string? EmployeeCode);

        ResponseModel UpdateWhyReasonMaster(string? otherName);
        ResponseModel WhyReason();
        ResponseModel GetAllLogConfig(string? Code);
        ResponseModel KpiPlanning(string? siteName);
        ResponseModelLogbook GetWTGLogbook(string? siteName, DateTime? LogDate, string? shiftCycle);
        ResponseModelLogbook ScheduleLogbook(string? siteName, DateTime? LogDate);
        ResponseModelLogbook GetScadaLogbook(string? siteName, DateTime? LogDate);
        ResponseModel GetDetailsEmployee(DateTime logDate, int? fksiteId, string siteName, string shiftCycle);
        ResponseModel GetDetailsGridBreakdown(DateTime logDate, int? fksiteId, string siteName, string shiftCycle);
        ResponseModel GetDetailsHoto(DateTime logDate, int? fksiteId, string siteName, string shiftCycle);
        ResponseModel GetDetailsScada(DateTime logDate, int? fksiteId, string siteName, string shiftCycle);
        ResponseModel GetDetailsScheduleMaintenance(DateTime logDate, int? fksiteId, string siteName, string shiftCycle);
        ResponseModel GetDetailsWtgBreakdown(DateTime logDate, int? fksiteId, string siteName, string shiftCycle);
        ResponseModel GetCommonMaster(string? masterCategory);
        ResponseModel GetEmployeeMaster(string? employeeCode);
        ResponseModel GetLogbookRecords(string? siteName, DateTime logFromDate, DateTime logToDate);

        ResponseModel GetKpiWindPowerGeneration(string site);
        ResponseModel GetKpiMA_GA(string SiteName);
        ResponseModel GetKpiWindspeed();
        ResponseModel GetKpiTotalGeneration();
        ResponseModel GetKpiCurrentActivePower();
        ResponseModel GetKpiReactivePower();
        ResponseModel Top10Error(string SiteName);
        ResponseModel KpiTypeCapacityPLFMA(string? siteName);
        ResponseModel GetKpiDetails(string? searchQuery, string? customerFilter, string? userSite, string? searchStatus);
        ResponseModel WTGCustomerbyPlantId(string? userSite);
        ResponseModel GetSiteKpiDetails(string? siteName, string? filter);
        ResponseModel GetRemarks(DateTime logDate, int? fksiteId, string siteName, string shiftCycle);
        ResponseModel GetKpiDropdown(string? parameter);
        ResponseModel GetKpiPM_LS_TCI(string? SiteName);
        ResponseModel GetKpiTCI(string? filter);
        ResponseModel GetKpiIDRV(string SiteName);
        ResponseModel GetKpiMTTR_MTBF(string SiteName);
        ResponseModel GetKpiLS(string? filter);


        ResponseModel GetKpiMTBF(string? filter);
        ResponseModel GetKpiMTTR(string? filter);
        ResponseModel GetKpiBelow95(string? userSite);
        ResponseModel GetKpiMA(string? userSite);
        ResponseModel GetKpiMTTRMTBF(string? userSite, string? plantRole);

        ResponseModel GetKpiGA(string? userSite);
        ResponseModel GetPKSiteToDoList();
        ResponseModel GetSPInspection();
        ResponseModel GetWhyAnalysis(DateTime? getDate,int? getWeek);
        ResponseModel GetWhyAnalysisDetail(int? id);
        ResponseModel GetWorkOrderManagement();
        ResponseModel GetPMPlanning();
        ResponseModel GetLubricationPlanning();
        ResponseModel GetTCIPlanning();
        ResponseModel GetBDPlanning();
        ResponseModel GetOilProcPlanning();
        ResponseModel GetScadaInfraPM();
        ResponseModel GetTrainingPlanning();
        ResponseModel GetResourcePlanning();
        ResponseModel GetInventoryPlanning();
        ResponseModel GetVehiclePlanning();
       
        ResponseModel GetScCountryOmsPbi();
        ResponseModel GetScStateOmsPbi();
        ResponseModel GetStateByCountryCode(string? countryCode);
        ResponseModel GetAreaByStateCode(string? stateCode);
        ResponseModel GetSiteByAreaCode(string? areaCode);

        ResponseModel AddUpdateEmployeeDetails(LogbookEmployeeDetail logbookEmployeeDetail, string username);
        ResponseModel AddUpdateGridBreakdown(LogbookGridBreakdownDetail logbookGridBreakdown, string username);
        ResponseModel AddUpdateScada(LogbookScadaDetail logbookScadaDetail, string username);
        ResponseModel AddUpdateHOTO(LogbookHoto logbookHoto, string username);
        ResponseModel AddUpdateScheduleMnt(LogbookScheduleMaintenanceActivity logbookScheduleMaintenanceActivity, string username);
        ResponseModel AddUpdateWTGBreakdown(LogbookWtgBreakdownDetail logbookWtgBreakdownDetail, string username);

        ResponseModel UpdateWTGBreakdownList(List<LogbookWtgBreakdownDetail> updateWTGLogbookList, string? userName);
        ResponseModel AddorUpdateRemarks(LogbookRemark logbookRemark, string username);
        ResponseModel GetLogbookSubmitStatus(string? ShiftCycle, string? SiteName, DateTime LogDate);
        ResponseModel LogbookSubmitButton(string? ShiftCycle, string? SiteName, DateTime LogDate,string? status, string? userName);
        ResponseModel AddorUpdateWhyAnalysis(PostWhyAnalysis postWhyAnalysis, string? userName);
        // ResponseModel AddorUpdateWhyAnalysisDetails(TWhyAnalysisDetail whyAnalysisDetail,string username);
        ResponseModel GetAllMainSite();
        ResponseModel DeleteEmployee(int id);
        ResponseModel DeleteGridBreakdown(int id);
        ResponseModel DeleteWtgBreakdown(int id);
        ResponseModel DeleteScheduleMaintenance(int id);
        ResponseModel DeleteScada(int id);
        ResponseModel DeleteHoto(int id);
        ResponseModel DeleteRemarks(int id);
    }
}
