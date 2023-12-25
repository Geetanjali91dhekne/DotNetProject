namespace KpiAnalyzerService.ViewModels
{
    public class DataFilterConditionView
    {
        public int Id { get; set; }
        public string? column_name { get; set; }
        public string? condition { get; set; }

        public string? value1 { get; set; }

        public string? value2 { get; set; }
    }
    public class ResponseDataFilterCondition
    {
        public string? table_name { get; set; }
        public DataFilterConditionView[]? columns { get; set; }
    }
}
