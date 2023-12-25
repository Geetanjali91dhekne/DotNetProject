namespace UsersManagementService.ViewModels
{
    public class RoleMaster
    {
        public int Id { get; set; }

        public string? RoleName { get; set; }

        public string? RoleDescription { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? Status { get; set; }

        public string? GroupCode { get; set; }
    }
}
