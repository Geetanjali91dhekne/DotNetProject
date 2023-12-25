using UsersManagementService.Models;

namespace UsersManagementService.ViewModels
{
    public class UserSiteResponse
    {
        public string? UserName { get; set; }
        public string? CountryName { get; set; }
        public string? StateName { get; set; }
        public string? AreaName { get; set; }
        public string? SiteName { get; set; }
        public string? Status { get; set; }
        public string? EmployeeCode { get; set; }
        public int? RoleId { get; set; }
    }
    public class ListUserSiteResponse
    {
        public TRoleMaster? roleObj { get; set; }
        public List<UserSiteResponse>? UserSiteResponses { get; set; }
    }
}