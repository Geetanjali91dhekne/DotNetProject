using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using LogbookManagementService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using LogbookManagementService.ViewModels;
using LogbookManagementService.CommonFiles;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Numerics;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;
using System.Globalization;
using System.Web;
using Nancy;
using Org.BouncyCastle.Asn1.Ocsp;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.VariantTypes;
using System.Net;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Math;
using System.Text.Json.Serialization;
using System.Text.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Org.BouncyCastle.Crypto.Prng;
using Microsoft.AspNetCore.Http;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Data;
using Nancy.Json;
using System.Linq;
using Azure;
using Org.BouncyCastle.Asn1.X509;

namespace LogbookManagementService.Repositories
{
    public class LogbookRepository : ILogbookRepository
    {
        private readonly FleetManagerContext _szFleetMgrContext;
        private readonly ResponseModel responseModel;
        private readonly IConfiguration _configuration;
        private readonly ResponseModelLogbook responseModelLogbook;
        private readonly CRMSContext _crmsContext;
        private readonly DatamartMobileContext _datamartMobileContext;
        private readonly OMSContext _omsContext;
        private readonly IDRVContext _idrvContext;
        public LogbookRepository(
            FleetManagerContext szFleetMgrContext,
            IConfiguration configuration,
            CRMSContext crmsContext,
            DatamartMobileContext datamartMobileContext,
            OMSContext omsContext,
            IDRVContext idrvContext
           )
        {
            _szFleetMgrContext = szFleetMgrContext;
            responseModel = new ResponseModel();
            responseModelLogbook = new ResponseModelLogbook();
            _configuration = configuration;
            _crmsContext = crmsContext;
            _datamartMobileContext = datamartMobileContext;
            _omsContext = omsContext;
            _idrvContext = idrvContext;
        }

