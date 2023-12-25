namespace AuthenticationService.ViewModels
{
	public class Permissions
	{
		public int Id { get; set; }

		public string? EntityCategory { get; set; }

		public string? EntityName { get; set; }

		public string? Actions { get; set; }
	}
}
