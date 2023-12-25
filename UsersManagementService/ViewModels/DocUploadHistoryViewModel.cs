namespace UsersManagementService.ViewModels
{
    public class DocUploadHistoryViewModel
    {
        public string? filename { get; set; }
        public int? mandate_id { get; set; }
        public string? entityname { get; set; }
        public int? RecordId { get; set; }
        public string? status { get; set; }
        public string? filepath { get; set; }
        public string? documenttype { get; set; }
        public string? remarks { get; set; }
        public string? VersionNumber { get; set; }
        public List<IFormFile>? file { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class DownloadUploadedFile
    {
        public string? base64String { get; set; }
        public string? filename { get; set; }
        
    }
}
