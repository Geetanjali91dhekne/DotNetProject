using System.Security.Claims;
using UsersManagementService.CommonFiles;
using UsersManagementService.Repositories;
using UsersManagementService.ViewModels;

namespace UsersManagementService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;

        }

        public ResponseModel AddGroup(string groupCode, List<RoleMaster> roleList)
        {
            string username = _httpContextAccessor?.HttpContext?.User.FindFirstValue("UserName");
            return _userRepository.AddGroup(groupCode, roleList, username);
        }
        public ResponseModel DeleteGroupByGroupCode(string groupCode)
        {
            return _userRepository.DeleteGroupByGroupCode(groupCode);
        }
        public ResponseModel GetGroupDetailsByGroupCode(string groupCode)
        {
            return _userRepository.GetGroupDetailsByGroupCode(groupCode);
        }

        public ResponseModel GetUserRoleMapping()
        {
            return _userRepository.GetUserRoleMapping();
        }
        public ResponseModel GetAllUserSiteMapping()
        {
            return _userRepository.GetAllUserSiteMapping();
        }
        public ResponseModel GetSiteByUser(string? userName, string? employeeCode, string? countryName, string? stateName, string? areaName, string? siteName)
        {
            return _userRepository.GetSiteByUser(userName, employeeCode, countryName, stateName, areaName, siteName);
        }
        public ResponseModel GetScCountryOmsPbi()
        {
            return _userRepository.GetScCountryOmsPbi();
        }
        public ResponseModel GetStateByCountryCode(string? countryCode)
        {
            return _userRepository.GetStateByCountryCode(countryCode);
        }
        public ResponseModel GetAreaByStateCode(string? stateCode)
        {
            return _userRepository.GetAreaByStateCode(stateCode);
        }
        public ResponseModel GetSiteByAreaCode(string? areaCode)
        {
            return _userRepository.GetSiteByAreaCode(areaCode);
        }
        public ResponseModel UpdateUserSite(List<UserSiteResponse> userSiteResponseList, string? userName)
        {
            return _userRepository.UpdateUserSite(userSiteResponseList, userName);
        }
        public ResponseModel GetGroupList()
        {
            return _userRepository.GetGroupList();
		}
        public ResponseModel SendEmail(string? templatename, int tableid, int taskid)
        {
            return _userRepository.SendEmail(templatename , tableid, taskid);
        }

    }
}
