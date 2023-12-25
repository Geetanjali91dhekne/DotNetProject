namespace UsersManagementService.Models
{
    public class TemplateConfigurationViewModel
    {
        public string? TemplateName { get; set; }
        public string? Subject { get; set; }
        public string? To { get; set; }
        public string? CC { get; set; }
        public string? BCC { get; set; }
        public string? MainBody { get; set; }
        public string? Keys { get; set; }
        public string? List1 { get; set; }
        public string? List2 { get; set; }
        public string? List3 { get; set; }
        public string? List4 { get; set; }
        public string? List5 { get; set; }
        public string? url { get; set; }
        public Dictionary<string, string> pairs { get; set; }

    }
}
