using UsersManagementService.CommonFiles;
using UsersManagementService.ViewModels;

namespace UsersManagementService.Services
{
    public interface IUserService
    {
        ResponseModel AddGroup(string groupCode, List<RoleMaster> roleList);
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
        ResponseModel GetGroupList();
        ResponseModel UpdateUserSite(List<UserSiteResponse> userSiteResponseList, string? userName);
        ResponseModel SendEmail(string? templatename, int tableid, int taskid);
    }
}
