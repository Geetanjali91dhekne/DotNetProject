namespace AuthenticationService.Models
{
	public class Login
	{
		public string domainId { get; set; } = string.Empty;
		public string password { get; set; } = string.Empty;
		public string appType { get; set; } = string.Empty;
		public string url { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
    }
}
