using UsersManagementService.CommonFiles;
using UsersManagementService.ViewModels;

namespace UsersManagementService.Repositories
{
    public interface IRoleRepository
    {
        ResponseModel GetAllRoles();
        ResponseModel GetRoleById(int roleId);
        ResponseModel AddRole(RoleMaster roleMaster, string userName);
        ResponseModel UpdateRole(RoleMaster roleMaster, string userName);
        ResponseModel GetDetailsByRoleName(List<string> roleName);
    }
}
