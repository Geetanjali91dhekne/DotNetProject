using LogbookManagementService.Models;

namespace LogbookManagementService.ViewModels
{
    public class ResponseKpiDetail
    {
        public string Category { get; set; }    
        public List<TKpiDetail> KpiList { get; set; }
    }
}
