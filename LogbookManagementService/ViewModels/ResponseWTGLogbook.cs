namespace LogbookManagementService.ViewModels
{
    public class ResponseWTGLogbook
    {
        public string? Turbine  { get; set; }

        public string? Error { get; set; }
        public TimeOnly? TimeFrom { get; set; }
        public TimeOnly? TimeTo { get; set;}
        public string? TotalTime { get; set; }

    }
    public class ResponseWTGLogbookList
    {
        public List<TurbineList> TurbineLists { get; set; }
        public List<ErrorList> ErrorLists { get; set; }
    }
    public class TurbineList
    {
        public string? Turbine { get; set; }

    }
    public class ErrorList
    {
        public string? Error { get; set; }
    }
    public class ResponseWTGLogbooks
    {
        public List<ResponseWTGLogbook> TurbineLists { get; set; }
    }
    public class ResponseWTGRecords
    {
        public string? Turbine { get; set; }
        public string? Date { get; set; }
        public string? EventCode { get; set; }
    }
}
