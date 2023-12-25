namespace LogbookManagementService.ViewModels
{
    public class ResponseKpiMT
    {
        public ResponseKpiMA ResponseData { get; set; }
        public  List<string>? ModelList { get; set; }
    }
    public class ResponseKpiMTD
    {
        public ResponseKpiDec ResponseData { get; set; }
        public List<string>? ModelList { get; set; }
    }
}
