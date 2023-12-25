using LogbookManagementService.Models;
using LogbookManagementService.Repositories;
using System.Security.Claims;
using LogbookManagementService.ViewModels;
using LogbookManagementService.CommonFiles;
using Microsoft.EntityFrameworkCore;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace LogbookManagementService.Services
{
    public class LogbookService : ILogbookService
    {
        private readonly ILogbookRepository _logbookRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly FleetManagerContext _fleetManagerContext;
        private IWebHostEnvironment environment;
        public LogbookService(ILogbookRepository logbookRepository, IHttpContextAccessor httpContextAccessor, FleetManagerContext fleetManagerContext, IWebHostEnvironment env)
        {
            _logbookRepository = logbookRepository;
            _httpContextAccessor = httpContextAccessor;
            _fleetManagerContext = fleetManagerContext;
            this.environment = env;
        }

        public  ResponseModel WhyReason()
        {
            return _logbookRepository.WhyReason();
        }
        public ResponseModel GetAllLogConfig(string? Code)
        {
            return _logbookRepository.GetAllLogConfig(Code);
        }
        public ResponseModel KpiTypeCapacityPLFMA(string? siteName)
        {
            return _logbookRepository.KpiTypeCapacityPLFMA(siteName);
        }
        public ResponseModel EmployeeDetails(string? EmployeeCode)
        {
            return _logbookRepository.EmployeeDetails(EmployeeCode);
        }

        public ResponseModel Top10Error(string SiteName)
        {
            return _logbookRepository.Top10Error(SiteName);
        }
        public ResponseModel WTGTop10Error(string TurbineNo)
        {
            return _logbookRepository.WTGTop10Error(TurbineNo);
        }
        public dynamic GeneratePDF(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            return _logbookRepository.GeneratePDF(logDate, fksiteId, siteName, shiftCycle);
        }
        public ResponseModel KpiPlanning(string? siteName)
        {
            return _logbookRepository.KpiPlanning(siteName);
        }
        public ResponseModelLogbook GetWTGLogbook(string? siteName, DateTime? LogDate, string? shiftCycle)
        {
            return _logbookRepository.GetWTGLogbook(siteName, LogDate,shiftCycle);
        }
        public ResponseModelLogbook ManualWTGLogbook(string siteName)
        {
            return _logbookRepository.ManualWTGLogbook(siteName);
        }
        public ResponseModelLogbook ScheduleLogbook(string? siteName, DateTime? LogDate)
        {
            return _logbookRepository.ScheduleLogbook(siteName, LogDate);
        }
        public ResponseModelLogbook GetScadaLogbook(string? siteName, DateTime? LogDate)
        {
            return _logbookRepository.GetScadaLogbook(siteName, LogDate);
        }
        public ResponseModel GetDetailsEmployee(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            return _logbookRepository.GetDetailsEmployee(logDate, fksiteId, siteName, shiftCycle);
        }
        public ResponseModel GetDetailsGridBreakdown(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            return _logbookRepository.GetDetailsGridBreakdown(logDate, fksiteId, siteName, shiftCycle);
        }
        public ResponseModel GetDetailsHoto(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            return _logbookRepository.GetDetailsHoto(logDate, fksiteId, siteName, shiftCycle);
        }
        public ResponseModel GetDetailsScada(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            return _logbookRepository.GetDetailsScada(logDate, fksiteId, siteName, shiftCycle);
        }
        public ResponseModel GetDetailsScheduleMaintenance(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            return _logbookRepository.GetDetailsScheduleMaintenance(logDate, fksiteId, siteName, shiftCycle);
        }
        public ResponseModel GetDetailsWtgBreakdown(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            return _logbookRepository.GetDetailsWtgBreakdown(logDate, fksiteId, siteName, shiftCycle);
        }

        public ResponseModel GetCommonMaster(string? masterCategory)
        {
            return _logbookRepository.GetCommonMaster(masterCategory);
        }

        public ResponseModel GetEmployeeMaster(string? employeeCode)
        {
            return _logbookRepository.GetEmployeeMaster(employeeCode);
        }
        public ResponseModel GetLogbookRecords(string? siteName, DateTime logFromDate, DateTime logToDate)
        {
            return _logbookRepository.GetLogbookRecords(siteName, logFromDate, logToDate);
        }

        public ResponseModel GetKpiWindPowerGeneration(string site)
        {
            return _logbookRepository.GetKpiWindPowerGeneration(site);
        }

        public ResponseModel GetKpiMA_GA(string SiteName)
        {
            return _logbookRepository.GetKpiMA_GA(SiteName);
        }
        public ResponseModel GetWTGKpiMA_GA(string TurbineNo)
        {
            return _logbookRepository.GetWTGKpiMA_GA(TurbineNo);
        }
        public ResponseModel GetKpiWindspeed()
        {
            return _logbookRepository.GetKpiWindspeed();
        }
        public ResponseModel GetKpiTotalGeneration()
        {
            return _logbookRepository.GetKpiTotalGeneration();
        }
        public ResponseModel GetKpiCurrentActivePower()
        {
            return _logbookRepository.GetKpiCurrentActivePower();
        }
        public ResponseModel GetKpiReactivePower()
        {
            return _logbookRepository.GetKpiReactivePower();
        }

        public ResponseModel GetKpiDetails( string? searchQuery, string? customerFilter, string? userSite, string? searchStatus)
        {
            return _logbookRepository.GetKpiDetails(searchQuery,customerFilter,userSite,searchStatus);
        }
        public ResponseModel WTGCustomerbyPlantId(string? userSite)
        {
            return _logbookRepository.WTGCustomerbyPlantId(userSite);
        }
        public ResponseModel GetSiteKpiDetails(string? siteName, string? filter)
        {
            return _logbookRepository.GetSiteKpiDetails(siteName, filter);
        }

        public ResponseModel GetRemarks(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            return _logbookRepository.GetRemarks(logDate, fksiteId, siteName, shiftCycle);
        }

        public ResponseModel GetKpiDropdown(string? parameter)
        {
            return _logbookRepository.GetKpiDropdown(parameter);
        }

        public ResponseModel GetKpiPM_LS_TCI(string? SiteName)

        {
            return _logbookRepository.GetKpiPM_LS_TCI(SiteName);
        }
        public ResponseModel GetKpiTCI(string? filter)
        {
            return _logbookRepository.GetKpiTCI(filter);
        }
        public ResponseModel GetKpiIDRV(string SiteName)
        {
            return _logbookRepository.GetKpiIDRV(SiteName);
        }
        public ResponseModel GetKpiMTTR_MTBF(string SiteName)
        {
            return _logbookRepository.GetKpiMTTR_MTBF(SiteName);
        }
        public ResponseModel GetWTGKpiMTTR_MTBF(string TurbineNo)
        {
            return _logbookRepository.GetWTGKpiMTTR_MTBF(TurbineNo);
        }
        public ResponseModel GetKpiLS(string? filter)
        {
            return _logbookRepository.GetKpiLS(filter);
        }

        public ResponseModel GetKpiMTBF(string? filter)
        {
            return _logbookRepository.GetKpiMTBF(filter);
        }
        public ResponseModel GetKpiMTTR(string? filter)
        {
            return _logbookRepository.GetKpiMTTR(filter);
        }
        public ResponseModel GetKpiMA(string? userSite)
        {
            return _logbookRepository.GetKpiMA(userSite);
        }
        public ResponseModel GetKpiMTTRMTBF(string? userSite, string? plantRole)
        {
            return _logbookRepository.GetKpiMTTRMTBF(userSite, plantRole);

        }
        public ResponseModel GetKpiBelow95(string? userSite)
        {
            return _logbookRepository.GetKpiBelow95(userSite);
        }
        public ResponseModel GetKpiGA(string? userSite)
        {
            return _logbookRepository.GetKpiGA(userSite);
        }
        public ResponseModel GetPKSiteToDoList()
        {
            return _logbookRepository.GetPKSiteToDoList();
        }
        public ResponseModel GetSPInspection()
        {
            return _logbookRepository.GetSPInspection();
        }
        public ResponseModel GetWorkOrderManagement()
        {
            return _logbookRepository.GetWorkOrderManagement();
        }
        public ResponseModel GetPMPlanning()
        {
            return _logbookRepository.GetPMPlanning();
        }
        public ResponseModel GetLubricationPlanning()
        {
            return _logbookRepository.GetLubricationPlanning();
        }
        public ResponseModel GetTCIPlanning()
        {
            return _logbookRepository.GetTCIPlanning();
        }
        public ResponseModel GetBDPlanning()
        {
            return _logbookRepository.GetBDPlanning();
        }
        public ResponseModel GetOilProcPlanning()
        {
            return _logbookRepository.GetOilProcPlanning();
        }
        public ResponseModel GetScadaInfraPM()
        {
            return _logbookRepository.GetScadaInfraPM();
        }
        public ResponseModel GetTrainingPlanning()
        {
            return _logbookRepository.GetTrainingPlanning();
        }
        public ResponseModel GetResourcePlanning()
        {
            return _logbookRepository.GetResourcePlanning();
        }
        public ResponseModel GetInventoryPlanning()
        {
            return _logbookRepository.GetInventoryPlanning();
        }
        public ResponseModel GetVehiclePlanning()
        {
            return _logbookRepository.GetVehiclePlanning();
        }

        public ResponseModel GetScCountryOmsPbi()
        {
            return _logbookRepository.GetScCountryOmsPbi();
        }
        public ResponseModel GetScStateOmsPbi()
        {
            return _logbookRepository.GetScStateOmsPbi();
        }
        public ResponseModel GetStateByCountryCode(string? countryCode)
        {
            return _logbookRepository.GetStateByCountryCode(countryCode);
        }
        public ResponseModel GetAreaByStateCode(string? stateCode)
        {
            return _logbookRepository.GetAreaByStateCode(stateCode);
        }
        public ResponseModel GetSiteByAreaCode(string? areaCode)
        {
            return _logbookRepository.GetSiteByAreaCode(areaCode);
        }

        public ResponseModel GetWhyAnalysis(DateTime? getDate, int? getWeek)
        {
            return _logbookRepository.GetWhyAnalysis(getDate,getWeek);
        }
        public ResponseModel GetWhyAnalysisDetail(int? id)
        {
            return _logbookRepository.GetWhyAnalysisDetail(id);
        }

        public ResponseModel GetAllMainSite()
        {
            return _logbookRepository.GetAllMainSite();
        }


        public ResponseModel AddUpdateEmployeeDetails(LogbookEmployeeDetail logbookEmployeeDetail,string? userName)
        {

            var query = from logbookStatus in _fleetManagerContext.TLogbookStatuses
                        where logbookStatus.SiteName == logbookEmployeeDetail.SiteName &&
                              logbookStatus.ShiftCycle == logbookEmployeeDetail.ShiftCycle &&
                              logbookStatus.LogDate == logbookEmployeeDetail.LogDate.Value.Date
                        select logbookStatus;

            var existingStatus = query.ToList();

            if (existingStatus.Count == 0)
            {
                var shiftQuery = from shiftCycle in _fleetManagerContext.TLogbookCommonMasters
                                 where shiftCycle.MasterCategory == "ShiftCycle"
                                 select shiftCycle;


                var getAllCycle = shiftQuery.ToList();
                   

                List<TLogbookStatus> logbookStatusList = new List<TLogbookStatus>();
                foreach (var list in getAllCycle)
                {
                    if (list.MasterName == logbookEmployeeDetail.ShiftCycle)
                    {
                        TLogbookStatus logbookStatus = new TLogbookStatus()
                        {
                            SiteName = logbookEmployeeDetail.SiteName,
                            LogDate = logbookEmployeeDetail.LogDate,
                            ShiftCycle = list.MasterName,
                            CreatedBy = userName,
                            CreatedDate = DateTime.Now,
                            ModifiedBy=userName,
                            ModifiedDate=DateTime.Now,
                            Status = "In Progress"

                        };
                        logbookStatusList.Add(logbookStatus);
                    }
                    else
                    {
                        TLogbookStatus logbookStatus = new TLogbookStatus()
                        {
                            SiteName = logbookEmployeeDetail.SiteName,
                            LogDate = logbookEmployeeDetail.LogDate,
                            ShiftCycle = list.MasterName,
                            CreatedBy = userName,
                            CreatedDate = DateTime.Now,
                            Status = "Not Submitted"
                        };
                        logbookStatusList.Add(logbookStatus);
                    }
                }

                    _fleetManagerContext.TLogbookStatuses.AddRange(logbookStatusList);
                
            }
            else
            {
                foreach (var status in existingStatus)
                {
                    status.Status = "In Progress";
                }
            }

            _fleetManagerContext.SaveChanges();

            return _logbookRepository.AddUpdateEmployeeDetails(logbookEmployeeDetail, userName);
        }
        public ResponseModel AddUpdateGridBreakdown(LogbookGridBreakdownDetail logbookGridBreakdownDetail,string? userName)
        {
            var query = from logbookStatus in _fleetManagerContext.TLogbookStatuses
                        where logbookStatus.SiteName == logbookGridBreakdownDetail.SiteName &&
                              logbookStatus.ShiftCycle == logbookGridBreakdownDetail.ShiftCycle &&
                              logbookStatus.LogDate == logbookGridBreakdownDetail.LogDate.Value.Date
                        select logbookStatus;

            var existingStatus = query.ToList();

            if (existingStatus.Count == 0)
            {
                var shiftQuery = from shiftCycle in _fleetManagerContext.TLogbookCommonMasters
                                 where shiftCycle.MasterCategory == "ShiftCycle"
                                 select shiftCycle;


                var getAllCycle = shiftQuery.ToList();


                List<TLogbookStatus> logbookStatusList = new List<TLogbookStatus>();
                foreach (var list in getAllCycle)
                {
                    if (list.MasterName == logbookGridBreakdownDetail.ShiftCycle)
                    {
                        TLogbookStatus logbookStatus = new TLogbookStatus()
                        {
                            SiteName = logbookGridBreakdownDetail.SiteName,
                            LogDate = logbookGridBreakdownDetail.LogDate,
                            ShiftCycle = list.MasterName,
                            CreatedBy = userName,
                            CreatedDate = DateTime.Now,
                            ModifiedBy = userName,
                            ModifiedDate = DateTime.Now,
                            Status = "In Progress"

                        };
                        logbookStatusList.Add(logbookStatus);
                    }
                    else
                    {
                        TLogbookStatus logbookStatus = new TLogbookStatus()
                        {
                            SiteName = logbookGridBreakdownDetail.SiteName,
                            LogDate = logbookGridBreakdownDetail.LogDate,
                            ShiftCycle = list.MasterName,
                            CreatedBy = userName,
                            CreatedDate = DateTime.Now,
                            Status = "Not Submitted"
                        };
                        logbookStatusList.Add(logbookStatus);
                    }
                }

                _fleetManagerContext.TLogbookStatuses.AddRange(logbookStatusList);

            }
            else
            {
                foreach (var status in existingStatus)
                {
                    status.Status = "In Progress";
                }
            }

            _fleetManagerContext.SaveChanges();


            return _logbookRepository.AddUpdateGridBreakdown(logbookGridBreakdownDetail, userName);
        }
        public ResponseModel AddUpdateHOTO(LogbookHoto logbookHoto,string? userName)
        {
            var query = from logbookStatus in _fleetManagerContext.TLogbookStatuses
                        where logbookStatus.SiteName == logbookHoto.SiteName &&
                              logbookStatus.ShiftCycle == logbookHoto.ShiftCycle &&
                              logbookStatus.LogDate == logbookHoto.LogDate.Value.Date
                        select logbookStatus;

            var existingStatus = query.ToList();

            if (existingStatus.Count == 0)
            {
                var shiftQuery = from shiftCycle in _fleetManagerContext.TLogbookCommonMasters
                                 where shiftCycle.MasterCategory == "ShiftCycle"
                                 select shiftCycle;


                var getAllCycle = shiftQuery.ToList();


                List<TLogbookStatus> logbookStatusList = new List<TLogbookStatus>();
                foreach (var list in getAllCycle)
                {
                    if (list.MasterName == logbookHoto.ShiftCycle)
                    {
                        TLogbookStatus logbookStatus = new TLogbookStatus()
                        {
                            SiteName = logbookHoto.SiteName,
                            LogDate = logbookHoto.LogDate,
                            ShiftCycle = list.MasterName,
                            CreatedBy = userName,
                            CreatedDate = DateTime.Now,
                            ModifiedBy = userName,
                            ModifiedDate = DateTime.Now,
                            Status = "In Progress"

                        };
                        logbookStatusList.Add(logbookStatus);
                    }
                    else
                    {
                        TLogbookStatus logbookStatus = new TLogbookStatus()
                        {
                            SiteName = logbookHoto.SiteName,
                            LogDate = logbookHoto.LogDate,
                            ShiftCycle = list.MasterName,
                            CreatedBy = userName,
                            CreatedDate = DateTime.Now,
                            Status = "Not Submitted"
                        };
                        logbookStatusList.Add(logbookStatus);
                    }
                }

                _fleetManagerContext.TLogbookStatuses.AddRange(logbookStatusList);

            }
            else
            {
                foreach (var status in existingStatus)
                {
                    status.Status = "In Progress";
                }
            }

            _fleetManagerContext.SaveChanges();

            return _logbookRepository.AddUpdateHOTO(logbookHoto, userName);
        }
        public ResponseModel AddUpdateScada(LogbookScadaDetail logbookScadaDetail,string? userName)
        {
            var query = from logbookStatus in _fleetManagerContext.TLogbookStatuses
                        where logbookStatus.SiteName == logbookScadaDetail.SiteName &&
                              logbookStatus.ShiftCycle == logbookScadaDetail.ShiftCycle &&
                              logbookStatus.LogDate == logbookScadaDetail.LogDate.Value.Date
                        select logbookStatus;

            var existingStatus = query.ToList();

            if (existingStatus.Count == 0)
            {
                var shiftQuery = from shiftCycle in _fleetManagerContext.TLogbookCommonMasters
                                 where shiftCycle.MasterCategory == "ShiftCycle"
                                 select shiftCycle;


                var getAllCycle = shiftQuery.ToList();


                List<TLogbookStatus> logbookStatusList = new List<TLogbookStatus>();
                foreach (var list in getAllCycle)
                {
                    if (list.MasterName == logbookScadaDetail.ShiftCycle)
                    {
                        TLogbookStatus logbookStatus = new TLogbookStatus()
                        {
                            SiteName = logbookScadaDetail.SiteName,
                            LogDate = logbookScadaDetail.LogDate,
                            ShiftCycle = list.MasterName,
                            CreatedBy = userName,
                            CreatedDate = DateTime.Now,
                            ModifiedBy = userName,
                            ModifiedDate = DateTime.Now,
                            Status = "In Progress"

                        };
                        logbookStatusList.Add(logbookStatus);
                    }
                    else
                    {
                        TLogbookStatus logbookStatus = new TLogbookStatus()
                        {
                            SiteName = logbookScadaDetail.SiteName,
                            LogDate = logbookScadaDetail.LogDate,
                            ShiftCycle = list.MasterName,
                            CreatedBy = userName,
                            CreatedDate = DateTime.Now,
                            Status = "Not Submitted"
                        };
                        logbookStatusList.Add(logbookStatus);
                    }
                }

                _fleetManagerContext.TLogbookStatuses.AddRange(logbookStatusList);

            }
            else
            {
                foreach (var status in existingStatus)
                {
                    status.Status = "In Progress";
                }
            }

            _fleetManagerContext.SaveChanges();

            
            return _logbookRepository.AddUpdateScada(logbookScadaDetail, userName);
        }
        public ResponseModel AddUpdateScheduleMnt(LogbookScheduleMaintenanceActivity logbookScheduleMaintenanceActivity,string? userName)
        {
            var query = from logbookStatus in _fleetManagerContext.TLogbookStatuses
                        where logbookStatus.SiteName == logbookScheduleMaintenanceActivity.SiteName &&
                              logbookStatus.ShiftCycle == logbookScheduleMaintenanceActivity.ShiftCycle &&
                              logbookStatus.LogDate == logbookScheduleMaintenanceActivity.LogDate.Value.Date
                        select logbookStatus;

            var existingStatus = query.ToList();

            if (existingStatus.Count == 0)
            {
                var shiftQuery = from shiftCycle in _fleetManagerContext.TLogbookCommonMasters
                                 where shiftCycle.MasterCategory == "ShiftCycle"
                                 select shiftCycle;


                var getAllCycle = shiftQuery.ToList();


                List<TLogbookStatus> logbookStatusList = new List<TLogbookStatus>();
                foreach (var list in getAllCycle)
                {
                    if (list.MasterName == logbookScheduleMaintenanceActivity.ShiftCycle)
                    {
                        TLogbookStatus logbookStatus = new TLogbookStatus()
                        {
                            SiteName = logbookScheduleMaintenanceActivity.SiteName,
                            LogDate = logbookScheduleMaintenanceActivity.LogDate,
                            ShiftCycle = list.MasterName,
                            CreatedBy = userName,
                            CreatedDate = DateTime.Now,
                            ModifiedBy = userName,
                            ModifiedDate = DateTime.Now,
                            Status = "In Progress"

                        };
                        logbookStatusList.Add(logbookStatus);
                    }
                    else
                    {
                        TLogbookStatus logbookStatus = new TLogbookStatus()
                        {
                            SiteName = logbookScheduleMaintenanceActivity.SiteName,
                            LogDate = logbookScheduleMaintenanceActivity.LogDate,
                            ShiftCycle = list.MasterName,
                            CreatedBy = userName,
                            CreatedDate = DateTime.Now,
                            Status = "Not Submitted"
                        };
                        logbookStatusList.Add(logbookStatus);
                    }
                }

                _fleetManagerContext.TLogbookStatuses.AddRange(logbookStatusList);

            }
            else
            {
                foreach (var status in existingStatus)
                {
                    status.Status = "In Progress";
                }
            }

            _fleetManagerContext.SaveChanges();

            
            return _logbookRepository.AddUpdateScheduleMnt(logbookScheduleMaintenanceActivity, userName);
        }
        public ResponseModel AddUpdateWTGBreakdown(LogbookWtgBreakdownDetail logbookWtgBreakdownDetail,string? userName)
        {
            var query = from logbookStatus in _fleetManagerContext.TLogbookStatuses
                        where logbookStatus.SiteName == logbookWtgBreakdownDetail.SiteName &&
                              logbookStatus.ShiftCycle == logbookWtgBreakdownDetail.ShiftCycle &&
                              logbookStatus.LogDate == logbookWtgBreakdownDetail.LogDate.Value.Date
                        select logbookStatus;

            var existingStatus = query.ToList();

            if (existingStatus.Count == 0)
            {
                var shiftQuery = from shiftCycle in _fleetManagerContext.TLogbookCommonMasters
                                 where shiftCycle.MasterCategory == "ShiftCycle"
                                 select shiftCycle;


                var getAllCycle = shiftQuery.ToList();


                List<TLogbookStatus> logbookStatusList = new List<TLogbookStatus>();
                foreach (var list in getAllCycle)
                {
                    if (list.MasterName == logbookWtgBreakdownDetail.ShiftCycle)
                    {
                        TLogbookStatus logbookStatus = new TLogbookStatus()
                        {
                            SiteName = logbookWtgBreakdownDetail.SiteName,
                            LogDate = logbookWtgBreakdownDetail.LogDate,
                            ShiftCycle = list.MasterName,
                            CreatedBy = userName,
                            CreatedDate = DateTime.Now,
                            ModifiedBy = userName,
                            ModifiedDate = DateTime.Now,
                            Status = "Non Complaince"

                        };
                        logbookStatusList.Add(logbookStatus);
                    }
                    else
                    {
                        TLogbookStatus logbookStatus = new TLogbookStatus()
                        {
                            SiteName = logbookWtgBreakdownDetail.SiteName,
                            LogDate = logbookWtgBreakdownDetail.LogDate,
                            ShiftCycle = list.MasterName,
                            CreatedBy = userName,
                            CreatedDate = DateTime.Now,
                            Status = "Not Submitted"
                        };
                        logbookStatusList.Add(logbookStatus);
                    }
                }

                _fleetManagerContext.TLogbookStatuses.AddRange(logbookStatusList);

            }
            else
            {
                foreach (var status in existingStatus)
                {
                    status.Status = "In Progress";
                }
            }

            _fleetManagerContext.SaveChanges();

           
            return _logbookRepository.AddUpdateWTGBreakdown(logbookWtgBreakdownDetail, userName);
        }
       
        public ResponseModel UpdateWTGBreakdownList(List<LogbookWtgBreakdownDetail> updateWTGLogbookList, string? userName)
        {
            return _logbookRepository.UpdateWTGBreakdownList(updateWTGLogbookList,userName);
        }
        public ResponseModel AddorUpdateRemarks(LogbookRemark logbookRemark,string? userName)
        {
            var query = from logbookStatus in _fleetManagerContext.TLogbookStatuses
                        where logbookStatus.SiteName == logbookRemark.SiteName &&
                              logbookStatus.ShiftCycle == logbookRemark.ShiftCycle &&
                              logbookStatus.LogDate == logbookRemark.LogDate.Value.Date
                        select logbookStatus;

            var existingStatus = query.ToList();

            if (existingStatus.Count == 0)
            {
                var shiftQuery = from shiftCycle in _fleetManagerContext.TLogbookCommonMasters
                                 where shiftCycle.MasterCategory == "ShiftCycle"
                                 select shiftCycle;


                var getAllCycle = shiftQuery.ToList();


                List<TLogbookStatus> logbookStatusList = new List<TLogbookStatus>();
                foreach (var list in getAllCycle)
                {
                    if (list.MasterName == logbookRemark.ShiftCycle)
                    {
                        TLogbookStatus logbookStatus = new TLogbookStatus()
                        {
                            SiteName = logbookRemark.SiteName,
                            LogDate = logbookRemark.LogDate,
                            ShiftCycle = list.MasterName,
                            CreatedBy = userName,
                            CreatedDate = DateTime.Now,
                            ModifiedBy = userName,
                            ModifiedDate = DateTime.Now,
                            Status = "In Progress"

                        };
                        logbookStatusList.Add(logbookStatus);
                    }
                    else
                    {
                        TLogbookStatus logbookStatus = new TLogbookStatus()
                        {
                            SiteName = logbookRemark.SiteName,
                            LogDate = logbookRemark.LogDate,
                            ShiftCycle = list.MasterName,
                            CreatedBy = userName,
                            CreatedDate = DateTime.Now,
                            Status = "Not Submitted"
                        };
                        logbookStatusList.Add(logbookStatus);
                    }
                }

                _fleetManagerContext.TLogbookStatuses.AddRange(logbookStatusList);

            }
            else
            {
                foreach (var status in existingStatus)
                {
                    status.Status = "In Progress";
                }
            }

            _fleetManagerContext.SaveChanges();

            return _logbookRepository.AddorUpdateRemarks(logbookRemark, userName);
        }
        public ResponseModel GetLogbookSubmitStatus(string? ShiftCycle, string? SiteName, DateTime LogDate)
        {
            return _logbookRepository.GetLogbookSubmitStatus(ShiftCycle, SiteName, LogDate);
        }
        public ResponseModel LogbookSubmitButton(string? ShiftCycle, string? SiteName, DateTime LogDate,string? status,string? userName, bool GridBreakdownNoActivity, bool ScadaNoActivity)
        {
            return _logbookRepository.LogbookSubmitButton(ShiftCycle, SiteName, LogDate,status,userName, GridBreakdownNoActivity, ScadaNoActivity);
        }
        public ResponseModel AddorUpdateWhyAnalysis(PostWhyAnalysis postWhyAnalysis, string? userName)
        {
            //var userName = _httpContextAccessor?.HttpContext?.User.FindFirstValue("UserName");
            return _logbookRepository.AddorUpdateWhyAnalysis(postWhyAnalysis, userName);
        }
        //public ResponseModel AddorUpdateWhyAnalysisDetails(TWhyAnalysisDetail whyAnalysisDetail)
        //{
        //    var userName = _httpContextAccessor?.HttpContext?.User.FindFirstValue("UserName");
        //    return _logbookRepository.AddorUpdateWhyAnalysisDetails(whyAnalysisDetail, userName);
        //}
        public ResponseModel DeleteEmployee(int id)
        {
            return _logbookRepository.DeleteEmployee(id);
        }
        public ResponseModel DeleteGridBreakdown(int id)
        {
            return _logbookRepository.DeleteGridBreakdown(id);
        }
        public ResponseModel DeleteWtgBreakdown(int id)
        {
            return _logbookRepository.DeleteWtgBreakdown(id);
        }
        public ResponseModel DeleteScheduleMaintenance(int id)
        {
            return _logbookRepository.DeleteScheduleMaintenance(id);
        }
        public ResponseModel DeleteScada(int id)
        {
            return _logbookRepository.DeleteScada(id);
        }
        public ResponseModel DeleteHoto(int id)
        {
            return _logbookRepository.DeleteHoto(id);
        }
        public ResponseModel DeleteRemarks(int id)
        {
            return _logbookRepository.DeleteRemarks(id);
        }

        public ResponseModel UpdateWhyReasonMaster(string? otherName)
        {
            return _logbookRepository.UpdateWhyReasonMaster(otherName);
        }
    }
}
