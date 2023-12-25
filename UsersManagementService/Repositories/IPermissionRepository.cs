using Microsoft.OpenApi.Models;
using UsersManagementService.CommonFiles;
using UsersManagementService.ViewModels;

namespace UsersManagementService.Repositories
{
    public interface IPermissionRepository
    {
        ResponseModel GetAllPermission();
        ResponseModel GetPermissionByRole(int rollId);
        ResponseModel AddRolePermissionMapping
            (RolePermissionMapping rolePermissionMapping, string userName);
        ResponseModel UpdateRolePermissionMapping
            (List<RolePermissionMapping> rolePermissionMapping, string userName);
        //ResponseModel UpdatePermissionByRoleId(int? id);
    }
}
