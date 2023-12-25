namespace LogbookManagementService.ViewModels
{
	public class LogbookHoto
	{
		public int Id { get; set; }

		public string? SiteName { get; set; }

		public int? FkSiteId { get; set; }

		public string? ShiftHandedOverBy { get; set; }

		public string? HandedOverDateTime { get; set; }

		public string? ShiftTakenOverBy { get; set; }
		public string? ShiftHours { get; set; }

		public string? TakenOverDateTime { get; set; }

		public DateTime? LogDate { get; set; }

		public string? CreatedBy { get; set; }

		public DateTime? CreatedDate { get; set; }

		public string? ModifiedBy { get; set; }

		public DateTime? ModifiedDate { get; set; }

        public string? ShiftCycle { get; set; }

        public string? Status { get; set; }
    }
}
