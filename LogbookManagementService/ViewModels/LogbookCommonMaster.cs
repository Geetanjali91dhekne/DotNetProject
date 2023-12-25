using LogbookManagementService.Models;

namespace LogbookManagementService.ViewModels
{
	public class LogbookCommonMaster
	{
		public string? MasterCategory { get; set; }
		public List<TLogbookCommonMaster> CommonMasterLists { get; set; }
	}
}
