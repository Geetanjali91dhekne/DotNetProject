namespace UsersManagementService.ViewModels
{
    public class UploadAndSaveExcelFormView
    {
        public string MasterName { get; set; }
        public string? UserName { get; set; }
        public int? recordId { get; set; }
        public List<IFormFile> files { get; set; }
    }
}
