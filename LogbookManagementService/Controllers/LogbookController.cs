using LogbookManagementService.ViewModels;
using LogbookManagementService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogbookManagementService.Models;
using ClosedXML.Excel;
using System.Reflection;
using LogbookManagementService.Repositories;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Net;
using DocumentFormat.OpenXml.Spreadsheet;
using LogbookManagementService.CommonFiles;
using System.Globalization;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using DocumentFormat.OpenXml.InkML;
using OfficeOpenXml;

namespace LogbookManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogbookController : ControllerBase
    {
        private readonly ILogbookService _logbookService;
        private readonly ILogger<LogbookController> _logger;
        private readonly FleetManagerContext _fleetManagerContext;
        private readonly ILogbookRepository _logbookRepository;
        private readonly OMSContext _omsContext;
        public LogbookController(ILogbookService logbookService, ILogger<LogbookController> logger,
            FleetManagerContext fleetManagerContext, ILogbookRepository logbookRepository
            , OMSContext omsContext)
        {
            _logbookService = logbookService;
            _logger = logger;
            _fleetManagerContext = fleetManagerContext;
            _logbookRepository = logbookRepository;
            _omsContext = omsContext;
        }

        [HttpGet("testoms")]
        public IEnumerable<string?> GetList()
        {
            var targetDate = new DateTime(2023, 9, 3);
            var getList = _omsContext.MstPmSchedules
                .Where(d => d.CreatedOn.Value.Date == targetDate)
                .Select(d => d.FunctionalLocation)
                .ToList();
            return getList;
        }

        [HttpGet("GenerateLogBookExcel")]
        public IActionResult GenerateLogBookExcel(string? StateName, string? AreaName, string? SiteName, DateTime? FromDate, DateTime? ToDate)
        {
            _logger.LogInformation("LogBookExcelReport Method Started");
            try
            {

                using (var workbook = new XLWorkbook())
                {
                    var infoWorksheet = workbook.Worksheets.Add("Summary");

                    infoWorksheet.Row(1).InsertRowsAbove(1);

                    infoWorksheet.Range("A1:B1").Merge();

                    infoWorksheet.Cell(1, 1).Value = "LogBook Summary Report";
                    infoWorksheet.Cell(1, 1).Style.Font.Bold = true;
                    infoWorksheet.Cell(1, 1).Style.Font.FontSize = 16;
                    infoWorksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    infoWorksheet.Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    var employeeData = GetEmployeeData(SiteName, FromDate, ToDate);
                    var breakdownData = GetBreakdownData(SiteName, FromDate, ToDate);
                    var scheduleMaintenanceData = GetScheduleMaintenanceData(SiteName, FromDate, ToDate);
                    var gridBreakdownData = GetGridBreakdownData(SiteName, FromDate, ToDate);
                    var scadaData = GetScadaData(SiteName, FromDate, ToDate);
                    var hotoData = GetHotoData(SiteName, FromDate, ToDate);
                    var remarkData = GetRemarkData(SiteName, FromDate, ToDate);

                    int breakdownDataCount = breakdownData != null ? breakdownData.Count() : 0;
                    int scheduleMaintenanceDataCount = scheduleMaintenanceData != null ? scheduleMaintenanceData.Count() : 0;
                    int gridBreakdownDataCount = gridBreakdownData != null ? gridBreakdownData.Count() : 0;
                    int scadaDataCount = scadaData != null ? scadaData.Count() : 0;


                    infoWorksheet.Cell(3, 1).Value = "State Name";
                    infoWorksheet.Cell(3, 2).Value = StateName;
                    infoWorksheet.Cell(4, 1).Value = "Area Name";
                    infoWorksheet.Cell(4, 2).Value = AreaName;
                    infoWorksheet.Cell(5, 1).Value = "Site Name";
                    infoWorksheet.Cell(5, 2).Value = SiteName;
                    infoWorksheet.Cell(6, 1).Value = "From Date";
                    infoWorksheet.Cell(6, 2).Value = FromDate?.ToString("dd-MMM-yyyy");
                    infoWorksheet.Cell(7, 1).Value = "To Date";
                    infoWorksheet.Cell(7, 2).Value = ToDate?.ToString("dd-MMM-yyyy");

                    infoWorksheet.Cell(3, 1).Style.Font.Bold = true;
                    infoWorksheet.Cell(4, 1).Style.Font.Bold = true;
                    infoWorksheet.Cell(5, 1).Style.Font.Bold = true;
                    infoWorksheet.Cell(6, 1).Style.Font.Bold = true;
                    infoWorksheet.Cell(7, 1).Style.Font.Bold = true;

                    infoWorksheet.Range("D1:E1").Merge();

                    infoWorksheet.Cell(1, 4).Value = "Count Summary";
                    infoWorksheet.Cell(1, 4).Style.Font.Bold = true;
                    infoWorksheet.Cell(1, 4).Style.Font.FontSize = 16;
                    infoWorksheet.Cell(1, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    infoWorksheet.Cell(1, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    infoWorksheet.Cell(3, 4).Value = "Total WTG BreakDown";
                    infoWorksheet.Cell(4, 4).Value = "Total Schedule Maintenance Activity";
                    infoWorksheet.Cell(5, 4).Value = "Total Grid BreakDown";
                    infoWorksheet.Cell(6, 4).Value = "Total Scada Connectivity";

                    infoWorksheet.Cell(3, 5).Value = breakdownDataCount;
                    infoWorksheet.Cell(4, 5).Value = scheduleMaintenanceDataCount;
                    infoWorksheet.Cell(5, 5).Value = gridBreakdownDataCount;
                    infoWorksheet.Cell(6, 5).Value = scadaDataCount;

                    infoWorksheet.Cell(3, 4).Style.Font.Bold = true;
                    infoWorksheet.Cell(4, 4).Style.Font.Bold = true;
                    infoWorksheet.Cell(5, 4).Style.Font.Bold = true;
                    infoWorksheet.Cell(6, 4).Style.Font.Bold = true;

                    infoWorksheet.Cell(9, 1).Value = "Shift-Wise LogBook Status";
                    infoWorksheet.Cell(9, 1).Style.Font.Bold = true;
                    infoWorksheet.Cell(9, 1).Style.Font.FontSize = 16;
                    infoWorksheet.Cell(9, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    infoWorksheet.Cell(9, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                    var startRow = 10;
                    var startColumn = 1;

                    infoWorksheet.Cell(startRow, startColumn).Value = "LogDate";
                    infoWorksheet.Cell(startRow, startColumn + 1).Value = "ShiftCycle";
                    infoWorksheet.Cell(startRow, startColumn + 2).Value = "Status";

                    for (int i = 0; i < 3; i++)
                    {
                        infoWorksheet.Cell(startRow, startColumn + i).Style.Font.Bold = true;
                        infoWorksheet.Cell(startRow, startColumn + i).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    }

                    var rowIndex = startRow + 1;
                    var getStatusDataList = GetStatusData(SiteName, FromDate, ToDate);
                    if (getStatusDataList != null)
                    {
                        foreach (var statusItem in getStatusDataList)
                        {
                            infoWorksheet.Cell(rowIndex, startColumn).Value = statusItem.LogDate?.ToString("dd-MMM-yyyy");
                            infoWorksheet.Cell(rowIndex, startColumn + 1).Value = statusItem.ShiftCycle;
                            infoWorksheet.Cell(rowIndex, startColumn + 2).Value = statusItem.Status;

                            rowIndex++;
                        }
                    }
                    infoWorksheet.Columns().AdjustToContents();


                    ExportDataToWorksheet(workbook, "Employee Details", employeeData);

                    ExportDataToWorksheet(workbook, "WTG Breakdown Details", breakdownData);

                    ExportDataToWorksheet(workbook, "Schedule Maintenance Activity", scheduleMaintenanceData);

                    ExportDataToWorksheet(workbook, "Grid Breakdown Details", gridBreakdownData);

                    ExportDataToWorksheet(workbook, "Scada Connectivity Details", scadaData);

                    ExportDataToWorksheet(workbook, "HOTO", hotoData);

                    ExportDataToWorksheet(workbook, "Remark", remarkData);

                    using (var memoryStream = new System.IO.MemoryStream())
                    {
                        workbook.SaveAs(memoryStream);
                        memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                        var excelBytes = memoryStream.ToArray();
                        var base64String = Convert.ToBase64String(excelBytes);

                        ResponseModel responseModel = new ResponseModel();
                        responseModel.code = 200;
                        responseModel.data = base64String;
                        responseModel.message = "LogBook Summary Downloaded Successfully";
                        responseModel.status = true;
                        return Ok(responseModel);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while generating Excel report: {ex.Message}");
                return StatusCode(500, "An error occurred while generating Excel report.");
            }
        }

        private List<TLogbookStatus> GetStatusData(string? SiteName, DateTime? FromDate, DateTime? ToDate)
        {
            var getStatusData = _fleetManagerContext.TLogbookStatuses
                .Where(data => data.SiteName == SiteName && data.LogDate >= FromDate && data.LogDate <= ToDate).ToList();
            if (getStatusData.Count == 0)
            {
                return null;
            }
            else
            {
                return getStatusData;
            }
        }
        private List<TLogbookEmployeeDetail> GetEmployeeData(string? SiteName, DateTime? FromDate, DateTime? ToDate)
        {
            var getEmployeeDetails = _fleetManagerContext.TLogbookEmployeeDetails
                .Where(data => data.SiteName == SiteName && data.LogDate >= FromDate && data.LogDate <= ToDate).ToList();
            if (getEmployeeDetails.Count == 0)
            {
                return null;
            }
            else
            {
                return getEmployeeDetails;
            }
        }

        private List<TLogbookWtgBreakdownDetail> GetBreakdownData(string? SiteName, DateTime? FromDate, DateTime? ToDate)
        {
            var getWtgBreakdownDetails = _fleetManagerContext.TLogbookWtgBreakdownDetails
                .Where(data => data.SiteName == SiteName && data.LogDate >= FromDate && data.LogDate <= ToDate).ToList();
            if (getWtgBreakdownDetails.Count == 0)
            {
                return null;
            }
            else
            {
                return getWtgBreakdownDetails;
            }
        }

        private List<TLogbookScheduleMaintenanceActivity> GetScheduleMaintenanceData(string? SiteName, DateTime? FromDate, DateTime? ToDate)
        {
            var getSchedulMaintenanceDetails = _fleetManagerContext.TLogbookScheduleMaintenanceActivities
                .Where(data => data.SiteName == SiteName && data.LogDate >= FromDate && data.LogDate <= ToDate).ToList();
            if (getSchedulMaintenanceDetails.Count == 0)
            {
                return null;
            }
            else
            {
                return getSchedulMaintenanceDetails;
            }
        }

        private List<TLogbookGridBreakdownDetail> GetGridBreakdownData(string? SiteName, DateTime? FromDate, DateTime? ToDate)
        {
            var getGridBreakdownDetails = _fleetManagerContext.TLogbookGridBreakdownDetails
                .Where(data => data.SiteName == SiteName && data.LogDate >= FromDate && data.LogDate <= ToDate).ToList();
            if (getGridBreakdownDetails.Count == 0)
            {
                return null;
            }
            else
            {
                return getGridBreakdownDetails;
            }
        }

        private List<TLogbookScadaDetail> GetScadaData(string? SiteName, DateTime? FromDate, DateTime? ToDate)
        {
            var getScadaDetails = _fleetManagerContext.TLogbookScadaDetails
                .Where(data => data.SiteName == SiteName && data.LogDate >= FromDate && data.LogDate <= ToDate).ToList();
            if (getScadaDetails.Count == 0)
            {
                return null;
            }
            else
            {
                return getScadaDetails;
            }
        }

        private List<TLogbookHoto> GetHotoData(string? SiteName, DateTime? FromDate, DateTime? ToDate)
        {
            var getHotoDetails = _fleetManagerContext.TLogbookHotos
                .Where(data => data.SiteName == SiteName && data.LogDate >= FromDate && data.LogDate <= ToDate).ToList();
            if (getHotoDetails.Count == 0)
            {
                return null;
            }
            else
            {
                return getHotoDetails;
            }
        }

        private List<TLogbookRemark> GetRemarkData(string? SiteName, DateTime? FromDate, DateTime? ToDate)
        {
            var getRemarkDetails = _fleetManagerContext.TLogbookRemarks
                .Where(data => data.SiteName == SiteName && data.LogDate >= FromDate && data.LogDate <= ToDate).ToList();
            if (getRemarkDetails.Count == 0)
            {
                return null;
            }
            else
            {
                return getRemarkDetails;
            }
        }

        private List<int> GenerateSerialNumbers(int count)
        {
            var serialNumbers = new List<int>();
            for (int i = 1; i <= count; i++)
            {
                serialNumbers.Add(i);
            }
            return serialNumbers;
        }

        private void ExportDataToWorksheet<T>(XLWorkbook workbook, string worksheetName, List<T> data)
        {
            if (data != null)
            {
                var worksheet = workbook.Worksheets.Add(worksheetName);

                var properties = typeof(T).GetProperties();

                var excludedColumns = new List<string>
        {
            "FkSiteId", "CreatedBy", "CreatedDate",
            "ModifiedBy", "ModifiedDate", "FkSite", "Status", "SiteName", "Id",
            "ShiftCycle", "LogDate", "ShiftHours", "WorkDoneShift"
        };

                worksheet.Column(1).InsertColumnsBefore(1);

                worksheet.Cell(1, 1).Value = "SR. No.";
                worksheet.Cell(1, 1).Style.Font.Bold = true;
                worksheet.Cell(1, 1).Style.Fill.BackgroundColor = XLColor.FromHtml("#00B0F0");
                worksheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(1, 2).Value = "Date";
                worksheet.Cell(1, 2).Style.Font.Bold = true;
                worksheet.Cell(1, 2).Style.Fill.BackgroundColor = XLColor.FromHtml("#00B0F0");
                worksheet.Cell(1, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell(1, 3).Value = "Shift Cycle";
                worksheet.Cell(1, 3).Style.Font.Bold = true;
                worksheet.Cell(1, 3).Style.Fill.BackgroundColor = XLColor.FromHtml("#00B0F0");
                worksheet.Cell(1, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                int columnIndex = 4;
                for (int i = 0; i < properties.Length; i++)
                {
                    var propertyName = properties[i].Name;
                    if (!excludedColumns.Contains(propertyName))
                    {
                        worksheet.Cell(1, columnIndex).Value = propertyName;
                        worksheet.Cell(1, columnIndex).Style.Font.Bold = true;
                        worksheet.Cell(1, columnIndex).Style.Fill.BackgroundColor = XLColor.FromHtml("#00B0F0");
                        worksheet.Cell(1, columnIndex).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        columnIndex++;
                    }
                }

                for (int rowIndex = 0; rowIndex < data.Count; rowIndex++)
                {
                    var item = data[rowIndex];
                    columnIndex = 4;

                    for (int colIndex = 0; colIndex < properties.Length; colIndex++)
                    {
                        var propertyName = properties[colIndex].Name;

                        if (!excludedColumns.Contains(propertyName))
                        {
                            var propertyValue = properties[colIndex].GetValue(item);


                            if (propertyValue is DateTime dateTimeValue)
                            {
                                worksheet.Cell(rowIndex + 2, columnIndex).Value = dateTimeValue.ToString("dd-MM-yyyy hh:mm tt", CultureInfo.InvariantCulture);
                            }
                            else if (propertyName == "TimeFrom" || propertyName == "TimeTo")
                            {
                                var dateTime = propertyValue as DateTime?;

                                if (dateTime.HasValue)
                                {
                                    var timeComponent = dateTime.Value.ToString("HH:mm:ss");
                                    worksheet.Cell(rowIndex + 2, columnIndex).Value = timeComponent;
                                }
                            }
                            else if (propertyName == "PasswordUsageBy" || propertyName == "ShiftTakenOverBy" || propertyName == "ShiftHandedOverBy")
                            {
                                worksheet.Cell(rowIndex + 2, columnIndex).Value = propertyValue?.ToString().Split('-')[1];
                            }
                            else
                            {
                                worksheet.Cell(rowIndex + 2, columnIndex).Value = propertyValue?.ToString();
                            }

                            columnIndex++;
                        }
                    }
                    worksheet.Cell(rowIndex + 2, 2).Value = ((DateTime?)properties.FirstOrDefault(p => p.Name == "LogDate")?.GetValue(item))?.ToString("dd-MMM-yyyy");
                    worksheet.Cell(rowIndex + 2, 3).Value = properties.FirstOrDefault(p => p.Name == "ShiftCycle")?.GetValue(item)?.ToString();

                    worksheet.Cell(rowIndex + 2, 1).Value = rowIndex + 1;
                }
                worksheet.Columns().AdjustToContents();
            }
        }


        [HttpGet("Kpi_Type_Capacity_PLF_MA")]
        public IActionResult KpiTypeCapacityPLFMA(string? siteName)
        {
            _logger.LogInformation("Kpi_Type_Capacity_PLF_MA  Method Started");
            try
            {
                var response = _logbookService.KpiTypeCapacityPLFMA(siteName);
                if (response.data == null)
                {
                    _logger.LogError("No Data exist.");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while executing  Method.");
                return StatusCode(500, "An error occurred while executing  Method.");
            }
        }

        [HttpGet("Top10Error")]
        public IActionResult Top10Error(string SiteName)
        {
            _logger.LogInformation("Top10Error  method started");
            try
            {
                var response = _logbookService.Top10Error(SiteName);
                if (response.data == null)
                {
                    _logger.LogError("no data exist.");
                }
                return Ok(response);

            }
            catch (Exception)
            {
                _logger.LogError("an error occurred while executing top10error method.");
                return StatusCode(500, "an error occurred while executing top10error method.");
            }
        }

        [HttpGet("WhyReasonMaster")]
        public IActionResult WhyReason()
        {
            _logger.LogInformation("WhyReason  Method Started");
            try
            {
                var response = _logbookService.WhyReason();
                if (response.data == null)
                {
                    _logger.LogError("No Data exist.");
                }
                return Ok(response);

            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while executing WHyReason Method.");
                return StatusCode(500, "An error occurred while executing WHyReason Method.");
            }
        }

        [HttpGet("GetLogConfig")]
        public IActionResult GetAllLogConfig(string? Code)
        {
            //_logger.LogInformation("Getting all Log Config List");
            //var response = _fleetManagerContext.LogbookConfigurations
            //    .Where(data => data.Code == Code || data.Code == null).ToList();
            //if (response.Count == 0)
            //{
            //    return null;
            //}
            //else
            //{
            //    return response;
            //}

            _logger.LogInformation("GetAllLogConfig Method Started");
            try
            {
                return Ok(_logbookService.GetAllLogConfig(Code));
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while Calling All Log Config");
                return StatusCode(500, "An error occurred while Calling All Log Config.");
            }


        }
        [HttpGet("EmployeeDetails")]
        public IActionResult EmployeeDetails(string? EmployeeCode)
        {
            _logger.LogInformation("EmployeeDetails Method Started");
            try
            {
                return Ok(_logbookService.EmployeeDetails(EmployeeCode));
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while generating pdf.");
                return StatusCode(500, "An error occurred while generating pdf.");
            }
        }


        [HttpGet("GenerateExcel")]
        public IActionResult WhyAnalysisExcelReport(DateTime? analysisDate,int? week)
        {
            _logger.LogInformation("WhyAnalysisExcelReport Method Started");
            try
            {
                DataSet ds = new DataSet();

                var conn = _fleetManagerContext.Database.GetDbConnection();
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "sp_why_analysis_report";
                    cmd.CommandType = CommandType.StoredProcedure;

              
                    cmd.Parameters.Add(new SqlParameter("@Date", analysisDate));
                    cmd.Parameters.Add(new SqlParameter("@weekno", week));

                    DbProviderFactory factory = DbProviderFactories.GetFactory(conn);
                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds);
                }

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("WhyAnalysisData");

                        var dataTable = ds.Tables[0];
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            var cell = worksheet.Cell(1, i + 1);
                            cell.Value = dataTable.Columns[i].ColumnName;

                            cell.Style.Font.Bold = true;
                            cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#00B0F0");
                            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        }

                        for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
                        {
                            var dataRow = dataTable.Rows[rowIndex];
                            for (int colIndex = 0; colIndex < dataTable.Columns.Count; colIndex++)
                            {
                                worksheet.Cell(rowIndex + 2, colIndex + 1).Value = dataRow[colIndex]?.ToString();
                            }
                        }

                        worksheet.Columns().AdjustToContents();

                        using (var memoryStream = new MemoryStream())
                        {
                            workbook.SaveAs(memoryStream);
                            memoryStream.Seek(0, SeekOrigin.Begin);
                            var excelBytes = memoryStream.ToArray();
                            var base64String = Convert.ToBase64String(excelBytes);

                            ResponseModel responseModel = new ResponseModel();
                            responseModel.code = 200;
                            responseModel.data = base64String;
                            responseModel.message = "Why Analysis Data Downloaded Successfully";
                            responseModel.status = true;
                            return Ok(responseModel);
                        }
                    }
                }
                else
                {
                    ResponseModel responseModel = new ResponseModel();
                    responseModel.code = 204;
                    responseModel.data = null;
                    responseModel.message = "No Data found for selected date.";
                    responseModel.status = false;
                    return Ok(responseModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while generating Excel report: {ex.Message}");
                return StatusCode(500, "An error occurred while generating Excel report.");
            }
        }


        [HttpGet("GeneratePDF")]
        public IActionResult GeneratePDF(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            _logger.LogInformation("GeneratePDF Method Started");
            try
            {
                return Ok(_logbookService.GeneratePDF(logDate, fksiteId, siteName, shiftCycle));
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while generating pdf.");
                return StatusCode(500, "An error occurred while generating pdf.");
            }
        }

        [HttpGet("KpiPlanning")]
        public IActionResult KpiPlanning(string? siteName)
        {
            {
                _logger.LogInformation("KpiPlanning Method Started");
                try
                {
                    var response = _logbookService.KpiPlanning(siteName);
                    if (response.data == null)
                    {
                        _logger.LogError("Details of logbook  not exist.");
                    }
                    return Ok(response);
                }
                catch (Exception)
                {
                    _logger.LogError("An error occurred while retrieving detials of logbook employee.");
                    return StatusCode(500, "An error occurred while retrieving detials of logbook employee.");
                }
            }
        }

        [HttpGet("WTGLogbook")]
        public IActionResult GetWTGLogbook(string? siteName, DateTime? LogDate, string? shiftCycle)
        {
            {
                _logger.LogInformation("GetWTGLogbook Method Started");
                try
                {
                    var response = _logbookService.GetWTGLogbook(siteName, LogDate, shiftCycle);
                    if (response.data == null)
                    {
                        _logger.LogError("Details of logbook  not exist.");
                    }
                    return Ok(response);
                }
                catch (Exception)
                {
                    _logger.LogError("An error occurred while retrieving detials of logbook employee.");
                    return StatusCode(500, "An error occurred while retrieving detials of logbook employee.");
                }
            }
        }

        [HttpGet("ScheduleLogbook")]
        public IActionResult ScheduleLogbook(string? siteName, DateTime? LogDate)
        {
            {
                _logger.LogInformation("GetScadaLogbook Method Started");
                try
                {
                    var response = _logbookService.ScheduleLogbook(siteName, LogDate);
                    if (response.data == null)
                    {
                        _logger.LogError("Details of Scada Logbook  not exist.");
                    }
                    return Ok(response);
                }
                catch (Exception)
                {
                    _logger.LogError("An error occurred while retrieving detials of logbook employee.");
                    return StatusCode(500, "An error occurred while retrieving detials of logbook employee.");
                }
            }
        }

        [HttpGet("ScadaLogbook")]
        public IActionResult GetScadaLogbook(string? siteName, DateTime? LogDate)
        {
            {
                _logger.LogInformation("GetScadaLogbook Method Started");
                try
                {
                    var response = _logbookService.GetScadaLogbook(siteName, LogDate);
                    if (response.data == null)
                    {
                        _logger.LogError("Details of Scada Logbook  not exist.");
                    }
                    return Ok(response);
                }
                catch (Exception)
                {
                    _logger.LogError("An error occurred while retrieving detials of logbook employee.");
                    return StatusCode(500, "An error occurred while retrieving detials of logbook employee.");
                }
            }
        }


        [HttpGet("GetDetailsEmployee")]
        public IActionResult GetDetailsEmployee(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            _logger.LogInformation("GetDetailsEmployee Method Started");
            try
            {
                var response = _logbookService.GetDetailsEmployee(logDate, fksiteId, siteName, shiftCycle);
                if (response.data == null)
                {
                    _logger.LogError("Details of logbook employee not exist.");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving detials of logbook employee.");
                return StatusCode(500, "An error occurred while retrieving detials of logbook employee.");
            }
        }
        [HttpGet("GetDetailsGridBreakdown")]
        public IActionResult GetDetailsGridBreakdown(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            _logger.LogInformation("GetDetailsGridBreakdown Method Started");
            try
            {
                var response = _logbookService.GetDetailsGridBreakdown(logDate, fksiteId, siteName, shiftCycle);
                if (response.data == null)
                {
                    _logger.LogError("Details of GridBreakdown are not there");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving detials of GridBreakdown.");
                return StatusCode(500, "An error occurred while retrieving detials of GridBreakdown.");
            }
        }
        [HttpGet("GetDetailsHoto")]
        public IActionResult GetDetailsHoto(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            _logger.LogInformation("GetDetailsHoto Method Started");
            try
            {
                var response = _logbookService.GetDetailsHoto(logDate, fksiteId, siteName, shiftCycle);
                if (response.data == null)
                {
                    _logger.LogError("Details of logbook Hoto are not there");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving detials of logbook Hoto.");
                return StatusCode(500, "An error occurred while retrieving detials of logbook Hoto.");
            }
        }
        [HttpGet("GetDetailsScada")]
        public IActionResult GetDetailsScada(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            _logger.LogInformation("GetDetailsScada Method Started");
            try
            {
                var response = _logbookService.GetDetailsScada(logDate, fksiteId, siteName, shiftCycle);
                if (response.data == null)
                {
                    _logger.LogError("Details of logbook scada are not there");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving detials of logbook scada.");
                return StatusCode(500, "An error occurred while retrieving detials of logbook scada.");
            }
        }
        [HttpGet("GetDetailsScheduleMaintenance")]
        public IActionResult GetDetailsScheduleMaintenance(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            _logger.LogInformation("GetDetailsScheduleMaintenance Method Started");
            try
            {
                var response = _logbookService.GetDetailsScheduleMaintenance(logDate, fksiteId, siteName, shiftCycle);
                if (response.data == null)
                {
                    _logger.LogError("Details of logbook ScheduleMaintenance are not there");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving detials of logbook ScheduleMaintenance.");
                return StatusCode(500, "An error occurred while retrieving detials of logbook ScheduleMaintenance.");
            }
        }
        [HttpGet("GetDetailsWtgBreakdown")]
        public IActionResult GetDetailsWtgBreakdown(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            _logger.LogInformation("GetDetailsWtgBreakdown Method Started");
            try
            {
                var response = _logbookService.GetDetailsWtgBreakdown(logDate, fksiteId, siteName, shiftCycle);
                if (response.data == null)
                {
                    _logger.LogError("Details of logbook WtgBreakdown are not there");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving detials of logbook WtgBreakdown.");
                return StatusCode(500, "An error occurred while retrieving detials of logbook WtgBreakdown.");
            }
        }

        [HttpGet("GetCommonMaster")]
        public IActionResult GetCommonMaster(string? masterCategory)
        {
            _logger.LogInformation("GetCommonMaster Method Started");
            try
            {
                var response = _logbookService.GetCommonMaster(masterCategory);
                if (response.data == null)
                {
                    _logger.LogError("Details are not there");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving detials of logbook WtgBreakdown.");
                return StatusCode(500, "An error occurred while retrieving detials of logbook WtgBreakdown.");
            }
        }

        [HttpGet("GetEmployeeMaster")]
        public IActionResult GetEmployeeMaster(string? employeeCode)
        {
            _logger.LogInformation("GetEmployeeMaster Method Started");
            try
            {
                var response = _logbookService.GetEmployeeMaster(employeeCode);
                if (response.data == null)
                {
                    _logger.LogError("Details not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving detials of logbook WtgBreakdown.");
                return StatusCode(500, "An error occurred while retrieving detials of logbook WtgBreakdown.");
            }
        }

        [HttpGet("GetLogbookRecords")]
        public IActionResult GetLogbookRecords(string? siteName, DateTime logFromDate, DateTime logToDate)
        {
            try
            {
                _logger.LogInformation("GetLogbookRecords Method Started");
                var response = _logbookService.GetLogbookRecords(siteName, logFromDate, logToDate);

                if (response.data == null)
                {
                    _logger.LogInformation("Details not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }

        [HttpGet("GetKpiWindPowerGeneration")]
        public IActionResult GetKpiWindPowerGeneration(string site)
        {
            try
            {
                _logger.LogInformation("GetKpiWindPowerGeneration Method Started");
                var response = _logbookService.GetKpiWindPowerGeneration(site);

                if (response.data == null)
                {
                    _logger.LogInformation("Details not Found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting KPI Details!");
                return StatusCode(500, "An error occurred while getting KPI Details.");
            }
        }

        [HttpGet("GetKpiMA_GA")]
        public IActionResult GetKpiMA_GA(string SiteName)
        {
            try
            {
                _logger.LogInformation("GetKpiMA_GA Method Started");
                var response = _logbookService.GetKpiMA_GA(SiteName);

                if (response.data == null)
                {
                    _logger.LogInformation("Details not Found");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting KPI Details!");
                return StatusCode(500, "An error occurred while getting KPI Details.");
            }
        }

        [HttpGet("GetKpiWindspeed")]
        public IActionResult GetKpiWindspeed()
        {
            try
            {
                _logger.LogInformation("GetKpiWindspeed Method Started");
                var response = _logbookService.GetKpiWindspeed();

                if (response.data == null)
                {
                    _logger.LogInformation("Details not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }
        [HttpGet("GetKpiTotalGeneration")]
        public IActionResult GetKpiTotalGeneration()
        {
            try
            {
                _logger.LogInformation("GetKpiWindspeed Method Started");
                var response = _logbookService.GetKpiTotalGeneration();

                if (response.data == null)
                {
                    _logger.LogInformation("Details not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }
        [HttpGet("GetKpiCurrentActivePower")]
        public IActionResult GetKpiCurrentActivePower()
        {
            try
            {
                _logger.LogInformation("GetKpiWindspeed Method Started");
                var response = _logbookService.GetKpiCurrentActivePower();

                if (response.data == null)
                {
                    _logger.LogInformation("Details not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }

        [HttpGet("GetKpiReactivePower")]
        public IActionResult GetKpiReactivePower()
        {
            try
            {
                _logger.LogInformation("GetKpiWindspeed Method Started");
                var response = _logbookService.GetKpiReactivePower();

                if (response.data == null)
                {
                    _logger.LogInformation("Details not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }
        [HttpGet("GetMainSite")]
        public IActionResult GetAllMainSite()
        {
            try
            {
                _logger.LogInformation("GetMainSite Method Started");
                var response = _logbookService.GetAllMainSite();

                if (response.data == null)
                {
                    _logger.LogInformation(" Details not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }

        }

        [HttpGet("GetKpiDetails")]

        public IActionResult GetKpiDetails(string? searchQuery, string? customerFilter, string? userSite,string? searchStatus)
        {
            try
            {
                _logger.LogInformation("GetKpiDetails Method Started");

                var response = _logbookService.GetKpiDetails(searchQuery, customerFilter, userSite, searchStatus);

                if (response.data == null)
                {
                    _logger.LogInformation("Details not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting GetKpiDetails!");
                return StatusCode(500, "An error occurred while getting GetKpiDetails.");
            }
        }

        [HttpGet("WTGCustomerbyPlantId")]
        public IActionResult WTGCustomerbyPlantId(string? userSite)
        {
            try
            {
                _logger.LogInformation("WTGCustomerbyPlantId Method Started");

                var response = _logbookService.WTGCustomerbyPlantId(userSite);

                if (response.data == null)
                {
                    _logger.LogInformation("Details not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }

        [HttpGet("GetSiteKpiDetails")]

        public IActionResult GetSiteKpiDetails(string? siteName, string? filter)
        {
            try
            {
                _logger.LogInformation("GetSiteKpiDetails Method Started");
                var response = _logbookService.GetSiteKpiDetails(siteName, filter);

                if (response.data == null)
                {
                    _logger.LogInformation("Details not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }


        [HttpGet("GetKpiDropdown")]
        public IActionResult GetKpiDropdown(string? parameter)
        {
            try
            {
                _logger.LogInformation("GetKpiDropdown Method Started");
                var response = _logbookService.GetKpiDropdown(parameter);

                if (response.data == null)
                {
                    _logger.LogInformation("Details not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }


        [HttpGet("GetRemarks")]
        public IActionResult GetRemarks(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            try
            {
                _logger.LogInformation("GetRemarks Method Started");
                var response = _logbookService.GetRemarks(logDate, fksiteId, siteName, shiftCycle);

                if (response.data == null)
                {
                    _logger.LogInformation("no remarks");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving remarks");
                return StatusCode(500, "An error occurred while retrieving remarks.");
            }
        }


        [HttpGet("GetKpiPM_LS_TCI")]
        public IActionResult GetKpiPM_LS_TCI(string? SiteName)
        {
            try
            {
                _logger.LogInformation("GetKpiPM_LS_TCI Method Started");
                var response = _logbookService.GetKpiPM_LS_TCI(SiteName);

                if (response.data == null)
                {
                    _logger.LogInformation("no KpiPM_LS_TCI");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving KpiPM_LS_TCI");
                return StatusCode(500, "An error occurred while retrieving KpiPM_LS_TCI.");
            }
        }

        [HttpGet("GetKpiTCI")]
        public IActionResult GetKpiTCI(string? filter)
        {
            try
            {
                _logger.LogInformation("GetKpiTCI Method Started");
                var response = _logbookService.GetKpiTCI(filter);

                if (response.data == null)
                {
                    _logger.LogInformation("no KpiTCI");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving KpiTCI");
                return StatusCode(500, "An error occurred while retrieving KpiTCI.");
            }
        }

        [HttpGet("GetKpiIDRV")]
        public IActionResult GetKpiIDRV(string SiteName)
        {
            try
            {
                _logger.LogInformation("GetKpiIDRV Method Started");
                var response = _logbookService.GetKpiIDRV(SiteName);

                if (response.data == null)
                {
                    _logger.LogInformation("no KpiIDRV");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving KpiIDRV");
                return StatusCode(500, "An error occurred while retrieving KpiIDRV.");
            }
        }

        [HttpGet("GetKpiMTTR_MTBF")]
        public IActionResult GetKpiMTTR_MTBF(string SiteName)
        {
            try
            {
                _logger.LogInformation("KpiMTTR_MTBF Method Started");
                var response = _logbookService.GetKpiMTTR_MTBF(SiteName);

                if (response.data == null)
                {
                    _logger.LogInformation("no KpiMTTR_MTBF");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving KpiMTTR_MTBF");
                return StatusCode(500, "An error occurred while retrieving KpiMTTR_MTBF.");
            }
        }

        [HttpGet("GetKpiLS")]
        public IActionResult GetKpiLS(string? filter)
        {
            try
            {
                _logger.LogInformation("GetKpiLS Method Started");
                var response = _logbookService.GetKpiLS(filter);

                if (response.data == null)
                {
                    _logger.LogInformation("no KpiLS");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving KpiLS");
                return StatusCode(500, "An error occurred while retrieving KpiLS.");
            }
        }
        [HttpGet("GetKpiMA")]
        public IActionResult GetKpiMA(string? userSite)
        {
            try
            {
                _logger.LogInformation("GetKpiMA Method Started");
                var response = _logbookService.GetKpiMA(userSite);

                if (response.data == null)
                {
                    _logger.LogInformation("No Data Found.");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving Kpi MA");
                return StatusCode(500, "An error occurred while retrieving Kpi MA.");
            }
        }

        [HttpGet("GetKpiMTTRMTBF")]
        public IActionResult GetKpiMTTRMTBF(string? userSite, string? plantRole)
        {
            try
            {
                _logger.LogInformation("GetKpiMA Method Started");
                var response = _logbookService.GetKpiMTTRMTBF(userSite, plantRole);

                if (response.data == null)
                {
                    _logger.LogInformation("No Data Found.");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving Kpi MA");
                return StatusCode(500, "An error occurred while retrieving Kpi MA.");
            }
        }
        [HttpGet("GetKpiGA")]
        public IActionResult GetKpiGA(string? userSite)
        {
            try
            {
                _logger.LogInformation("GetKpiGA Method Started");
                var response = _logbookService.GetKpiGA(userSite);

                if (response.data == null)
                {
                    _logger.LogInformation("No Data Found.");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving Kpi MA");
                return StatusCode(500, "An error occurred while retrieving Kpi MA.");
            }
        }

        [HttpGet("GetKpiMTBF")]
        public IActionResult GetKpiMTBF(string? filter)
        {
            try
            {
                _logger.LogInformation("GetKpiPM Method Started");
                var response = _logbookService.GetKpiMTBF(filter);

                if (response.data == null)
                {
                    _logger.LogInformation("no Kpi MTTR");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving KpiPM");
                return StatusCode(500, "An error occurred while retrieving KpiPM.");
            }
        }

        [HttpGet("GetKpiMTTR")]
        public IActionResult GetKpiMTTR(string? filter)
        {
            try
            {
                _logger.LogInformation("GetKpiMTTR Method Started");
                var response = _logbookService.GetKpiMTTR(filter);

                if (response.data == null)
                {
                    _logger.LogInformation("no Kpi MTTR");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving KpiPM");
                return StatusCode(500, "An error occurred while retrieving KpiPM.");
            }
        }
        [HttpGet("GetKpiBelow95")]
        public IActionResult GetKpiBelow95(string? userSite)
        {
            try
            {
                _logger.LogInformation("GetKpiBelow95 Method Started");
                var response = _logbookService.GetKpiBelow95(userSite);

                if (response.data == null)
                {
                    _logger.LogInformation("No Data Found.");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while retrieving Kpi MA");
                return StatusCode(500, "An error occurred while retrieving Kpi MA.");
            }
        }

        [HttpGet("GetPKSiteToDoList")]
        public IActionResult GetPKSiteToDoList()
        {
            try
            {
                _logger.LogInformation("GetPKSiteToDoList Method Started");
                var response = _logbookService.GetPKSiteToDoList();

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }

        [HttpGet("GetSpecialProjectsInspection")]
        public IActionResult GetSPInspection()
        {
            try
            {
                _logger.LogInformation("GetSPInspection Method Started");
                var response = _logbookService.GetSPInspection();

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }
        [HttpGet("GetWorkOrderManagement")]
        public IActionResult GetWorkOrderManagement()
        {
            try
            {
                _logger.LogInformation("GetWorkOrderManagement Method Started");
                var response = _logbookService.GetWorkOrderManagement();

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }

        [HttpGet("GetPMPlanning")]
        public IActionResult GetPMPlanning()
        {
            try
            {
                _logger.LogInformation("GetPMPlanning Method Started");
                var response = _logbookService.GetPMPlanning();

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }
        [HttpGet("GetLubricationPlanning")]
        public IActionResult GetLubricationPlanning()
        {
            try
            {
                _logger.LogInformation("GetLubricationPlanning Method Started");
                var response = _logbookService.GetLubricationPlanning();

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }
        [HttpGet("GetTCIPlanning")]
        public IActionResult GetTCIPlanning()
        {
            try
            {
                _logger.LogInformation("GetTCIPlanning Method Started");
                var response = _logbookService.GetTCIPlanning();

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }
        [HttpGet("GetBDPlanning")]
        public IActionResult GetBDPlanning()
        {
            try
            {
                _logger.LogInformation("GetBDPlanning Method Started");
                var response = _logbookService.GetBDPlanning();

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }
        [HttpGet("GetOilProcessPlanning")]
        public IActionResult GetOilProcPlanning()
        {
            try
            {
                _logger.LogInformation("GetOilProcPlanning Method Started");
                var response = _logbookService.GetOilProcPlanning();

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }
        [HttpGet("GetScadaInfraPM")]
        public IActionResult GetScadaInfraPM()
        {
            try
            {
                _logger.LogInformation("GetScadaInfraPM Method Started");
                var response = _logbookService.GetScadaInfraPM();

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }
        [HttpGet("GetTrainingPlanning")]
        public IActionResult GetTrainingPlanning()
        {
            try
            {
                _logger.LogInformation("GetTrainingPlanning Method Started");
                var response = _logbookService.GetTrainingPlanning();

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }
        [HttpGet("GetResourcePlanning")]
        public IActionResult GetResourcePlanning()
        {
            try
            {
                _logger.LogInformation("GetResourcePlanning Method Started");
                var response = _logbookService.GetResourcePlanning();

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }
        [HttpGet("GetInventoryResource")]
        public IActionResult GetInventoryPlanning()
        {
            try
            {
                _logger.LogInformation("GetInventoryPlanning Method Started");
                var response = _logbookService.GetInventoryPlanning();

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }
        [HttpGet("GetVehicleResource")]
        public IActionResult GetVehiclePlanning()
        {
            try
            {
                _logger.LogInformation("GetVehiclePlanning Method Started");
                var response = _logbookService.GetVehiclePlanning();

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }

        [HttpGet("GetScCountryOmsPbi")]
        public IActionResult GetScCountryOmsPbi()
        {
            try
            {
                _logger.LogInformation("GetScCountryOmsPbi Method Started");
                var response = _logbookService.GetScCountryOmsPbi();
                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting Country Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }

        [HttpGet("GetScStateOmsPbi")]
        public IActionResult GetScStateOmsPbi()
        {
            try
            {
                _logger.LogInformation("GetScStateOmsPbi Method Started");
                var response = _logbookService.GetScStateOmsPbi();
                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while getting State Details!");
                return StatusCode(500, "An error occurred while getting State.");
            }
        }

        [HttpGet("GetStateByCountryCode")]
        public IActionResult GetStateByCountryCode(string? countryCode)
        {
            try
            {
                _logger.LogInformation("GetStateByCountryCode Method Started");
                var response = _logbookService.GetStateByCountryCode(countryCode);
                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }

        [HttpGet("GetAreaByStateCode")]
        public IActionResult GetAreaByStateCode(string? stateCode)
        {
            try
            {
                _logger.LogInformation("GetAreaByStateCode Method Started");
                var response = _logbookService.GetAreaByStateCode(stateCode);
                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }

        [HttpGet("GetSiteByAreaCode")]
        public IActionResult GetSiteByAreaCode(string? areaCode)
        {
            try
            {
                _logger.LogInformation("GetSiteByAreaCode Method Started");
                var response = _logbookService.GetSiteByAreaCode(areaCode);
                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }

        [HttpGet("GetWhyAnalysis")]
        public IActionResult GetWhyAnalysis(DateTime? getDate, int? getWeek)
        {
            try
            {
                _logger.LogInformation("GetWhyAnalysis Method Started");
                var response = _logbookService.GetWhyAnalysis(getDate, getWeek);

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }
        [HttpGet("GetWhyAnalysisDetail")]
        public IActionResult GetWhyAnalysisDetail(int? id)
        {
            try
            {
                _logger.LogInformation("GetWhyAnalysisDetail Method Started");
                var response = _logbookService.GetWhyAnalysisDetail(id);

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }
        }




        [HttpPost("AddorUpdateEmployeeDetails")]
        [Authorize]
        public IActionResult AddUpdateEmployeeDetails(LogbookEmployeeDetail logbookEmployeeDetail)
        {
            try
            {
                _logger.LogInformation("AddorUpdateEmployeeDetails Method Started");
                var nameClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                string userName = "";
                if (nameClaim != null)
                {
                    userName = nameClaim.Value;
                }
                var response = _logbookService.AddUpdateEmployeeDetails(logbookEmployeeDetail, userName);

                if (response == null)
                {
                    _logger.LogInformation(" Details not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }

        }

        [HttpPost("AddorUpdateGridBreakdown")]
        [Authorize]
        public IActionResult AddUpdateGridBreakdown(LogbookGridBreakdownDetail logbookGridBreakdown)
        {
            try
            {
                _logger.LogInformation("AddorUpdate Grid Breakdown Details");
                var nameClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                string userName = "";
                if (nameClaim != null)
                {
                    userName = nameClaim.Value;
                }
                var response = _logbookService.AddUpdateGridBreakdown(logbookGridBreakdown, userName);

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }

        }

        [HttpPost("AddorUpdateHOTO")]
        [Authorize]
        public IActionResult AddUpdateHOTO(LogbookHoto logbookHoto)
        {
            try
            {
                _logger.LogInformation("AddorUpdateHOTO Method Started");
                var nameClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                string userName = "";
                if (nameClaim != null)
                {
                    userName = nameClaim.Value;
                }
                var response = _logbookService.AddUpdateHOTO(logbookHoto, userName);

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }

        }

        [HttpPost("AddorUpdateScada")]
        [Authorize]
        public IActionResult AddUpdateScada(LogbookScadaDetail logbookScadaDetail)
        {
            try
            {
                _logger.LogInformation("AddorUpdateScada Method Started");
                var nameClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                string userName = "";
                if (nameClaim != null)
                {
                    userName = nameClaim.Value;
                }
                var response = _logbookService.AddUpdateScada(logbookScadaDetail, userName);

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }

        }

        [HttpPost("AddorUpdateScheduleMnt")]
        [Authorize]
        public IActionResult AddUpdateScheduleMnt(LogbookScheduleMaintenanceActivity logbookScheduleMaintenanceActivity)
        {
            try
            {
                _logger.LogInformation("AddorUpdateScheduleMnt Method Started");
                var nameClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                string userName = "";
                if (nameClaim != null)
                {
                    userName = nameClaim.Value;
                }
                var response = _logbookService.AddUpdateScheduleMnt(logbookScheduleMaintenanceActivity, userName);

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }

        }

        [HttpPost("AddorUpdateWTGBreakdown")]
        [Authorize]
        public IActionResult AddUpdateWTGBreakdown(LogbookWtgBreakdownDetail logbookWtgBreakdownDetail)
        {
            try
            {
                _logger.LogInformation("AddorUpdateWTGBreakdown Method Started");
                var nameClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                string userName = "";
                if (nameClaim != null)
                {
                    userName = nameClaim.Value;
                }
                var response = _logbookService.AddUpdateWTGBreakdown(logbookWtgBreakdownDetail, userName);

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Employee Details!");
                return StatusCode(500, "An error occurred while adding Role.");
            }

        }


        [HttpPost("UpdateWTGBreakdownList")]
        [Authorize]
        public IActionResult UpdateWTGBreakdownList(List<LogbookWtgBreakdownDetail> updateWTGLogbookList)
        {
            try
            {
                _logger.LogInformation("UpdateWTGBreakdownList Method Started");
                var nameClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                string userName = "";
                if (nameClaim != null)
                {
                    userName = nameClaim.Value;
                }
                var response = _logbookService.UpdateWTGBreakdownList(updateWTGLogbookList, userName);

                if (response.data == null)
                {
                    _logger.LogInformation(" Details Not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding WTG Logbook!");
                return StatusCode(500, "An error occurred while adding WTG Logbook.");
            }

        }

        [HttpPost("AddorUpdateRemarks")]
        [Authorize]
        public IActionResult AddorUpdateRemarks(LogbookRemark logbookRemark)
        {
            try
            {
                _logger.LogInformation("AddorUpdateRemarks Method Started");
                var nameClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                string userName = "";
                if (nameClaim != null)
                {
                    userName = nameClaim.Value;
                }
                var response = _logbookService.AddorUpdateRemarks(logbookRemark, userName);

                if (response.data == null)
                {
                    _logger.LogInformation("Details not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Details!");
                return StatusCode(500, "An error occurred while adding.");
            }

        }

        [HttpGet("GetLogbookSubmitStatus")]

        public IActionResult GetLogbookSubmitStatus(string? ShiftCycle, string? SiteName, DateTime LogDate)
        {
            try
            {
                _logger.LogInformation("AddorUpdateRemarks Method Started");
                var response = _logbookService.GetLogbookSubmitStatus(ShiftCycle, SiteName, LogDate);

                if (response.data == null)
                {
                    _logger.LogInformation("Details not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Details!");
                return StatusCode(500, "An error occurred while adding.");
            }
        }

        [HttpPost("LogbookSubmitButton")]
        [Authorize]
        public IActionResult LogbookSubmitButton(string? ShiftCycle, string? SiteName, DateTime LogDate, string? status)
        {
            try
            {
                _logger.LogInformation("LogbookSubmitButton Method Started");
                var nameClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                string userName = "";
                if (nameClaim != null)
                {
                    userName = nameClaim.Value;
                }
                var response = _logbookService.LogbookSubmitButton(ShiftCycle, SiteName, LogDate, status, userName);

                if (response.data == null)
                {
                    _logger.LogInformation("Details not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Details!");
                return StatusCode(500, "An error occurred while adding.");
            }

        }

        [HttpPost("AddorUpdateWhyAnalysis")]
        [Authorize]
        public IActionResult AddorUpdateWhyAnalysis(PostWhyAnalysis postwhyAnalysis)
        {
            try
            {
                _logger.LogInformation("AddorUpdateWhyAnalysis Method Started");
                var nameClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                string userName = "";
                if (nameClaim != null)
                {
                    userName = nameClaim.Value;
                }
                var response = _logbookService.AddorUpdateWhyAnalysis(postwhyAnalysis, userName);

                if (response.data == null)
                {
                    _logger.LogInformation("Details not Added");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while adding Details!");
                return StatusCode(500, "An error occurred while adding.");
            }

        }



        [HttpPost("DeleteEmployee")]
        public IActionResult DeleteEmployee(int id)
        {
            _logger.LogInformation("Delete Employee Method Started");
            try
            {
                var response = _logbookService.DeleteEmployee(id);
                if (response.data == null)
                {
                    _logger.LogError("Details of logbook employee not exist.");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while deleting detials of logbook employee.");
                return StatusCode(500, "An error occurred while deleting detials of logbook employee.");
            }
        }

        [HttpPost("DeleteGridBreakdown")]
        public IActionResult DeleteGridBreakdown(int id)
        {
            _logger.LogInformation("Delete GridBreakdown Method Started");
            try
            {
                var response = _logbookService.DeleteGridBreakdown(id);
                if (response.data == null)
                {
                    _logger.LogError("Details of logbook GridBreakdown not exist.");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while deleting detials of logbook GridBreakdown.");
                return StatusCode(500, "An error occurred while deleting detials of logbook GridBreakdown.");
            }
        }
        [HttpPost("DeleteWtgBreakdown")]
        public IActionResult DeleteWtgBreakdown(int id)
        {
            _logger.LogInformation("Delete WtgBreakdown Method Started");
            try
            {
                var response = _logbookService.DeleteWtgBreakdown(id);
                if (response.data == null)
                {
                    _logger.LogError("Details of logbook WtgBreakdown not exist.");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while deleting detials of logbook WtgBreakdown.");
                return StatusCode(500, "An error occurred while deleting detials of logbook WtgBreakdown.");
            }
        }
        [HttpPost("DeleteScheduleMaintenance")]
        public IActionResult DeleteScheduleMaintenance(int id)
        {
            _logger.LogInformation("Delete ScheduleMaintenance Method Started");
            try
            {
                var response = _logbookService.DeleteScheduleMaintenance(id);
                if (response.data == null)
                {
                    _logger.LogError("Details of logbook ScheduleMaintenance not exist.");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while deleting detials of logbook ScheduleMaintenance.");
                return StatusCode(500, "An error occurred while deleting detials of logbook ScheduleMaintenance.");
            }
        }
        [HttpPost("DeleteScada")]
        public IActionResult DeleteScada(int id)
        {
            _logger.LogInformation("Delete Scada Method Started");
            try
            {
                var response = _logbookService.DeleteScada(id);
                if (response.data == null)
                {
                    _logger.LogError("Details of logbook Scada not exist.");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while deleting detials of logbook Scada.");
                return StatusCode(500, "An error occurred while deleting detials of logbook Scada.");
            }
        }
        [HttpPost("DeleteHoto")]
        public IActionResult DeleteHoto(int id)
        {
            _logger.LogInformation("Delete Hoto Method Started");
            try
            {
                var response = _logbookService.DeleteHoto(id);
                if (response.data == null)
                {
                    _logger.LogError("Details of logbook Hoto not exist.");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while deleting detials of logbook Hoto.");
                return StatusCode(500, "An error occurred while deleting detials of logbook Hoto.");
            }
        }
        [HttpPost("DeleteRemarks")]
        public IActionResult DeleteRemarks(int id)
        {
            _logger.LogInformation("Delete remarks Method Started");
            try
            {
                var response = _logbookService.DeleteRemarks(id);
                if (response.data == null)
                {
                    _logger.LogError("Details of logbook remarks not exist.");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while deleting detials of logbook remarks.");
                return StatusCode(500, "An error occurred while deleting detials of logbook remarks.");
            }
        }

        [HttpPost("ImportExcelMttrMtbf")]
        public IActionResult ImportExcelMttrMtbf(IFormFile file, string? CreatedBy, string? ModifiedBy)
        {
            _logger.LogInformation("ImportExcelMttrMtbf Method Started");
            try
            {
                // Set the EPPlus license context
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                if (file != null && file.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        using (var package = new ExcelPackage(stream))
                        {

                            var sheetNames = package.Workbook.Worksheets.Select(sheet => sheet.Name).ToList();
                            //return Ok(sheetNames[0]);
                            bool isModel = false;
                            //for (int i = 0; i < sheetNames.Count; i++)
                            //{

                                //ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetNames[i]];
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetNames[0]];
                            string sheetHeading = worksheet.Cells[1, 1].Value?.ToString()?.Trim(); // Assuming the header is in the first row
                            //return Ok(sheetHeading);

                            //if (sheetHeading == "Plant")
                            //{
                            //    isModel = true;
                            //}
                            //else
                            //{
                            //    isModel = false;
                            //}

                            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                                {
                                    var kpimttrmtbf = new KpiMttrMtbf
                                    {
                                        PlantRole = worksheet.Cells[row, 1].Value?.ToString()?.Trim(),
                                        Event = worksheet.Cells[row, 2].Value?.ToString()?.Trim(),
                                        EventDescription = worksheet.Cells[row, 3].Value?.ToString()?.Trim(),
                                        SystemComponent = worksheet.Cells[row, 4].Value?.ToString()?.Trim(),
                                        Duration = worksheet.Cells[row, 5].Value?.ToString()?.Trim(),
                                        Instance = worksheet.Cells[row, 6].Value?.ToString()?.Trim(),
                                        LostProdKwh = worksheet.Cells[row, 7].Value?.ToString()?.Trim(),
                                        MttrHours = decimal.TryParse(worksheet.Cells[row, 8].Value?.ToString()?.Trim(), out decimal mttr) ? mttr : (decimal?)null,
                                        MtbfHours = decimal.TryParse(worksheet.Cells[row, 9].Value?.ToString()?.Trim(), out decimal mtbf) ? mtbf : (decimal?)null,
                                        AvailDistPer = decimal.TryParse(worksheet.Cells[row, 10].Value?.ToString()?.Trim(), out decimal availDistPer) ? availDistPer : (decimal?)null,
                                        AvailImpactPer = decimal.TryParse(worksheet.Cells[row, 11].Value?.ToString()?.Trim(), out decimal availImpactPer) ? availImpactPer : (decimal?)null,
                                        IsType = sheetNames[0],
                                        IsModel = isModel,
                                        CreatedBy = CreatedBy,
                                        CreatedDate = DateTime.Now,
                                        ModifiedBy = ModifiedBy,
                                        ModifiedDate = DateTime.Now,
                                    };
                                //if (!_fleetManagerContext.KpiMttrMtbfs.Any(x => x.PlantRole == kpimttrmtbf.PlantRole && x.Event == kpimttrmtbf.Event && x.EventDescription == kpimttrmtbf.EventDescription && x.SystemComponent == kpimttrmtbf.SystemComponent && x.Duration == kpimttrmtbf.Duration && x.Instance == kpimttrmtbf.Instance && x.LostProdKwh == kpimttrmtbf.LostProdKwh && x.MttrHours == kpimttrmtbf.MttrHours && x.MtbfHours == kpimttrmtbf.MtbfHours && x.AvailDistPer == kpimttrmtbf.AvailDistPer && x.AvailImpactPer == kpimttrmtbf.AvailImpactPer))
                                //{
                                    _fleetManagerContext.KpiMttrMtbfs.Add(kpimttrmtbf);
                                //}
                            }

                                _fleetManagerContext.SaveChanges();
                            //}
                        }
                    }

                }
                return Ok("Data inserted successfully");
            }
            catch (Exception)
            {
                _logger.LogError("An error occurred while Import Excel MttrMtbf.");
                return StatusCode(500, "An error occurred while Import Excel MttrMtbf.");
            }
        }

        [HttpPost("UpdateWhyReasonMaster")]
        public IActionResult UpdateWhyReasonMaster(string? otherName)
        {
            var response = _logbookService.UpdateWhyReasonMaster(otherName);
            if (response.data == null)
            {
                _logger.LogError("No Data exist.");
            }
            return Ok(response);
        }

    }
}
