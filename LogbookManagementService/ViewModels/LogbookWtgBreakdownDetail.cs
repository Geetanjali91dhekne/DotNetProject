namespace LogbookManagementService.ViewModels
{
	public class LogbookWtgBreakdownDetail
	{
		public int Id { get; set; }

		public string? SiteName { get; set; }

		public int? FkSiteId { get; set; }

		public string? Turbine { get; set; }

		public string? Error { get; set; }

		public string? ActionTaken { get; set; }

		public string? EptwNumber { get; set; }

		public string? PasswordUsage { get; set; }

		public string? PasswordUsageBy { get; set; }

		public DateTime? LogDate { get; set; }

		public string? CreatedBy { get; set; }

		public DateTime? CreatedDate { get; set; }

		public string? ModifiedBy { get; set; }

		public DateTime? ModifiedDate { get; set; }

		public string? TimeFrom { get; set; }

		public string? TimeTo { get; set; }

		public string? TotalTime { get; set; }

        public string? ShiftCycle { get; set; }

        public string? Closure { get; set; }

        public string? Status { get; set; }
        public int? FkTaskId { get; set; }

        public string? RowId { get; set; }

        public string? BreakdownCategory { get; set; }
    }

}
