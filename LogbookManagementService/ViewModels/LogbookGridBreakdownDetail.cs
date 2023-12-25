namespace LogbookManagementService.ViewModels
{
	public class LogbookGridBreakdownDetail
	{
		public int Id { get; set; }

		public string? SiteName { get; set; }

		public int? FkSiteId { get; set; }

		public string? FeederName { get; set; }

		public string? GridDropReason { get; set; }

		public string? TimeFrom { get; set; }

		public string? TimeTo { get; set; }

		public string? TotalTime { get; set; }

		public string? RemarkAction { get; set; }

		public string? EptwNumber { get; set; }

		public DateTime? LogDate { get; set; }

		public string? CreatedBy { get; set; }

		public DateTime? CreatedDate { get; set; }

		public string? ModifiedBy { get; set; }

		public DateTime? ModifiedDate { get; set; }

        public string? ShiftCycle { get; set; }

        public string? Status { get; set; }
    }

}
