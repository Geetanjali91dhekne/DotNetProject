namespace LogbookManagementService.ViewModels
{
	public class LogbookScheduleMaintenanceActivity
	{
		public int Id { get; set; }

		public string? SiteName { get; set; }

		public int? FkSiteId { get; set; }

		public string? Turbine { get; set; }

		public string? Activity { get; set; }

		public string? Observation { get; set; }

		public string? EptwNumber { get; set; }

		public DateTime? LogDate { get; set; }

		public string? CreatedBy { get; set; }

		public DateTime? CreatedDate { get; set; }

		public string? ModifiedBy { get; set; }

		public DateTime? ModifiedDate { get; set; }

        public string? ShiftCycle { get; set; }

        public string? Closure { get; set; }

        public DateTime? RescheduleDate { get; set; }

        public string? Status { get; set; }
    }
}