        public dynamic GeneratePDF(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            //var responseModel = new ResponseModelPDF();
            try
            {
                //Data Collection

                //ResponseModel responseModel = GetDetailsEmployee(logDate, fksiteId, siteName);
                //var employeeDetail = responseModel.data;
                var employeeDetail = GetDetailsEmployeePdf(logDate, fksiteId, siteName, shiftCycle);
                var wtgBreakdownDetail = GetDetailsWtgBreakdownPdf(logDate, fksiteId, siteName, shiftCycle);
                var scheduleMaintenanceDetail = GetDetailsScheduleMaintenancePdf(logDate, fksiteId, siteName, shiftCycle);
                var gridBreakdownDetail = GetDetailsGridBreakdownPdf(logDate, fksiteId, siteName, shiftCycle);
                var scadaDetail = GetDetailsScadaPdf(logDate, fksiteId, siteName, shiftCycle);
                var hotoDetail = GetDetailsHotopdf(logDate, fksiteId, siteName, shiftCycle);
                var remarkDetail = GetRemarksPdf(logDate, fksiteId, siteName, shiftCycle);



                string json = "null";
                string filename = "LogbookEmployee.pdf";

                //string LogoPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"UploadedFiles");
                //string currentDirectory = Environment.CurrentDirectory;
                // string FileName = "UploadsFiles/suzlon_logo.jpg";
                //string relativePath = "images/myfile.jpg";
                // string LogoPath = Path.Combine(currentDirectory, FileName);
                //string LogoPath= Server.MapPath("/Images");

                //string FilePath = Path.Combine(LogoPath, FileName);
                // byte[] imageBytes = File.ReadAllBytes("/home/ubuntu/FleetManager/fleetmanager-bk/LogbookManagementService/UploadsFiles/suzlon_logo.jpg");

                //string imagePath = Path.Combine(Directory.GetCurrentDirectory().ToString(), "UploadsFiles", "suzlon_logo.jpg");
                //byte[] imageBytes = File.ReadAllBytes(imagePath);
                //Image Logoimg;
                //using (MemoryStream memstr = new MemoryStream(imageBytes))
                //{
                //    Logoimg = Image.GetInstance(memstr);
                //}

                //PDF Function
                string filePath = Path.Combine(Directory.GetCurrentDirectory().ToString(), "wwwroot");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string tempFile = System.IO.Path.Combine(filePath, filename);

                if (System.IO.File.Exists(filename))
                {
                    System.IO.File.Delete(filename);
                }

                using (FileStream stream = new FileStream(tempFile, FileMode.Create))
                {
                    Document document = new Document();
                    document.SetPageSize(iTextSharp.text.PageSize.A4);
                    PdfWriter writer = PdfWriter.GetInstance(document, stream);

                    string slipName = "LogbookEmployee";
                    document.AddTitle(slipName);
                    document.Open();

                    BaseColor headerColor1 = WebColors.GetRGBColor("#000000");
                    BaseColor whiteColor = WebColors.GetRGBColor("#ffffff");
                    BaseColor backgroundColor = WebColors.GetRGBColor("#f4f7fe");//new BaseColor(244, 247, 254);
                    //BaseColor backgroundColor = WebColors.GetRGBColor("#D3D3D3");
                    BaseColor myColor = WebColors.GetRGBColor("#000000");
                    BaseColor redColor = WebColors.GetRGBColor("#FF0000");
                    iTextSharp.text.Font fontH11 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 15F, iTextSharp.text.Font.NORMAL | Font.BOLD, headerColor1);
                    iTextSharp.text.Font fontH1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL, headerColor1);
                    iTextSharp.text.Font fontH2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL, headerColor1);
                    iTextSharp.text.Font fontH21 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 9, iTextSharp.text.Font.NORMAL | Font.BOLD, headerColor1);
                    iTextSharp.text.Font fontH22 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL | Font.BOLD, headerColor1);
                    iTextSharp.text.Font fontH2B = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL, headerColor1);
                    iTextSharp.text.Font fontH3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL, headerColor1);
                    iTextSharp.text.Font fontH4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, headerColor1);
                    iTextSharp.text.Font fontU = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL | Font.UNDERLINE | Font.ITALIC, whiteColor);
                    iTextSharp.text.Font fontH_new = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 5, iTextSharp.text.Font.NORMAL, headerColor1);
                    iTextSharp.text.Font fontH31 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 12, iTextSharp.text.Font.NORMAL, headerColor1);
                    iTextSharp.text.Font fontH2Red = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, redColor);

                    //heading table
                    PdfPTable tableHeading = new PdfPTable(2);
                    tableHeading.WidthPercentage = 500f;
                    tableHeading.DefaultCell.Border = Rectangle.NO_BORDER;
                    tableHeading.DefaultCell.BorderWidthBottom = Rectangle.NO_BORDER;
                    float[] width1sHeading = new float[] { 30f, 150f };
                    tableHeading.SetWidths(width1sHeading);

                    PdfPTable tableLogo = new PdfPTable(1);
                    tableLogo.WidthPercentage = 500f;
                    tableLogo.DefaultCell.Border = Rectangle.NO_BORDER;
                    tableLogo.DefaultCell.BorderWidthBottom = Rectangle.NO_BORDER;
                    float[] width1sLogo = new float[] { 30f };
                    tableLogo.SetWidths(width1sLogo);

                    //PdfPCell CellHead1 = new PdfPCell(Logoimg, true);
                    PdfPCell CellHead1 = new PdfPCell(new Phrase("Suzlon", fontH2));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 0;
                    CellHead1.PaddingLeft = 3;
                    CellHead1.PaddingRight = 0;
                    CellHead1.PaddingTop = 5;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.Colspan = 1;
                    CellHead1.BackgroundColor = whiteColor;
                    tableLogo.AddCell(CellHead1);
                    //tableHeading.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(tableLogo);
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 0;
                    CellHead1.PaddingLeft = 3;
                    CellHead1.PaddingRight = 0;
                    CellHead1.PaddingTop = 5;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.Colspan = 1;
                    CellHead1.BackgroundColor = whiteColor;
                    tableHeading.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Suzlon Global Energy Ltd", fontH11));
                    CellHead1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 3;
                    CellHead1.PaddingLeft = 0;
                    CellHead1.PaddingRight = 90;
                    CellHead1.PaddingTop = 0;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.BackgroundColor = whiteColor;
                    tableHeading.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase(Chunk.NEWLINE.ToString(), fontH2));
                    CellHead1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 0;
                    CellHead1.PaddingTop = 0;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.BackgroundColor = whiteColor;
                    tableHeading.AddCell(CellHead1);

                    //CellHead1 = new PdfPCell(new Phrase("One Earth,Hadapsar,Pune 411028, India", fontH2));
                    CellHead1 = new PdfPCell(new Phrase("Satara,Maharashtra,415001, India", fontH2));
                    CellHead1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 5;
                    CellHead1.PaddingLeft = 0;
                    CellHead1.PaddingRight = 90;
                    CellHead1.PaddingTop = 0;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.BackgroundColor = whiteColor;
                    tableHeading.AddCell(CellHead1);

                    //heading main table
                    PdfPTable tableMainHeading = new PdfPTable(5);
                    tableMainHeading.WidthPercentage = 500f;
                    tableMainHeading.DefaultCell.Border = Rectangle.NO_BORDER;
                    tableMainHeading.DefaultCell.BorderWidthBottom = Rectangle.NO_BORDER;
                    float[] width1sMHeading = new float[] { 22f, 30f, 150f, 25f, 30f };
                    tableMainHeading.SetWidths(width1sMHeading);

                    CellHead1 = new PdfPCell(new Phrase("Site Name:", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 5;
                    CellHead1.PaddingLeft = 3;
                    CellHead1.PaddingRight = 0;
                    CellHead1.PaddingTop = 5;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.Colspan = 1;
                    CellHead1.BackgroundColor = whiteColor;
                    tableMainHeading.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase(siteName, fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 5;
                    CellHead1.PaddingLeft = 0;
                    CellHead1.PaddingRight = 0;
                    CellHead1.PaddingTop = 5;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.Colspan = 1;
                    CellHead1.BackgroundColor = whiteColor;
                    tableMainHeading.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Log Book : Shift Handing & Taking Over (Shift HOTO)", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 5;
                    CellHead1.PaddingLeft = 35;
                    CellHead1.PaddingRight = 10;
                    CellHead1.PaddingTop = 5;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.BackgroundColor = whiteColor;
                    tableMainHeading.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Log Date:", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_RIGHT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 5;
                    CellHead1.PaddingLeft = 0;
                    CellHead1.PaddingRight = 0;
                    CellHead1.PaddingTop = 5;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.Colspan = 1;
                    CellHead1.BackgroundColor = whiteColor;
                    tableMainHeading.AddCell(CellHead1);

                    CultureInfo cultureInfo = new CultureInfo("en-US");


                    CellHead1 = new PdfPCell(new Phrase(logDate.ToString("dd/MM/yyyy", cultureInfo), fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 5;
                    CellHead1.PaddingLeft = 3;
                    CellHead1.PaddingRight = 3;
                    CellHead1.PaddingTop = 5;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.Colspan = 1;
                    CellHead1.BackgroundColor = whiteColor;
                    tableMainHeading.AddCell(CellHead1);


                    //Employee present table
                    PdfPTable tableEmployeePresent = new PdfPTable(5);
                    tableEmployeePresent.WidthPercentage = 260f;
                    tableEmployeePresent.DefaultCell.Border = Rectangle.NO_BORDER;
                    tableEmployeePresent.DefaultCell.BorderWidthBottom = Rectangle.NO_BORDER;
                    float[] width1spresent = new float[] { 6.5f, 15f, 60f, 10f, 25f };
                    tableEmployeePresent.SetWidths(width1spresent);

                    PdfPTable tableEmployeeHeader = new PdfPTable(3);
                    tableEmployeeHeader.WidthPercentage = 260f;
                    tableEmployeeHeader.DefaultCell.Border = Rectangle.NO_BORDER;
                    tableEmployeeHeader.DefaultCell.BorderWidthBottom = Rectangle.NO_BORDER;
                    float[] width1spresentH = new float[] { 160f, 20f, 20f };
                    tableEmployeeHeader.SetWidths(width1spresentH);

                    CellHead1 = new PdfPCell(new Phrase("Employee Present/Report.", fontH21));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 2;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.BackgroundColor = whiteColor;
                    tableEmployeeHeader.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Shift Cycle : ", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_RIGHT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 2;
                    CellHead1.PaddingTop = 1;
                    CellHead1.PaddingLeft = 0;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.BackgroundColor = whiteColor;
                    tableEmployeeHeader.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase(shiftCycle, fontH2));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 2;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.BackgroundColor = whiteColor;
                    tableEmployeeHeader.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(tableEmployeeHeader);
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 3;
                    CellHead1.PaddingTop = 0;
                    CellHead1.Colspan = 5;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = whiteColor;
                    tableEmployeePresent.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Sr.No", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableEmployeePresent.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Employee Code", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableEmployeePresent.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Name of Employee", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableEmployeePresent.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Shift Cycle", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableEmployeePresent.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Role", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableEmployeePresent.AddCell(CellHead1);

                    if (employeeDetail != null)
                    {
                        for (int i = 0; i < employeeDetail.Count; i++)
                        {
                            var data = employeeDetail[i];

                            CellHead1 = new PdfPCell(new Phrase((i + 1).ToString(), fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableEmployeePresent.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.EmployeeCode, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableEmployeePresent.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.EmployeeName, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableEmployeePresent.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.ShiftCycle, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableEmployeePresent.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.Role, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableEmployeePresent.AddCell(CellHead1);
                        }
                    }


                    //wtg breakdown table
                    PdfPTable tableWtgBreakdown = new PdfPTable(11);
                    tableWtgBreakdown.WidthPercentage = 260f;
                    tableWtgBreakdown.DefaultCell.Border = Rectangle.NO_BORDER;
                    tableWtgBreakdown.DefaultCell.BorderWidthBottom = Rectangle.NO_BORDER;
                    float[] width1swtg = new float[] { 10f, 14f, 35f, 13f, 13f, 10f, 35f, 15f, 13f, 14f, 11f };
                    tableWtgBreakdown.SetWidths(width1swtg);

                    CellHead1 = new PdfPCell(new Phrase("WTG Breakdown Details", fontH21));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 11;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.BackgroundColor = whiteColor;
                    tableWtgBreakdown.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Sr.No", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableWtgBreakdown.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Turbine Number", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableWtgBreakdown.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Error", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableWtgBreakdown.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("From Time", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableWtgBreakdown.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("To Time", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableWtgBreakdown.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Total Time", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableWtgBreakdown.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Action taken", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableWtgBreakdown.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("EPTW Number", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableWtgBreakdown.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Password Usage", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                     tableWtgBreakdown.AddCell(CellHead1);
                   
                    CellHead1 = new PdfPCell(new Phrase("Password Usage By", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableWtgBreakdown.AddCell(CellHead1);                  

                    CellHead1 = new PdfPCell(new Phrase("Closure", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableWtgBreakdown.AddCell(CellHead1);

                    if (wtgBreakdownDetail != null)
                    {
                        for (int i = 0; i < wtgBreakdownDetail.Count; i++)
                        {
                            var data = wtgBreakdownDetail[i];

                            CellHead1 = new PdfPCell(new Phrase((i + 1).ToString(), fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableWtgBreakdown.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.TurbineNumber, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableWtgBreakdown.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.Error, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableWtgBreakdown.AddCell(CellHead1);

                            if (!string.IsNullOrEmpty(data.TimeFrom) && DateTime.TryParse(data.TimeFrom, cultureInfo, DateTimeStyles.None, out DateTime timeFrom))
                            {
                                CellHead1 = new PdfPCell(new Phrase(timeFrom.ToString("hh:mm tt"), fontH2));
                                CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                                CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                                CellHead1.PaddingBottom = 4;
                                CellHead1.PaddingTop = 1;
                                CellHead1.Colspan = 1;
                                CellHead1.Border = Rectangle.BOX;
                                CellHead1.BackgroundColor = whiteColor;
                            }
                            else
                            {
                                CellHead1 = new PdfPCell(new Phrase(string.Empty, fontH2));
                                CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                                CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                                CellHead1.PaddingBottom = 4;
                                CellHead1.PaddingTop = 1;
                                CellHead1.Colspan = 1;
                                CellHead1.Border = Rectangle.BOX;
                                CellHead1.BackgroundColor = whiteColor;
                            }

                            tableWtgBreakdown.AddCell(CellHead1);


                            if (!string.IsNullOrEmpty(data.TimeTo) && DateTime.TryParse(data.TimeTo, cultureInfo, DateTimeStyles.None, out DateTime timeTo))
                            {
                                CellHead1 = new PdfPCell(new Phrase(timeTo.ToString("hh:mm tt"), fontH2));
                                CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                                CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                                CellHead1.PaddingBottom = 4;
                                CellHead1.PaddingTop = 1;
                                CellHead1.Colspan = 1;
                                CellHead1.Border = Rectangle.BOX;
                                CellHead1.BackgroundColor = whiteColor;
                            }
                            else
                            {
                                CellHead1 = new PdfPCell(new Phrase(string.Empty, fontH2));
                                CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                                CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                                CellHead1.PaddingBottom = 4;
                                CellHead1.PaddingTop = 1;
                                CellHead1.Colspan = 1;
                                CellHead1.Border = Rectangle.BOX;
                                CellHead1.BackgroundColor = whiteColor;
                            }

                            tableWtgBreakdown.AddCell(CellHead1);



                            CellHead1 = new PdfPCell(new Phrase(data.TotalTime, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableWtgBreakdown.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.ActionTaken, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableWtgBreakdown.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.EptwNumber, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableWtgBreakdown.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.PasswordUsage, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                           
                                tableWtgBreakdown.AddCell(CellHead1);
                            
                            CellHead1 = new PdfPCell(new Phrase(data.PasswordUsageBy, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            
                                tableWtgBreakdown.AddCell(CellHead1);
                            
                            CellHead1 = new PdfPCell(new Phrase(data.Closure, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableWtgBreakdown.AddCell(CellHead1);
                        }
                    }

                    //scheduled maintenance table
                    PdfPTable tableScheduledMaintenance = new PdfPTable(7);
                    tableScheduledMaintenance.WidthPercentage = 260f;
                    tableScheduledMaintenance.DefaultCell.Border = Rectangle.NO_BORDER;
                    tableScheduledMaintenance.DefaultCell.BorderWidthBottom = Rectangle.NO_BORDER;
                    float[] width1sScheduled = new float[] { 9f, 15f, 25f, 70f, 15f, 10f, 15f };
                    tableScheduledMaintenance.SetWidths(width1sScheduled);

                    CellHead1 = new PdfPCell(new Phrase("Scheduled Maintenance Carried-Out", fontH21));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 7;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.BackgroundColor = whiteColor;
                    tableScheduledMaintenance.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Sr.No", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableScheduledMaintenance.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Turbine Number", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableScheduledMaintenance.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Activity", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableScheduledMaintenance.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Observation", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableScheduledMaintenance.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("EPTW Number", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableScheduledMaintenance.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Closure", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableScheduledMaintenance.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Reschedule Date", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableScheduledMaintenance.AddCell(CellHead1);

                    if (scheduleMaintenanceDetail != null)
                    {
                        for (int i = 0; i < scheduleMaintenanceDetail.Count; i++)
                        {
                            var data = scheduleMaintenanceDetail[i];

                            CellHead1 = new PdfPCell(new Phrase((i + 1).ToString(), fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableScheduledMaintenance.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.TurbineNumber, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableScheduledMaintenance.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.Activity, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableScheduledMaintenance.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.Observation, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableScheduledMaintenance.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.EptwNumber, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableScheduledMaintenance.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.Closure, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableScheduledMaintenance.AddCell(CellHead1);

                            if (data.RescheduleDate != null)
                                CellHead1 = new PdfPCell(new Phrase(data.RescheduleDate.ToString().Substring(0, 10), fontH2));
                            else
                                CellHead1 = new PdfPCell(new Phrase(" ", fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableScheduledMaintenance.AddCell(CellHead1);

                        }
                    }


                    //table grid breakdown details
                    PdfPTable tableGridBreakdown = new PdfPTable(8);
                    tableGridBreakdown.WidthPercentage = 260f;
                    tableGridBreakdown.DefaultCell.Border = Rectangle.NO_BORDER;
                    tableGridBreakdown.DefaultCell.BorderWidthBottom = Rectangle.NO_BORDER;
                    float[] width1sGrid = new float[] { 12f, 20f, 35f, 21f, 21f, 10f, 75f, 20f };
                    tableGridBreakdown.SetWidths(width1sGrid);

                    CellHead1 = new PdfPCell(new Phrase("Grid breakdown details", fontH21));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 8;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.BackgroundColor = whiteColor;
                    tableGridBreakdown.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Sr No.", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableGridBreakdown.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Feeder Number/Sub Station", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableGridBreakdown.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Grid drop reason", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableGridBreakdown.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("From Time", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableGridBreakdown.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("To Time", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableGridBreakdown.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Total Time", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableGridBreakdown.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Action Taken", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableGridBreakdown.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("EPTW Number", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableGridBreakdown.AddCell(CellHead1);

                    if (gridBreakdownDetail != null)
                    {
                        for (int i = 0; i < gridBreakdownDetail.Count; i++)
                        {
                            var data = gridBreakdownDetail[i];

                            CellHead1 = new PdfPCell(new Phrase((i + 1).ToString(), fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableGridBreakdown.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.FeederName, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableGridBreakdown.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.GridDropReason, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableGridBreakdown.AddCell(CellHead1);

                            if (DateTime.TryParse(data.TimeFrom, cultureInfo, DateTimeStyles.None, out DateTime parsedTimeFrom))
                            {
                                CellHead1 = new PdfPCell(new Phrase(parsedTimeFrom.ToString("hh:mm tt"), fontH2));
                                CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                                CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                                CellHead1.PaddingBottom = 4;
                                CellHead1.PaddingTop = 1;
                                CellHead1.Colspan = 1;
                                CellHead1.Border = Rectangle.BOX;
                                CellHead1.BackgroundColor = whiteColor;
                                tableGridBreakdown.AddCell(CellHead1);
                            }

                            if (DateTime.TryParse(data.TimeTo, cultureInfo, DateTimeStyles.None, out DateTime parsedTimeTo))
                            {
                                CellHead1 = new PdfPCell(new Phrase(parsedTimeTo.ToString("hh:mm tt"), fontH2));
                                CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                                CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                                CellHead1.PaddingBottom = 4;
                                CellHead1.PaddingTop = 1;
                                CellHead1.Colspan = 1;
                                CellHead1.Border = Rectangle.BOX;
                                CellHead1.BackgroundColor = whiteColor;
                                tableGridBreakdown.AddCell(CellHead1);
                            }

                            CellHead1 = new PdfPCell(new Phrase(data.TotalTime, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableGridBreakdown.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.RemarkAction, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableGridBreakdown.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.EptwNumber, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableGridBreakdown.AddCell(CellHead1);

                        }
                    }



                    //table scada detail
                    PdfPTable tableScada = new PdfPTable(3);
                    tableScada.WidthPercentage = 260f;
                    tableScada.DefaultCell.Border = Rectangle.NO_BORDER;
                    tableScada.DefaultCell.BorderWidthBottom = Rectangle.NO_BORDER;
                    float[] width1sScada = new float[] { 7f, 32.5f, 87.5f };
                    tableScada.SetWidths(width1sScada);

                    CellHead1 = new PdfPCell(new Phrase("Scada Connectivity details/Scada BD details", fontH21));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 3;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.BackgroundColor = whiteColor;
                    tableScada.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Sr No.", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableScada.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Issue Description", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableScada.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Action Taken", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableScada.AddCell(CellHead1);

                    if (scadaDetail != null)
                    {
                        for (int i = 0; i < scadaDetail.Count; i++)
                        {
                            var data = scadaDetail[i];

                            CellHead1 = new PdfPCell(new Phrase((i + 1).ToString(), fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableScada.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.IssueDesc, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableScada.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.ActionTaken, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableScada.AddCell(CellHead1);
                        }
                    }



                    //table hoto details -- Footer
                    PdfPTable tableHoto = new PdfPTable(4);
                    tableHoto.WidthPercentage = 260f;
                    tableHoto.DefaultCell.Border = Rectangle.NO_BORDER;
                    tableHoto.DefaultCell.BorderWidthBottom = Rectangle.NO_BORDER;
                    float[] width1sHoto = new float[] { 30f, 30f, 30f, 30f };
                    tableHoto.SetWidths(width1sHoto);
                    tableHoto.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin; // new

                    string shiftHandedOverBy = null;
                    string handedOverDateTime = null;
                    string shiftTakenOverBy = null;
                    string takenOverDateTime = null;
                    if (hotoDetail != null)
                    {
                        shiftHandedOverBy = hotoDetail[0].ShiftHandedOverBy;
                        handedOverDateTime = hotoDetail[0].HandedOverDateTime;
                        shiftTakenOverBy = hotoDetail[0].ShiftTakenOverBy;
                        takenOverDateTime = hotoDetail[0].TakenOverDateTime;
                    }

                    CellHead1 = new PdfPCell(new Phrase("Shift Handed-Over By : ", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.BackgroundColor = whiteColor;
                    tableHoto.AddCell(CellHead1);


                    CellHead1 = new PdfPCell(new Phrase(shiftHandedOverBy, fontH2));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_LEFT;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.PaddingLeft = -45;
                    CellHead1.PaddingRight = 0;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.BackgroundColor = whiteColor;
                    tableHoto.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Shift Taken-Over By : ", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_RIGHT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.BackgroundColor = whiteColor;
                    tableHoto.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase(shiftTakenOverBy, fontH2));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.BackgroundColor = whiteColor;
                    tableHoto.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Date and Time : ", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.BackgroundColor = whiteColor;
                    tableHoto.AddCell(CellHead1);

                    if (DateTime.TryParse(handedOverDateTime, cultureInfo, DateTimeStyles.None, out DateTime parsedTime))
                    {
                        CellHead1 = new PdfPCell(new Phrase(parsedTime.ToString("dd/MM/yyyy hh:mm:ss tt", cultureInfo), fontH2));
                        CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                        CellHead1.VerticalAlignment = Element.ALIGN_LEFT;
                        CellHead1.PaddingBottom = 4;
                        CellHead1.PaddingTop = 1;
                        CellHead1.PaddingLeft = -70;
                        CellHead1.PaddingRight = 0;
                        CellHead1.Colspan = 1;
                        CellHead1.Border = Rectangle.NO_BORDER;
                        CellHead1.BackgroundColor = whiteColor;
                        tableHoto.AddCell(CellHead1);
                    }


                    CellHead1 = new PdfPCell(new Phrase("Date and Time : ", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.PaddingLeft = 53;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.BackgroundColor = whiteColor;
                    tableHoto.AddCell(CellHead1);

                    if (DateTime.TryParse(takenOverDateTime, cultureInfo, DateTimeStyles.None, out DateTime parsedtimehanded))
                    {
                        CellHead1 = new PdfPCell(new Phrase(parsedtimehanded.ToString("dd/MM/yyyy hh:mm:ss tt", cultureInfo), fontH2));
                        CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                        CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        CellHead1.PaddingBottom = 4;
                        CellHead1.PaddingTop = 1;
                        CellHead1.PaddingLeft = -20;
                        CellHead1.Colspan = 1;
                        CellHead1.Border = Rectangle.NO_BORDER;
                        CellHead1.BackgroundColor = whiteColor;
                        tableHoto.AddCell(CellHead1);
                    }

                    // Add the footer table to the document
                    tableHoto.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin, writer.DirectContent);


                    //table REMARK 
                    PdfPTable tableRemark = new PdfPTable(2);
                    tableRemark.WidthPercentage = 260f;
                    tableRemark.DefaultCell.Border = Rectangle.NO_BORDER;
                    tableRemark.DefaultCell.BorderWidthBottom = Rectangle.NO_BORDER;
                    float[] width1sRemark = new float[] { 7f, 120f };
                    tableRemark.SetWidths(width1sRemark);

                    CellHead1 = new PdfPCell(new Phrase("Remark Details", fontH21));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 2;
                    CellHead1.Border = Rectangle.NO_BORDER;
                    CellHead1.BackgroundColor = whiteColor;
                    tableRemark.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Sr No.", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableRemark.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(new Phrase("Remark", fontH22));
                    CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 4;
                    CellHead1.PaddingTop = 1;
                    CellHead1.Colspan = 1;
                    CellHead1.Border = Rectangle.BOX;
                    CellHead1.BackgroundColor = backgroundColor;
                    tableRemark.AddCell(CellHead1);

                    if (remarkDetail != null)
                    {
                        for (int i = 0; i < remarkDetail.Count; i++)
                        {
                            var data = remarkDetail[i];

                            CellHead1 = new PdfPCell(new Phrase((i + 1).ToString(), fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableRemark.AddCell(CellHead1);

                            CellHead1 = new PdfPCell(new Phrase(data.Remarks, fontH2));
                            CellHead1.HorizontalAlignment = Element.ALIGN_LEFT;
                            CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                            CellHead1.PaddingBottom = 4;
                            CellHead1.PaddingTop = 1;
                            CellHead1.Colspan = 1;
                            CellHead1.Border = Rectangle.BOX;
                            CellHead1.BackgroundColor = whiteColor;
                            tableRemark.AddCell(CellHead1);
                        }
                    }


                    //final table to all table
                    #region
                    PdfPTable finalTable = new PdfPTable(9);
                    finalTable.WidthPercentage = 100;
                    finalTable.DefaultCell.BorderColor = BaseColor.WHITE;
                    float[] widths = new float[] { 30f, 30f, 30f, 30f, 30f, 30f, 30f, 30f, 30f };

                    //table1.HeaderRows = 1;
                    finalTable.SetWidths(widths);

                    CellHead1 = new PdfPCell(tableHeading);
                    CellHead1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 0;
                    CellHead1.PaddingTop = 0;
                    CellHead1.PaddingLeft = 0;
                    CellHead1.PaddingRight = 0;
                    CellHead1.Colspan = 12;
                    CellHead1.BackgroundColor = whiteColor;
                    finalTable.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(tableMainHeading);
                    CellHead1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 0;
                    CellHead1.PaddingTop = 0;
                    CellHead1.PaddingLeft = 0;
                    CellHead1.PaddingRight = 0;
                    CellHead1.Colspan = 12;
                    CellHead1.BackgroundColor = whiteColor;
                    finalTable.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(tableEmployeePresent);
                    CellHead1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 0;
                    CellHead1.PaddingTop = 0;
                    CellHead1.PaddingLeft = 0;
                    CellHead1.PaddingRight = 0;
                    CellHead1.Colspan = 12;
                    CellHead1.BackgroundColor = whiteColor;
                    finalTable.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(tableWtgBreakdown);
                    CellHead1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 0;
                    CellHead1.PaddingTop = 0;
                    CellHead1.PaddingLeft = 0;
                    CellHead1.PaddingRight = 0;
                    CellHead1.Colspan = 12;
                    CellHead1.BackgroundColor = whiteColor;
                    finalTable.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(tableScheduledMaintenance);
                    CellHead1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 0;
                    CellHead1.PaddingTop = 0;
                    CellHead1.PaddingLeft = 0;
                    CellHead1.PaddingRight = 0;
                    CellHead1.Colspan = 12;
                    CellHead1.BackgroundColor = whiteColor;
                    finalTable.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(tableGridBreakdown);
                    CellHead1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 0;
                    CellHead1.PaddingTop = 0;
                    CellHead1.PaddingLeft = 0;
                    CellHead1.PaddingRight = 0;
                    CellHead1.Colspan = 12;
                    CellHead1.BackgroundColor = whiteColor;
                    finalTable.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(tableScada);
                    CellHead1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 0;
                    CellHead1.PaddingTop = 0;
                    CellHead1.PaddingLeft = 0;
                    CellHead1.PaddingRight = 0;
                    CellHead1.Colspan = 12;
                    CellHead1.BackgroundColor = whiteColor;
                    finalTable.AddCell(CellHead1);

                    CellHead1 = new PdfPCell(tableRemark);
                    CellHead1.HorizontalAlignment = Element.ALIGN_CENTER;
                    CellHead1.VerticalAlignment = Element.ALIGN_MIDDLE;
                    CellHead1.PaddingBottom = 0;
                    CellHead1.PaddingTop = 0;
                    CellHead1.PaddingLeft = 0;
                    CellHead1.PaddingRight = 0;
                    CellHead1.Colspan = 12;
                    CellHead1.BackgroundColor = whiteColor;
                    finalTable.AddCell(CellHead1);

                    document.Add(finalTable);
                    #endregion
                    document.Close();
                }

                if (File.Exists(tempFile))
                {
                    byte[] StringByte = System.IO.File.ReadAllBytes(tempFile);
                    string base64String = Convert.ToBase64String(StringByte);
                    json = base64String;
                    File.Delete(tempFile);
                }
                return json;
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        
        public List<TLogbookEmployeeDetail>? GetDetailsEmployeePdf(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            try
            {
                var query = _szFleetMgrContext.TLogbookEmployeeDetails
                    .Where(s => s.LogDate.HasValue && s.LogDate.Value.Date == logDate.Date
                   && s.ShiftCycle == shiftCycle
                   && s.SiteName == siteName);

                //if (fksiteId != null)
                //{
                //    query = query.Where(s => s.FkSiteId == fksiteId);
                //}

                var employeeDetail = query.FirstOrDefault();
                var employeeDetailList = query.ToList();

                if (employeeDetailList.Count == 0)
                {
                    //return GetResponseModel(Constants.httpCodeSuccess, null, "No data Available", false);
                    return null;
                }
                //return GetResponseModel(Constants.httpCodeSuccess, employeeDetailList, "All Data Get", true);
                return employeeDetailList;
            }
            catch (Exception ex)
            {
                //return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
                return null;
            }
        }
        public List<TLogbookGridBreakdownDetail>? GetDetailsGridBreakdownPdf(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            try
            {
                var query = _szFleetMgrContext.TLogbookGridBreakdownDetails
                    .Where(s => s.LogDate.HasValue && s.LogDate.Value.Date == logDate.Date
                   && s.ShiftCycle == shiftCycle
                   && s.SiteName == siteName);

                //if (fksiteId != null)
                //{
                //    query = query.Where(s => s.FkSiteId == fksiteId);
                //}

                var gridBreakdownDetail = query.FirstOrDefault();
                var gridBreakdownDetailList = query.ToList();

                if (gridBreakdownDetailList.Count == 0)
                {
                    //return GetResponseModel(Constants.httpCodeSuccess, null, "No data Available", false);
                    return null;
                }
                //return GetResponseModel(Constants.httpCodeSuccess, employeeDetailList, "All Data Get", true);
                return gridBreakdownDetailList;
            }
            catch (Exception ex)
            {
                //return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
                return null;
            }
        }
        public List<TLogbookHoto>? GetDetailsHotopdf(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            try
            {
                var query = _szFleetMgrContext.TLogbookHotos
                    .Where(s => s.LogDate.HasValue && s.LogDate.Value.Date == logDate.Date
                   && s.ShiftCycle == shiftCycle
                   && s.SiteName == siteName);

                //if (fksiteId != null)
                //{
                //    query = query.Where(s => s.FkSiteId == fksiteId);
                //}

                var hotoDetail = query.FirstOrDefault();
                var hotoDetailList = query.ToList();

                if (hotoDetailList.Count == 0)
                {
                    //return GetResponseModel(Constants.httpCodeSuccess, null, "No data Available", false);
                    return null;
                }
                //return GetResponseModel(Constants.httpCodeSuccess, employeeDetailList, "All Data Get", true);
                return hotoDetailList;
            }
            catch (Exception ex)
            {
                //return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
                return null;
            }
        }
        public List<TLogbookScadaDetail>? GetDetailsScadaPdf(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            try
            {
                var query = _szFleetMgrContext.TLogbookScadaDetails
                    .Where(s => s.LogDate.HasValue && s.LogDate.Value.Date == logDate.Date
                   && s.ShiftCycle == shiftCycle
                   && s.SiteName == siteName);

                //if (fksiteId != null)
                //{
                //    query = query.Where(s => s.FkSiteId == fksiteId);
                //}

                var scadaDetail = query.FirstOrDefault();
                var scadaDetailList = query.ToList();

                if (scadaDetailList.Count == 0)
                {
                    //return GetResponseModel(Constants.httpCodeSuccess, null, "No data Available", false);
                    return null;
                }
                //return GetResponseModel(Constants.httpCodeSuccess, employeeDetailList, "All Data Get", true);
                return scadaDetailList;
            }
            catch (Exception ex)
            {
                //return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
                return null;
            }
        }
        public List<TLogbookScheduleMaintenanceActivity>? GetDetailsScheduleMaintenancePdf(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            try
            {
                var query = _szFleetMgrContext.TLogbookScheduleMaintenanceActivities
                    .Where(s => s.LogDate.HasValue && s.LogDate.Value.Date == logDate.Date
                   && s.ShiftCycle == shiftCycle
                   && s.SiteName == siteName);

                //if (fksiteId != null)
                //{
                //    query = query.Where(s => s.FkSiteId == fksiteId);
                //}

                var maintenanceDetail = query.FirstOrDefault();
                var maintenanceDetaillList = query.ToList();

                if (maintenanceDetaillList.Count == 0)
                {
                    //return GetResponseModel(Constants.httpCodeSuccess, null, "No data Available", false);
                    return null;
                }
                //return GetResponseModel(Constants.httpCodeSuccess, employeeDetailList, "All Data Get", true);
                return maintenanceDetaillList;
            }
            catch (Exception ex)
            {
                //return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
                return null;
            }
        }
        public List<TLogbookWtgBreakdownDetail>? GetDetailsWtgBreakdownPdf(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            try
            {
                var query = _szFleetMgrContext.TLogbookWtgBreakdownDetails
                    .Where(s => s.LogDate.HasValue && s.LogDate.Value.Date == logDate.Date
                   && s.ShiftCycle == shiftCycle
                   && s.SiteName == siteName);

                //if (fksiteId != null)
                //{
                //    query = query.Where(s => s.FkSiteId == fksiteId);
                //}

                var wtgBreakdownDetail = query.FirstOrDefault();
                var wtgBreakdownDetailList = query.ToList();

                if (wtgBreakdownDetailList.Count == 0)
                {
                    //return GetResponseModel(Constants.httpCodeSuccess, null, "No data Available", false);
                    return null;
                }
                //return GetResponseModel(Constants.httpCodeSuccess, employeeDetailList, "All Data Get", true);
                return wtgBreakdownDetailList;
            }
            catch (Exception ex)
            {
                //return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
                return null;
            }
        }
        public List<TLogbookRemark>? GetRemarksPdf(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            try
            {
                var query = _szFleetMgrContext.TLogbookRemarks
                    .Where(s => s.LogDate.HasValue && s.LogDate.Value.Date == logDate.Date
                   && s.ShiftCycle == shiftCycle
                   && s.SiteName == siteName);

                var remarkDetail = query.FirstOrDefault();
                var remarkDetailList = query.ToList();


                if (remarkDetailList.Count == 0)
                {
                    return null;
                }
                //return GetResponseModel(Constants.httpCodeSuccess, employeeDetailList, "All Data Get", true);
                return remarkDetailList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ResponseModel GetAllLogConfig(string? Code)
        {
            try
            {
                var response = _szFleetMgrContext.LogbookConfigurations
                             .Where(data => data.Code == Code || data.Code == null).ToList();
                return GetResponseModel(Constants.httpCodeSuccess, response, "Data Retrieve Successfully", true);

            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeSuccess, null, ex.Message, false);
            }
        }

        public ResponseModel KpiPlanning(string? siteName)
        {
            try
            {
                DataSet ds = new DataSet();

                var conn = _szFleetMgrContext.Database.GetDbConnection();
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "sp_KPI_PM_LS_TCI";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@SiteName", siteName));

                    DbProviderFactory factory = DbProviderFactories.GetFactory(conn);
                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds);
                }
                List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    foreach (DataColumn column in ds.Tables[1].Columns)
                    {
                        rowData[column.ColumnName] = row[column];
                    }
                    data.Add(rowData);
                }
                return GetResponseModel(Constants.httpCodeSuccess, data, "Data Retrieved Successfully", true);
            }

            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel EmployeeDetails(string? EmployeeCode)
        {
            try
            {
                var getEmployeeDetail = GetUserDetails(EmployeeCode);
                return GetResponseModel(Constants.httpCodeSuccess, getEmployeeDetail, "Data Retrieve Successfully", true);

            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeSuccess, null, ex.Message, false);
            }
        }
        private ResponseEmployeeDetail GetUserDetails(string? EmployeeCode)
        {

            try
            {
                
                string domainId = Base64Encode(EmployeeCode);
                string password = "a";
                string appType = "windowsAuth";
               //string URI = "http://172.16.11.74:8090/api/SuzlonActiveUser/IsUserActive";
                string URI = "https://uat-mob.suzlon.com/SuzlonActiveUser/api/SuzlonActiveUser/IsUserActive";
                string myParameters = $"domainId={domainId}&password={password}&appType={appType}";

               
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

                // Checking data from api
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string htmlResult = wc.UploadString(URI, myParameters);
                    var userResult = ConvertUserDetailsToJson(htmlResult);

                    if (userResult != null && userResult.IsActive)
                    {
                        ResponseEmployeeDetail response = new ResponseEmployeeDetail();
                        response.Code = userResult.DomainId;
                        response.Name = userResult.Name;
                        return response;
                    }
                    else
                    {

                        return null;
                    }

                }


            }
            catch (Exception)
            {

                return null;

            }

        }

        private static string Base64Encode(string text)
        {
            var textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(textBytes);
        }
        private static UserLogin ConvertUserDetailsToJson(string text)
        {
            return JsonConvert.DeserializeObject<UserLogin>(text)!;
        }

      public ResponseModelLogbook GetWTGLogbook(string? siteName, DateTime? LogDate,string? shiftCycle)
        {
            try
            {
              int  countWTGLogbook = 0;
              
                var ShiftQuery = _szFleetMgrContext.TLogbookCommonMasters
                .SingleOrDefault(item => item.MasterCategory == "ShiftCycle" && item.MasterName == shiftCycle);
               if(ShiftQuery==null)
                {
                    return GetResponseModelLogbook(Constants.httpCodeSuccess, null, "Shift cycle not fetching properly.", false, false,0);
                }
                var shiftTimeFrom = ShiftQuery.CreatedDate?.ToString("HH:mm:ss");
                var shiftTimeTo = ShiftQuery.ModifiedDate?.ToString("HH:mm:ss");
                var logDateAddDays = LogDate;
                if (TimeSpan.Parse(shiftTimeFrom) >= TimeSpan.Parse(shiftTimeTo))
                {
                     logDateAddDays = LogDate.Value.AddDays(1);
                }
                
                ResponseWTGLogbooks allResponse = new ResponseWTGLogbooks();
                List<ResponseWTGLogbook> responseWTGLogbooks = new List<ResponseWTGLogbook>();
                var query = from main in _crmsContext.ScMainSites
                            join crm in _crmsContext.CrmBreakdownDetails on main.MainSiteCode equals crm.MainSiteCode
                           
                            where main.MainSite == siteName &&
                                  crm.BdStartTime >= LogDate.Value.Date.Add(TimeSpan.Parse(shiftTimeFrom)) &&
                                  crm.BdEndTime >= LogDate.Value.Date.Add(TimeSpan.Parse(shiftTimeFrom)) &&
                                  crm.BdEndTime <= logDateAddDays.Value.Date.Add(TimeSpan.Parse(shiftTimeTo))


                            let timeFrom = new TimeOnly(crm.BdStartTime.Value.Hour, crm.BdStartTime.Value.Minute)
                            let timeTo = new TimeOnly(crm.BdEndTime.Value.Hour, crm.BdEndTime.Value.Minute)
                            let totalHoursNullable = crm.TotalDuration
                            let totalHours = totalHoursNullable ?? 0.0
                            let hours = (int)totalHours
                            let remainingMinutes = (totalHours - hours) * 60
                            let minutes = (int)Math.Round(remainingMinutes)
                            let timeAsString = $"{hours:D2}:{minutes:D2}"
                            select new 
                            {
                                Turbine = crm.SapFuncLocCode,
                                TimeFrom = timeFrom,
                                TimeTo = timeTo,
                                TotalTime = timeAsString,
                                RowId=crm.Rowid
                            };
                var data = query.ToList();

                if(data.Count==0)
                {
                    var siteTurbine = _crmsContext.VScAllMachineStaticDetailOmsPbis
                        .Where(d => d.MainSite == siteName).Select(d => d.SapFuncLocCode).ToList();
                    List<ResponseWTGRecords> liveData = new List<ResponseWTGRecords>();

                    foreach (var list in siteTurbine)
                    {
                        
                        var records = (from mainRecord in _datamartMobileContext.VwWindFarms
                                       join turbinerecord in _datamartMobileContext.CatalogTurbines
                                       on mainRecord.PlantUnitId equals turbinerecord.PlantUnitId
                                       join eventrecord in _datamartMobileContext.TurbineOnlines
                                       on turbinerecord.PlantUnitId equals eventrecord.PlantUnitId
                                       where turbinerecord.FunctionalLocation == list
                                       && mainRecord.ProductionStateCode == 3  

                                       select new ResponseWTGRecords
                                       {
                                           Turbine = turbinerecord.FunctionalLocation, 
                                           EventCode = eventrecord.EventCode.ToString(),
                                           Date = mainRecord.ControllerTimestamp
                                       }).ToList();
                        
                        liveData.AddRange(records);
                    }

                    var count = 0;
                    DateOnly getDateCheck = new DateOnly(LogDate.Value.Year, LogDate.Value.Month, LogDate.Value.Day);
                   
                    foreach (var dataList in liveData)
                    {
                        
                        string dateString = dataList.Date.Substring(0,10);

                        DateOnly date = DateOnly.FromDateTime(DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture));

                        if (date == getDateCheck)
                        {
                            count = count + 1;
                            countWTGLogbook = countWTGLogbook + 1;
                        }
                    }


                    if (liveData.Count == 0)
                    {
                        
                        ResponseWTGLogbookList response = new ResponseWTGLogbookList();
                        response.TurbineLists = new List<TurbineList>();
                        response.ErrorLists = new List<ErrorList>();

                        var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();

                        string connectionString = configuration.GetConnectionString("MyConnectionString"); 

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            using (SqlCommand command = new SqlCommand("sp_noConn_to_turbine", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@siteName", siteName);

                                connection.Open();
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        countWTGLogbook = countWTGLogbook + 1;
                                        response.TurbineLists.Add(new TurbineList
                                        {
                                            Turbine = reader["TurbineName"].ToString()
                                            
                                    });
                                    }

                                    
                                    reader.NextResult();

                                    while (reader.Read())
                                    {
                                        response.ErrorLists.Add(new ErrorList
                                        {
                                            Error = reader["ErrorName"].ToString()
                                        });
                                    }
                                }
                            }
                        }

                      return  GetResponseModelLogbook(Constants.httpCodeSuccess, response, "Data Received", true, false, countWTGLogbook);

                    }

                     if (liveData.Count != 0 && count > 0)
                    {
                        List<ResponseWTGRecords> filterLiveData = new List<ResponseWTGRecords>();
                        foreach (var dat in liveData)
                        {
                            string dateString = dat.Date;

                            if (DateTime.TryParse(dateString, out DateTime datDateTime))
                            {
                                var existingData = _szFleetMgrContext.TLogbookWtgBreakdownDetails
                                                    .FirstOrDefault(d => d.TurbineNumber == dat.Turbine &&
                                                                d.LogDate.HasValue &&
                                                                d.LogDate == datDateTime &&
                                                                d.Closure == "Closed");

                                var existingNextShiftData = _szFleetMgrContext.TLogbookWtgBreakdownDetails
                                                    .FirstOrDefault(d => d.TurbineNumber == dat.Turbine &&
                                                                d.LogDate.HasValue &&
                                                                d.ShiftCycle==shiftCycle &&
                                                                d.LogDate == datDateTime &&
                                         d.Closure == "Handover to Next shift");

                                if (existingData==null && existingNextShiftData==null)
                                {
                                    filterLiveData.Add(dat);
                                }
                            }
                            else
                            {
                                
                                Console.WriteLine("Invalid date string: " + dat);
                            }

                            
                        }

                        foreach (var dataList in filterLiveData)
                        {
                            string dateString = dataList.Date.Substring(0, 10);

                            DateOnly date = DateOnly.FromDateTime(DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture));

                            if (date == getDateCheck)
                            {
                                
                                ResponseWTGLogbook responseWTGLogbook = new ResponseWTGLogbook();

                                responseWTGLogbook.Turbine = dataList.Turbine;
                                var getErrorDes = _szFleetMgrContext.ErrorMasters
                                    .FirstOrDefault(d => d.EventCode.ToString() == dataList.EventCode).Name;

                                if (getErrorDes != null)
                                {
                                    responseWTGLogbook.Error = getErrorDes;
                                }
                                responseWTGLogbooks.Add(responseWTGLogbook);
                                allResponse.TurbineLists = responseWTGLogbooks;
                            }
                        }
                    }

                    return GetResponseModelLogbook(Constants.httpCodeSuccess, allResponse, "Data Retrieve Successfully",
                        true, true,countWTGLogbook);
                }
                
                foreach(var listData in data)
                {

                    countWTGLogbook = countWTGLogbook + 1;
                    ResponseWTGLogbook responseWTGLogbook = new ResponseWTGLogbook();
                    responseWTGLogbook.Turbine = listData.Turbine;
                    responseWTGLogbook.TimeFrom = listData.TimeFrom;
                    responseWTGLogbook.TimeTo = listData.TimeTo;
                    responseWTGLogbook.TotalTime = listData.TotalTime;
                    var turbineToPlantId = _datamartMobileContext.CatalogTurbines
                    .FirstOrDefault(d => d.FunctionalLocation == listData.Turbine)?.PlantUnitId;

                    if (turbineToPlantId != null)
                    {
                        var plantIdtoEventCode = _datamartMobileContext.TurbineOnlines
                            .FirstOrDefault(d => d.PlantUnitId == turbineToPlantId)?.EventCode;

                        if (plantIdtoEventCode != null)
                        {
                            var eventCodetoErrorDes = _szFleetMgrContext.ErrorMasters
                                .FirstOrDefault(d => d.EventCode == plantIdtoEventCode)?.Name;

                            if (eventCodetoErrorDes != null)
                            {
                                responseWTGLogbook.Error = eventCodetoErrorDes;

                            }
                            else
                            {
                                responseWTGLogbook.Error = "";
                            }
                        }
                        else
                        {
                            responseWTGLogbook.Error = "";
                        }
                    }
                    else
                    {
                        responseWTGLogbook.Error = "";
                    }
                    responseWTGLogbooks.Add(responseWTGLogbook);

                }

                if (responseWTGLogbooks.Count!=0)
                {
                    allResponse.TurbineLists = responseWTGLogbooks;
                    return GetResponseModelLogbook(Constants.httpCodeSuccess, allResponse, "Data Retrieve Successfully",
                        true, true, countWTGLogbook);
                }
                

                return GetResponseModelLogbook(Constants.httpCodeSuccess, null, "No data found.", false, true,0);

            }
            catch (Exception ex)
            {

               return GetResponseModelLogbook(Constants.httpCodeFailure, null, ex.Message, false, false,0);
            }
        }
       
      public ResponseModel WhyReason()
        {
            try
            {
                var whyAnalysisReasons = _szFleetMgrContext.WhyReasonMasters.ToList();
                if(whyAnalysisReasons!=null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess,whyAnalysisReasons,"All Data Retrieve Successfully.",true);
                }
                return GetResponseModel(Constants.httpCodeSuccess,null,"No Data Found",false);
            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeFailure,null,ex.Message,false);
            }
        }
        public ResponseModelLogbook ScheduleLogbook(string? siteName, DateTime? LogDate)
        {
            try
            {
                List<TurbineList> turbineLists = new List<TurbineList>();
                List<TurbineList> turbineLists2 = new List<TurbineList>();
                var configuration = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json")
                           .Build();

                string connectionString = configuration.GetConnectionString("MyConnectionString");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_schedule_logbook", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@siteName", siteName);
                        command.Parameters.AddWithValue("@LogDate", LogDate);
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                turbineLists.Add(new TurbineList
                                {
                                    Turbine = reader["Turbine"].ToString()
                                });
                            }


                            reader.NextResult();

                            while (reader.Read())
                            {
                                turbineLists2.Add(new TurbineList
                                {
                                    Turbine = reader["Turbine"].ToString()
                                });
                            }
                        }
                    }
                }
                if (turbineLists.Count > 0)
                {
                    return GetResponseModelLogbook(Constants.httpCodeSuccess, turbineLists, "Success", true, false,0);
                }
                else if (turbineLists2.Count>0)
                {
                    return GetResponseModelLogbook(Constants.httpCodeSuccess, turbineLists2, "Success", true, true,0);
                }
                else
                {
                    return GetResponseModelLogbook(Constants.httpCodeSuccess, null, "Success", true, false,0);
                }



            }
            catch (Exception ex)
            {
                return GetResponseModelLogbook(Constants.httpCodeFailure, null, ex.Message, false, false,0);
            }
        }

        public ResponseModel GetDetailsEmployee(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            try
            {


                var query = _szFleetMgrContext.TLogbookEmployeeDetails
                   .Where(s => s.LogDate.HasValue && s.LogDate.Value.Date == logDate.Date
                   && s.ShiftCycle == shiftCycle
                   && s.SiteName == siteName);

                if (fksiteId != null)
                {
                    query = query.Where(s => s.FkSiteId == fksiteId);
                }

                var EmployeeLists = query.ToList();
                var response = EmployeeLists.Select(data => new LogbookEmployeeDetail
                {
                    Id = data.Id,
                    SiteName = data.SiteName,
                    FkSiteId = data.FkSiteId,
                    EmployeeCode = data.EmployeeCode,
                    EmployeeName = data.EmployeeName,
                    Role = data.Role,
                    ShiftCycle = data.ShiftCycle,
                    WorkDoneShift = data.WorkDoneShift,
                    LogDate = data.LogDate,
                    CreatedBy = data.CreatedBy,
                    CreatedDate = data.CreatedDate,
                    ModifiedBy = data.ModifiedBy,
                    ModifiedDate = data.ModifiedDate,
                    Status = data.Status
                }).ToList();


                if (response.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "No data Available", false);

                }
                return GetResponseModel(Constants.httpCodeSuccess, response, "All Data Get", true);

            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);


            }
        }
        public ResponseModel GetDetailsGridBreakdown(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            try
            {
                var query = _szFleetMgrContext.TLogbookGridBreakdownDetails
                    .Where(s => s.LogDate.HasValue && s.LogDate.Value.Date == logDate.Date
                   && s.ShiftCycle == shiftCycle
                   && s.SiteName == siteName);

                if (fksiteId != null)
                {
                    query = query.Where(s => s.FkSiteId == fksiteId);
                }

                var GridBreakdownLists = query.ToList();
                var response = GridBreakdownLists.Select(data => new LogbookGridBreakdownDetail
                {
                    Id = data.Id,
                    SiteName = data.SiteName,
                    FkSiteId = data.FkSiteId,
                    FeederName = data.FeederName,
                    GridDropReason = data.GridDropReason,
                    TimeFrom = data.TimeFrom,
                    TimeTo = data.TimeTo,
                    TotalTime = data.TotalTime,
                    RemarkAction = data.RemarkAction,
                    EptwNumber = data.EptwNumber,
                    LogDate = data.LogDate,
                    CreatedBy = data.CreatedBy,
                    CreatedDate = data.CreatedDate,
                    ModifiedBy = data.ModifiedBy,
                    ModifiedDate = data.ModifiedDate,
                    ShiftCycle = data.ShiftCycle,
                    Status = data.Status
                }).ToList();

                if (response.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "No data Available", false);

                }
                return GetResponseModel(Constants.httpCodeSuccess, response, "All Data Get", true);

            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);


            }
        }
        public ResponseModel GetDetailsHoto(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            try
            {
                var query = _szFleetMgrContext.TLogbookHotos
                    .Where(s => s.LogDate.HasValue && s.LogDate.Value.Date == logDate.Date
                   && s.ShiftCycle == shiftCycle
                   && s.SiteName == siteName);

                if (fksiteId != null)
                {
                    query = query.Where(s => s.FkSiteId == fksiteId);
                }

                var HotoLists = query.ToList();
                var response = HotoLists.Select(data => new LogbookHoto
                {
                    Id = data.Id,
                    SiteName = data.SiteName,
                    FkSiteId = data.FkSiteId,
                    ShiftHandedOverBy = data.ShiftHandedOverBy,
                    HandedOverDateTime = data.HandedOverDateTime,
                    ShiftTakenOverBy = data.ShiftTakenOverBy,
                    TakenOverDateTime = data.TakenOverDateTime,
                    ShiftHours = data.ShiftHours,
                    LogDate = data.LogDate,
                    CreatedBy = data.CreatedBy,
                    CreatedDate = data.CreatedDate,
                    ModifiedBy = data.ModifiedBy,
                    ModifiedDate = data.ModifiedDate,
                    ShiftCycle = data.ShiftCycle,
                    Status = data.Status
                }).ToList();

                if (response.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "No data Available", false);

                }
                return GetResponseModel(Constants.httpCodeSuccess, response, "All Data Get", true);

            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);


            }
        }
        public ResponseModel GetDetailsScada(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            try
            {
                var query = _szFleetMgrContext.TLogbookScadaDetails
                    .Where(s => s.LogDate.HasValue && s.LogDate.Value.Date == logDate.Date
                   && s.ShiftCycle == shiftCycle
                   && s.SiteName == siteName);

                if (fksiteId != null)
                {
                    query = query.Where(s => s.FkSiteId == fksiteId);
                }

                var scadaLists = query.ToList();
                var response = scadaLists.Select(data => new LogbookScadaDetail
                {
                    Id = data.Id,
                    SiteName = data.SiteName,
                    FkSiteId = data.FkSiteId,
                    IssueDesc = data.IssueDesc,
                    ActionTaken = data.ActionTaken,
                    LogDate = data.LogDate,
                    CreatedBy = data.CreatedBy,
                    CreatedDate = data.CreatedDate,
                    ModifiedBy = data.ModifiedBy,
                    ModifiedDate = data.ModifiedDate,
                    ShiftCycle = data.ShiftCycle,
                    Status = data.Status
                }).ToList();

                if (response.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "No data Available", false);

                }
                return GetResponseModel(Constants.httpCodeSuccess, response, "All Data Get", true);

            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);


            }
        }
        public ResponseModel GetDetailsScheduleMaintenance(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            try
            {
                var query = _szFleetMgrContext.TLogbookScheduleMaintenanceActivities
                    .Where(s => s.LogDate.HasValue && s.LogDate.Value.Date == logDate.Date
                   && s.ShiftCycle == shiftCycle
                   && s.SiteName == siteName);

                if (fksiteId != null)
                {
                    query = query.Where(s => s.FkSiteId == fksiteId);
                }

                var scheduleMaintenance = query.ToList();
                var scheduleMaintenanceData = scheduleMaintenance.Select(data => new LogbookScheduleMaintenanceActivity
                {
                    Id = data.Id,
                    FkSiteId = data.FkSiteId,
                    SiteName = data.SiteName,
                    Turbine = data.TurbineNumber,
                    Activity = data.Activity,
                    Observation = data.Observation,
                    EptwNumber = data.EptwNumber,
                    LogDate = data.LogDate,
                    CreatedBy = data.CreatedBy,
                    CreatedDate = data.CreatedDate,
                    ModifiedBy = data.ModifiedBy,
                    ModifiedDate = data.ModifiedDate,
                    ShiftCycle = data.ShiftCycle,
                    Closure = data.Closure,
                    RescheduleDate = data.RescheduleDate,
                    Status = data.Status
                }).ToList();


                if (scheduleMaintenanceData.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "No data Available", false);

                }
                return GetResponseModel(Constants.httpCodeSuccess, scheduleMaintenanceData, "All Data Get", true);

            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);


            }
        }
        public ResponseModel GetDetailsWtgBreakdown(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            try
            {
                var query = _szFleetMgrContext.TLogbookWtgBreakdownDetails
                    .Where(s => s.LogDate.HasValue && s.LogDate.Value.Date == logDate.Date
                   && s.ShiftCycle == shiftCycle
                   && s.SiteName == siteName && s.Closure!="");

                if (fksiteId != null)
                {
                    query = query.Where(s => s.FkSiteId == fksiteId);
                }

                var WTGBreakdownLists = query.ToList();
                var response = WTGBreakdownLists.Select(data => new LogbookWtgBreakdownDetail
                {
                    Id = data.Id,
                    SiteName = data.SiteName,
                    FkSiteId = data.FkSiteId,
                    Turbine = data.TurbineNumber,
                    Error = data.Error,
                    TimeFrom = data.TimeFrom,
                    TimeTo = data.TimeTo,
                    TotalTime = data.TotalTime,
                    ActionTaken = data.ActionTaken,
                    EptwNumber = data.EptwNumber,
                    PasswordUsage = data.PasswordUsage,
                    PasswordUsageBy = data.PasswordUsageBy,
                    LogDate = data.LogDate,
                    CreatedBy = data.CreatedBy,
                    CreatedDate = data.CreatedDate,
                    ModifiedBy = data.ModifiedBy,
                    ModifiedDate = data.ModifiedDate,
                    ShiftCycle = data.ShiftCycle,
                    Closure = data.Closure,
                    Status = data.Status,
                    BreakdownCategory=data.BreakdownCategory,
                    RowId=data.RowId,
                    FkTaskId=data.FkTaskId
                }).ToList();


                if (response.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "No data Available", false);

                }
                return GetResponseModel(Constants.httpCodeSuccess, response, "All Data Get", true);

            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);


            }
        }
        public ResponseModel GetRemarks(DateTime logDate, int? fksiteId, string siteName, string shiftCycle)
        {
            try
            {
                var response = _szFleetMgrContext.TLogbookRemarks
                    .Where(s => s.LogDate.HasValue && s.LogDate.Value.Date == logDate.Date
                   && s.ShiftCycle == shiftCycle
                   && s.SiteName == siteName).ToList();

                if (response.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "No data Available", false);
                }
                return GetResponseModel(Constants.httpCodeSuccess, response, "All Data Get", true);

            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetCommonMaster(string? masterCategory)
        {
            try
            {

                var masterCategoryList = _szFleetMgrContext.TLogbookCommonMasters
                .Where(c => c.MasterCategory == masterCategory)
                .ToList();
                if (masterCategoryList.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "Master Category Not Found", false);

                }
                LogbookCommonMaster logbookCommonMaster = new LogbookCommonMaster();
                logbookCommonMaster.MasterCategory = masterCategory;
                logbookCommonMaster.CommonMasterLists = masterCategoryList;
                return GetResponseModel(Constants.httpCodeSuccess, logbookCommonMaster, "List of Master Category Values", true);

            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);

            }

        }
        public ResponseModel GetEmployeeMaster(string? employeeCode)
        {
            try
            {
                if (employeeCode != null)
                {
                    List<CmrEmployeeMaster> response = new List<CmrEmployeeMaster>();
                    var getUserDetail = GetUserDetails(employeeCode);
                    CmrEmployeeMaster singleData = new CmrEmployeeMaster
                    {
                        EmpCode = getUserDetail.Code,
                        EmpName=getUserDetail.Name
                    };
                    response.Add(singleData);
                   
                    return GetResponseModel(Constants.httpCodeSuccess, response, "All Employee List", true);
                }
               
                else
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "Employee Code Not Present", false);

                }
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);

            }
        }
        public ResponseModel GetLogbookRecords(string? siteName, DateTime logFromDate, DateTime logToDate)
        {
            try
            {
                var query = _szFleetMgrContext.TLogbookEmployeeDetails.Where(logbook => logbook.LogDate >= logFromDate && logbook.LogDate <= logToDate);

                if (siteName != null)
                {
                    query = query.Where(logbook => logbook.SiteName == siteName);

                }
                List<TLogbookEmployeeDetail> logbookEmployeeDetail = query.ToList();
                if (logbookEmployeeDetail.Count == 0)
                {
                    return GetResponseModel(200, null, "Logbook records not found.", false);
                }
                List<TLogbookEmployeeDetail> distinctEntries = new List<TLogbookEmployeeDetail>();
                foreach (var entry in logbookEmployeeDetail)
                {
                    bool alreadyExists = distinctEntries.Any(de => de.LogDate == entry.LogDate && de.ShiftCycle == entry.ShiftCycle);
                    if (!alreadyExists)
                    {
                        distinctEntries.Add(entry);
                    }
                }
                return GetResponseModel(200, distinctEntries, "Logbook records retrieved successfully.", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetKpiWindPowerGeneration(string site)
        {
            try
            {

                //System.Data.DataTable ds = new System.Data.DataTable();
                DataSet ds = new DataSet();

                var conn = _szFleetMgrContext.Database.GetDbConnection();
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SP_Kpi_Wind_Power_Generation";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@site", site));

                    DbProviderFactory factory = DbProviderFactories.GetFactory(conn);
                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds);
                }

                Dictionary<string, object> data = new Dictionary<string, object>();
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    foreach (DataColumn column in ds.Tables[0].Columns)
                    {
                        data[column.ColumnName] = row[column];
                    }
                }
                
                return GetResponseModel(Constants.httpCodeSuccess, data, "Data Retrieved Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetKpiMA_GA(string SiteName)
        {
            try
            {
                DataSet ds = new DataSet();

                var conn = _szFleetMgrContext.Database.GetDbConnection();
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "sp_MA_GA_KPI_Data";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@SiteName", SiteName));

                    DbProviderFactory factory = DbProviderFactories.GetFactory(conn);
                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds);
                }
                List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    foreach (DataColumn column in ds.Tables[0].Columns)
                    {
                        rowData[column.ColumnName] = row[column];
                    }
                    data.Add(rowData);
                }
                return GetResponseModel(Constants.httpCodeSuccess, data, "Data Retrieved Successfully", true);
            }
            
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }


        public ResponseModel GetKpiWindspeed()
        {
            try
            {
                var currentDate = DateTime.Now.Date;
                var allTurbineData = _datamartMobileContext.TurbineOnlines.ToList();
                var problematicRecords = new List<string>();
                var GenerationData = allTurbineData
                    .Where(d =>
                    {
                        if (DateTime.TryParseExact(d.ControllerTimestamp, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                        {
                            if (parsedDate.Date == currentDate)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            problematicRecords.Add(d.ControllerTimestamp);
                        }

                        return false;
                    })
                    .ToList();
                decimal? totalWindspeed = GenerationData.Sum(w => w.WindSpeed);
                int? totalCount = GenerationData.Count;
                decimal? totalWindSpeedGeneration = totalCount > 0 ? totalWindspeed / (totalCount) : 0;
                totalWindSpeedGeneration = Math.Round(totalWindSpeedGeneration.GetValueOrDefault(), 2);

                return GetResponseModel(Constants.httpCodeSuccess, totalWindSpeedGeneration, "All Data Reterived Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetKpiTotalGeneration()
        {
            try
            {
                var currentDate = DateTime.Now.Date;
                List<TurbineOnline> turbineOnlineList = new List<TurbineOnline>();
                var allTurbineData = _datamartMobileContext.TurbineOnlines.ToList();
                var problematicRecords = new List<string>();
                var GenerationData = allTurbineData
                    .Where(d =>
                    {
                        if (DateTime.TryParseExact(d.ControllerTimestamp, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                        {
                            if (parsedDate.Date == currentDate)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            problematicRecords.Add(d.ControllerTimestamp);
                        }

                        return false;
                    })
                    .ToList();

                decimal totalAccumulatedProduction = GenerationData.Sum(t => t.AccumulatedProduction ?? 0);
                int totalCount = GenerationData.Count;
                int totalGeneration = (int)(totalCount > 0 ? totalAccumulatedProduction / (totalCount * 1000) : 0);

                return GetResponseModel(Constants.httpCodeSuccess, totalGeneration, "All Data Reterived Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetKpiCurrentActivePower()
        {
            try
            {
                var currentDate = DateTime.Now.Date;
                List<TurbineOnline> turbineOnlineList = new List<TurbineOnline>();
                var allTurbineData = _datamartMobileContext.TurbineOnlines.ToList();
                var problematicRecords = new List<string>();
                var GenerationData = allTurbineData
                    .Where(d =>
                    {
                        if (DateTime.TryParseExact(d.ControllerTimestamp, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                        {
                            if (parsedDate.Date == currentDate)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            problematicRecords.Add(d.ControllerTimestamp);
                        }

                        return false;
                    })
                    .ToList();
                decimal totalProduction = GenerationData.Sum(p => p.Production ?? 0);
                int totalCount = GenerationData.Count;
                decimal totalActivePower = totalCount > 0 ? totalProduction / (totalCount * 1000) : 0;
                totalActivePower = Math.Round(totalActivePower, 3);

                return GetResponseModel(Constants.httpCodeSuccess, totalActivePower, "All Data Reterived Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetKpiReactivePower()
        {
            try
            {
                return GetResponseModel(Constants.httpCodeFailure, null, "Message", false);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

      
        public ResponseModel GetKpiDetails(string? searchQuery, string? customerFilter,string userSite,string? searchStatus)
        {
            try

            {
                var getAllTurbine = _crmsContext.VScAllMachineStaticDetailOmsPbis
                        .Where(d => d.MainSite == userSite)
                        .Select(d => d.SapFuncLocCode)
                        .ToList();
                List<TurbineName> turbineDatas = new List<TurbineName>();
                List<PlantName> plantDatas = new List<PlantName>();
                foreach (var turbine in getAllTurbine)
                {
                    var turbineData = new TurbineName();
                    var plantData = new PlantName();

                    var getWTGName = _datamartMobileContext.CatalogTurbines.FirstOrDefault(d => d.FunctionalLocation == turbine);
                    var getSiteData = _datamartMobileContext.VwWindFarms.FirstOrDefault(d => getWTGName != null && d.PlantUnitId == getWTGName.PlantUnitId);
                    var getErrorName = _datamartMobileContext.ManifestProductionStates.FirstOrDefault(d => getSiteData != null && d.ProductionStateCode == getSiteData.ProductionStateCode);
                    var getErrorCode = _datamartMobileContext.TurbineOnlines.FirstOrDefault(d => getWTGName != null && d.PlantUnitId == getWTGName.PlantUnitId);

                    var statusName = getErrorName != null ? (getErrorName.ProductionStateName == "Manual" ? "Manual Stop" : getErrorName.ProductionStateName) : "NoComm";
                    var currentActivePower = getSiteData != null ? Math.Round(getSiteData.Production ?? 0, 2).ToString() : "";
                    var windspeed = getSiteData != null && getSiteData.WindSpeed.HasValue ? Math.Round(getSiteData.WindSpeed.Value, 2).ToString() : "";
                    var errorDescription = "";
                    var breakdownCount = "";
                    
                    DateTime todaysDate = DateTime.Now.Date;
                    var breakdownCategory = _szFleetMgrContext.TLogbookWtgBreakdownDetails
                        .FirstOrDefault(d => d.LogDate == todaysDate && d.TurbineNumber==turbine)
                        ?.BreakdownCategory ?? "";

                    if (getErrorCode != null)
                    {
                        var getErrorDes = _szFleetMgrContext.ErrorMasters.FirstOrDefault(d => d.EventCode == getErrorCode.EventCode);
                       
                        DateTime? serverTimestamp = getErrorCode.ServerTimestamp;

                      

                        if (getErrorName?.ProductionStateName == "Alarm" && getErrorDes != null)
                        {
                            errorDescription = getErrorDes.Name;
                            if (serverTimestamp.HasValue)
                            {
                                DateTime turbineBdTime = serverTimestamp.Value;

                                TimeSpan timeDifference = DateTime.Now - turbineBdTime;

                                int hours = (int)timeDifference.TotalHours;
                                int minutes = timeDifference.Minutes;
                                breakdownCount = $"{hours:D2}:{minutes:D2}";
                            }
                            
                        }
                        else
                        {
                            errorDescription = "No Alarm";
                            breakdownCount = "";
                        }
                    }
                   
                    plantData.Name = getSiteData?.PlantName;
                    if (!string.IsNullOrEmpty(plantData.Name))
                    {
                        plantDatas.Add(plantData);
                    }
                    turbineData.Name = getWTGName != null ? getWTGName.PlantUnitName : "";
                    turbineData.Turbine = turbine;
                    turbineData.StatusName = statusName;
                    turbineData.ErrorDescription = errorDescription;
                    turbineData.CurrentActivePower = currentActivePower;
                    turbineData.WindSpeed = windspeed;
                    turbineData.DownTimeCount = breakdownCount;
                    turbineData.BreakdownCategory = breakdownCategory;
                    turbineDatas.Add(turbineData);
                }


                if (turbineDatas.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "no data found.", false);
                }

                var statusOrders = new List<string> { "Alarm", "Total WTG", "Manual Stop", "NoComm", "Generating", "Curtailment", "Idling" };

                turbineDatas = turbineDatas.OrderBy(ts =>
                {
                    var sequence = statusOrders.IndexOf(ts.StatusName);
                    return sequence == -1 ? int.MaxValue : sequence;
                }).ToList();

                

                if (!string.IsNullOrEmpty(searchQuery))
                {
                    turbineDatas = turbineDatas
                .Where(name => name.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                name.StatusName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                .ToList();
                }


                if (!string.IsNullOrEmpty(customerFilter))
                {
                    var customerTurbines = _datamartMobileContext.CatalogTurbines
                        .Where(d => d.OwnerId.ToString() == customerFilter)
                        .ToList();

                    if (customerTurbines.Count == 0)
                        return GetResponseModel(Constants.httpCodeSuccess, null, "No turbine for this customer", false);

                    turbineDatas = turbineDatas
                        .Where(name => customerTurbines.Any(cust => cust.PlantUnitName.Contains(name.Name, StringComparison.OrdinalIgnoreCase)))
                        .ToList();
                }


                var sitePlantName = plantDatas
                 .Select(data => data.Name)
                 .Distinct()
                 .Select(name => new PlantName
                 {
                     Name = name
                 })
                 .ToList();


                var totalStatusCodes = _datamartMobileContext.ManifestProductionStates.ToList();
                totalStatusCodes.Add(new ManifestProductionState
                {
                    ProductionStateName = "NoComm",
                    ProductionStateCode = 0

                });

                List<TurbineStatus> turbineStatusData = new List<TurbineStatus>();


                foreach (var status in totalStatusCodes)
                {
                    TurbineStatus statuses = new TurbineStatus();

                    statuses.StatusCode = status.ProductionStateName;

                    statuses.Value = turbineDatas.Count(data => data.StatusName == statuses.StatusCode);
                    if (statuses.StatusCode == "Manual")
                    {
                        statuses.StatusCode = "Manual Stop";
                        statuses.Value = turbineDatas.Count(data => data.StatusName == "Manual Stop");
                    }
                    turbineStatusData.Add(statuses);
                }


                turbineStatusData.Add(new TurbineStatus
                {
                    StatusCode = "Total WTG",
                    Value = turbineDatas.Count
                });

                turbineStatusData = turbineStatusData.OrderBy(ts =>
                {
                    var sequence = statusOrders.IndexOf(ts.StatusCode);
                    return sequence == -1 ? int.MaxValue : sequence;
                }).ToList();

                if (searchStatus=="Total WTG")
                {
                    turbineDatas = turbineDatas
                .ToList();
                }

                else if (!string.IsNullOrEmpty(searchStatus))
                {
                    turbineDatas = turbineDatas
                .Where(name => name.Name.Contains(searchStatus, StringComparison.OrdinalIgnoreCase) ||
                name.StatusName.Contains(searchStatus, StringComparison.OrdinalIgnoreCase))
                .ToList();
                }

                if (string.IsNullOrEmpty(searchStatus))
                {
                    turbineDatas = turbineDatas
                .Where(name => name.Name.Contains("Alarm", StringComparison.OrdinalIgnoreCase) ||
                name.StatusName.Contains("Alarm", StringComparison.OrdinalIgnoreCase))
                .ToList();
                }

                var responseTurbineStatuses = new ResponseTurbineStatus
                {
                    TurbineStatuses = turbineStatusData.ToArray(),
                    TurbineNames = turbineDatas.ToArray(),
                    PlantNames = sitePlantName.ToArray()
                };

                return GetResponseModel(Constants.httpCodeSuccess, responseTurbineStatuses, "Success", true);

                // return GetResponseModel(Constants.httpCodeSuccess, null, "No Sites related Data present.", false);
            }

            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

       
        public ResponseModel WTGCustomerbyPlantId(string? userSite)
        {
            try
            { 
            DataSet ds = new DataSet();

            var conn = _szFleetMgrContext.Database.GetDbConnection();
            conn.Open();

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "sp_customer_turbine";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@userSite", userSite));

                DbProviderFactory factory = DbProviderFactories.GetFactory(conn);
                DbDataAdapter adapter = factory.CreateDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }
            List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Dictionary<string, object> rowData = new Dictionary<string, object>();
                foreach (DataColumn column in ds.Tables[0].Columns)
                {
                    rowData[column.ColumnName] = row[column];
                }
                data.Add(rowData);
            }
            return GetResponseModel(Constants.httpCodeSuccess, data, "Data Retrieved Successfully", true);
            }

            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetSiteKpiDetails(string? siteName, string? filter)
        {
            try
            {
                var getData = _szFleetMgrContext.SiteKpiDetails
                    .Where(p => p.SiteName == siteName && (string.IsNullOrEmpty(filter) ||
                    p.TurbineNumber.Contains(filter) || p.Status.Contains(filter))).ToList();

                if (getData.Count != 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, getData, "Site details found successfully.", true);
                }
                return GetResponseModel(Constants.httpCodeFailure, null, "Site details not found.", false);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetKpiDropdown(string? parameter)
        {
            try
            {
                ResponseKpiDropdown responseKpiDropdown = new ResponseKpiDropdown();
                List<string> filtersArray = new List<string>();
                var kpiContextList = _szFleetMgrContext.TKpiDetails.Where(p => p.Parameter == parameter).ToList();
                if (kpiContextList.Count != 0)
                {
                    foreach (var data in kpiContextList)
                    {

                        var Filters = data.Filters == "" ? " " : data.Filters.Replace(
                                   data.ReplaceFilter == "" ? " " : data.ReplaceFilter,
                                   data.ReplaceFilter == "" ? " " : data.Years);
                        filtersArray.Add(Filters);

                    }
                    var array = filtersArray.Distinct().ToList();
                    responseKpiDropdown.Category = parameter;
                    responseKpiDropdown.Filters = array;
                    return GetResponseModel(Constants.httpCodeSuccess, responseKpiDropdown, "Kpi Dropdown Retrieve Completed", true);
                }
                else
                    return GetResponseModel(Constants.httpCodeSuccess, null, "Parameter Not Found.", false);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetKpiPM_LS_TCI(string? SiteName)
        {
            try
            {
                DataSet ds = new DataSet();

                var conn = _szFleetMgrContext.Database.GetDbConnection();
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "sp_KPI_PM_LS_TCI";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@SiteName", SiteName));

                    DbProviderFactory factory = DbProviderFactories.GetFactory(conn);
                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds);
                }
                List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Dictionary<string, object> rowData = new Dictionary<string, object>();
                    foreach (DataColumn column in ds.Tables[0].Columns)
                    {
                        rowData[column.ColumnName] = row[column];
                    }
                    data.Add(rowData);
                }
                return GetResponseModel(Constants.httpCodeSuccess, data, "Data Retrieved Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetAllMainSite()
        {
            try
            {
                var allDataList = _crmsContext.ScMainSites.ToList();
                if (allDataList.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "No Data Found.", false);
                }
                return GetResponseModel(Constants.httpCodeSuccess, allDataList, "All Data Retreive Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetKpiTCI(string? filter)
        {
            try
            {
                filter ??= "FY23-24";
                ResponseKpi responseKpi = new ResponseKpi();
                int year = 0;

                string extractedYear = filter.Substring(filter.Length - 2); //Extract the year ("23")
                int extractedYearInt = int.Parse(extractedYear); //Convert the extracted year to a full year ("2023")
                year = 2000 + extractedYearInt - 1;
                var query = _szFleetMgrContext.KpiTcis
                    .Where(s => s.LogDate.HasValue && s.LogDate.Value.Year == year);

                var totalWtg = query.ToList();
                var planned = query.Where(s => s.Status == "planned").ToList();
                var completed = query.Where(s => s.Status == "completed").ToList();
                var pending = query.Where(s => s.Status == "pending").ToList();

                CalculatedData calculatedData = new CalculatedData
                {
                    Total = totalWtg.Count,
                    Planned = planned.Count,
                    Completed = completed.Count,
                    Pending = pending.Count
                };
                responseKpi.CalculatedData = calculatedData;
                responseKpi.YearList = new List<string> { "FY23-24" };

                return GetResponseModel(200, responseKpi, "all data", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetKpiIDRV(string SiteName)
        {
            try
            {
                    
                    DataSet ds = new DataSet();

                    var conn = _szFleetMgrContext.Database.GetDbConnection();
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "sp_Get_NCCategoryWiseCount";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@SiteName", SiteName));

                        DbProviderFactory factory = DbProviderFactories.GetFactory(conn);
                        DbDataAdapter adapter = factory.CreateDataAdapter();
                        adapter.SelectCommand = cmd;
                        adapter.Fill(ds);
                    }

                List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Dictionary<string, string> rowData = new Dictionary<string, string>();
                    foreach (DataColumn column in ds.Tables[0].Columns)
                    {
                        rowData[column.ColumnName] = row[column].ToString();
                    }
                    data.Add(rowData);
                }

                return GetResponseModel(Constants.httpCodeSuccess, data, "Data Retrieved Successfully", true);
                }
                catch (Exception ex)
                {
                    return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
                }
            
        }
        public ResponseModel GetKpiMTTR_MTBF(string SiteName)
        {
            try
            {

                DataSet ds = new DataSet();

                var conn = _szFleetMgrContext.Database.GetDbConnection();
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "sp_KPI_MTTR_MTBF";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@SiteName", SiteName));

                    DbProviderFactory factory = DbProviderFactories.GetFactory(conn);
                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds);
                }

                List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Dictionary<string, string> rowData = new Dictionary<string, string>();
                    foreach (DataColumn column in ds.Tables[0].Columns)
                    {
                        rowData[column.ColumnName] = row[column].ToString();
                    }
                    data.Add(rowData);
                }

                return GetResponseModel(Constants.httpCodeSuccess, data, "Data Retrieved Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }

        }
        public ResponseModel GetKpiLS(string? filter)
        {
            try
            {
                ResponseKpi responseKpi = new ResponseKpi();
                int year = DateTime.Now.Year;

                DateTime startDate, endDate;
                string quarterName = "";

                if (filter == null || filter == "Q1")
                {
                    startDate = new DateTime(year, 1, 1);
                    endDate = new DateTime(year, 3, 31);
                    quarterName = "Q1";
                }
                else if (filter == "Q2")
                {
                    startDate = new DateTime(year, 4, 1);
                    endDate = new DateTime(year, 6, 30);
                    quarterName = "Q2";
                }
                else if (filter == "Q3")
                {
                    startDate = new DateTime(year, 7, 1);
                    endDate = new DateTime(year, 9, 30);
                    quarterName = "Q3";
                }
                else if (filter == "Q4")
                {
                    startDate = new DateTime(year, 10, 1);
                    endDate = new DateTime(year, 12, 31);
                    quarterName = "Q4";
                }
                else if (filter == "Half Yrl-1")
                {
                    startDate = new DateTime(year, 1, 1);
                    endDate = new DateTime(year, 6, 30);
                    quarterName = "Half Yrl-1";
                }
                else if (filter == "Half Yrl-2")
                {
                    startDate = new DateTime(year, 7, 1);
                    endDate = new DateTime(year, 12, 31);
                    quarterName = "Half Yrl-2";
                }
                else
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "Wrong Input", false);
                }

                var KpiList = _szFleetMgrContext.KpiLs
                    .Where(p => p.LogDate >= startDate && p.LogDate <= endDate)
                    .ToList();



                var totalWtg = KpiList.Count;
                var planned = KpiList.Count(s => s.Status == "planned");
                var completed = KpiList.Count(s => s.Status == "completed");
                var pending = KpiList.Count(s => s.Status == "pending");

                CalculatedData calculatedData = new CalculatedData
                {
                    Total = totalWtg,
                    Planned = planned,
                    Completed = completed,
                    Pending = pending
                };

                responseKpi.CalculatedData = calculatedData;
                responseKpi.YearList = new List<string> { "Q1", "Q2", "Q3", "Q4", "Half Yrl-1", "Half Yrl-2" };

                return GetResponseModel(200, responseKpi, "all data", true);
            }
            catch (Exception ex)
            {
                

                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetKpiMTBF(string? filter)
        {
            try
            {
                filter ??= "Model S6X";

                var currentDate = DateTime.Now;
                var currentYear = currentDate.Year;
                var currentMonth = currentDate.Month;
                DateTime financialYearStart = new DateTime(currentYear, 4, 1);

                if (currentDate < financialYearStart)
                {
                    currentYear--;
                }

                var logDates = _szFleetMgrContext.KpiMtbfs
                    .Where(data => data.Model == filter)
                    .ToList();

                int financialYearValue = 0;
                int comingYearValue = 0;
                int thisMonthValue = 0;
                int thisWeekValue = 0;

                foreach (var tpi in logDates)
                {
                    DateTime? logDateNullable = tpi.LogDate;
                    if (logDateNullable.HasValue)
                    {
                        DateTime logDate = logDateNullable.Value;
                        int year = logDate.Year;

                        if (logDate < financialYearStart && logDate.Year == financialYearStart.Year - 1 && logDate.Month > 4)
                        {
                            year++;
                        }
                        else
                        {
                            year++;
                        }

                        int value = tpi.Value ?? 0;

                        if (year == currentYear)
                        {
                            financialYearValue += value;
                        }
                        else if (year == currentYear + 1)
                        {
                            comingYearValue += value;
                        }

                        if (year == currentYear + 1 && logDate.Month == currentMonth)
                        {
                            thisMonthValue += value;
                        }

                        if (logDate >= currentDate.AddDays(-7))
                        {
                            thisWeekValue += value;
                        }
                    }
                }

                var responseKpiMT = new ResponseKpiMT
                {
                    ResponseData = new ResponseKpiMA
                    {
                        financialYear = financialYearValue,
                        ComingYear = comingYearValue,
                        thisMonth = thisMonthValue,
                        thisWeek = thisWeekValue
                    },
                    ModelList = new List<string> { "Model S6X", "Model S8X" }
                };

                return GetResponseModel(Constants.httpCodeSuccess, responseKpiMT, "Success", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetKpiMTTRMTBF(string? userSite, string? plantRole)
        {
            try
            {
                var listOfPlantName = _szFleetMgrContext.KpiMttrMtbfs.Where(d => d.IsModel == true).Select(d => d.PlantRole).ToList();
                //if (userSite == null)
                //{
                //    return GetResponseModel(Constants.httpCodeSuccess, null, "Please Select Site", false);
                //}

                List<string> siteLists = new List<string>();

                foreach (var list in listOfPlantName)
                {
                    string siteList = string.Empty; // Or simply string siteList = "";

                    var plantName = _datamartMobileContext.VwWindFarms
                        .Where(d => d.PlantName == list && d.ServiceOrganizationName == userSite)
                        .Select(d => d.PlantName) // Assuming 'PlantName' is the property you want to add
                        .Distinct()
                        .ToList();

                    siteList = string.Join(", ", plantName); // Convert the list to a comma-separated string

                    siteLists.Add(siteList);
                }
                var listModelUnit = _szFleetMgrContext.KpiMttrMtbfs.Where(d => d.IsModel == false).Select(d => d.PlantRole).Distinct().ToList();

                var modelUnitData = _szFleetMgrContext.KpiMttrMtbfs
                        .Where(d => d.PlantRole == plantRole)
                        .Select(d => new { d.PlantRole, d.SystemComponent, d.MttrHours, d.MtbfHours }) // Assuming 'PlantName' is the property you want to add
                        .Distinct()
                        .ToList();
                return GetResponseModel(Constants.httpCodeSuccess, listModelUnit, "Success", true);
            }

            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetKpiMTTR(string? filter )
        {
            try
            {
                filter ??= "Model S6X";

                var currentDate = DateTime.Now;
                var currentYear = currentDate.Year;
                var currentMonth = currentDate.Month;
                DateTime financialYearStart = new DateTime(currentYear, 4, 1);

                if (currentDate < financialYearStart)
                {
                    currentYear--;
                }

                var logDates = _szFleetMgrContext.KpiMttrs
                    .Where(data => data.Model == filter)
                    .ToList();

                decimal financialYearValue = 0;
                decimal comingYearValue = 0;
                decimal thisMonthValue = 0;
                decimal thisWeekValue = 0;

                foreach (var tpi in logDates)
                {
                    DateTime? logDateNullable = tpi.LogDate;
                    if (logDateNullable.HasValue)
                    {
                        DateTime logDate = logDateNullable.Value;
                        int year = logDate.Year;

                        if (logDate < financialYearStart && logDate.Year == financialYearStart.Year - 1 && logDate.Month > 4)
                        {
                            year++;
                        }
                        else
                        {
                            year++;
                        }

                        decimal value = tpi.Value ?? 0;

                        if (year == currentYear)
                        {
                            financialYearValue += value;
                        }
                        else if (year == currentYear + 1)
                        {
                            comingYearValue += value;
                        }

                        if (year == currentYear + 1 && logDate.Month == currentMonth)
                        {
                            thisMonthValue += value;
                        }

                        if (logDate >= currentDate.AddDays(-7))
                        {
                            thisWeekValue += value;
                        }
                    }
                }

                var responseKpiMT = new ResponseKpiMTD
                {
                    ResponseData = new ResponseKpiDec
                    {
                        financialYear = financialYearValue,
                        ComingYear = comingYearValue,
                        thisMonth = thisMonthValue,
                        thisWeek = thisWeekValue
                    },
                    ModelList = new List<string> { "Model S6X", "Model S8X" }
                };

                return GetResponseModel(Constants.httpCodeSuccess, responseKpiMT, "Success", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        //public ResponseModel GetKpiBelow95(string? userSite)
        //{
        //    try
        //    {
        //        var responseKpiMA = new ResponseKpiMA();
        //        var currentYear = DateTime.Now.Year;
        //        var currentMonth = DateTime.Now.Month;
        //        var financialYearStart = new DateTime(currentYear, 4, 1);
        //        var previousYearStart = new DateTime(currentYear - 1, 4, 1);
        //        if (DateTime.Now < financialYearStart)
        //        {
        //            currentYear--;
        //        }
        //        // var getSite = _crmsContext.ScMainSites
        //        //    .FirstOrDefault(d => d.MainSite == userSite).MainSiteCode;
        //        // if (getSite == null)
        //        // {
        //        //     return GetResponseModel(Constants.httpCodeSuccess, null, "Site is not present", false);
        //        // }
        //        // var logDates = _crmsContext.VCrmGenerationAllIndiaOmsPbis
        //        //.Where(d => d.DateOfGen.HasValue && d.DateOfGen.Value.Date >=
        //        // previousYearStart && d.MainSiteCode == getSite).ToList();
        //        var logDates = _crmsContext.VCrmGenerationAllIndiaOmsPbis
        //       .Where(d => d.DateOfGen.HasValue && d.DateOfGen.Value.Date >=
        //       previousYearStart).ToList();
        //        decimal? financialYearValue = 0;
        //        decimal? comingYearValue = 0;
        //        decimal? thisMonthValue = 0;
        //        decimal? thisWeekValue = 0;

        //        foreach (var data in logDates)
        //        {

        //            DateTime? logDateNullable = data.DateOfGen;
        //            DateTime logDate = logDateNullable ?? DateTime.MinValue;
        //            int year = logDate.Year;

        //            if (logDate <= financialYearStart)
        //            {

        //                if (logDate.Year == financialYearStart.Year - 1 && logDate.Month >= 4)

        //                {
        //                    year++;
        //                }
        //            }
        //            else
        //            {
        //                year++;
        //            }

        //            decimal? numerator = data.CmaNumerator;
        //            decimal? denominator = data.CmaDenominator;
        //            decimal? totalNumerator = 0;
        //            decimal? totalDenominator = 0;
        //            decimal? calculation = denominator > 0 ? (numerator / denominator) * 100 : 0;
        //            if (calculation < 0.95m)
        //            {
        //                totalNumerator = totalNumerator + numerator;
        //                totalDenominator = totalDenominator + denominator;
        //            }
        //            decimal? totalCalculation = totalDenominator > 0 ? (totalNumerator / totalDenominator) * 100 : 0;
        //            totalCalculation = Math.Round(totalCalculation.GetValueOrDefault(), 4);

        //            if (year == currentYear)
        //            {
        //                financialYearValue = totalCalculation;
        //            }
        //            else if (year == currentYear + 1)
        //            {
        //                comingYearValue = totalCalculation;
        //            }

        //            if (year == currentYear + 1 && logDate.Month == currentMonth)
        //            {
        //                thisMonthValue = totalCalculation;
        //            }

        //            if (logDate >= DateTime.Now.AddDays(-7))
        //            {
        //                thisWeekValue = totalCalculation;
        //            }
        //        }

        //        responseKpiMA.financialYear = financialYearValue;
        //        responseKpiMA.ComingYear = comingYearValue;
        //        responseKpiMA.thisMonth = thisMonthValue;
        //        responseKpiMA.thisWeek = thisWeekValue;

        //        return GetResponseModel(Constants.httpCodeSuccess, responseKpiMA, "Success", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
        //    }
        //}

        public ResponseModel GetKpiBelow95(string? userSite)
        {
            try
            {
                    ResponseKpiMA responseKpiMA = new ResponseKpiMA();
                    responseKpiMA.financialYear = 94.6M;
                    responseKpiMA.ComingYear = 0M;
                    responseKpiMA.thisMonth = 94.3M;
                    responseKpiMA.thisWeek = 93.2M;
                
                return GetResponseModel(Constants.httpCodeSuccess, responseKpiMA, "Success", true);

            }

            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetKpiMA(string? userSite)
        {
            try
            {
                ResponseKpiMA responseKpiMA = new ResponseKpiMA();
                    responseKpiMA.financialYear = 99.5M;
                    responseKpiMA.ComingYear = 99.5M;
                    responseKpiMA.thisMonth =99.2M;
                    responseKpiMA.thisWeek = 98.7M;
                
                return GetResponseModel(Constants.httpCodeSuccess, responseKpiMA, "Success", true);

            }

            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetKpiGA(string? userSite)
        {
            try
            {
                ResponseKpiMA responseKpiMA = new ResponseKpiMA();
                    responseKpiMA.financialYear = 99.6M;
                    responseKpiMA.ComingYear = 99.3M;
                    responseKpiMA.thisMonth = 99.3M;
                    responseKpiMA.thisWeek = 99.4M;
                
                return GetResponseModel(Constants.httpCodeSuccess, responseKpiMA, "Success", true);

            }

            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        //public ResponseModel GetKpiMA(string? userSite)
        //{
        //    try
        //    {
        //        var responseKpiMA = new ResponseKpiMA();
        //        var currentYear = DateTime.Now.Year;
        //        var currentMonth = DateTime.Now.Month;
        //        var financialYearStart = new DateTime(currentYear, 4, 1);
        //        var previousYearStart = new DateTime(currentYear - 1, 4, 1);
        //        if (DateTime.Now < financialYearStart)
        //        {
        //            currentYear--;
        //        }

        //        //var getSite = _crmsContext.ScMainSites
        //        //    .FirstOrDefault(d => d.MainSite == userSite).MainSiteCode;

        //        //if (getSite == null)
        //        //{
        //        //    return GetResponseModel(Constants.httpCodeSuccess, null, "Site is not present", false);
        //        //}

        //        //var logDates = _crmsContext.VCrmGenerationAllIndiaOmsPbis
        //        //.Where(d => d.DateOfGen.HasValue && d.DateOfGen.Value.Date >= previousYearStart
        //        //&& d.MainSiteCode == getSite)
        //        //.ToList();
        //        var logDates = _crmsContext.VCrmGenerationAllIndiaOmsPbis
        //      .Where(d => d.DateOfGen.HasValue && d.DateOfGen.Value.Date >=
        //      previousYearStart).ToList();

        //        decimal? financialYearValue = 0;
        //        decimal? comingYearValue = 0;
        //        decimal? thisMonthValue = 0;
        //        decimal? thisWeekValue = 0;

        //        foreach (var data in logDates)
        //        {

        //            DateTime? logDateNullable = data.DateOfGen;
        //            DateTime logDate = logDateNullable ?? DateTime.MinValue;
        //            int year = logDate.Year;

        //            if (logDate <= financialYearStart)
        //            {

        //                if (logDate.Year == financialYearStart.Year - 1 && logDate.Month >= 4)

        //                {
        //                    year++;
        //                }
        //            }
        //            else
        //            {
        //                year++;
        //            }


        //            if (year == currentYear)
        //            {
        //                decimal? totalNumerator = 0;
        //                decimal? totalDenominator = 0;

        //                totalNumerator = totalNumerator + data.CmaNumerator;
        //                totalDenominator = totalDenominator + data.CmaDenominator;
        //                decimal? calculation = totalDenominator > 0 ? (totalNumerator / totalDenominator) * 100 : 0;
        //                calculation = Math.Round(calculation.GetValueOrDefault(), 4);
        //                financialYearValue = calculation;
        //            }
        //            else if (year == currentYear + 1)
        //            {
        //                decimal? totalNumerator = 0;
        //                decimal? totalDenominator = 0;

        //                totalNumerator = totalNumerator + data.CmaNumerator;
        //                totalDenominator = totalDenominator + data.CmaDenominator;
        //                decimal? calculation = totalDenominator > 0 ? (totalNumerator / totalDenominator) * 100 : 0;
        //                calculation = Math.Round(calculation.GetValueOrDefault(), 4);
        //                comingYearValue = calculation;
        //            }
        //            if (year == currentYear + 1 && logDate.Month == currentMonth)
        //            {
        //                decimal? totalNumerator = 0;
        //                decimal? totalDenominator = 0;

        //                totalNumerator = totalNumerator + data.CmaNumerator;
        //                totalDenominator = totalDenominator + data.CmaDenominator;
        //                decimal? calculation = totalDenominator > 0 ? (totalNumerator / totalDenominator) * 100 : 0;
        //                calculation = Math.Round(calculation.GetValueOrDefault(), 4);
        //                thisMonthValue = calculation;
        //            }
        //            if (logDate >= DateTime.Now.AddDays(-7))
        //            {
        //                decimal? totalNumerator = 0;
        //                decimal? totalDenominator = 0;

        //                totalNumerator = totalNumerator + data.CmaNumerator;
        //                totalDenominator = totalDenominator + data.CmaDenominator;
        //                decimal? calculation = totalDenominator > 0 ? (totalNumerator / totalDenominator) * 100 : 0;
        //                calculation = Math.Round(calculation.GetValueOrDefault(), 4);
        //                thisWeekValue = calculation;
        //            }

        //            responseKpiMA.financialYear = financialYearValue;
        //            responseKpiMA.ComingYear = comingYearValue;
        //            responseKpiMA.thisMonth = thisMonthValue;
        //            responseKpiMA.thisWeek = thisWeekValue;
        //        }
        //        return GetResponseModel(Constants.httpCodeSuccess, responseKpiMA, "Success", true);

        //    }

        //    catch (Exception ex)
        //    {
        //        return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
        //    }
        //}

        public ResponseModelLogbook GetScadaLogbook(string? siteName, DateTime? LogDate)
        {
            try
            {
                var getSiteName = _crmsContext.ScMainSites.SingleOrDefault(data => data.MainSite == siteName);

                List<TurbineList> turbineLists = new List<TurbineList>();
                if (getSiteName == null)
                {
                    return GetResponseModelLogbook(Constants.httpCodeSuccess, null, "Sitename is not available.", false, false,0);
                }
                var pmSchedulesQuery = _omsContext.MstPmSchedules
                    .Where(d => d.CreatedOn.Value.Date == LogDate.Value.Date)
                    .ToList();


                var machineStaticDetailQuery = _crmsContext.VScAllMachineStaticDetailOmsPbis
                    .Where(e => e.MainSite == siteName)
                    .ToList();


                var getDetails = pmSchedulesQuery
                    .Where(d => machineStaticDetailQuery.Any(e => e.SapFuncLocCode == d.FunctionalLocation))
                    .ToList();

                if (getDetails.Count != 0)
                {

                    foreach (var list in getDetails)
                    {
                        TurbineList turbine = new TurbineList();
                        turbine.Turbine = list.FunctionalLocation;
                        turbineLists.Add(turbine);
                    }


                    return GetResponseModelLogbook(Constants.httpCodeSuccess, turbineLists, "yes", true, true,0);

                }
                else
                {
                    var getTurbineList = _crmsContext.VScAllMachineStaticDetailOmsPbis
                        .Where(d => d.MainSite == siteName).ToList();
                    foreach (var list in getTurbineList)
                    {
                        TurbineList turbine = new TurbineList();
                        turbine.Turbine = list.SapFuncLocCode;
                        turbineLists.Add(turbine);
                    }
                    return GetResponseModelLogbook(Constants.httpCodeSuccess, turbineLists, "yes", true, false,0);
                }
                return GetResponseModelLogbook(Constants.httpCodeSuccess, null, "No Data Found", false, false,0);
            }
            catch (Exception ex)
            {

                return GetResponseModelLogbook(Constants.httpCodeSuccess, null, ex.Message, false, false,0);
            }
        }
        //public ResponseModel GetKpiGA(string? userSite)
        //{
        //    try
        //    {
        //        var responseKpiMA = new ResponseKpiMA();
        //        var currentYear = DateTime.Now.Year;
        //        var currentMonth = DateTime.Now.Month;
        //        var financialYearStart = new DateTime(currentYear, 4, 1);
        //        var previousYearStart = new DateTime(currentYear - 1, 4, 1);
        //        if (DateTime.Now < financialYearStart)
        //        {
        //            currentYear--;
        //        }

        //        // var getSite = _crmsContext.ScMainSites
        //        //    .FirstOrDefault(d => d.MainSite == userSite).MainSiteCode;
        //        // if (getSite == null)
        //        // {
        //        //     return GetResponseModel(Constants.httpCodeSuccess, null, "Site is not present", false);
        //        // }

        //        // var logDates = _crmsContext.VCrmGenerationAllIndiaOmsPbis
        //        //.Where(d => d.DateOfGen.HasValue && d.DateOfGen.Value.Date >=
        //        // previousYearStart && d.MainSiteCode == getSite).ToList();

        //        var logDates = _crmsContext.VCrmGenerationAllIndiaOmsPbis
        //      .Where(d => d.DateOfGen.HasValue && d.DateOfGen.Value.Date >=
        //      previousYearStart).ToList();

        //        decimal? financialYearValue = 0;
        //        decimal? comingYearValue = 0;
        //        decimal? thisMonthValue = 0;
        //        decimal? thisWeekValue = 0;

        //        foreach (var data in logDates)
        //        {

        //            DateTime? logDateNullable = data.DateOfGen;
        //            DateTime logDate = logDateNullable ?? DateTime.MinValue;
        //            int year = logDate.Year;

        //            if (logDate <= financialYearStart)
        //            {

        //                if (logDate.Year == financialYearStart.Year - 1 && logDate.Month >= 4)

        //                {
        //                    year++;
        //                }
        //            }
        //            else
        //            {
        //                year++;
        //            }

        //            decimal? totalNumerator = 0;
        //            decimal? totalDenominator = 0;

        //            totalNumerator = totalNumerator + data.CgaNumerator;
        //            totalDenominator = totalDenominator + data.CgaDenominator;
        //            decimal? calculation = totalDenominator > 0 ? (totalNumerator / totalDenominator) * 100 : 0;
        //            calculation = Math.Round(calculation.GetValueOrDefault(), 4);

        //            if (year == currentYear)
        //            {
        //                financialYearValue = calculation;
        //            }
        //            else if (year == currentYear + 1)
        //            {
        //                comingYearValue = calculation;
        //            }

        //            if (year == currentYear + 1 && logDate.Month == currentMonth)
        //            {
        //                thisMonthValue = calculation;
        //            }

        //            if (logDate >= DateTime.Now.AddDays(-7))
        //            {
        //                thisWeekValue = calculation;
        //            }
        //        }

        //        responseKpiMA.financialYear = financialYearValue;
        //        responseKpiMA.ComingYear = comingYearValue;
        //        responseKpiMA.thisMonth = thisMonthValue;
        //        responseKpiMA.thisWeek = thisWeekValue;

        //        return GetResponseModel(Constants.httpCodeSuccess, responseKpiMA, "Success", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
        //    }
        //}
        public ResponseModel GetPKSiteToDoList()
        {
            try
            {
                var allList = _szFleetMgrContext.PkSiteToDoLists.ToList();
                var countOpen = 0;
                var countClose = 0;
                var countTotal = 0;
                foreach (var list in allList)
                {
                    if (list.Status == "Open")
                    {
                        countOpen += 1;
                    }
                    else
                    {
                        countClose += 1;
                    }

                }
                countTotal = countClose + countOpen;
                double percentage = (countOpen * 100.0) / countTotal;
                var getData = new
                {
                    Name = "Site To Do List",
                    Open = countOpen,
                    Percentage = (int?)percentage

                };
                return GetResponseModel(Constants.httpCodeSuccess, getData, "Successfull", true);

            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetSPInspection()
        {
            try
            {
                var allList = _szFleetMgrContext.PKProjectsInspections.ToList();
                var countOpen = 0;
                var countClose = 0;
                var countTotal = 0;
                foreach (var list in allList)
                {
                    if (list.Status == "Open")
                    {
                        countOpen += 1;
                    }
                    else
                    {
                        countClose += 1;
                    }

                }
                countTotal = countClose + countOpen;
                double percentage = (countOpen * 100.0) / countTotal;
                var getData = new
                {
                    Name = "Special Projects Inspection",
                    Open = countOpen,
                    Percentage = (int?)percentage

                };
                return GetResponseModel(Constants.httpCodeSuccess, getData, "Successfull", true);

            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetWorkOrderManagement()
        {
            try
            {
                var allList = _szFleetMgrContext.PKWorkOrderMangements.ToList();
                var countOpen = 0;
                var countClose = 0;
                var countTotal = 0;
                foreach (var list in allList)
                {
                    if (list.Status == "Open")
                    {
                        countOpen += 1;
                    }
                    else
                    {
                        countClose += 1;
                    }

                }
                countTotal = countClose + countOpen;
                double percentage = (countOpen * 100.0) / countTotal;
                var getData = new
                {
                    Name = "Work Order Management",
                    Open = countOpen,
                    Percentage = (int?)percentage

                };
                return GetResponseModel(Constants.httpCodeSuccess, getData, "Successfull", true);

            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetPMPlanning()
        {
            try
            {
                var allList = _szFleetMgrContext.PKPMPlannings.ToList();
                var countOpen = 0;
                var countClose = 0;
                var countTotal = 0;
                foreach (var list in allList)
                {
                    if (list.Status == "Open")
                    {
                        countOpen += 1;
                    }
                    else
                    {
                        countClose += 1;
                    }

                }
                countTotal = countClose + countOpen;
                double percentage = (countOpen * 100.0) / countTotal;
                var getData = new
                {
                    Name = "PM Planning",
                    Open = countOpen,
                    Percentage = (int?)percentage

                };
                return GetResponseModel(Constants.httpCodeSuccess, getData, "Successfull", true);

            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetLubricationPlanning()
        {
            try
            {
                var allList = _szFleetMgrContext.PKLubricationPlannings.ToList();
                var countOpen = 0;
                var countClose = 0;
                var countTotal = 0;
                foreach (var list in allList)
                {
                    if (list.Status == "Open")
                    {
                        countOpen += 1;
                    }
                    else
                    {
                        countClose += 1;
                    }

                }
                countTotal = countClose + countOpen;
                double percentage = (countOpen * 100.0) / countTotal;
                var getData = new
                {
                    Name = "Lubrication Planning",
                    Open = countOpen,
                    Percentage = (int?)percentage

                };
                return GetResponseModel(Constants.httpCodeSuccess, getData, "Successfull", true);

            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetTCIPlanning()
        {
            try
            {
                var allList = _szFleetMgrContext.PKTCIPlannings.ToList();
                var countOpen = 0;
                var countClose = 0;
                var countTotal = 0;
                foreach (var list in allList)
                {
                    if (list.Status == "Open")
                    {
                        countOpen += 1;
                    }
                    else
                    {
                        countClose += 1;
                    }

                }
                countTotal = countClose + countOpen;
                double percentage = (countOpen * 100.0) / countTotal;
                var getData = new
                {
                    Name = "TCI Planning",
                    Open = countOpen,
                    Percentage = (int?)percentage

                };
                return GetResponseModel(Constants.httpCodeSuccess, getData, "Successfull", true);

            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetBDPlanning()
        {
            try
            {
                var allList = _szFleetMgrContext.PKBDPlannings.ToList();
                var countOpen = 0;
                var countClose = 0;
                var countTotal = 0;
                foreach (var list in allList)
                {
                    if (list.Status == "Open")
                    {
                        countOpen += 1;
                    }
                    else
                    {
                        countClose += 1;
                    }

                }
                countTotal = countClose + countOpen;
                double percentage = (countOpen * 100.0) / countTotal;
                var getData = new
                {
                    Name = "BD Planning",
                    Open = countOpen,
                    Percentage = (int?)percentage

                };
                return GetResponseModel(Constants.httpCodeSuccess, getData, "Successfull", true);

            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetOilProcPlanning()
        {
            try
            {
                var allList = _szFleetMgrContext.PKOilProcessPlannings.ToList();
                var countOpen = 0;
                var countClose = 0;
                var countTotal = 0;
                foreach (var list in allList)
                {
                    if (list.Status == "Open")
                    {
                        countOpen += 1;
                    }
                    else
                    {
                        countClose += 1;
                    }

                }
                countTotal = countClose + countOpen;
                double percentage = (countOpen * 100.0) / countTotal;
                var getData = new
                {
                    Name = "Oil Process Planning",
                    Open = countOpen,
                    Percentage = (int?)percentage

                };
                return GetResponseModel(Constants.httpCodeSuccess, getData, "Successfull", true);

            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetScadaInfraPM()
        {
            try
            {
                var allList = _szFleetMgrContext.PKScadaInfraPMs.ToList();
                var countOpen = 0;
                var countClose = 0;
                var countTotal = 0;
                foreach (var list in allList)
                {
                    if (list.Status == "Open")
                    {
                        countOpen += 1;
                    }
                    else
                    {
                        countClose += 1;
                    }

                }
                countTotal = countClose + countOpen;
                double percentage = (countOpen * 100.0) / countTotal;
                var getData = new
                {
                    Name = "Scada Infrastructure PM",
                    Open = countOpen,
                    Percentage = (int?)percentage

                };
                return GetResponseModel(Constants.httpCodeSuccess, getData, "Successfull", true);

            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetTrainingPlanning()
        {
            try
            {
                var allList = _szFleetMgrContext.PKTrainingPlannings.ToList();
                var countOpen = 0;
                var countClose = 0;
                var countTotal = 0;
                foreach (var list in allList)
                {
                    if (list.Status == "Open")
                    {
                        countOpen += 1;
                    }
                    else
                    {
                        countClose += 1;
                    }

                }
                countTotal = countClose + countOpen;
                double percentage = (countOpen * 100.0) / countTotal;
                var getData = new
                {
                    Name = "Training Planning",
                    Open = countOpen,
                    Percentage = (int?)percentage

                };
                return GetResponseModel(Constants.httpCodeSuccess, getData, "Successfull", true);

            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetResourcePlanning()
        {
            try
            {
                var allList = _szFleetMgrContext.PKResourcePlannings.ToList();
                var available = 0;
                var unavailable = 0;
                var Total = 0;
                foreach (var list in allList)
                {
                    if (list.Status == "Available")
                    {
                        available += 1;
                    }
                    else
                    {
                        unavailable += 1;
                    }

                }
                Total = available + unavailable;
                double percentage = (available * 100.0) / Total;
                var getData = new
                {
                    Name = "Resource Planning",
                    Available = available,
                    Unavailable = unavailable,
                    total = Total,
                    Percentage = (int?)percentage

                };
                return GetResponseModel(Constants.httpCodeSuccess, getData, "Successfull", true);

            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel KpiTypeCapacityPLFMA(string? siteName)
        {
            try
            {
                DataSet ds = new DataSet();

                var conn = _szFleetMgrContext.Database.GetDbConnection();
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "sp_KPI_Tyoe_Capasity_PLF_MA";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@SiteName", siteName));

                    DbProviderFactory factory = DbProviderFactories.GetFactory(conn);
                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds);
                }
                List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Dictionary<string, string> rowData = new Dictionary<string, string>();
                    foreach (DataColumn column in ds.Tables[0].Columns)
                    {
                        rowData[column.ColumnName] = row[column] != DBNull.Value ? row[column].ToString() : null;
                    }
                    data.Add(rowData);
                }
                return GetResponseModel(Constants.httpCodeSuccess, data, "Data Retrieved Successfully", true);
            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeFailure,null,ex.Message,false);
            }
        }
        public ResponseModel GetInventoryPlanning()
        {
            try
            {
                var allList = _szFleetMgrContext.PKInventoryPlannings.ToList();
                var available = 0;
                var unavailable = 0;
                var Total = 0;
                foreach (var list in allList)
                {
                    if (list.Status == "Available")
                    {
                        available += 1;
                    }
                    else
                    {
                        unavailable += 1;
                    }

                }
                Total = available + unavailable;
                double percentage = (available * 100.0) / Total;
                var getData = new
                {
                    Name = "Inventory Planning",
                    Available = available,
                    Unavailable = unavailable,
                    total = Total,
                    Percentage = (int?)percentage

                };
                return GetResponseModel(Constants.httpCodeSuccess, getData, "Successfull", true);

            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetVehiclePlanning()
        {
            try
            {
                var allList = _szFleetMgrContext.PKVehiclePlannings.ToList();
                var available = 0;
                var unavailable = 0;
                var Total = 0;
                foreach (var list in allList)
                {
                    if (list.Status == "Available")
                    {
                        available += 1;
                    }
                    else
                    {
                        unavailable += 1;
                    }

                }
                Total = available + unavailable;
                double percentage = (available * 100.0) / Total;
                var getData = new
                {
                    Name = "Vehicle Planning",
                    Available = available,
                    Unavailable = unavailable,
                    total = Total,
                    Percentage = (int?)percentage

                };
                return GetResponseModel(Constants.httpCodeSuccess, getData, "Successfull", true);

            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetWhyAnalysis(DateTime? getDate, int? getWeek)
        {
            try
            {
                List<WhyAnalysis> newAnalysisList = new List<WhyAnalysis>();
                List<WhyAnalysis> WTGAnalysisList = new List<WhyAnalysis>();
                TimeSpan tenHours = TimeSpan.FromHours(10);

                if (getDate != null && getWeek != null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "Select Date or Week Number", false);
                }

                if (getDate != null)
                {
                    var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();

                    string connectionString = configuration.GetConnectionString("MyConnectionString");

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand("sp_why_analysis_wtg", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@getDate", getDate);
                            command.Parameters.AddWithValue("@getWeek", getWeek);
                            connection.Open();

                            if (getDate != null)
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        WTGAnalysisList.Add(new WhyAnalysis
                                        {
                                            AnalysisDate = getDate,
                                            GrandTotal = reader["GrandTotal"].ToString(),
                                            State = reader["state"].ToString(),
                                            Site = reader["site"].ToString(),
                                            Section = reader["section"].ToString(),
                                            SapCode = reader["SapCode"].ToString(),
                                            ModelName = reader["ModelName"].ToString(),
                                            TowerType = reader["TowerType"].ToString(),
                                            Status="",
                                            Week = CalculateWeekNumber(getDate.Value.Date),
                                            CheckMark = true
                                        });
                                    }

                                }
                            }

                            if (getWeek != null)
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        WTGAnalysisList.Add(new WhyAnalysis
                                        {
                                            AnalysisDate = null,
                                            GrandTotal = reader["GrandTotal"].ToString(),
                                            State = reader["state"].ToString(),
                                            Site = reader["site"].ToString(),
                                            Section = reader["section"].ToString(),
                                            SapCode = reader["SapCode"].ToString(),
                                            ModelName = reader["ModelName"].ToString(),
                                            TowerType = reader["TowerType"].ToString(),
                                            FromDate = reader["FromDate"].ToString(),
                                            ToDate = reader["ToDate"].ToString(),
                                            Week = getWeek,
                                            Status="",
                                            CheckMark = true
                                        });
                                    }

                                }
                            }
                        }
                    }


                    var query = from detail in _crmsContext.CrmBreakdownDetails
                                where detail.BdStartTime >= getDate
                                    && detail.BdStartTime <= getDate.Value.AddDays(1)
                                    && detail.TotalDuration >= 10
                                select detail;

                    var checkCRMSBreakdown = query.ToList();

                    if (checkCRMSBreakdown.Count == 0 && WTGAnalysisList.Count == 0)
                    {
                        return GetResponseModel(Constants.httpCodeSuccess, null, "No Breakdown Retrieve.", false);
                    }

                    else if (checkCRMSBreakdown.Count != 0 && WTGAnalysisList.Count != 0)
                    {
                        foreach (var detail2 in WTGAnalysisList)
                        {
                            var existingAnalysis2 = _szFleetMgrContext.WhyAnalyses
                             .SingleOrDefault(d => d.AnalysisDate.Value.Date == getDate.Value.Date && d.SapCode == detail2.SapCode);
                            if (existingAnalysis2 != null)
                            {
                                WhyAnalysis newWhyAnalysis = new WhyAnalysis();

                                newWhyAnalysis.Id = existingAnalysis2.Id;
                                newWhyAnalysis.AnalysisDate = getDate;
                                newWhyAnalysis.GrandTotal = detail2.GrandTotal;
                                newWhyAnalysis.State = existingAnalysis2.State;
                                newWhyAnalysis.Site = existingAnalysis2.Site;
                                newWhyAnalysis.SapCode = existingAnalysis2.SapCode;
                                newWhyAnalysis.ModelName = existingAnalysis2.ModelName;
                                newWhyAnalysis.TowerType = existingAnalysis2.TowerType;
                                newWhyAnalysis.Week = CalculateWeekNumber(getDate.Value.Date);
                                newWhyAnalysis.Remarks1 = existingAnalysis2.Remarks1;
                                newWhyAnalysis.Remarks2 = existingAnalysis2.Remarks2;
                                newWhyAnalysis.StandardRemarks = existingAnalysis2.StandardRemarks;
                                newWhyAnalysis.OverallActionItem = existingAnalysis2.OverallActionItem;
                                newWhyAnalysis.Section = existingAnalysis2.Section;
                                newWhyAnalysis.MainBucket = existingAnalysis2.MainBucket;
                                newWhyAnalysis.SubBucket = existingAnalysis2.SubBucket;
                                newWhyAnalysis.Status = existingAnalysis2.Status;
                                newWhyAnalysis.CheckMark = true;
                                
                                newAnalysisList.Add(newWhyAnalysis);

                            }


                            else
                            {
                                var updateAnalysis2 = _crmsContext.VScAllMachineStaticDetailOmsPbis
                               .FirstOrDefault(d => d.MainSite == detail2.Site && d.SapFuncLocCode == detail2.SapCode);

                                if (updateAnalysis2 != null)
                                {
                                    WhyAnalysis newWhyAnalysis = new WhyAnalysis();
                                    newWhyAnalysis.AnalysisDate = getDate;
                                    newWhyAnalysis.GrandTotal = detail2.GrandTotal;
                                    newWhyAnalysis.State = detail2.State;
                                    newWhyAnalysis.Site = detail2.Site;
                                    newWhyAnalysis.Section = detail2.Section;
                                    newWhyAnalysis.SapCode = detail2.SapCode;
                                    newWhyAnalysis.ModelName = detail2.ModelName;
                                    newWhyAnalysis.TowerType = detail2.TowerType;
                                    newWhyAnalysis.Week = CalculateWeekNumber(getDate.Value.Date);
                                    newWhyAnalysis.CheckMark = true;
                                    newWhyAnalysis.Status = "";
                                    newAnalysisList.Add(newWhyAnalysis);
                                }
                            }
                        }

                        foreach (var detail in checkCRMSBreakdown)
                        {

                            var existingAnalysis = _szFleetMgrContext.WhyAnalyses
                            .SingleOrDefault(d => d.AnalysisDate.Value.Date == getDate.Value.Date && d.SapCode == detail.SapFuncLocCode);

                            if (existingAnalysis != null)
                            {
                                WhyAnalysis newWhyAnalysis = new WhyAnalysis();

                                newWhyAnalysis.Id = existingAnalysis.Id;
                                newWhyAnalysis.AnalysisDate = getDate;
                                newWhyAnalysis.GrandTotal = detail.TotalDuration.ToString();
                                newWhyAnalysis.State = existingAnalysis.State;
                                newWhyAnalysis.Site = existingAnalysis.Site;
                                newWhyAnalysis.SapCode = existingAnalysis.SapCode;
                                newWhyAnalysis.ModelName = existingAnalysis.ModelName;
                                newWhyAnalysis.TowerType = existingAnalysis.TowerType;
                                newWhyAnalysis.Week = CalculateWeekNumber(getDate.Value.Date);
                                newWhyAnalysis.Remarks1 = existingAnalysis.Remarks1;
                                newWhyAnalysis.Remarks2 = existingAnalysis.Remarks2;
                                newWhyAnalysis.StandardRemarks = existingAnalysis.StandardRemarks;
                                newWhyAnalysis.OverallActionItem = existingAnalysis.OverallActionItem;
                                newWhyAnalysis.Section = existingAnalysis.Section;
                                newWhyAnalysis.MainBucket = existingAnalysis.MainBucket;
                                newWhyAnalysis.SubBucket = existingAnalysis.SubBucket;
                                newWhyAnalysis.CheckMark = true;
                                newWhyAnalysis.Status = existingAnalysis.Status;
                                newAnalysisList.Add(newWhyAnalysis);

                            }
                            else
                            {
                                var updateAnalysis = _crmsContext.VScAllMachineStaticDetailOmsPbis
                               .FirstOrDefault(d => d.MainSiteCode == detail.MainSiteCode && d.SapFuncLocCode == detail.SapFuncLocCode);

                                if (updateAnalysis != null)
                                {
                                    WhyAnalysis newWhyAnalysis = new WhyAnalysis();
                                    newWhyAnalysis.AnalysisDate = getDate;
                                    newWhyAnalysis.GrandTotal = detail.TotalDuration.ToString();
                                    newWhyAnalysis.State = updateAnalysis.State;
                                    newWhyAnalysis.Site = updateAnalysis.Site;
                                    newWhyAnalysis.Section = updateAnalysis.Area;
                                    newWhyAnalysis.SapCode = detail.SapFuncLocCode;
                                    newWhyAnalysis.ModelName = updateAnalysis.WtgModelName;
                                    newWhyAnalysis.TowerType = updateAnalysis.Towertype;
                                    newWhyAnalysis.Week = CalculateWeekNumber(getDate.Value.Date);
                                    newWhyAnalysis.CheckMark = true;
                                    newWhyAnalysis.Status = "";
                                    newAnalysisList.Add(newWhyAnalysis);
                                }
                            }


                        }
                    }

                    else if (checkCRMSBreakdown.Count != 0)
                    {
                        foreach (var detail in checkCRMSBreakdown)
                        {
                            var existingAnalysis = _szFleetMgrContext.WhyAnalyses
                               .SingleOrDefault(d => d.AnalysisDate.Value.Date == getDate.Value.Date && d.SapCode == detail.SapFuncLocCode);
                            if (existingAnalysis != null)
                            {
                                WhyAnalysis newWhyAnalysis = new WhyAnalysis();

                                newWhyAnalysis.Id = existingAnalysis.Id;
                                newWhyAnalysis.AnalysisDate = getDate;
                                newWhyAnalysis.GrandTotal = detail.TotalDuration.ToString();
                                newWhyAnalysis.State = existingAnalysis.State;
                                newWhyAnalysis.Site = existingAnalysis.Site;
                                newWhyAnalysis.SapCode = existingAnalysis.SapCode;
                                newWhyAnalysis.ModelName = existingAnalysis.ModelName;
                                newWhyAnalysis.TowerType = existingAnalysis.TowerType;
                                newWhyAnalysis.Week = CalculateWeekNumber(getDate.Value.Date);
                                newWhyAnalysis.Remarks1 = existingAnalysis.Remarks1;
                                newWhyAnalysis.Remarks2 = existingAnalysis.Remarks2;
                                newWhyAnalysis.StandardRemarks = existingAnalysis.StandardRemarks;
                                newWhyAnalysis.OverallActionItem = existingAnalysis.OverallActionItem;
                                newWhyAnalysis.Section = existingAnalysis.Section;
                                newWhyAnalysis.MainBucket = existingAnalysis.MainBucket;
                                newWhyAnalysis.Status = existingAnalysis.Status;
                                newWhyAnalysis.SubBucket = existingAnalysis.SubBucket;
                                newWhyAnalysis.CheckMark = true;
                                newAnalysisList.Add(newWhyAnalysis);

                            }

                            else
                            {

                                var updateAnalysis = _crmsContext.VScAllMachineStaticDetailOmsPbis
                               .FirstOrDefault(d => d.MainSiteCode == detail.MainSiteCode && d.SapFuncLocCode == detail.SapFuncLocCode);
                                if (updateAnalysis != null)
                                {
                                    WhyAnalysis newWhyAnalysis = new WhyAnalysis();
                                    newWhyAnalysis.AnalysisDate = getDate;
                                    newWhyAnalysis.GrandTotal = detail.TotalDuration.ToString();
                                    newWhyAnalysis.State = updateAnalysis.State;
                                    newWhyAnalysis.Site = updateAnalysis.Site;
                                    newWhyAnalysis.Section = updateAnalysis.Area;
                                    newWhyAnalysis.SapCode = detail.SapFuncLocCode;
                                    newWhyAnalysis.ModelName = updateAnalysis.WtgModelName;
                                    newWhyAnalysis.TowerType = updateAnalysis.Towertype;
                                    newWhyAnalysis.Week = CalculateWeekNumber(getDate.Value.Date);
                                    newWhyAnalysis.Status = "";
                                    newWhyAnalysis.CheckMark = true;
                                    newAnalysisList.Add(newWhyAnalysis);
                                }
                            }
                        }
                    }

                    else if (WTGAnalysisList.Count != 0)
                    {
                        foreach (var detail2 in WTGAnalysisList)
                        {
                            var existingAnalysis = _szFleetMgrContext.WhyAnalyses
                               .SingleOrDefault(d => d.AnalysisDate.Value.Date == getDate.Value.Date && d.SapCode == detail2.SapCode);
                            if (existingAnalysis != null)
                            {
                                WhyAnalysis newWhyAnalysis = new WhyAnalysis();

                                newWhyAnalysis.Id = existingAnalysis.Id;
                                newWhyAnalysis.AnalysisDate = getDate;
                                newWhyAnalysis.GrandTotal = detail2.GrandTotal;
                                newWhyAnalysis.State = existingAnalysis.State;
                                newWhyAnalysis.Site = existingAnalysis.Site;
                                newWhyAnalysis.SapCode = existingAnalysis.SapCode;
                                newWhyAnalysis.ModelName = existingAnalysis.ModelName;
                                newWhyAnalysis.TowerType = existingAnalysis.TowerType;
                                newWhyAnalysis.Week = CalculateWeekNumber(getDate.Value.Date);
                                newWhyAnalysis.Remarks1 = existingAnalysis.Remarks1;
                                newWhyAnalysis.Remarks2 = existingAnalysis.Remarks2;
                                newWhyAnalysis.StandardRemarks = existingAnalysis.StandardRemarks;
                                newWhyAnalysis.OverallActionItem = existingAnalysis.OverallActionItem;
                                newWhyAnalysis.Section = existingAnalysis.Section;
                                newWhyAnalysis.MainBucket = existingAnalysis.MainBucket;
                                newWhyAnalysis.Status = existingAnalysis.Status;
                                newWhyAnalysis.SubBucket = existingAnalysis.SubBucket;
                                newWhyAnalysis.CheckMark = true;
                                newAnalysisList.Add(newWhyAnalysis);

                            }

                            else
                            {

                                var updateAnalysis = _crmsContext.VScAllMachineStaticDetailOmsPbis
                               .FirstOrDefault(d => d.MainSite == detail2.Site && d.SapFuncLocCode == detail2.SapCode);
                                if (updateAnalysis != null)
                                {
                                    WhyAnalysis newWhyAnalysis = new WhyAnalysis();
                                    newWhyAnalysis.AnalysisDate = getDate;
                                    newWhyAnalysis.GrandTotal = detail2.GrandTotal;
                                    newWhyAnalysis.State = updateAnalysis.State;
                                    newWhyAnalysis.Site = updateAnalysis.Site;
                                    newWhyAnalysis.Section = updateAnalysis.Area;
                                    newWhyAnalysis.SapCode = detail2.SapCode;
                                    newWhyAnalysis.ModelName = updateAnalysis.WtgModelName;
                                    newWhyAnalysis.TowerType = updateAnalysis.Towertype;
                                    newWhyAnalysis.Week = CalculateWeekNumber(getDate.Value.Date);
                                    newWhyAnalysis.Status = "";
                                    newWhyAnalysis.CheckMark = true;
                                    newAnalysisList.Add(newWhyAnalysis);
                                }
                            }
                        }
                    }
                    else
                    {
                        return GetResponseModel(Constants.httpCodeSuccess, null, "No Date Available", false);

                    }
                }

                if (getWeek != null)
                {
                    var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();

                    string connectionString = configuration.GetConnectionString("MyConnectionString");

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        using (SqlCommand command = new SqlCommand("sp_why_analysis_wtg", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@getDate", getDate);
                            command.Parameters.AddWithValue("@getWeek", getWeek);
                            connection.Open();

                            if (getDate != null)
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        WTGAnalysisList.Add(new WhyAnalysis
                                        {
                                            
                                            GrandTotal = reader["GrandTotal"].ToString(),
                                            State = reader["state"].ToString(),
                                            Site = reader["site"].ToString(),
                                            Section = reader["section"].ToString(),
                                            SapCode = reader["SapCode"].ToString(),
                                            ModelName = reader["ModelName"].ToString(),
                                            TowerType = reader["TowerType"].ToString(),
                                            Status="",
                                            Week = CalculateWeekNumber(getDate.Value.Date),
                                            CheckMark = true
                                        });
                                    }

                                }
                            }

                            if (getWeek != null)
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        WTGAnalysisList.Add(new WhyAnalysis
                                        {
                                            AnalysisDate = getDate,
                                            GrandTotal = reader["GrandTotal"].ToString(),
                                            State = reader["state"].ToString(),
                                            Site = reader["site"].ToString(),
                                            Section = reader["section"].ToString(),
                                            SapCode = reader["SapCode"].ToString(),
                                            ModelName = reader["ModelName"].ToString(),
                                            TowerType = reader["TowerType"].ToString(),
                                            FromDate = reader["FromDate"].ToString(),
                                            ToDate = reader["ToDate"].ToString(),
                                            Status ="",
                                            Week = getWeek,
                                            CheckMark = true
                                        });
                                    }

                                }
                            }
                        }
                    }


                    var currentYear = DateTime.Now.Year;
                    DateTime jan1 = new DateTime(currentYear, 1, 1);

                    //
                    if (jan1.DayOfWeek != DayOfWeek.Tuesday)
                    {
                        
                        int daysToAdd = ((int)DayOfWeek.Tuesday - (int)jan1.DayOfWeek + 7) % 7;
                        jan1 = jan1.AddDays(daysToAdd);
                    }


                    int weekValue = getWeek.Value;

                    DateTime startOfWeek = jan1.AddDays((weekValue-1) * 7);

                    
                    int daysUntilFirstDayOfWeek = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)startOfWeek.DayOfWeek;
                    if (daysUntilFirstDayOfWeek > 0)
                    {
                        startOfWeek = startOfWeek.AddDays(daysUntilFirstDayOfWeek+1);
                    }
                    startOfWeek = startOfWeek;
                    DateTime endOfWeek = startOfWeek.AddDays(6);

                    var query = from record in _crmsContext.CrmBreakdownDetails
                                where record.BdStartTime >= startOfWeek && record.BdStartTime <= endOfWeek
                                && record.BdRemark != "Contract Violation"
                                group record by new { record.SapFuncLocCode, record.MainSiteCode } into grouped
                                where grouped.Sum(x => x.TotalDuration) > 50
                                select new
                                {
                                    MAIN_SITE_CODE = grouped.Key.MainSiteCode,
                                    TotalDuration = (decimal)Math.Round(grouped.Sum(x => x.TotalDuration) ?? 0, 2),
                                    SAP_FUNC_LOC_CODE = grouped.Key.SapFuncLocCode
                                };

    
                    var checkCRMSBreakdown = query.ToList();



                    if (checkCRMSBreakdown.Count == 0 && WTGAnalysisList.Count == 0)
                    {
                        return GetResponseModel(Constants.httpCodeSuccess, null, "No Breakdown Retrieve.", false);
                    }

                    else if (checkCRMSBreakdown.Count != 0 && WTGAnalysisList.Count != 0)
                    {
                        foreach (var detail2 in WTGAnalysisList)
                        {
                            var existingAnalysis2 = _szFleetMgrContext.WhyAnalyses
                             .SingleOrDefault(d => d.AnalysisDate.Value.Date == startOfWeek && d.SapCode == detail2.SapCode);
                            if (existingAnalysis2 != null)
                            {
                                WhyAnalysis newWhyAnalysis = new WhyAnalysis();

                                newWhyAnalysis.Id = existingAnalysis2.Id;
                                newWhyAnalysis.AnalysisDate = startOfWeek;
                                newWhyAnalysis.GrandTotal = detail2.GrandTotal;
                                newWhyAnalysis.State = existingAnalysis2.State;
                                newWhyAnalysis.Site = existingAnalysis2.Site;
                                newWhyAnalysis.SapCode = existingAnalysis2.SapCode;
                                newWhyAnalysis.ModelName = existingAnalysis2.ModelName;
                                newWhyAnalysis.TowerType = existingAnalysis2.TowerType;
                                newWhyAnalysis.Week = CalculateWeekNumber(startOfWeek);
                                newWhyAnalysis.Remarks1 = existingAnalysis2.Remarks1;
                                newWhyAnalysis.Remarks2 = existingAnalysis2.Remarks2;
                                newWhyAnalysis.StandardRemarks = existingAnalysis2.StandardRemarks;
                                newWhyAnalysis.OverallActionItem = existingAnalysis2.OverallActionItem;
                                newWhyAnalysis.Section = existingAnalysis2.Section;
                                newWhyAnalysis.MainBucket = existingAnalysis2.MainBucket;
                                newWhyAnalysis.SubBucket = existingAnalysis2.SubBucket;
                                newWhyAnalysis.CheckMark = true;
                                newWhyAnalysis.Status = existingAnalysis2.Status;
                                newWhyAnalysis.FromDate = startOfWeek.ToString("dd-MM-yyyy");
                                newWhyAnalysis.ToDate = endOfWeek.ToString("dd-MM-yyyy");
                                newAnalysisList.Add(newWhyAnalysis);

                            }


                            else
                            {
                                var updateAnalysis2 = _crmsContext.VScAllMachineStaticDetailOmsPbis
                               .FirstOrDefault(d => d.MainSite == detail2.Site && d.SapFuncLocCode == detail2.SapCode);

                                if (updateAnalysis2 != null)
                                {
                                    WhyAnalysis newWhyAnalysis = new WhyAnalysis();
                                    newWhyAnalysis.AnalysisDate = startOfWeek;
                                    newWhyAnalysis.GrandTotal = detail2.GrandTotal;
                                    newWhyAnalysis.State = detail2.State;
                                    newWhyAnalysis.Site = detail2.Site;
                                    newWhyAnalysis.Section = detail2.Section;
                                    newWhyAnalysis.SapCode = detail2.SapCode;
                                    newWhyAnalysis.ModelName = detail2.ModelName;
                                    newWhyAnalysis.TowerType = detail2.TowerType;
                                    
                                    newWhyAnalysis.Status = "";
                                    newWhyAnalysis.Week = CalculateWeekNumber(startOfWeek);
                                    newWhyAnalysis.CheckMark = true;
                                    newAnalysisList.Add(newWhyAnalysis);
                                }
                            }
                        }

                        foreach (var detail in checkCRMSBreakdown)
                        {

                            var existingAnalysis = _szFleetMgrContext.WhyAnalyses
                            .FirstOrDefault(d => d.Week == getWeek && d.SapCode == detail.SAP_FUNC_LOC_CODE);

                            if (existingAnalysis != null)
                            {
                                WhyAnalysis newWhyAnalysis = new WhyAnalysis();

                                newWhyAnalysis.Id = existingAnalysis.Id;
                                newWhyAnalysis.AnalysisDate = null;
                                newWhyAnalysis.GrandTotal = detail.TotalDuration.ToString();
                                newWhyAnalysis.State = existingAnalysis.State;
                                newWhyAnalysis.Site = existingAnalysis.Site;
                                newWhyAnalysis.SapCode = detail.SAP_FUNC_LOC_CODE;
                                newWhyAnalysis.ModelName = existingAnalysis.ModelName;
                                newWhyAnalysis.TowerType = existingAnalysis.TowerType;
                                newWhyAnalysis.Status = existingAnalysis.Status;
                                newWhyAnalysis.Week = getWeek;
                                newWhyAnalysis.Remarks1 = existingAnalysis.Remarks1;
                                newWhyAnalysis.Remarks2 = existingAnalysis.Remarks2;
                                newWhyAnalysis.StandardRemarks = existingAnalysis.StandardRemarks;
                                newWhyAnalysis.OverallActionItem = existingAnalysis.OverallActionItem;
                                newWhyAnalysis.Section = existingAnalysis.Section;
                                newWhyAnalysis.MainBucket = existingAnalysis.MainBucket;
                                newWhyAnalysis.SubBucket = existingAnalysis.SubBucket;
                                newWhyAnalysis.CheckMark = true;
                                newAnalysisList.Add(newWhyAnalysis);

                            }
                            else
                            {
                                var updateAnalysis = _crmsContext.VScAllMachineStaticDetailOmsPbis
                               .FirstOrDefault(d => d.MainSiteCode == detail.MAIN_SITE_CODE && d.SapFuncLocCode == detail.SAP_FUNC_LOC_CODE);

                                if (updateAnalysis != null)
                                {
                                    WhyAnalysis newWhyAnalysis = new WhyAnalysis();
                                    newWhyAnalysis.AnalysisDate = null;
                                    newWhyAnalysis.GrandTotal = detail.TotalDuration.ToString();
                                    newWhyAnalysis.State = updateAnalysis.State;
                                    newWhyAnalysis.Site = updateAnalysis.Site;
                                    newWhyAnalysis.Section = updateAnalysis.Area;
                                    newWhyAnalysis.SapCode = detail.SAP_FUNC_LOC_CODE;
                                    newWhyAnalysis.ModelName = updateAnalysis.WtgModelName;
                                    newWhyAnalysis.TowerType = updateAnalysis.Towertype;
                                    newWhyAnalysis.Week = getWeek;
                                    newWhyAnalysis.Status = "";
                                    newWhyAnalysis.CheckMark = true;
                                    newAnalysisList.Add(newWhyAnalysis);
                                }
                            }


                        }
                    }

                    else if (checkCRMSBreakdown.Count != 0)
                    {
                        foreach (var detail in checkCRMSBreakdown)
                        {

                            var existingAnalysis = _szFleetMgrContext.WhyAnalyses
                            .FirstOrDefault(d => d.Week == getWeek && d.SapCode == detail.SAP_FUNC_LOC_CODE);

                            if (existingAnalysis != null)
                            {
                                WhyAnalysis newWhyAnalysis = new WhyAnalysis();

                                newWhyAnalysis.Id = existingAnalysis.Id;
                                newWhyAnalysis.AnalysisDate = null;
                                newWhyAnalysis.GrandTotal = detail.TotalDuration.ToString();
                                newWhyAnalysis.State = existingAnalysis.State;
                                newWhyAnalysis.Site = existingAnalysis.Site;
                                newWhyAnalysis.SapCode = detail.SAP_FUNC_LOC_CODE;
                                newWhyAnalysis.ModelName = existingAnalysis.ModelName;
                                newWhyAnalysis.TowerType = existingAnalysis.TowerType;
                                newWhyAnalysis.Week = getWeek;
                                newWhyAnalysis.Remarks1 = existingAnalysis.Remarks1;
                                newWhyAnalysis.Remarks2 = existingAnalysis.Remarks2;
                                newWhyAnalysis.StandardRemarks = existingAnalysis.StandardRemarks;
                                newWhyAnalysis.OverallActionItem = existingAnalysis.OverallActionItem;
                                newWhyAnalysis.Section = existingAnalysis.Section;
                                newWhyAnalysis.MainBucket = existingAnalysis.MainBucket;
                                newWhyAnalysis.SubBucket = existingAnalysis.SubBucket;
                                newWhyAnalysis.CheckMark = true;
                                newWhyAnalysis.FromDate = startOfWeek.ToString("dd-MM-yyyy");
                                newWhyAnalysis.ToDate = endOfWeek.ToString("dd-MM-yyyy");
                                newWhyAnalysis.Status = existingAnalysis.Status;
                                newAnalysisList.Add(newWhyAnalysis);

                            }
                            else
                            {
                                var updateAnalysis = _crmsContext.VScAllMachineStaticDetailOmsPbis
                               .FirstOrDefault(d => d.MainSiteCode == detail.MAIN_SITE_CODE && d.SapFuncLocCode == detail.SAP_FUNC_LOC_CODE);

                                if (updateAnalysis != null)
                                {
                                    WhyAnalysis newWhyAnalysis = new WhyAnalysis();
                                    newWhyAnalysis.AnalysisDate = null;
                                    newWhyAnalysis.GrandTotal = detail.TotalDuration.ToString();
                                    newWhyAnalysis.State = updateAnalysis.State;
                                    newWhyAnalysis.Site = updateAnalysis.Site;
                                    newWhyAnalysis.Section = updateAnalysis.Area;
                                    newWhyAnalysis.SapCode = detail.SAP_FUNC_LOC_CODE;
                                    newWhyAnalysis.ModelName = updateAnalysis.WtgModelName;
                                    newWhyAnalysis.TowerType = updateAnalysis.Towertype;
                                    newWhyAnalysis.Week = getWeek;
                                    newWhyAnalysis.FromDate = startOfWeek.ToString("dd-MM-yyyy");
                                    newWhyAnalysis.ToDate = endOfWeek.ToString("dd-MM-yyyy");
                                    newWhyAnalysis.CheckMark = true;
                                    newWhyAnalysis.Status = "";
                                    newAnalysisList.Add(newWhyAnalysis);
                                }
                            }


                        }
                    }

                    else if (WTGAnalysisList.Count != 0)
                    {
                        foreach (var detail2 in WTGAnalysisList)
                        {
                            var existingAnalysis = _szFleetMgrContext.WhyAnalyses
                               .FirstOrDefault(d=>d.SapCode == detail2.SapCode);
                            if (existingAnalysis != null)
                            {
                                WhyAnalysis newWhyAnalysis = new WhyAnalysis();

                                newWhyAnalysis.Id = existingAnalysis.Id;
                                newWhyAnalysis.AnalysisDate = startOfWeek;
                                newWhyAnalysis.GrandTotal = detail2.GrandTotal;
                                newWhyAnalysis.State = existingAnalysis.State;
                                newWhyAnalysis.Site = existingAnalysis.Site;
                                newWhyAnalysis.SapCode = existingAnalysis.SapCode;
                                newWhyAnalysis.ModelName = existingAnalysis.ModelName;
                                newWhyAnalysis.TowerType = existingAnalysis.TowerType;
                                newWhyAnalysis.Week = CalculateWeekNumber(startOfWeek);
                                newWhyAnalysis.Remarks1 = existingAnalysis.Remarks1;
                                newWhyAnalysis.Remarks2 = existingAnalysis.Remarks2;
                                newWhyAnalysis.StandardRemarks = existingAnalysis.StandardRemarks;
                                newWhyAnalysis.OverallActionItem = existingAnalysis.OverallActionItem;
                                newWhyAnalysis.Section = existingAnalysis.Section;
                                newWhyAnalysis.MainBucket = existingAnalysis.MainBucket;
                                newWhyAnalysis.SubBucket = existingAnalysis.SubBucket;
                                newWhyAnalysis.Status = existingAnalysis.Status;
                                newWhyAnalysis.CheckMark = true;
                                newAnalysisList.Add(newWhyAnalysis);

                            }

                            else
                            {

                                var updateAnalysis = _crmsContext.VScAllMachineStaticDetailOmsPbis
                               .FirstOrDefault(d => d.MainSite == detail2.Site && d.SapFuncLocCode == detail2.SapCode);
                                if (updateAnalysis != null)
                                {
                                    WhyAnalysis newWhyAnalysis = new WhyAnalysis();
                                    newWhyAnalysis.AnalysisDate = startOfWeek;
                                    newWhyAnalysis.GrandTotal = detail2.GrandTotal;
                                    newWhyAnalysis.State = updateAnalysis.State;
                                    newWhyAnalysis.Site = updateAnalysis.Site;
                                    newWhyAnalysis.Section = updateAnalysis.Area;
                                    newWhyAnalysis.SapCode = detail2.SapCode;
                                    newWhyAnalysis.Status = "";
                                    newWhyAnalysis.ModelName = updateAnalysis.WtgModelName;
                                    newWhyAnalysis.TowerType = updateAnalysis.Towertype;
                                    newWhyAnalysis.Week = CalculateWeekNumber(startOfWeek);
                                    newWhyAnalysis.CheckMark = true;
                                    newAnalysisList.Add(newWhyAnalysis);
                                }
                            }
                        }
                    }
                    else
                    {
                        return GetResponseModel(Constants.httpCodeSuccess, null, "No Date Available", false);

                    }
                }

                return GetResponseModel(Constants.httpCodeSuccess, newAnalysisList, "All Date Retreive Successfully", true);


                TimeSpan ParseTotalTime(string totalTimeString)
                {
                    string[] parts = totalTimeString.Split(':');
                    if (parts.Length == 2 && int.TryParse(parts[0], out int hours) && int.TryParse(parts[1], out int minutes))
                    {
                        return new TimeSpan(hours, minutes, 0);
                    }
                    else
                    {
                        return TimeSpan.Zero;
                    }
                }

                TimeSpan ParseTotalTime2(double? totalTimeDouble)
                {
                    // Convert the double value to hours and minutes
                    int hours = (int)totalTimeDouble;
                    double? remainingMinutes = (totalTimeDouble - hours) * 60;
                    int minutes = (int)remainingMinutes;

                    // Create a TimeSpan
                    return new TimeSpan(hours, minutes, 0);
                }

                int CalculateWeekNumber(DateTime date)
                {
                    // Calculate the week number for the given date.
                    DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                    Calendar cal = dfi.Calendar;
                    int week = cal.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

                    return week;
                }

            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetWhyAnalysisDetail(int? id)
        {
            try
            {
                var analysisDetails = _szFleetMgrContext.TWhyAnalysisDetails.Where(data => data.FkAnalysisId == id).ToList();
              
                List<ObjectWhyAnalysisDetail> updateAnalysisDetail = new List<ObjectWhyAnalysisDetail>();
                foreach (var data in analysisDetails)
                {
                    var getTypeIdList = _szFleetMgrContext.TLogbookCommonMasters.SingleOrDefault(p => p.Id == data.FkTypeId);
                    ObjectWhyAnalysisDetail whyAnalysisDetail = new ObjectWhyAnalysisDetail();
                    whyAnalysisDetail.Id = data.Id;
                    whyAnalysisDetail.FkTypeId = getTypeIdList;
                    whyAnalysisDetail.Hrs = data.Hrs;
                    whyAnalysisDetail.Ai = data.Ai;
                    whyAnalysisDetail.whyDrop1 = _szFleetMgrContext.WhyReasonMasters
                        .FirstOrDefault(d=>d.Name==data.Why1) ?? null;
                    
                    whyAnalysisDetail.whyDrop2 = _szFleetMgrContext.WhyReasonMasters
                        .FirstOrDefault(d => d.Name == data.Why2)?? null;
                   
                    whyAnalysisDetail.whyDrop3 = _szFleetMgrContext.WhyReasonMasters
                        .FirstOrDefault(d => d.Name == data.Why3)?? null;
                   
                    whyAnalysisDetail.whyDrop4 = _szFleetMgrContext.WhyReasonMasters
                        .FirstOrDefault(d => d.Name == data.Why4)?? null;
                    
                    whyAnalysisDetail.whyDrop5 = _szFleetMgrContext.WhyReasonMasters
                        .FirstOrDefault(d => d.Name == data.Why5) ?? null;
                   
                    whyAnalysisDetail.whyDrop6 = _szFleetMgrContext.WhyReasonMasters
                        .FirstOrDefault(d => d.Name == data.Why6) ?? null;
                   
                    whyAnalysisDetail.FkAnalysisId = data.FkAnalysisId;
                    whyAnalysisDetail.CreatedBy = data.CreatedBy;
                    whyAnalysisDetail.CreatedDate = data.CreatedDate;
                    whyAnalysisDetail.ModifiedBy = data.ModifiedBy;
                    whyAnalysisDetail.ModifiedDate = data.ModifiedDate;
                    updateAnalysisDetail.Add(whyAnalysisDetail);
                }

                if (analysisDetails.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "Data Not Found.", false);
                }
                return GetResponseModel(Constants.httpCodeSuccess, updateAnalysisDetail, "Data Retreive Successfully.", true);

            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        //public ResponseModel GetScAllMachineStaticDetailOmsPbi()
        //{
        //    try
        //    {
        //        var allDataList = _crmsContext.VScAllMachineStaticDetailOmsPbis.ToList();

        //        if (allDataList.Count == 0)
        //        {
        //            return GetResponseModel(Constants.httpCodeSuccess, null, "No Data Found.", false);
        //        }
        //        return GetResponseModel(Constants.httpCodeSuccess, allDataList, "All Data Retreive Successfully", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
        //    }
        //}
        //public ResponseModel GetScAreaOmsPbi()
        //{
        //    try
        //    {
        //        var allDataList = _crmsContext.ScAreas.ToList();
        //        if (allDataList.Count == 0)
        //        {
        //            return GetResponseModel(Constants.httpCodeSuccess, null, "No Data Found.", false);
        //        }
        //        return GetResponseModel(Constants.httpCodeSuccess, allDataList, "All Data Retreive Successfully", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
        //    }
        //}

        //public ResponseModel GetScCustMktGroupMstOmsPbi()
        //{
        //    try
        //    {
        //        var allDataList = _crmsContext.VScCustMktGroupMstOmsPbis.ToList();
        //        if (allDataList.Count == 0)
        //        {
        //            return GetResponseModel(Constants.httpCodeSuccess, null, "No Data Found.", false);
        //        }
        //        return GetResponseModel(Constants.httpCodeSuccess, allDataList, "All Data Retreive Successfully", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
        //    }
        //}
        //public ResponseModel GetScCustSubGroupMstOmsPbi()
        //{
        //    try
        //    {
        //        var allDataList = _crmsContext.VScCustSubGroupMstOmsPbis.ToList();

        //        if (allDataList.Count == 0)
        //        {
        //            return GetResponseModel(Constants.httpCodeSuccess, null, "No Data Found.", false);
        //        }
        //        return GetResponseModel(Constants.httpCodeSuccess, allDataList, "All Data Retreive Successfully", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
        //    }
        //}
        //public ResponseModel GetAllMainSite()
        //{
        //    try
        //    {
        //        var allDataList = _crmsContext.ScMainSites.ToList();
        //        if (allDataList.Count == 0)
        //        {
        //            return GetResponseModel(Constants.httpCodeSuccess, null, "No Data Found.", false);
        //        }
        //        return GetResponseModel(Constants.httpCodeSuccess, allDataList, "All Data Retreive Successfully", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
        //    }
        //}
        //public ResponseModel GetScSiteStateMasterOmsPbi()
        //{
        //    try
        //    {
        //        //var allDataList = _szFleetMgrContext.VScSiteStateMasterOmsPbis.ToList();
        //        var allDataList = _crmsContext.VScCustMktGroupMstOmsPbis.ToList();
        //        if (allDataList.Count == 0)
        //        {
        //            return GetResponseModel(Constants.httpCodeSuccess, null, "No Data Found.", false);
        //        }
        //        return GetResponseModel(Constants.httpCodeSuccess, allDataList, "All Data Retreive Successfully", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
        //    }
        //}
        //public ResponseModel GetScStateOmsPbi()
        //{
        //    try
        //    {
        //        var allDataList = _crmsContext.ScStates.ToList();
        //        if (allDataList.Count == 0)
        //        {
        //            return GetResponseModel(Constants.httpCodeSuccess, null, "No Data Found.", false);
        //        }
        //        return GetResponseModel(Constants.httpCodeSuccess, allDataList, "All Data Retreive Successfully", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
        //    }
        //}
        //public ResponseModel GetScWtgModelMasterOmsPbi()
        //{
        //    try
        //    {
        //        var allDataList = _crmsContext.VScWtgModelMasterOmsPbis.ToList();

        //        if (allDataList.Count == 0)
        //        {
        //            return GetResponseModel(Constants.httpCodeSuccess, null, "No Data Found.", false);
        //        }
        //        return GetResponseModel(Constants.httpCodeSuccess, allDataList, "All Data Retreive Successfully", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
        //    }
        //}
        public ResponseModel GetScCountryOmsPbi()
        {
            try
            {
                var allDataList = _crmsContext.ScCountries.ToList();
                if (allDataList.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "No Data Found.", false);
                }
                return GetResponseModel(Constants.httpCodeSuccess, allDataList, "All Data Retreive Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetScStateOmsPbi()
        {
            try
            {
                var allDataList = _crmsContext.ScStates.ToList();
                if (allDataList.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "No Data Found.", false);
                }
                return GetResponseModel(Constants.httpCodeSuccess, allDataList, "All Data Retreive Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel GetStateByCountryCode(string? countryCode)
        {
            try
            {
                var state = (from a in _crmsContext.ScMainSites
                                      join b in _crmsContext.ScAreas on a.AreaCode equals b.AreaCode
                                      join c in _crmsContext.ScStates on b.StateCode equals c.StateCode
                                      join d in _crmsContext.ScCountries on c.CountryCode equals d.CountryCode
                                      where d.CountryCode == countryCode
                                      select new { c.State, c.StateCode }).Distinct().ToList();
                if (state == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null,
                        " No States Available", false);
                }
                var stateData = state.Select(state => new
                {
                    state.StateCode,
                    state.State
                }).ToList();
                return GetResponseModel(Constants.httpCodeSuccess, stateData,
                        "data", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetAreaByStateCode(string? stateCode)
        {
            try
            {
                var area = (from a in _crmsContext.ScMainSites
                            join b in _crmsContext.ScAreas on a.AreaCode equals b.AreaCode
                            where b.StateCode == stateCode
                            select new { b.Area, b.AreaCode }).Distinct().ToList();

                if (area == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null,
                        " No Areas Available", false);
                }
                var areaData = area.Select(area => new
                {
                    area.AreaCode,
                    area.Area
                }).ToList();
                return GetResponseModel(Constants.httpCodeSuccess, areaData,
                        "data", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetSiteByAreaCode(string? areaCode)
        {
            try
            {
                var site = _crmsContext.ScMainSites.Where(a => a.AreaCode == areaCode).ToList();
                if (site == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null,
                        " No Sites Available", false);
                }
                var siteData = site.Select(site => new
                {
                    site.MainSiteCode,
                    site.MainSite
                }).ToList();
                return GetResponseModel(Constants.httpCodeSuccess, siteData,
                        "data", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel AddUpdateEmployeeDetails(LogbookEmployeeDetail logbookEmployeeDetail, string username)
        {
            try
            {
              
                var existingLogbookData =  _szFleetMgrContext.TLogbookEmployeeDetails
                    .SingleOrDefault(c => c.Id == logbookEmployeeDetail.Id);

                if (existingLogbookData == null)
                {
                    var createLogbookEmployee = CreateEmployeeDetails(logbookEmployeeDetail, username);
                    createLogbookEmployee.CreatedBy = username;
                    createLogbookEmployee.CreatedDate = DateTime.Now;
                    createLogbookEmployee.ModifiedBy = username;
                    createLogbookEmployee.ModifiedDate = DateTime.Now;

                    _szFleetMgrContext.TLogbookEmployeeDetails.Add(createLogbookEmployee);
                     _szFleetMgrContext.SaveChanges(); 
                    return GetResponseModel(Constants.httpCodeSuccess, createLogbookEmployee, "New Role Added in Logbook Successfully", true);
                }
                else
                {
                    var updatedLogbookDetail = MapLogbookDetails(existingLogbookData, logbookEmployeeDetail);
                    updatedLogbookDetail.ModifiedBy = username;
                    updatedLogbookDetail.ModifiedDate = DateTime.Now;
                    _szFleetMgrContext.TLogbookEmployeeDetails.Update(updatedLogbookDetail);
                     _szFleetMgrContext.SaveChanges(); 
                    return GetResponseModel(Constants.httpCodeSuccess, updatedLogbookDetail, "Updated Logbook Employee Details", true);
                }
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        private TLogbookEmployeeDetail CreateEmployeeDetails(LogbookEmployeeDetail logbookEmployeeDetail, string username)
        {
            TLogbookEmployeeDetail newlogbookEmployeeDetail = new TLogbookEmployeeDetail();
            newlogbookEmployeeDetail.EmployeeCode = logbookEmployeeDetail.EmployeeCode;
            newlogbookEmployeeDetail.EmployeeName = logbookEmployeeDetail.EmployeeName;
            newlogbookEmployeeDetail.Role = logbookEmployeeDetail.Role;
            newlogbookEmployeeDetail.ShiftCycle = logbookEmployeeDetail.ShiftCycle;
            newlogbookEmployeeDetail.WorkDoneShift = logbookEmployeeDetail.WorkDoneShift;
            newlogbookEmployeeDetail.FkSiteId = logbookEmployeeDetail.FkSiteId;
            newlogbookEmployeeDetail.SiteName = logbookEmployeeDetail.SiteName;
            newlogbookEmployeeDetail.LogDate = logbookEmployeeDetail.LogDate;
            newlogbookEmployeeDetail.Status = "added";
            return newlogbookEmployeeDetail;
        }
        private TLogbookEmployeeDetail MapLogbookDetails(TLogbookEmployeeDetail existingLogbookDetail, LogbookEmployeeDetail updateLogbookDetail)
        {

            existingLogbookDetail.EmployeeName = updateLogbookDetail.EmployeeName;
            existingLogbookDetail.Role = updateLogbookDetail.Role;
            existingLogbookDetail.ShiftCycle = updateLogbookDetail.ShiftCycle;
            existingLogbookDetail.WorkDoneShift = updateLogbookDetail.WorkDoneShift;
            existingLogbookDetail.FkSiteId = updateLogbookDetail.FkSiteId;
            existingLogbookDetail.SiteName = updateLogbookDetail.SiteName;
            existingLogbookDetail.LogDate = updateLogbookDetail.LogDate;
            existingLogbookDetail.Status = "updated";
            return existingLogbookDetail;
        }


        public ResponseModel AddUpdateGridBreakdown(LogbookGridBreakdownDetail logbookGridBreakdownDetail, string username)
        {
           
            try
            {
                //checking FK SiteId is there or not
                if (!_szFleetMgrContext.SiteIds.Any(c => c.Id == logbookGridBreakdownDetail.FkSiteId))
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "SiteId not available", false);
                }

                var existingLogbookGridBdDetail = _szFleetMgrContext.TLogbookGridBreakdownDetails.SingleOrDefault(c => c.Id == logbookGridBreakdownDetail.Id);
                if (existingLogbookGridBdDetail == null)
                {
                    var createLogbookGridBreakdown = CreateLogbookGridBdDetails(logbookGridBreakdownDetail);

                    createLogbookGridBreakdown.CreatedBy = username;
                    createLogbookGridBreakdown.CreatedDate = DateTime.Now;
                    createLogbookGridBreakdown.ModifiedBy = username;
                    createLogbookGridBreakdown.ModifiedDate = DateTime.Now;

                    _szFleetMgrContext.TLogbookGridBreakdownDetails.Add(createLogbookGridBreakdown);
                    _szFleetMgrContext.SaveChanges();
                    return GetResponseModel(Constants.httpCodeSuccess, createLogbookGridBreakdown, "Logbook Grid BreakDown Activity Added Succesfully", true);

                }

                else
                {
                    var updatedLogbookGridBdDetails = MapLogbookGridBdDetails(existingLogbookGridBdDetail, logbookGridBreakdownDetail);
                    updatedLogbookGridBdDetails.ModifiedBy = username;
                    updatedLogbookGridBdDetails.ModifiedDate = DateTime.Now;
                    _szFleetMgrContext.TLogbookGridBreakdownDetails.Update(updatedLogbookGridBdDetails);
                    _szFleetMgrContext.SaveChanges();
                    return GetResponseModel(Constants.httpCodeSuccess, updatedLogbookGridBdDetails, "Updated Logbook Grid Breakdown Activity Details", true);

                }
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);

            }

        }
        public TLogbookGridBreakdownDetail CreateLogbookGridBdDetails(LogbookGridBreakdownDetail logbookGridBreakdown)
        {
            TLogbookGridBreakdownDetail newlogbookGridBreakdown = new TLogbookGridBreakdownDetail();
            newlogbookGridBreakdown.SiteName = logbookGridBreakdown.SiteName;
            newlogbookGridBreakdown.FkSiteId = logbookGridBreakdown.FkSiteId;
            newlogbookGridBreakdown.FeederName = logbookGridBreakdown.FeederName;
            newlogbookGridBreakdown.GridDropReason = logbookGridBreakdown.GridDropReason;
            newlogbookGridBreakdown.TimeFrom = logbookGridBreakdown.TimeFrom;
            newlogbookGridBreakdown.TimeTo = logbookGridBreakdown.TimeTo;
            newlogbookGridBreakdown.TotalTime = logbookGridBreakdown.TotalTime;
            newlogbookGridBreakdown.RemarkAction = logbookGridBreakdown.RemarkAction;
            newlogbookGridBreakdown.EptwNumber = logbookGridBreakdown.EptwNumber;
            newlogbookGridBreakdown.LogDate = logbookGridBreakdown.LogDate;
            newlogbookGridBreakdown.ShiftCycle = logbookGridBreakdown.ShiftCycle;
            newlogbookGridBreakdown.Status = "added";
            return newlogbookGridBreakdown;
        }
        public TLogbookGridBreakdownDetail MapLogbookGridBdDetails(TLogbookGridBreakdownDetail existingLogbookDetail, LogbookGridBreakdownDetail updateLogbookDetail)
        {
            existingLogbookDetail.SiteName = updateLogbookDetail.SiteName;
            existingLogbookDetail.FkSiteId = updateLogbookDetail.FkSiteId;
            existingLogbookDetail.FeederName = updateLogbookDetail.FeederName;
            existingLogbookDetail.GridDropReason = updateLogbookDetail.GridDropReason;
            existingLogbookDetail.TimeFrom = updateLogbookDetail.TimeFrom;
            existingLogbookDetail.TimeTo = updateLogbookDetail.TimeTo;
            existingLogbookDetail.TotalTime = updateLogbookDetail.TotalTime;
            existingLogbookDetail.RemarkAction = updateLogbookDetail.RemarkAction;
            existingLogbookDetail.EptwNumber = updateLogbookDetail.EptwNumber;
            existingLogbookDetail.LogDate = updateLogbookDetail.LogDate;
            existingLogbookDetail.ShiftCycle = updateLogbookDetail.ShiftCycle;
            existingLogbookDetail.Status = "updated";
            return existingLogbookDetail;
        }
        public ResponseModel AddUpdateHOTO(LogbookHoto logbookHoto, string username)
        {
            
            try
            {
                //checking FK SiteId is there or not
                if (!_szFleetMgrContext.SiteIds.Any(c => c.Id == logbookHoto.FkSiteId))
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "SiteId not available", false);
                }

                var existingLogbookHOTODetail = _szFleetMgrContext.TLogbookHotos.SingleOrDefault(c => c.Id == logbookHoto.Id);
                if (existingLogbookHOTODetail == null)
                {
                    var createLogbookHoto = CreateLogbookHOTODetails(logbookHoto);

                    createLogbookHoto.CreatedBy = username;
                    createLogbookHoto.CreatedDate = DateTime.Now;
                    createLogbookHoto.ModifiedBy = username;
                    createLogbookHoto.ModifiedDate = DateTime.Now;

                    _szFleetMgrContext.TLogbookHotos.Add(createLogbookHoto);
                    _szFleetMgrContext.SaveChanges();
                    return GetResponseModel(Constants.httpCodeSuccess, createLogbookHoto, "Logbook HOTO Added Succesfully", true);

                }

                else
                {
                    var updatedLogbookHOTODetails = MapLogbookHOTODetails(existingLogbookHOTODetail, logbookHoto);
                    updatedLogbookHOTODetails.ModifiedBy = username;
                    updatedLogbookHOTODetails.ModifiedDate = DateTime.Now;
                    _szFleetMgrContext.TLogbookHotos.Update(updatedLogbookHOTODetails);
                    _szFleetMgrContext.SaveChanges();

                    return GetResponseModel(Constants.httpCodeSuccess, updatedLogbookHOTODetails, "Updated Logbook HOTO Activity Details", true);

                }
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);

            }
        }
        private TLogbookHoto CreateLogbookHOTODetails(LogbookHoto logbookHoto)
        {
            TLogbookHoto newlogbookHoto = new TLogbookHoto();
            newlogbookHoto.SiteName = logbookHoto.SiteName;
            newlogbookHoto.FkSiteId = logbookHoto.FkSiteId;
            newlogbookHoto.ShiftHandedOverBy = logbookHoto.ShiftHandedOverBy;
            newlogbookHoto.HandedOverDateTime = logbookHoto.HandedOverDateTime;
            newlogbookHoto.ShiftTakenOverBy = logbookHoto.ShiftTakenOverBy;
            newlogbookHoto.TakenOverDateTime = logbookHoto.TakenOverDateTime;
            newlogbookHoto.ShiftHours = logbookHoto.ShiftHours;
            newlogbookHoto.LogDate = logbookHoto.LogDate;
            newlogbookHoto.ShiftCycle = logbookHoto.ShiftCycle;
            newlogbookHoto.Status = "added";
            return newlogbookHoto;
        }
        private TLogbookHoto MapLogbookHOTODetails(TLogbookHoto existingLogbookDetail, LogbookHoto updateLogbookDetail)
        {
            existingLogbookDetail.SiteName = updateLogbookDetail.SiteName;
            existingLogbookDetail.FkSiteId = updateLogbookDetail.FkSiteId;
            existingLogbookDetail.ShiftHandedOverBy = updateLogbookDetail.ShiftHandedOverBy;
            existingLogbookDetail.ShiftTakenOverBy = updateLogbookDetail.ShiftTakenOverBy;
            existingLogbookDetail.TakenOverDateTime = updateLogbookDetail.TakenOverDateTime;
            existingLogbookDetail.HandedOverDateTime = updateLogbookDetail.HandedOverDateTime;
            existingLogbookDetail.ShiftHours = updateLogbookDetail.ShiftHours;
            existingLogbookDetail.LogDate = updateLogbookDetail.LogDate;
            existingLogbookDetail.ShiftCycle = updateLogbookDetail.ShiftCycle;
            existingLogbookDetail.Status = "updated";
            return existingLogbookDetail;
        }
        public ResponseModel AddUpdateScada(LogbookScadaDetail logbookScadaDetail, string username)
        {
            
            try
            {
                //checking FK SiteId is there or not
                if (!_szFleetMgrContext.SiteIds.Any(c => c.Id == logbookScadaDetail.FkSiteId))
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "SiteId not available", false);
                }

                var existingLogbookScadaDetails = _szFleetMgrContext.TLogbookScadaDetails.SingleOrDefault(c => c.Id == logbookScadaDetail.Id);
                if (existingLogbookScadaDetails == null)
                {
                    var createLogbookScada = CreateLogbookScadaDetails(logbookScadaDetail);

                    createLogbookScada.CreatedBy = username;
                    createLogbookScada.CreatedDate = DateTime.Now;
                    createLogbookScada.ModifiedBy = username;
                    createLogbookScada.ModifiedDate = DateTime.Now;

                    _szFleetMgrContext.TLogbookScadaDetails.Add(createLogbookScada);
                    _szFleetMgrContext.SaveChanges();
                    return GetResponseModel(Constants.httpCodeSuccess, createLogbookScada, "Logbook Scada Details Added Succesfully", true);

                }

                else
                {
                    var updatedLogbookScadaDetails = MapLogbookScadaDetails(existingLogbookScadaDetails, logbookScadaDetail);
                    updatedLogbookScadaDetails.ModifiedBy = username;
                    updatedLogbookScadaDetails.ModifiedDate = DateTime.Now;
                    _szFleetMgrContext.TLogbookScadaDetails.Update(updatedLogbookScadaDetails);
                    _szFleetMgrContext.SaveChanges();
                    return GetResponseModel(Constants.httpCodeSuccess, updatedLogbookScadaDetails, "Updated Logbook Scada Details", true);

                }
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);

            }
        }
        private TLogbookScadaDetail CreateLogbookScadaDetails(LogbookScadaDetail logbookScadaDetail)
        {
            TLogbookScadaDetail newlogbookScadaDetail = new TLogbookScadaDetail();
            newlogbookScadaDetail.SiteName = logbookScadaDetail.SiteName;
            newlogbookScadaDetail.FkSiteId = logbookScadaDetail.FkSiteId;
            newlogbookScadaDetail.IssueDesc = logbookScadaDetail.IssueDesc;
            newlogbookScadaDetail.ActionTaken = logbookScadaDetail.ActionTaken;
            newlogbookScadaDetail.LogDate = logbookScadaDetail.LogDate;
            newlogbookScadaDetail.ShiftCycle = logbookScadaDetail.ShiftCycle;
            newlogbookScadaDetail.Status = "added";
            return newlogbookScadaDetail;
        }
        private TLogbookScadaDetail MapLogbookScadaDetails(TLogbookScadaDetail existingLogbookDetail, LogbookScadaDetail updateLogbookDetail)
        {
            existingLogbookDetail.SiteName = updateLogbookDetail.SiteName;
            existingLogbookDetail.FkSiteId = updateLogbookDetail.FkSiteId;
            existingLogbookDetail.IssueDesc = updateLogbookDetail.IssueDesc;
            existingLogbookDetail.ActionTaken = updateLogbookDetail.ActionTaken;
            existingLogbookDetail.LogDate = updateLogbookDetail.LogDate;
            existingLogbookDetail.ShiftCycle = updateLogbookDetail.ShiftCycle;
            existingLogbookDetail.Status = "updated";
            return existingLogbookDetail;
        }

        public ResponseModel AddUpdateScheduleMnt(LogbookScheduleMaintenanceActivity logbookScheduleMaintenanceActivity, string username)
        {
            
            try
            {
                ////checking FK SiteId is there or not
                //if (!_szFleetMgrContext.SiteIds.Any(c => c.Id == logbookScheduleMaintenanceActivity.FkSiteId))
                //{
                //	return GetResponseModel(Constants.httpCodeSuccess,null, "SiteId not available", false);

                //}

                var existingLogbookScheduleMntDetails = _szFleetMgrContext.TLogbookScheduleMaintenanceActivities.SingleOrDefault(c => c.Id == logbookScheduleMaintenanceActivity.Id);
                if (existingLogbookScheduleMntDetails == null)
                {
                    var createLogbookScheduleMnt = CreateLogbookScheduleMntDetails(logbookScheduleMaintenanceActivity);

                    createLogbookScheduleMnt.CreatedBy = username;
                    createLogbookScheduleMnt.CreatedDate = DateTime.Now;
                    createLogbookScheduleMnt.ModifiedBy = username;
                    createLogbookScheduleMnt.ModifiedDate = DateTime.Now;

                    _szFleetMgrContext.TLogbookScheduleMaintenanceActivities.Add(createLogbookScheduleMnt);
                    _szFleetMgrContext.SaveChanges();

                    if (createLogbookScheduleMnt.Closure == Constants.HandovertoNextshift)
                    {
                        var newCreateLogbookScheduleMnt = CreateLogbookScheduleMntDetails(logbookScheduleMaintenanceActivity);
                        newCreateLogbookScheduleMnt = ScheduleHandOver(newCreateLogbookScheduleMnt, username);
                    }

                    if (createLogbookScheduleMnt.Closure == Constants.Reschedule)
                    {
                        var newCreateLogbookScheduleMnt = CreateLogbookScheduleMntDetails(logbookScheduleMaintenanceActivity);
                        newCreateLogbookScheduleMnt = ScheduleReschedule(newCreateLogbookScheduleMnt, username);
                    }
                    
                    return GetResponseModel(Constants.httpCodeSuccess, createLogbookScheduleMnt, "Logbook Schedule Maintainence Details Added Succesfully", true);

                }

                else
                {
                    var updatedLogbookScheduleMntDetails = MapLogbookScheduleMntDetails(existingLogbookScheduleMntDetails, logbookScheduleMaintenanceActivity);
                    updatedLogbookScheduleMntDetails.ModifiedBy = username;
                    updatedLogbookScheduleMntDetails.ModifiedDate = DateTime.Now;
                    _szFleetMgrContext.TLogbookScheduleMaintenanceActivities.Update(updatedLogbookScheduleMntDetails);
                    _szFleetMgrContext.SaveChanges();

                    if (updatedLogbookScheduleMntDetails.Closure == Constants.HandovertoNextshift)
                    {
                        var newCreateLogbookScheduleMnt = CreateLogbookScheduleMntDetails(logbookScheduleMaintenanceActivity);
                        newCreateLogbookScheduleMnt = ScheduleHandOver(newCreateLogbookScheduleMnt, username);
                    }

                    if (updatedLogbookScheduleMntDetails.Closure == Constants.Reschedule)
                    {
                        var newCreateLogbookScheduleMnt = CreateLogbookScheduleMntDetails(logbookScheduleMaintenanceActivity);
                        newCreateLogbookScheduleMnt = ScheduleReschedule(newCreateLogbookScheduleMnt, username);
                    }

                    return GetResponseModel(Constants.httpCodeSuccess, updatedLogbookScheduleMntDetails, "Updated Logbook Schedule Maintainence Details", true);

                }
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);

            }
        }

        private TLogbookScheduleMaintenanceActivity CreateLogbookScheduleMntDetails(LogbookScheduleMaintenanceActivity logbookScheduleMaintenanceActivity)
        {
            TLogbookScheduleMaintenanceActivity newlogbookScheduleMaintenanceActivity = new TLogbookScheduleMaintenanceActivity();
            newlogbookScheduleMaintenanceActivity.SiteName = logbookScheduleMaintenanceActivity.SiteName;
            newlogbookScheduleMaintenanceActivity.FkSiteId = logbookScheduleMaintenanceActivity.FkSiteId;
            newlogbookScheduleMaintenanceActivity.TurbineNumber = logbookScheduleMaintenanceActivity.Turbine;
            newlogbookScheduleMaintenanceActivity.Observation = logbookScheduleMaintenanceActivity.Observation;
            newlogbookScheduleMaintenanceActivity.EptwNumber = logbookScheduleMaintenanceActivity.EptwNumber;
            newlogbookScheduleMaintenanceActivity.Activity = logbookScheduleMaintenanceActivity.Activity;
            newlogbookScheduleMaintenanceActivity.LogDate = logbookScheduleMaintenanceActivity.LogDate;
            newlogbookScheduleMaintenanceActivity.Closure = logbookScheduleMaintenanceActivity.Closure;
            newlogbookScheduleMaintenanceActivity.RescheduleDate = logbookScheduleMaintenanceActivity.RescheduleDate;
            newlogbookScheduleMaintenanceActivity.ShiftCycle = logbookScheduleMaintenanceActivity.ShiftCycle;
            newlogbookScheduleMaintenanceActivity.Status = "added";
            return newlogbookScheduleMaintenanceActivity;
        }

        private TLogbookScheduleMaintenanceActivity MapLogbookScheduleMntDetails(TLogbookScheduleMaintenanceActivity existingLogbookDetail, LogbookScheduleMaintenanceActivity updateLogbookDetail)
        {
            existingLogbookDetail.SiteName = updateLogbookDetail.SiteName;
            existingLogbookDetail.FkSiteId = updateLogbookDetail.FkSiteId;
            existingLogbookDetail.TurbineNumber = updateLogbookDetail.Turbine;
            existingLogbookDetail.Observation = updateLogbookDetail.Observation;
            existingLogbookDetail.EptwNumber = updateLogbookDetail.EptwNumber;
            existingLogbookDetail.Activity = updateLogbookDetail.Activity;
            existingLogbookDetail.LogDate = updateLogbookDetail.LogDate;
            existingLogbookDetail.Closure = updateLogbookDetail.Closure;
            existingLogbookDetail.RescheduleDate = updateLogbookDetail.RescheduleDate;
            existingLogbookDetail.ShiftCycle = updateLogbookDetail.ShiftCycle;
            existingLogbookDetail.Status = "updated";
            return existingLogbookDetail;
        }

        private TLogbookScheduleMaintenanceActivity ScheduleHandOver
            (TLogbookScheduleMaintenanceActivity newCreateLogbookScheduleMnt, String username)
        {
            var currentShift = _szFleetMgrContext.TLogbookCommonMasters
                .Where(i => i.MasterName == newCreateLogbookScheduleMnt.ShiftCycle).FirstOrDefault();
            var nextShift = _szFleetMgrContext.TLogbookCommonMasters
                .Where(i => i.Id == (currentShift.Id + 1) && i.MasterCategory == "ShiftCycle").FirstOrDefault();
            if (nextShift == null)
            {
                //first entry of the table
                nextShift = _szFleetMgrContext.TLogbookCommonMasters
                    .Where(i => i.MasterCategory == "ShiftCycle").FirstOrDefault();
                //transfer the task to next day
                newCreateLogbookScheduleMnt.LogDate = newCreateLogbookScheduleMnt.LogDate.Value.AddDays(1);
            }
            // update the shift cycle
            newCreateLogbookScheduleMnt.ShiftCycle = nextShift.MasterName;
            // make closure empty
            newCreateLogbookScheduleMnt.Closure = null;
            newCreateLogbookScheduleMnt.CreatedBy = username;
            newCreateLogbookScheduleMnt.CreatedDate = DateTime.Now;
            newCreateLogbookScheduleMnt.ModifiedBy = username;
            newCreateLogbookScheduleMnt.ModifiedDate = DateTime.Now;
            // add to the database
            _szFleetMgrContext.TLogbookScheduleMaintenanceActivities.Add(newCreateLogbookScheduleMnt);
            _szFleetMgrContext.SaveChanges();

            return newCreateLogbookScheduleMnt;
        }
        private TLogbookScheduleMaintenanceActivity ScheduleReschedule
            (TLogbookScheduleMaintenanceActivity newCreateLogbookScheduleMnt, String username)
        {
            newCreateLogbookScheduleMnt.LogDate = newCreateLogbookScheduleMnt.RescheduleDate;
            newCreateLogbookScheduleMnt.Closure = null;
            newCreateLogbookScheduleMnt.RescheduleDate = null;
            newCreateLogbookScheduleMnt.CreatedBy = username;
            newCreateLogbookScheduleMnt.CreatedDate = DateTime.Now;
            newCreateLogbookScheduleMnt.ModifiedBy = username;
            newCreateLogbookScheduleMnt.ModifiedDate = DateTime.Now;

            _szFleetMgrContext.TLogbookScheduleMaintenanceActivities.Add(newCreateLogbookScheduleMnt);
            _szFleetMgrContext.SaveChanges();

            return newCreateLogbookScheduleMnt;
        }

        public ResponseModel Top10Error(string SiteName)
        {
            try
            {
                List<ResponseFilterData> response = new List<ResponseFilterData>();

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                string connectionString = configuration.GetConnectionString("MyConnectionString");

                if (string.IsNullOrEmpty(connectionString))
                {
                    return GetResponseModel(Constants.httpCodeFailure, null, "Connection string not found in appsettings.json", true);
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_Get_BD_REMARK_Data", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@SiteName", SiteName);


                        connection.Open();

                        // Initialize currentFilter and currentResponseData outside the loop
                        string currentFilter = null;
                        ResponseFilterData currentResponseData = null;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string filter = reader["FilterName"].ToString();
                                string value = reader["Value"].ToString();
                                string count = reader["Remark_Count"].ToString();
                                string total_Duration = reader["TOTAL_DURATION"].ToString();

                                if (currentFilter != filter)
                                {
                                    // Create a new ResponseFilterData only when the filter changes
                                    currentResponseData = new ResponseFilterData
                                    {
                                        Filter = filter,
                                        ErrorCount = new List<FilterCount>(),
                                        TotalDurationCount = new List<FilterDurCount>()
                                    };

                                    response.Add(currentResponseData);
                                    currentFilter = filter;
                                }

                                // Use correct property names in the FilterCount and FilterDurCount objects
                                currentResponseData.ErrorCount.Add(new FilterCount
                                {
                                    Error = value,
                                    Count = count
                                });

                                currentResponseData.TotalDurationCount.Add(new FilterDurCount
                                {
                                    Error = value,
                                    Count = total_Duration
                                });
                            }
                        }
                    }
                }

                return GetResponseModel(Constants.httpCodeSuccess, response, "Data Received", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, true);
            }
        }

        public ResponseModel AddUpdateWTGBreakdown(LogbookWtgBreakdownDetail logbookWtgBreakdownDetail, string username)
        {
           
            try
            {
               
                var existingLogbookWTGBreakdownDetails = _szFleetMgrContext.TLogbookWtgBreakdownDetails.SingleOrDefault(c => c.Id == logbookWtgBreakdownDetail.Id);
                if (existingLogbookWTGBreakdownDetails == null)
                {
                    var createLogbookWTGBreakdown = CreateLogbookWTGBreakdownDetails(logbookWtgBreakdownDetail);
                    var getTurbinefromCRMS = _crmsContext.VScAllMachineStaticDetailOmsPbis
                        .FirstOrDefault(d => d.SapFuncLocCode == logbookWtgBreakdownDetail.Turbine
                        && d.MainSite == logbookWtgBreakdownDetail.SiteName);

                    var rowIdResult = "";
                    if (getTurbinefromCRMS != null)
                    {
                        var getPlantIdFromDatamart = _datamartMobileContext.CatalogTurbines
                             .FirstOrDefault(d => d.FunctionalLocation == getTurbinefromCRMS.SapFuncLocCode);
                        if (getPlantIdFromDatamart != null)
                        {
                            var windFarmDataFromDatamart = _datamartMobileContext.VwWindFarms
                                .FirstOrDefault(d => d.PlantUnitId == getPlantIdFromDatamart.PlantUnitId);
                            if (windFarmDataFromDatamart != null)
                            {
                                var turbineDataFromDatamart = _datamartMobileContext.TurbineOnlines
                                    .FirstOrDefault(d => d.PlantUnitId == windFarmDataFromDatamart.PlantUnitId);
                                if (turbineDataFromDatamart != null)
                                {

                                    var dateTimeServerTimestamp = turbineDataFromDatamart.ServerTimestamp?.ToString("yyyy-MM-dd HH:mm:ss.fff") ?? "null";
                                    rowIdResult = windFarmDataFromDatamart.PlantUnitId + "|" + windFarmDataFromDatamart.PlantId
                                        + "|" + windFarmDataFromDatamart.ControllerTimestamp + "|" + windFarmDataFromDatamart.ServiceOrganizationName
                                        + "|" + dateTimeServerTimestamp;


                                }
                                else
                                {
                                    rowIdResult = "";
                                }
                            }
                            else
                            {
                                rowIdResult = "";
                            }
                        }
                        else
                        {
                            rowIdResult = "";
                        }
                    }
                    else
                    {
                        rowIdResult = "";
                    }

                    createLogbookWTGBreakdown.RowId = rowIdResult;

                    createLogbookWTGBreakdown.CreatedBy = username;
                    createLogbookWTGBreakdown.CreatedDate = DateTime.Now;
                    createLogbookWTGBreakdown.ModifiedBy = username;
                    createLogbookWTGBreakdown.ModifiedDate = DateTime.Now;

                    _szFleetMgrContext.TLogbookWtgBreakdownDetails.Add(createLogbookWTGBreakdown);
                    _szFleetMgrContext.SaveChanges();

                    int getId = createLogbookWTGBreakdown.Id;
                    if (rowIdResult != null)
                    {
                        //update TaskMain with TableId and TableName matching rowId and Extractfrom from TaskMain for auto logbook
                        var getTaskTable = _szFleetMgrContext.TaskMains.FirstOrDefault(d => d.ExtractfromId ==rowIdResult) ?? null;
                        if (getTaskTable != null)
                        {
    
                            getTaskTable.ModifiedBy = username;
                            getTaskTable.ModifiedDate = DateTime.Now;
                            getTaskTable.TableId = getId;
                            getTaskTable.Tablename = "T_Logbook_WTG_Breakdown_Details";

                            _szFleetMgrContext.SaveChanges();
                        }


                    }
                //if (logbookWtgBreakdownDetail.Closure == Constants.HandovertoNextshift)
                //    {
                //        var newCreateLogbookWTGBreakdown = CreateLogbookWTGBreakdownDetails(logbookWtgBreakdownDetail);
                //        newCreateLogbookWTGBreakdown = WtgHandover(newCreateLogbookWTGBreakdown, username);
                //    }

                    return GetResponseModel(Constants.httpCodeSuccess, createLogbookWTGBreakdown, "Logbook WTG Breakdown  Details Added Succesfully", true);
                }
                else
                {
                    var updatedLogbookWTGBreakdownDetails = MapLogbookWTGBreakdownDetails(existingLogbookWTGBreakdownDetails, logbookWtgBreakdownDetail);
                    var getTurbinefromCRMS = _crmsContext.VScAllMachineStaticDetailOmsPbis
                         .FirstOrDefault(d => d.SapFuncLocCode == updatedLogbookWTGBreakdownDetails.TurbineNumber
                         && d.MainSite == updatedLogbookWTGBreakdownDetails.SiteName);
                    var rowIdResult = "";
                    if (getTurbinefromCRMS != null)
                    {
                        var getPlantIdFromDatamart = _datamartMobileContext.CatalogTurbines
                             .FirstOrDefault(d => d.FunctionalLocation == getTurbinefromCRMS.SapFuncLocCode);
                        if (getPlantIdFromDatamart != null)
                        {
                            var windFarmDataFromDatamart = _datamartMobileContext.VwWindFarms
                                .FirstOrDefault(d => d.PlantUnitId == getPlantIdFromDatamart.PlantUnitId);
                            if (windFarmDataFromDatamart != null)
                            {
                                var turbineDataFromDatamart = _datamartMobileContext.TurbineOnlines
                                    .FirstOrDefault(d => d.PlantUnitId == windFarmDataFromDatamart.PlantUnitId);
                                if (turbineDataFromDatamart != null)
                                {

                                    var dateTimeServerTimestamp = turbineDataFromDatamart.ServerTimestamp?.ToString("yyyy-MM-dd HH:mm:ss.fff") ?? "null";
                                    rowIdResult = windFarmDataFromDatamart.PlantUnitId + "|" + windFarmDataFromDatamart.PlantId
                                        + "|" + windFarmDataFromDatamart.ControllerTimestamp + "|" + windFarmDataFromDatamart.ServiceOrganizationName
                                        + "|" + dateTimeServerTimestamp;


                                }
                                else
                                {
                                    rowIdResult = "";
                                }
                            }
                            else
                            {
                                rowIdResult = "";
                            }
                        }
                        else
                        {
                            rowIdResult = "";
                        }
                    }
                    else
                    {
                        rowIdResult = "";
                    }


                    updatedLogbookWTGBreakdownDetails.RowId = rowIdResult;
                    updatedLogbookWTGBreakdownDetails.ModifiedBy = username;
                    updatedLogbookWTGBreakdownDetails.ModifiedDate = DateTime.Now;
                    _szFleetMgrContext.TLogbookWtgBreakdownDetails.Update(updatedLogbookWTGBreakdownDetails);
                    _szFleetMgrContext.SaveChanges();

                    //if (logbookWtgBreakdownDetail.Closure == Constants.HandovertoNextshift)
                    //{
                    //    var newCreateLogbookWTGBreakdown = CreateLogbookWTGBreakdownDetails(logbookWtgBreakdownDetail);
                    //    newCreateLogbookWTGBreakdown = WtgHandover(newCreateLogbookWTGBreakdown, username);
                    //}

                    return GetResponseModel(Constants.httpCodeSuccess, updatedLogbookWTGBreakdownDetails, "Updated Logbook WTG Breakdown Details", true);

                }
            }
             catch(Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);

            }
        }

        private TLogbookWtgBreakdownDetail CreateLogbookWTGBreakdownDetails(LogbookWtgBreakdownDetail logbookWtgBreakdownDetail)
        {
            TLogbookWtgBreakdownDetail newlogbookWtgBreakdownDetail = new TLogbookWtgBreakdownDetail();
            newlogbookWtgBreakdownDetail.SiteName = logbookWtgBreakdownDetail.SiteName;
            newlogbookWtgBreakdownDetail.FkSiteId = logbookWtgBreakdownDetail.FkSiteId;
            newlogbookWtgBreakdownDetail.ActionTaken = logbookWtgBreakdownDetail.ActionTaken;
            newlogbookWtgBreakdownDetail.EptwNumber = logbookWtgBreakdownDetail.EptwNumber;
            newlogbookWtgBreakdownDetail.Error = logbookWtgBreakdownDetail.Error;
            newlogbookWtgBreakdownDetail.PasswordUsage = logbookWtgBreakdownDetail.PasswordUsage;
            newlogbookWtgBreakdownDetail.PasswordUsageBy = logbookWtgBreakdownDetail.PasswordUsageBy;
            newlogbookWtgBreakdownDetail.TurbineNumber = logbookWtgBreakdownDetail.Turbine;
            newlogbookWtgBreakdownDetail.TimeFrom = logbookWtgBreakdownDetail.TimeFrom;
            newlogbookWtgBreakdownDetail.TimeTo = logbookWtgBreakdownDetail.TimeTo;
            newlogbookWtgBreakdownDetail.TotalTime = logbookWtgBreakdownDetail.TotalTime;
            newlogbookWtgBreakdownDetail.LogDate = logbookWtgBreakdownDetail.LogDate;
            newlogbookWtgBreakdownDetail.Closure = logbookWtgBreakdownDetail.Closure;
            newlogbookWtgBreakdownDetail.ShiftCycle = logbookWtgBreakdownDetail.ShiftCycle;
            newlogbookWtgBreakdownDetail.RowId = logbookWtgBreakdownDetail.RowId;
            newlogbookWtgBreakdownDetail.FkTaskId = logbookWtgBreakdownDetail.FkTaskId;
            newlogbookWtgBreakdownDetail.BreakdownCategory = logbookWtgBreakdownDetail.BreakdownCategory;
            newlogbookWtgBreakdownDetail.Status = "added";
            return newlogbookWtgBreakdownDetail;
        }

        private TLogbookWtgBreakdownDetail MapLogbookWTGBreakdownDetails(TLogbookWtgBreakdownDetail existingLogbookDetail, LogbookWtgBreakdownDetail updateLogbookDetail)
        {
            existingLogbookDetail.SiteName = updateLogbookDetail.SiteName;
            existingLogbookDetail.FkSiteId = updateLogbookDetail.FkSiteId;
            existingLogbookDetail.ActionTaken = updateLogbookDetail.ActionTaken;
            existingLogbookDetail.EptwNumber = updateLogbookDetail.EptwNumber;
            existingLogbookDetail.Error = updateLogbookDetail.Error;
            existingLogbookDetail.PasswordUsage = updateLogbookDetail.PasswordUsage;
            existingLogbookDetail.PasswordUsageBy = updateLogbookDetail.PasswordUsageBy;
            existingLogbookDetail.TurbineNumber = updateLogbookDetail.Turbine;
            existingLogbookDetail.TimeFrom = updateLogbookDetail.TimeFrom;
            existingLogbookDetail.TimeTo = updateLogbookDetail.TimeTo;
            existingLogbookDetail.TotalTime = updateLogbookDetail.TotalTime;
            existingLogbookDetail.LogDate = updateLogbookDetail.LogDate;
            existingLogbookDetail.Closure = updateLogbookDetail.Closure;
            existingLogbookDetail.ShiftCycle = updateLogbookDetail.ShiftCycle;
            existingLogbookDetail.RowId = updateLogbookDetail.RowId;
            existingLogbookDetail.FkTaskId = updateLogbookDetail.FkTaskId;
            existingLogbookDetail.BreakdownCategory = updateLogbookDetail.BreakdownCategory;
            existingLogbookDetail.Status = "updated";
          
          
            return existingLogbookDetail;
        }

        private TLogbookWtgBreakdownDetail WtgHandover
            (TLogbookWtgBreakdownDetail newCreateLogbookWTGBreakdown, String username)
        {
            // new entry with next shift
            // find cuurent shift to find next shift
            var currentShift = _szFleetMgrContext.TLogbookCommonMasters
                .Where(i => i.MasterName == newCreateLogbookWTGBreakdown.ShiftCycle).FirstOrDefault();
            var nextShift = _szFleetMgrContext.TLogbookCommonMasters
                .Where(i => i.Id == (currentShift.Id + 1) && i.MasterCategory == "ShiftCycle").FirstOrDefault();
            if (nextShift == null)
            {
                //first entry of the table
                nextShift = _szFleetMgrContext.TLogbookCommonMasters
                    .Where(i => i.MasterCategory == "ShiftCycle").FirstOrDefault();
                //transfer the task to next day
                newCreateLogbookWTGBreakdown.LogDate = newCreateLogbookWTGBreakdown.LogDate.Value.AddDays(1);
            }
            // update the shift cycle
            newCreateLogbookWTGBreakdown.ShiftCycle = nextShift.MasterName;
            // make closure empty
            newCreateLogbookWTGBreakdown.Closure = null;
            newCreateLogbookWTGBreakdown.CreatedBy = username;
            newCreateLogbookWTGBreakdown.CreatedDate = DateTime.Now;
            newCreateLogbookWTGBreakdown.ModifiedBy = username;
            newCreateLogbookWTGBreakdown.ModifiedDate = DateTime.Now;
            // add to the database
            _szFleetMgrContext.TLogbookWtgBreakdownDetails.Add(newCreateLogbookWTGBreakdown);
            _szFleetMgrContext.SaveChanges();

            return newCreateLogbookWTGBreakdown;
        }

        public ResponseModel UpdateWTGBreakdownList(List<LogbookWtgBreakdownDetail> updateWTGLogbookList, string? userName)
        {
            try
            {
                List<TLogbookWtgBreakdownDetail> allData = new List<TLogbookWtgBreakdownDetail>();

                foreach (var list in updateWTGLogbookList)
                {
                    var existingWTGData = _szFleetMgrContext.TLogbookWtgBreakdownDetails
                        .FirstOrDefault(d => d.TurbineNumber == list.Turbine 
                        && d.LogDate == list.LogDate && d.SiteName == list.SiteName
                        && d.ShiftCycle==list.ShiftCycle && d.Closure=="Closed");

                    if (existingWTGData != null)
                    {
                        var updatedLogbookWTGBreakdownDetails = MapLogbookWTGBreakdownDetails(existingWTGData, list);
                        updatedLogbookWTGBreakdownDetails.Closure = "Closed";
                        updatedLogbookWTGBreakdownDetails.ModifiedBy = userName;
                        updatedLogbookWTGBreakdownDetails.ModifiedDate = DateTime.Now;
                        _szFleetMgrContext.TLogbookWtgBreakdownDetails.Update(updatedLogbookWTGBreakdownDetails);
                        _szFleetMgrContext.SaveChanges();
                    }

                    else if(existingWTGData==null)
                    {
                        var existingOtherData = _szFleetMgrContext.TLogbookWtgBreakdownDetails
                        .FirstOrDefault(d => d.TurbineNumber == list.Turbine
                        && d.LogDate == list.LogDate && d.SiteName == list.SiteName
                        && d.ShiftCycle == list.ShiftCycle);
                        if (existingOtherData == null)
                        {
                            var createLogbookWTGBreakdown = CreateLogbookWTGBreakdownDetails(list);
                            createLogbookWTGBreakdown.Closure = "Non Complaince";
                            createLogbookWTGBreakdown.Status = "Non Complaince";
                            createLogbookWTGBreakdown.CreatedBy = userName;
                            createLogbookWTGBreakdown.CreatedDate = DateTime.Now;
                            createLogbookWTGBreakdown.ModifiedBy = userName;
                            createLogbookWTGBreakdown.ModifiedDate = DateTime.Now;
                            allData.Add(createLogbookWTGBreakdown);
                            _szFleetMgrContext.TLogbookWtgBreakdownDetails.Add(createLogbookWTGBreakdown);
                            _szFleetMgrContext.SaveChanges();
                        }
                        else
                        {
                            var updatedLogbookWTGBreakdownDetails = MapLogbookWTGBreakdownDetails(existingOtherData, list);
                            updatedLogbookWTGBreakdownDetails.Closure = "Non Complaince";
                            updatedLogbookWTGBreakdownDetails.Status = "Non Complaince";
                            updatedLogbookWTGBreakdownDetails.ModifiedBy = userName;
                            updatedLogbookWTGBreakdownDetails.ModifiedDate = DateTime.Now;
                            _szFleetMgrContext.TLogbookWtgBreakdownDetails.Update(updatedLogbookWTGBreakdownDetails);
                            _szFleetMgrContext.SaveChanges();
                        }
                    }
                   
                    var existingStatus = _szFleetMgrContext.TLogbookStatuses
                        .FirstOrDefault(d => d.LogDate == list.LogDate && d.SiteName == list.SiteName
                        && d.ShiftCycle == list.ShiftCycle);

                   if(existingStatus!=null && existingStatus.Status!="Closed")
                    {
                        existingStatus.Status = "NotComplaince";
                        existingStatus.ModifiedBy = userName;
                        existingStatus.ModifiedDate= DateTime.Now;
                        _szFleetMgrContext.TLogbookStatuses.Update(existingStatus);
                    } 
                    _szFleetMgrContext.SaveChanges();
                }

                return GetResponseModel(Constants.httpCodeSuccess, allData,"Submit Successfully.", true);
            }

            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeFailure,null,ex.Message,false);
            }
        }
        public ResponseModel AddorUpdateRemarks(LogbookRemark logbookRemark, string username)
        {
            
            try
            {

                if (!_szFleetMgrContext.SiteIds.Any(c => c.Id == logbookRemark.FkSiteId))
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "SiteId not available", false);
                }

                var existingRemarks = _szFleetMgrContext.TLogbookRemarks.SingleOrDefault(c => c.Id == logbookRemark.Id);
                if (existingRemarks == null)
                {
                    var createRemarks = CreateRemarks(logbookRemark);
                    createRemarks.CreatedBy = username;
                    createRemarks.CreatedDate = DateTime.Now;
                    createRemarks.ModifiedBy = username;
                    createRemarks.ModifiedDate = DateTime.Now;

                    _szFleetMgrContext.TLogbookRemarks.Add(createRemarks);
                    _szFleetMgrContext.SaveChanges();
                    return GetResponseModel(Constants.httpCodeSuccess, createRemarks, "New Remarks Added in Logbook Successfully", true);

                }

                else
                {
                    var updatedRemarks = MapRemarks(existingRemarks, logbookRemark);
                    updatedRemarks.ModifiedBy = username;
                    updatedRemarks.ModifiedDate = DateTime.Now;
                    _szFleetMgrContext.TLogbookRemarks.Update(updatedRemarks);
                    _szFleetMgrContext.SaveChanges();
                    return GetResponseModel(Constants.httpCodeSuccess, updatedRemarks, "Updated Logbook Remarks Details", true);

                }
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);

            }
        }
        private TLogbookRemark CreateRemarks(LogbookRemark logbookRemark)
        {
            TLogbookRemark newlogbookRemark = new TLogbookRemark
            {
                FkSiteId = logbookRemark.FkSiteId,
                SiteName = logbookRemark.SiteName,
                ShiftCycle = logbookRemark.ShiftCycle,
                LogDate = logbookRemark.LogDate,
                Remarks = logbookRemark.Remarks,
                Status = "added"
            };
            return newlogbookRemark;
        }
        private TLogbookRemark MapRemarks(TLogbookRemark existingLogbookRemark, LogbookRemark updateLogbookRemark)
        {
            existingLogbookRemark.FkSiteId = updateLogbookRemark.FkSiteId;
            existingLogbookRemark.SiteName = updateLogbookRemark.SiteName;
            existingLogbookRemark.ShiftCycle = updateLogbookRemark.ShiftCycle;
            existingLogbookRemark.LogDate = updateLogbookRemark.LogDate;
            existingLogbookRemark.Remarks = updateLogbookRemark.Remarks;
            existingLogbookRemark.Status = "updated";
            return existingLogbookRemark;
        }

        //public ResponseModel GetLogbookSubmitStatus(string? ShiftCycle, string? SiteName, DateTime LogDate)
        //{
        //    try
        //    {
        //        var flag="In Progress";
        //        var info=GetWTGLogbook(SiteName, LogDate, ShiftCycle);
        //        int? countWTG=info.countWtg;

        //        var allDetailSubmitted = _szFleetMgrContext.TLogbookWtgBreakdownDetails
        //           .Where(d=>d.ShiftCycle==ShiftCycle && d.SiteName==SiteName && d.LogDate==LogDate) .Count();

        //        if(countWTG==allDetailSubmitted)
        //        {
        //            flag = "Completed";
        //        }
        //        else if(countWTG>0 && countWTG!=allDetailSubmitted)
        //        {
        //            flag = "Non Complaince";
        //        }
        //        var existingStatus = _szFleetMgrContext.TLogbookStatuses.SingleOrDefault(d => d.ShiftCycle == ShiftCycle &&
        //        d.SiteName == SiteName && d.LogDate == LogDate);

        //        if (existingStatus != null)
        //        {
        //            return GetResponseModel(Constants.httpCodeSuccess, existingStatus,flag, true);
        //        }

        //        return GetResponseModel(Constants.httpCodeSuccess, null, "No Status Found", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
        //    }
        //}
        //public ResponseModel LogbookSubmitButton(string? ShiftCycle, string? SiteName, DateTime LogDate,string? status, string? userName)
        //{

        //    try
        //    {
        //        var flag = "In Progress";
        //        var info = GetWTGLogbook(SiteName, LogDate, ShiftCycle);
        //        int? countWTG = info.countWtg;

        //        var allDetailSubmitted = _szFleetMgrContext.TLogbookWtgBreakdownDetails
        //           .Where(d => d.ShiftCycle == ShiftCycle && d.SiteName == SiteName && d.LogDate == LogDate).Count();

        //        if (countWTG == allDetailSubmitted)
        //        {
        //            flag = "Completed";
        //        }
        //        else if (countWTG > 0 && countWTG != allDetailSubmitted)
        //        {
        //            flag = "Non Complaince";
        //        }

        //        var existingStatus = _szFleetMgrContext.TLogbookStatuses
        //            .Where(entry => entry.SiteName == SiteName &&
        //                            entry.LogDate == LogDate &&
        //                            entry.ShiftCycle == ShiftCycle )
        //            .ToList();

        //        if (existingStatus.Count == 0)
        //        {
        //            return GetResponseModel(Constants.httpCodeFailure, null, "No Data Found", false);
        //        }

        //        foreach (var list in existingStatus)
        //        {
        //            list.Status = status;
        //            list.ModifiedDate = DateTime.Now;
        //            list.ModifiedBy = userName;
        //        }

        //        _szFleetMgrContext.UpdateRange(existingStatus);
        //        _szFleetMgrContext.SaveChanges();

        //        return GetResponseModel(Constants.httpCodeSuccess, existingStatus, "Submitted Successfully", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
        //    }
        //}


        public ResponseModel GetLogbookSubmitStatus(string? ShiftCycle, string? SiteName, DateTime LogDate)
        {
            try
            {
                var existingStatus = _szFleetMgrContext.TLogbookStatuses.SingleOrDefault(d => d.ShiftCycle == ShiftCycle &&
                d.SiteName == SiteName && d.LogDate == LogDate);
                if (existingStatus != null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, existingStatus, existingStatus.Status, true);
                }

                return GetResponseModel(Constants.httpCodeSuccess, null, "No Status Found", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel LogbookSubmitButton(string? ShiftCycle, string? SiteName, DateTime LogDate, string? status, string? userName)
        {
            try
            {
                var existingStatus = _szFleetMgrContext.TLogbookStatuses
                    .Where(entry => entry.SiteName == SiteName &&
                                    entry.LogDate == LogDate &&
                                    entry.ShiftCycle == ShiftCycle)
                    .ToList();

                if (existingStatus.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeFailure, null, "No Data Found", false);
                }

                foreach (var list in existingStatus)
                {
                    list.Status = status;
                    list.ModifiedDate = DateTime.Now;
                    list.ModifiedBy = userName;
                }

                _szFleetMgrContext.UpdateRange(existingStatus);
                _szFleetMgrContext.SaveChanges();

                return GetResponseModel(Constants.httpCodeSuccess, existingStatus, "Submitted Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }


        public ResponseModel AddorUpdateWhyAnalysis(PostWhyAnalysis postWhyAnalysis, string? userName)
        {
            try
            {
                PostWhyAnalysis allResponse = new PostWhyAnalysis();

                var getWhyAnalysisData = _szFleetMgrContext.WhyAnalyses
                    .FirstOrDefault(data => data.Id == postWhyAnalysis.TWhyAnalysisList.Id);


                if (getWhyAnalysisData == null)
                {
                    
                    TWhyAnalysis createTWhyAnalysis = CreateTWhyAnalysis(postWhyAnalysis);
                    createTWhyAnalysis.CreatedBy = userName;
                    createTWhyAnalysis.CreatedDate = DateTime.Now;
                    createTWhyAnalysis.ModifiedBy = userName;
                    createTWhyAnalysis.ModifiedDate = DateTime.Now;

                   
                    _szFleetMgrContext.WhyAnalyses.Add(createTWhyAnalysis);
                    _szFleetMgrContext.SaveChanges();

                    
                    int fkAnalysisId = createTWhyAnalysis.Id;

                    List<TWhyAnalysisDetail> createWhyAnalysisDetail = CreateWhyAnalysisDetail(postWhyAnalysis, fkAnalysisId);

                    foreach (var detail in createWhyAnalysisDetail)
                    {
                        detail.CreatedBy = userName;
                        detail.CreatedDate = DateTime.Now;
                        detail.ModifiedBy = userName;
                        detail.ModifiedDate = DateTime.Now;

                      
                        _szFleetMgrContext.TWhyAnalysisDetails.Add(detail);
                    }

                    _szFleetMgrContext.SaveChanges();

                    
                    List<PostWhyAnalysisDetail> convertPostWhyAnalysis = ConvertToPostWhyAnalysisDetail(createWhyAnalysisDetail);
                    allResponse.TWhyAnalysisList = createTWhyAnalysis;
                    allResponse.whyAnalysisDetailList = convertPostWhyAnalysis;

                    return GetResponseModel(Constants.httpCodeSuccess, allResponse, "Why Analysis Details Added Successfully", true);
                }
                else
                {
 
                    PostWhyAnalysis updatedpostWhyAnalysis = new PostWhyAnalysis();

                    var updatedWhyAnalysis = MapWhyAnalysis(getWhyAnalysisData, postWhyAnalysis.TWhyAnalysisList);
                    _szFleetMgrContext.WhyAnalyses.Update(updatedWhyAnalysis);
                    _szFleetMgrContext.SaveChanges();

                    List<TWhyAnalysisDetail> updatedAnalysisTypes = new List<TWhyAnalysisDetail>();

                    var existingDetails = _szFleetMgrContext.TWhyAnalysisDetails
                        .Where(data => data.FkAnalysisId == postWhyAnalysis.TWhyAnalysisList.Id).ToList();

                    if (existingDetails.Count == 0 && postWhyAnalysis.whyAnalysisDetailList.Count == 0)
                    {
                        return GetResponseModel(Constants.httpCodeSuccess, existingDetails, "Updated Why Analysis Details", true);
                    }

                    else if (existingDetails.Count > 0)
                    {
                        _szFleetMgrContext.TWhyAnalysisDetails.RemoveRange(existingDetails);
                        _szFleetMgrContext.SaveChanges();
                    }

                    foreach (var postWhyAnalysisType in postWhyAnalysis.whyAnalysisDetailList)
                    {
                        TWhyAnalysisDetail whyAnalysisType = new TWhyAnalysisDetail();
                        whyAnalysisType.FkAnalysisId = getWhyAnalysisData.Id;
                        TWhyAnalysisDetail updatedWhyAnalysisType = MapWhyAnalysisType(whyAnalysisType, postWhyAnalysisType);
                        updatedAnalysisTypes.Add(whyAnalysisType);
                        _szFleetMgrContext.TWhyAnalysisDetails.Update(updatedWhyAnalysisType);
                        _szFleetMgrContext.SaveChanges();
                    }

                    List<PostWhyAnalysisDetail> ConvertPostWhyAnalysisUpdate = ConvertToPostWhyAnalysisDetail(updatedAnalysisTypes);
                    updatedpostWhyAnalysis.TWhyAnalysisList = updatedWhyAnalysis;
                    updatedpostWhyAnalysis.whyAnalysisDetailList = ConvertPostWhyAnalysisUpdate;

                    return GetResponseModel(Constants.httpCodeSuccess, updatedpostWhyAnalysis, "Updated Why Analysis Details", true);
                }
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        private TWhyAnalysis CreateTWhyAnalysis(PostWhyAnalysis postWhyAnalysis)
        {
            TWhyAnalysis newWhyAnalysis = new TWhyAnalysis();
            newWhyAnalysis.Id = postWhyAnalysis.TWhyAnalysisList.Id;
            newWhyAnalysis.AnalysisDate = postWhyAnalysis.TWhyAnalysisList.AnalysisDate;
            newWhyAnalysis.State = postWhyAnalysis.TWhyAnalysisList.State;
            newWhyAnalysis.Site = postWhyAnalysis.TWhyAnalysisList.Site;
            newWhyAnalysis.Section = postWhyAnalysis.TWhyAnalysisList.Section;
            newWhyAnalysis.ModelName = postWhyAnalysis.TWhyAnalysisList.ModelName;
            newWhyAnalysis.SapCode = postWhyAnalysis.TWhyAnalysisList.SapCode;
            newWhyAnalysis.TowerType = postWhyAnalysis.TWhyAnalysisList.TowerType;
            newWhyAnalysis.Week = postWhyAnalysis.TWhyAnalysisList.Week;
            newWhyAnalysis.GrandTotal = postWhyAnalysis.TWhyAnalysisList.GrandTotal;
            newWhyAnalysis.CheckMark = postWhyAnalysis.TWhyAnalysisList.CheckMark;
            newWhyAnalysis.Remarks1 = postWhyAnalysis.TWhyAnalysisList.Remarks1;
            newWhyAnalysis.Remarks2 = postWhyAnalysis.TWhyAnalysisList.Remarks2;
            newWhyAnalysis.StandardRemarks = postWhyAnalysis.TWhyAnalysisList.StandardRemarks;
            newWhyAnalysis.OverallActionItem = postWhyAnalysis.TWhyAnalysisList.OverallActionItem;
            newWhyAnalysis.MainBucket = postWhyAnalysis.TWhyAnalysisList.MainBucket;
            newWhyAnalysis.SubBucket = postWhyAnalysis.TWhyAnalysisList.SubBucket;
            newWhyAnalysis.Status = "true";
            return newWhyAnalysis;
        }
        private List<TWhyAnalysisDetail> CreateWhyAnalysisDetail(PostWhyAnalysis postWhyAnalysis, int? analysisId)
        {
            List<TWhyAnalysisDetail> newWhyAnalysisTypes = new List<TWhyAnalysisDetail>();

            foreach (var whyAnalysisType in postWhyAnalysis.whyAnalysisDetailList)
            {
                TWhyAnalysisDetail newWhyAnalysisType = new TWhyAnalysisDetail();
                newWhyAnalysisType.Id = whyAnalysisType.Id;
                newWhyAnalysisType.FkAnalysisId = analysisId;
                newWhyAnalysisType.FkTypeId = whyAnalysisType.FkTypeId;
                newWhyAnalysisType.Hrs = whyAnalysisType.Hrs;
                newWhyAnalysisType.Ai = whyAnalysisType.Ai;
                var checkDropdownisOther1 = whyAnalysisType.WhyDrop1?.Name ?? "";
                var checkDropdownisOther2 = whyAnalysisType.WhyDrop2?.Name ?? "";
                var checkDropdownisOther3 = whyAnalysisType.WhyDrop3?.Name ?? "";
                var checkDropdownisOther4 = whyAnalysisType.WhyDrop4?.Name ?? "";
                var checkDropdownisOther5 = whyAnalysisType.WhyDrop5?.Name ?? "";
                var checkDropdownisOther6 = whyAnalysisType.WhyDrop6?.Name ?? "";
                if (whyAnalysisType.Why1 != "" || whyAnalysisType.Why1 == null
                    || checkDropdownisOther1=="Others")
                {
                    newWhyAnalysisType.Why1 = insertDropValue(whyAnalysisType.Why1);
                }
               
                else
                {
                    newWhyAnalysisType.Why1 = whyAnalysisType.WhyDrop1?.Name??"";
                }
                if (whyAnalysisType.Why2 != "" || whyAnalysisType.Why2 == null
                    || checkDropdownisOther2 == "Others")
                {
                    newWhyAnalysisType.Why2 = insertDropValue(whyAnalysisType.Why2);
                }
                
                else
                {
                    newWhyAnalysisType.Why2 = whyAnalysisType.WhyDrop2?.Name??"";
                }
                if (whyAnalysisType.Why3 != "" || whyAnalysisType.Why3 == null
                    || checkDropdownisOther3 == "Others")
                {
                    newWhyAnalysisType.Why3 = insertDropValue(whyAnalysisType.Why3);
                }
               
                else
                {
                    newWhyAnalysisType.Why3 = whyAnalysisType.WhyDrop3?.Name?? "";
                }
                if (whyAnalysisType.Why4 != "" || whyAnalysisType.Why4 == null
                    || checkDropdownisOther4 == "Others")
                {
                    newWhyAnalysisType.Why4 = insertDropValue(whyAnalysisType.Why4);
                }
               
                else
                {
                    newWhyAnalysisType.Why4 = whyAnalysisType.WhyDrop4?.Name??"";
                }
                if (whyAnalysisType.Why5 != "" || whyAnalysisType.Why5 == null
                    || checkDropdownisOther5 == "Others")
                {
                    newWhyAnalysisType.Why5 = insertDropValue(whyAnalysisType.Why5);
                }
               
                else
                {
                    newWhyAnalysisType.Why5 = whyAnalysisType.WhyDrop5?.Name??"";
                }
                if (whyAnalysisType.Why6 != "" || whyAnalysisType.Why6 == null
                    || checkDropdownisOther6 == "Others")
                {
                    newWhyAnalysisType.Why6 = insertDropValue(whyAnalysisType.Why6);
                }
               
                else
                {
                    newWhyAnalysisType.Why6 = whyAnalysisType.WhyDrop6?.Name??"";
                }
                


                newWhyAnalysisTypes.Add(newWhyAnalysisType);
            }
            return newWhyAnalysisTypes;
        }

        private string? insertDropValue(string? value)
        {
            var existInTable = _szFleetMgrContext.WhyReasonMasters
                .FirstOrDefault(d => d.Name == value);
            if(existInTable!=null)
            {
                return value;
            }
            var updateReason = CreateWhyReason(value);

            _szFleetMgrContext.WhyReasonMasters.Add(updateReason);
            _szFleetMgrContext.SaveChanges();
            return updateReason.Name;

        }
        private List<PostWhyAnalysisDetail> ConvertToPostWhyAnalysisDetail(List<TWhyAnalysisDetail> whyAnalysisDetails)
        {
            List<PostWhyAnalysisDetail> postWhyAnalysisDetails = new List<PostWhyAnalysisDetail>();

            foreach (var whyAnalysisDetail in whyAnalysisDetails)
            {
                PostWhyAnalysisDetail postWhyDetail = new PostWhyAnalysisDetail
                {
                    Id = whyAnalysisDetail.Id,
                    FkAnalysisId = whyAnalysisDetail.FkAnalysisId,
                    FkTypeId = whyAnalysisDetail.FkTypeId,
                    Hrs = whyAnalysisDetail.Hrs,
                    Ai = whyAnalysisDetail.Ai,
                    Why1 = whyAnalysisDetail.Why1,
                    Why2 = whyAnalysisDetail.Why2,
                    Why3 = whyAnalysisDetail.Why3,
                    Why4 = whyAnalysisDetail.Why4,
                    Why5 = whyAnalysisDetail.Why5,
                    Why6 = whyAnalysisDetail.Why6,
                   
                    CreatedBy = whyAnalysisDetail.CreatedBy,
                    CreatedDate = whyAnalysisDetail.CreatedDate,
                    ModifiedBy = whyAnalysisDetail.ModifiedBy,
                    ModifiedDate = whyAnalysisDetail.ModifiedDate,
                    WhyDrop1 = _szFleetMgrContext.WhyReasonMasters.FirstOrDefault(d=>d.Name==whyAnalysisDetail.Why1)??null, 
                    WhyDrop2 = _szFleetMgrContext.WhyReasonMasters.FirstOrDefault(d => d.Name == whyAnalysisDetail.Why2) ?? null,
                    WhyDrop3 = _szFleetMgrContext.WhyReasonMasters.FirstOrDefault(d => d.Name == whyAnalysisDetail.Why3) ?? null,
                    WhyDrop4 = _szFleetMgrContext.WhyReasonMasters.FirstOrDefault(d => d.Name == whyAnalysisDetail.Why4) ?? null,
                    WhyDrop5 = _szFleetMgrContext.WhyReasonMasters.FirstOrDefault(d => d.Name == whyAnalysisDetail.Why5) ?? null,
                    WhyDrop6 = _szFleetMgrContext.WhyReasonMasters.FirstOrDefault(d => d.Name == whyAnalysisDetail.Why6) ?? null
                };

                postWhyAnalysisDetails.Add(postWhyDetail);
            }

            return postWhyAnalysisDetails;
        }

        private TWhyAnalysis MapWhyAnalysis(TWhyAnalysis existingWhyAnalysisData, TWhyAnalysis updateWhyAnalysisData)
        {
            existingWhyAnalysisData.Id = updateWhyAnalysisData.Id;

            existingWhyAnalysisData.AnalysisDate = updateWhyAnalysisData.AnalysisDate;
            existingWhyAnalysisData.CheckMark = updateWhyAnalysisData.CheckMark;
            existingWhyAnalysisData.StandardRemarks = updateWhyAnalysisData.StandardRemarks;
            existingWhyAnalysisData.Site = updateWhyAnalysisData.Site;
            existingWhyAnalysisData.Section = updateWhyAnalysisData.Section;
            existingWhyAnalysisData.Week = updateWhyAnalysisData.Week;
            existingWhyAnalysisData.SapCode = updateWhyAnalysisData.SapCode;
            existingWhyAnalysisData.State = updateWhyAnalysisData.State;
            existingWhyAnalysisData.GrandTotal = updateWhyAnalysisData.GrandTotal;
            existingWhyAnalysisData.ModelName = updateWhyAnalysisData.ModelName;
            existingWhyAnalysisData.OverallActionItem = updateWhyAnalysisData.OverallActionItem;
            existingWhyAnalysisData.TowerType = updateWhyAnalysisData.TowerType;
            existingWhyAnalysisData.Remarks1 = updateWhyAnalysisData.Remarks1;
            existingWhyAnalysisData.Remarks2 = updateWhyAnalysisData.Remarks2;
            existingWhyAnalysisData.SubBucket = updateWhyAnalysisData.SubBucket;
            existingWhyAnalysisData.MainBucket = updateWhyAnalysisData.MainBucket;
            existingWhyAnalysisData.Status = updateWhyAnalysisData.Status;
            return existingWhyAnalysisData;
        }
        private TWhyAnalysisDetail MapWhyAnalysisType(TWhyAnalysisDetail existingWhyAnalysisData, PostWhyAnalysisDetail updateWhyAnalysisData)
        {

            existingWhyAnalysisData.FkTypeId = updateWhyAnalysisData.FkTypeId;
            existingWhyAnalysisData.Ai = updateWhyAnalysisData.Ai;
            existingWhyAnalysisData.Hrs = updateWhyAnalysisData.Hrs;
            var checkDropdownisOther1 = updateWhyAnalysisData.WhyDrop1?.Name ?? "";
            var checkDropdownisOther2 = updateWhyAnalysisData.WhyDrop2?.Name ?? "";
            var checkDropdownisOther3 = updateWhyAnalysisData.WhyDrop3?.Name ?? "";
            var checkDropdownisOther4 = updateWhyAnalysisData.WhyDrop4?.Name ?? "";
            var checkDropdownisOther5 = updateWhyAnalysisData.WhyDrop5?.Name ?? "";
            var checkDropdownisOther6 = updateWhyAnalysisData.WhyDrop6?.Name ?? "";

            if (updateWhyAnalysisData.Why1 != "" || updateWhyAnalysisData.Why1==null
                || checkDropdownisOther1=="Others")
            {
                existingWhyAnalysisData.Why1 = insertDropValue(updateWhyAnalysisData.Why1);
            }

            else
            {
                existingWhyAnalysisData.Why1 = updateWhyAnalysisData.WhyDrop1?.Name?? "";
            }


            if (updateWhyAnalysisData.Why2 != "" || updateWhyAnalysisData.Why2 == null
                || checkDropdownisOther2 == "Others")
            {
                existingWhyAnalysisData.Why2 = insertDropValue(updateWhyAnalysisData.Why2);
            }
            else
            {
                existingWhyAnalysisData.Why2 = updateWhyAnalysisData.WhyDrop2?.Name??"";
            }
            if (updateWhyAnalysisData.Why3 != "" || updateWhyAnalysisData.Why3 == null
                || checkDropdownisOther3 == "Others")
            {
                existingWhyAnalysisData.Why3 = insertDropValue(updateWhyAnalysisData.Why3);
            }
            else
            {
                existingWhyAnalysisData.Why3 = updateWhyAnalysisData.WhyDrop3?.Name??"";
            }
            if (updateWhyAnalysisData.Why4 != "" || updateWhyAnalysisData.Why4 == null
                || checkDropdownisOther4 == "Others")
            {
                existingWhyAnalysisData.Why4 = insertDropValue(updateWhyAnalysisData.Why4);
            }
            else
            {
                existingWhyAnalysisData.Why4 = updateWhyAnalysisData.WhyDrop4?.Name??"";
            }
            if (updateWhyAnalysisData.Why5 != "" || updateWhyAnalysisData.Why5 == null
                || checkDropdownisOther5 == "Others")
             {
                existingWhyAnalysisData.Why5 = insertDropValue(updateWhyAnalysisData.Why5);
            }
            else
            {
                existingWhyAnalysisData.Why5 = updateWhyAnalysisData.WhyDrop5?.Name??"";
            }
            if (updateWhyAnalysisData.Why6 != "" || updateWhyAnalysisData.Why6 == null
                || checkDropdownisOther6 == "Others")
            {
                existingWhyAnalysisData.Why6 = insertDropValue(updateWhyAnalysisData.Why6);
            }
            else
            {
                existingWhyAnalysisData.Why6 = updateWhyAnalysisData.WhyDrop6?.Name??"";
            }
            return existingWhyAnalysisData;
        }

        public ResponseModel DeleteEmployee(int id)
        {
            try
            {
                var employee = _szFleetMgrContext.TLogbookEmployeeDetails
                        .Where(i => i.Id == id).FirstOrDefault();
                if (employee == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "employee does not exists", false);

                }

                _szFleetMgrContext.TLogbookEmployeeDetails.Remove(employee);
                _szFleetMgrContext.SaveChanges();

                return GetResponseModel(Constants.httpCodeSuccess, employee, "employe data deleted successfully", true);

            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel DeleteGridBreakdown(int id)
        {
            try
            {
                var gridBreakdown = _szFleetMgrContext.TLogbookGridBreakdownDetails
                        .Where(i => i.Id == id).FirstOrDefault();
                if (gridBreakdown == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "gridBreakdown does not exists", false);

                }

                _szFleetMgrContext.TLogbookGridBreakdownDetails.Remove(gridBreakdown);
                _szFleetMgrContext.SaveChanges();

                return GetResponseModel(Constants.httpCodeSuccess, gridBreakdown, "gridBreakdown data deleted successfully", true);

            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel DeleteWtgBreakdown(int id)
        {
            try
            {
                var wtgBreakdown = _szFleetMgrContext.TLogbookWtgBreakdownDetails
                        .Where(i => i.Id == id).FirstOrDefault();
                if (wtgBreakdown == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "wtgBreakdown does not exists", false);

                }

                _szFleetMgrContext.TLogbookWtgBreakdownDetails.Remove(wtgBreakdown);
                _szFleetMgrContext.SaveChanges();

                return GetResponseModel(Constants.httpCodeSuccess, wtgBreakdown, "wtgBreakdown data deleted successfully", true);

            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel DeleteScheduleMaintenance(int id)
        {
            try
            {
                var scheduleMaintenance = _szFleetMgrContext.TLogbookScheduleMaintenanceActivities
                        .Where(i => i.Id == id).FirstOrDefault();
                if (scheduleMaintenance == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "scheduleMaintenance does not exists", false);

                }

                _szFleetMgrContext.TLogbookScheduleMaintenanceActivities.Remove(scheduleMaintenance);
                _szFleetMgrContext.SaveChanges();

                return GetResponseModel(Constants.httpCodeSuccess, scheduleMaintenance, "scheduleMaintenance data deleted successfully", true);

            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel DeleteScada(int id)
        {
            try
            {
                var scada = _szFleetMgrContext.TLogbookScadaDetails
                        .Where(i => i.Id == id).FirstOrDefault();
                if (scada == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "scada does not exists", false);

                }

                _szFleetMgrContext.TLogbookScadaDetails.Remove(scada);
                _szFleetMgrContext.SaveChanges();

                return GetResponseModel(Constants.httpCodeSuccess, scada, "scada data deleted successfully", true);

            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel DeleteHoto(int id)
        {
            try
            {
                var hoto = _szFleetMgrContext.TLogbookHotos
                        .Where(i => i.Id == id).FirstOrDefault();
                if (hoto == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "hoto does not exists", false);

                }

                _szFleetMgrContext.TLogbookHotos.Remove(hoto);
                _szFleetMgrContext.SaveChanges();

                return GetResponseModel(Constants.httpCodeSuccess, hoto, "hoto data deleted successfully", true);

            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel DeleteRemarks(int id)
        {
            try
            {
                var remark = _szFleetMgrContext.TLogbookRemarks
                        .Where(i => i.Id == id).FirstOrDefault();
                if (remark == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "remarks does not exists", false);

                }

                _szFleetMgrContext.TLogbookRemarks.Remove(remark);
                _szFleetMgrContext.SaveChanges();

                return GetResponseModel(Constants.httpCodeSuccess, remark, "remarks data deleted successfully", true);

            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel UpdateWhyReasonMaster(string? otherName)
        {
            try
            {
                if(otherName==null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "Name should not be empty", false);
                }
                var existingData = _szFleetMgrContext.WhyReasonMasters.FirstOrDefault(d => d.Name == otherName);

                if(existingData!=null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "Already name exist in dropdown", true);

                }
                var updateReason = CreateWhyReason(otherName);

                _szFleetMgrContext.WhyReasonMasters.Add(updateReason);
                _szFleetMgrContext.SaveChanges();
                return GetResponseModel(Constants.httpCodeSuccess, updateReason, "New Reason Added in Logbook Successfully", true);

            }
            catch (Exception ex)
            {

                return GetResponseModel(Constants.httpCodeFailure,null,ex.Message,false);
            }
        }

        private WhyReasonMaster CreateWhyReason(string?  otherName)
        {
            WhyReasonMaster whyReasonMaster=new WhyReasonMaster();
            whyReasonMaster.Name = otherName;
            return whyReasonMaster;
        }
        private string ConvertDataTableToJSONString(System.Data.DataTable data)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in data.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in data.Columns)
                {
                    row.Add(col.ColumnName, dr[col] ?? "");
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }

        private ResponseModel GetResponseModel
            (int code, object? data, string message, bool status)
        {
            responseModel.code = code;
            responseModel.data = data;
            responseModel.message = message;
            responseModel.status = status;

            return responseModel;
        }
        private ResponseModelLogbook GetResponseModelLogbook
            (int code, object? data, string message, bool status, bool valid,int? countWtg)
        {
            responseModelLogbook.code = code;
            responseModelLogbook.data = data;
            responseModelLogbook.message = message;
            responseModelLogbook.status = status;
            responseModelLogbook.Validation = valid;
            responseModelLogbook.countWtg = countWtg;
            return responseModelLogbook;
        }
    }
}
