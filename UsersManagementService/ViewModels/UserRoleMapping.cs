namespace UsersManagementService.ViewModels
{
    public class UserRoleMapping
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        public int? FkRoleId { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? Status { get; set; }

        public string? GroupCode { get; set; }
    }
}
