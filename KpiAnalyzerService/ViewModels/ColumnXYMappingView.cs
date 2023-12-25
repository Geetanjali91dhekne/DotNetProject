namespace KpiAnalyzerService.ViewModels
{
    public class ColumnXYMappingView
    {
        public int Id { get; set; }

        public string? table_name { get; set; }

        public string? column_name { get; set; }

        public string? axis { get; set; }

    }
    public class ResponseColumnXYMapping
    {
        public string? table_name { get; set; }
        public string[]? Xaxis { get; set; }
        public string[]? Yaxis { get; set; }
    }
}
