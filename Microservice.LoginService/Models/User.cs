namespace AuthenticationService.Models
{
    public class User
    {
        public string Name { get; set; } = string.Empty;
        public string DomainId { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string EmpGrade { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string[]? Groups { get; set; }
        public string EmailId { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
    }
}
