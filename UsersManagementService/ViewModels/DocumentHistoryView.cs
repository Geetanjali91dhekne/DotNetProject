namespace UsersManagementService.ViewModels
{
    public class DocumentHistoryView
    {
        public int Id { get; set; }

        public int? TaskId { get; set; }

        public string? FileName { get; set; }

        public string? FilePath { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? Guid { get; set; }

        public string? Base64Data { get; set; }
    }
}
