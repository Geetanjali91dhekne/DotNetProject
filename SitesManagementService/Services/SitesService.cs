using SitesManagementService.CommonFiles;
using SitesManagementService.Repositories;
using SitesManagementService.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;

namespace SitesManagementService.Services
{
    public class SitesService : ISitesService
    {
        private readonly ISitesRepository _sitesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SitesService(ISitesRepository sitesRepository, IHttpContextAccessor httpContextAccessor)
        {
            _sitesRepository = sitesRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public ResponseModel GetDetails(UserSiteMapping userSiteMapping)
        {
            return _sitesRepository.GetDetails(userSiteMapping);
        }
        public ResponseModel DeleteByUserId(List<UserSiteMapping> userSiteMapping)
        {
            return _sitesRepository.DeleteByUserId(userSiteMapping);
        }
        public ResponseModel AddOrUpdateSites(List<UserSiteMapping> userSiteMapping)
        {
            var userName = _httpContextAccessor?.HttpContext?.User.FindFirstValue("UserName");
            return _sitesRepository.AddOrUpdateSites(userSiteMapping, userName);
        }
    }
}
