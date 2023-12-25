namespace UsersManagementService.ViewModels
{
    public class Permission
    {
        public int Id { get; set; }

        public string? EntityCategory { get; set; }

        public string? EntityName { get; set; }

        public string? Actions { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? Status { get; set; }
    }
}
