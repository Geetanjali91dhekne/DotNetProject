namespace SitesManagementService.ViewModels
{
    public class UserSiteMapping
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        public int? FkSiteId { get; set; }

        public int? FkCountry { get; set; }

        public int? FkState { get; set; }

        public int? FkArea { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? Status { get; set; }
    }
}
