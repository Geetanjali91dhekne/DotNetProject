using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UsersManagementService.Repositories;
using System.Security.Claims;
using UsersManagementService.ViewModels;
using UsersManagementService.CommonFiles;

namespace UsersManagementService.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleService(IRoleRepository roleRepository, IHttpContextAccessor httpContextAccessor)
        {
            _context = roleRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public ResponseModel GetAllRoles()
        {
            return _context.GetAllRoles();
        }

        public ResponseModel GetRoleById(int roleid)
        {
            return _context.GetRoleById(roleid);
        }
        public ResponseModel AddRole(RoleMaster roleMaster,string? userName)
        {
            return _context.AddRole(roleMaster, userName);
        }
        public ResponseModel UpdateRole(RoleMaster roleMaster, string? userName)
        { 
            return _context.UpdateRole(roleMaster, userName);
        }
        public ResponseModel GetDetailsByRoleName(List<String> roleName)
        {
            return _context.GetDetailsByRoleName(roleName);
        }
    }
}

