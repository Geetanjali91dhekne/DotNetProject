namespace LogbookManagementService.ViewModels
{
    public class ResponseKpiMA
    {
        public decimal? financialYear {get;set;}
        public decimal? ComingYear { get;set;}
        public decimal? thisMonth { get;set;}
        public decimal? thisWeek { get; set; }
    }
    public class ResponseKpiDec
    {
        public decimal? financialYear { get; set; }
        public decimal? ComingYear { get; set; }
        public decimal? thisMonth { get; set; }
        public decimal? thisWeek { get; set; }
    }
}
