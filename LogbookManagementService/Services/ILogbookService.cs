using LogbookManagementService.CommonFiles;
using LogbookManagementService.ViewModels;
using LogbookManagementService.Models;
using Microsoft.AspNetCore.Mvc;

namespace LogbookManagementService.Services
{
    public interface ILogbookService
    {

        dynamic GeneratePDF(DateTime logDate, int? fksiteId, string siteName, string shiftCycle);
        ResponseModelLogbook GetWTGLogbook(string? siteName, DateTime? LogDate,string? shiftCycle);
        ResponseModelLogbook ManualWTGLogbook(string siteName);
        ResponseModelLogbook ScheduleLogbook(string? siteName, DateTime? LogDate);
        
        ResponseModelLogbook GetScadaLogbook(string? siteName, DateTime? LogDate);
        ResponseModel GetAllLogConfig(string? Code);
        ResponseModel EmployeeDetails(string? EmployeeCode);
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
        ResponseModel GetWTGKpiMA_GA(string TurbineNo);
        ResponseModel GetKpiWindspeed();
        ResponseModel GetKpiTotalGeneration();
        ResponseModel GetKpiCurrentActivePower();
        ResponseModel GetKpiReactivePower();

        ResponseModel Top10Error(string SiteName);
        ResponseModel WTGTop10Error(string TurbineNo);
        ResponseModel KpiTypeCapacityPLFMA(string? siteName);
        ResponseModel GetKpiDetails(string? searchQuery, string? customerFilter,string? userSite,string? searchStatus);
        ResponseModel WTGCustomerbyPlantId(string? userSite);
        ResponseModel GetSiteKpiDetails(string? siteName, string? searchQuery);

        ResponseModel GetRemarks(DateTime logDate, int? fksiteId, string siteName, string shiftCycle);
        ResponseModel GetKpiDropdown(string? parameter);
        ResponseModel KpiPlanning(string? siteName);
        ResponseModel UpdateWhyReasonMaster(string? otherName);
        ResponseModel WhyReason();
        ResponseModel GetKpiPM_LS_TCI(string? SiteName);
        ResponseModel GetKpiTCI(string? filter);
        ResponseModel GetKpiIDRV(string SiteName);
        ResponseModel GetKpiMTTR_MTBF(string SiteName);
        ResponseModel GetWTGKpiMTTR_MTBF(string TurbineNo);
        ResponseModel GetKpiLS(string? filter);
        ResponseModel GetKpiMTBF(string? filter);
        ResponseModel GetKpiMTTR(string? filter);
        ResponseModel GetKpiBelow95(string? userSite);
        ResponseModel GetKpiMA(string? userSite); 
        ResponseModel GetKpiMTTRMTBF(string? userSite, string? plantRole);


        ResponseModel GetKpiGA(string? userSite);
        ResponseModel GetPKSiteToDoList();
        ResponseModel GetSPInspection();
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

        ResponseModel GetWhyAnalysis(DateTime? getDate,int? getWeek);
        ResponseModel GetWhyAnalysisDetail(int? id);
        ResponseModel GetAllMainSite();
        ResponseModel AddUpdateEmployeeDetails(LogbookEmployeeDetail logbookEmployeeDetail,string? userName);
        ResponseModel AddUpdateGridBreakdown(LogbookGridBreakdownDetail logbookGridBreakdownDetail,string? userName);
        ResponseModel AddUpdateScada(LogbookScadaDetail logbookScadaDetail,string? userName);
        ResponseModel AddUpdateScheduleMnt(LogbookScheduleMaintenanceActivity logbookScheduleMaintenanceActivity, string? userName);
        ResponseModel AddUpdateHOTO(LogbookHoto logbookHoto, string? userName);
        ResponseModel AddUpdateWTGBreakdown(LogbookWtgBreakdownDetail logbookWtgBreakdownDetail, string? userName);

        ResponseModel UpdateWTGBreakdownList(List<LogbookWtgBreakdownDetail> updateWTGLogbookList, string? userName);
        ResponseModel AddorUpdateRemarks(LogbookRemark logbookRemark, string? userName);

        ResponseModel GetLogbookSubmitStatus(string? ShiftCycle, string? SiteName, DateTime LogDate);
        ResponseModel LogbookSubmitButton(string? ShiftCycle, string? SiteName, DateTime LogDate,string? status, string? userName, bool GridBreakdownNoActivity, bool ScadaNoActivity);
        ResponseModel AddorUpdateWhyAnalysis(PostWhyAnalysis postWhyAnalysis, string? userName);
        //ResponseModel AddorUpdateWhyAnalysisDetails(TWhyAnalysisDetail whyAnalysisDetail);
        ResponseModel DeleteEmployee(int id);
        ResponseModel DeleteGridBreakdown(int id);
        ResponseModel DeleteWtgBreakdown(int id);
        ResponseModel DeleteScheduleMaintenance(int id);
        ResponseModel DeleteScada(int id);
        ResponseModel DeleteHoto(int id);
        ResponseModel DeleteRemarks(int id);

    }
}
