using SitesManagementService.CommonFiles;
using SitesManagementService.ViewModels;

namespace SitesManagementService.Repositories
{
    public interface ISitesRepository
    {
        ResponseModel GetDetails(UserSiteMapping userSiteMapping);
        ResponseModel DeleteByUserId(List<UserSiteMapping> userSiteMapping);
        ResponseModel AddOrUpdateSites(List<UserSiteMapping> userSiteMapping, 
            string userName);
    }
}
