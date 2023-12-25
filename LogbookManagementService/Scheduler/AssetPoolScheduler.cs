using LogbookManagementService.Models;
using OfficeOpenXml;
using Quartz;

namespace LogbookManagementService.Scheduler
{
    public class AssetPoolScheduler : IJob, IDisposable
    {
        private readonly IServiceScope _scope;
        private readonly IConfiguration _Configuration;
        //private readonly FleetManagerContext _fleetManagerContext;
        public AssetPoolScheduler(IServiceProvider services, IConfiguration configuration, IServiceProvider _ServiceProvider)
            //, FleetManagerContext fleetManagerContext)
        {
            _scope = services.CreateScope();
            _Configuration = configuration;
            //_fleetManagerContext = fleetManagerContext;
        }
        public Task Execute(IJobExecutionContext context)
        {
            AddAssetPoolData();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _scope?.Dispose();
        }

        public async void AddAssetPoolData()
        {
            using (var context = _scope.ServiceProvider.GetRequiredService<FleetManagerContext>())
            {
                //string folderpath = @"D:\FleetManager - Live\upload\bulkUploadFile\";
                string folderpath = @"D:\FleetManager\upload\MTTR_MTBF_Files\MTBF-WithAutoResetEvents.xlsx";
                //string folderpath = "/home/ubuntu/FleetManager/fleetmanager-bk/UsersManagementService/FileUpload";
                //string folderpath = @"D:\Git Clones\fleetmanager-bk\LogbookManagementService\UploadsFiles\MTBF-WithAutoResetEvents.xlsx";
                if (File.Exists(folderpath))
                {
                    using (FileStream fileStream = new FileStream(folderpath, FileMode.Open, FileAccess.Read))
                    {
                        var memoryStream = new MemoryStream();
                        fileStream.CopyTo(memoryStream);
                        memoryStream.Position = 0;
                        var file = new FormFile(memoryStream, 0, memoryStream.Length, fileStream.Name, fileStream.Name);

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
                                            CreatedBy = "Auto",
                                            CreatedDate = DateTime.Now,
                                            ModifiedBy = "Auto",
                                            ModifiedDate = DateTime.Now,
                                        };
                                        //if (!_fleetManagerContext.KpiMttrMtbfs.Any(x => x.PlantRole == kpimttrmtbf.PlantRole && x.Event == kpimttrmtbf.Event && x.EventDescription == kpimttrmtbf.EventDescription && x.SystemComponent == kpimttrmtbf.SystemComponent && x.Duration == kpimttrmtbf.Duration && x.Instance == kpimttrmtbf.Instance && x.LostProdKwh == kpimttrmtbf.LostProdKwh && x.MttrHours == kpimttrmtbf.MttrHours && x.MtbfHours == kpimttrmtbf.MtbfHours && x.AvailDistPer == kpimttrmtbf.AvailDistPer && x.AvailImpactPer == kpimttrmtbf.AvailImpactPer))
                                        //{
                                        context.KpiMttrMtbfs.Add(kpimttrmtbf);
                                        //}
                                    }

                                    context.SaveChanges();
                                    //}
                                }
                            }

                        }
                    }
                }
            }

        }
    }
}
