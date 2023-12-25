namespace AuthenticationService.ViewModels
{
	public class RolePermissionList
	{
		public int RoleId { get; set; }
		public string Role { get; set; }

		public List<Permissions> Permission { get; set; }
	}
}
