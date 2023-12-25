using UsersManagementService.CommonFiles;
using UsersManagementService.ViewModels;

namespace UsersManagementService.Repositories
{
    public interface IUserRepository
    {
        ResponseModel AddGroup(string userName, List<RoleMaster> roleList, string username);
        ResponseModel DeleteGroupByGroupCode(string groupCode);
        ResponseModel GetGroupDetailsByGroupCode(string groupCode);
        ResponseModel GetUserRoleMapping();
        ResponseModel GetAllUserSiteMapping();
        ResponseModel GetSiteByUser(string? userName, string? employeeCode, string? countryName, string? stateName, string? areaName, string? siteName);
        //ResponseModel UpdateSiteStatus(List<Dictionary<string, string>> siteStatusUpdates, string userName);
        ResponseModel GetScCountryOmsPbi();
        ResponseModel GetStateByCountryCode(string? countryCode);
        ResponseModel GetAreaByStateCode(string? stateCode);
        ResponseModel GetSiteByAreaCode(string? areaCode);
        ResponseModel UpdateUserSite(List<UserSiteResponse> userSiteResponseList, string? userName);
        
        ResponseModel GetGroupList();

    }
}
