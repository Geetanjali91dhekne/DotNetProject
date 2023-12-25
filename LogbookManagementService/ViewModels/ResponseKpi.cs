using LogbookManagementService.Models;

namespace LogbookManagementService.ViewModels
{
    public class ResponseKpi
    {
        //public List<string>? KpiPmList { get; set; }
        public CalculatedData? CalculatedData { get; set; }
        public List<string>? YearList { get; set; }
    }

    public class CalculatedData
    {
        public int Total { get; set; }
        public int Planned { get; set; }
        public int Pending { get; set; }
        public int Completed { get; set; }
    }
}
