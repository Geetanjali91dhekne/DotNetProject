
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UsersManagementService.CommonFiles;
using UsersManagementService.Repositories;
using UsersManagementService.ViewModels;

namespace UsersManagementService.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _dbcontext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionService(IPermissionRepository dbcontext, IHttpContextAccessor httpContextAccessor)
        {
            _dbcontext = dbcontext;
            _httpContextAccessor = httpContextAccessor;
        }

        //Get All Permissions
        public ResponseModel GetAllPermission()
        {
            return _dbcontext.GetAllPermission();
        }


        //Get Permissions By Role
        public ResponseModel GetPermissionByRole(int roleId)
        {
            return _dbcontext.GetPermissionByRole(roleId);
        }

        public ResponseModel AddRolePermissionMapping(RolePermissionMapping rolePermissionMapping)
        {
            string userName = _httpContextAccessor?.HttpContext?.User.FindFirstValue("UserName");
            return _dbcontext.AddRolePermissionMapping(rolePermissionMapping, userName);
        }

        public ResponseModel UpdateRolePermissionMapping(List<RolePermissionMapping> rolePermissionMapping)
        {
            string userName = _httpContextAccessor?.HttpContext?.User.FindFirstValue("UserName");
            return _dbcontext.UpdateRolePermissionMapping(rolePermissionMapping, userName);
        }
        //public ResponseModel UpdatePermissionByRoleId(int? id)
        //{
        //    return _dbcontext.UpdatePermissionByRoleId(id);
        //}
    }
}
