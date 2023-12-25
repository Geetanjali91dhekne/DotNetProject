using UsersManagementService.CommonFiles;
using UsersManagementService.ViewModels;

namespace UsersManagementService.Services
{
    public interface IPermissionService
    {
        ResponseModel GetAllPermission();
        ResponseModel GetPermissionByRole(int rollId);
        ResponseModel AddRolePermissionMapping(RolePermissionMapping rolePermissionMapping);
        ResponseModel UpdateRolePermissionMapping(List<RolePermissionMapping> rolePermissionMapping);

        //ResponseModel UpdatePermissionByRoleId(int? id);
    }
}
