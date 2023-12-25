namespace LogbookManagementService.ViewModels
{
    public class ResponseFilterData
    {
        public string? Filter { get; set; }

        public List<FilterCount>? ErrorCount { get; set; }
        public List<FilterDurCount>? TotalDurationCount  {get;set;}
    }
    public class FilterCount
    {
        public string? Error { get; set; }   
        public string? Count { get; set; }
    }
    public class FilterDurCount
    {
        public string? Error { get; set; }
        public string? Count { get; set; }
    }
 
}