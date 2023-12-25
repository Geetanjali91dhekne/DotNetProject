using SitesManagementService.CommonFiles;
using SitesManagementService.ViewModels;

namespace SitesManagementService.Services
{
    public interface ISitesService
    {
        ResponseModel GetDetails(UserSiteMapping userSiteMapping);
        ResponseModel DeleteByUserId(List<UserSiteMapping> userSiteMapping);
        ResponseModel AddOrUpdateSites(List<UserSiteMapping> userSiteMapping);
    }
}
