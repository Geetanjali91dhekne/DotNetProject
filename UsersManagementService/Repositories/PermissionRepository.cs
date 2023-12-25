using Microsoft.EntityFrameworkCore;
using UsersManagementService.CommonFiles;
using UsersManagementService.Models;
using UsersManagementService.ViewModels;

namespace UsersManagementService.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly FleetManagerContext _context;
        private readonly ResponseModel responseModel;

        public PermissionRepository(FleetManagerContext mntFleetContext)
        {
            _context = mntFleetContext;
            this.responseModel = new ResponseModel();
        }

        
        public ResponseModel GetAllPermission()
        {  
            try
            {
                var permissionList = _context.TPermissions.ToList();
                if (permissionList == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null,
                        "Permissions Doesn't Exist", false);
                }
                var newpermissionList = permissionList.Select(permission => CreatePermission(permission,""))
                    .ToList();

                return GetResponseModel(Constants.httpCodeSuccess, newpermissionList,
                        "data", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

       
        public ResponseModel GetPermissionByRole(int roleId)
        {
            try
            {
                List<Permission> allPermissionList = new List<Permission>();
                var role = _context.TRoleMasters.Find(roleId);
                if (role == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null,
                        "Role Doesn't Exist", false);
                }

                var permissionList = _context.TRolePermissionMappings
                    .Where(r => r.FkRoleId == roleId && r.FkPermissionId != 0 && r.Status=="Active")
                    .Select(r => r.FkPermissionId)
                    .ToList();
                
                    

                if (permissionList.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null,
                        "Role Doesn't have any Permissions", false);
                }

                var permissionLists = _context.TPermissions
                    .Where(p => permissionList.Contains(p.Id))
                    .ToList();

                //Not Active Permission List
                var permissionNoActiveList = _context.TPermissions
                 .Where(p => !permissionList.Contains(p.Id))
                .ToList();


                var newpermissionList = permissionLists.Select(permission => CreatePermission(permission, "Active"))
                    .ToList();
                foreach (var list in newpermissionList)
                {
                    allPermissionList.Add(list);
                }
                var newpermissionNoActiveList = permissionNoActiveList.Select(permission => CreatePermission(permission, "In Active"))
                   .ToList();
                foreach (var list in newpermissionNoActiveList)
                {
                    allPermissionList.Add(list);
                }

                return GetResponseModel(Constants.httpCodeSuccess, allPermissionList,
                        "data", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }


        public ResponseModel AddRolePermissionMapping
            (RolePermissionMapping rolePermissionMap, string userName)
        {
            try
            {
                if (rolePermissionMap == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null,
                        "Role or Permission doesn't exists", false);
                }
                var role = _context.TRoleMasters
                        .FirstOrDefault(r => r.Id == rolePermissionMap.FkRoleId);
                if (role == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, 
                        "Role ID is not existed", false);
                }
                var permission = _context.TPermissions
                    .FirstOrDefault(p => p.Id == rolePermissionMap.FkPermissionId);
                if (permission == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null,
                        "Permission ID is not existed", false);
                }
                //new
                var existedRolePermissionMapping = _context.TRolePermissionMappings
                    .FirstOrDefault(r => r.FkRoleId == rolePermissionMap.FkRoleId &&
                                    r.FkPermissionId == rolePermissionMap.FkPermissionId);

                if(existedRolePermissionMapping != null)
                {
                    //rolePermissionMappingDTO.Id = existedRolePermissionMapping.Id;
                    rolePermissionMap = CreateRolePermissions(existedRolePermissionMapping);

                    return GetResponseModel(Constants.httpCodeSuccess, rolePermissionMap,
                        "Role Permission mapping Already available", false);
                }
                
                var createRolePermissionMap = CreateRolePermissionMapping(rolePermissionMap);
                createRolePermissionMap.CreatedBy = userName;
                createRolePermissionMap.CreatedDate = DateTime.Now;
                createRolePermissionMap.ModifiedBy = userName;
                createRolePermissionMap.ModifiedDate = DateTime.Now;

                _context.TRolePermissionMappings.Add(createRolePermissionMap);
                _context.SaveChanges();

                rolePermissionMap = CreateRolePermissions(createRolePermissionMap);

                return GetResponseModel(Constants.httpCodeSuccess, rolePermissionMap,
                        "Role Permission mapping added successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel UpdateRolePermissionMapping
            (List<RolePermissionMapping> rolePermissionMap, string userName)
        {
            try
            {
                
                List<TRolePermissionMapping> newRolePermissionMapping = new List<TRolePermissionMapping>();
                foreach (var map in rolePermissionMap)
                {
                  
                    ////cases for FK
                    //var checkRole = _context.TRoleMasters.SingleOrDefault(data => data.Id == map.FkRoleId);
                    //if(checkRole==null)
                    //{
                    //    return GetResponseModel(Constants.httpCodeSuccess, null, "Role Id Not Found.", false);
                    //}
                    //var checkPermission = _context.TPermissions.SingleOrDefault(data => data.Id == map.FkPermissionId);
                    //if(checkPermission==null)
                    //{
                    //    return GetResponseModel(Constants.httpCodeSuccess, null, "Permission Id Not Found.", false);
                    //}
                   
                    
                    var existingRolePermission = _context.TRolePermissionMappings
                        .SingleOrDefault(data => data.FkRoleId == map.FkRoleId &&
                        data.FkPermissionId == map.FkPermissionId);

                    if(existingRolePermission!=null)
                    {
                      var updateRolePermission= MapRolePermisissionProperties
                        (existingRolePermission, map);
                        updateRolePermission.CreatedBy = userName;
                        updateRolePermission.CreatedDate = DateTime.Now;
                        _context.Update(updateRolePermission);
                        _context.SaveChanges();
                        newRolePermissionMapping.Add(updateRolePermission);

                     

                    }
                    else
                    {
                        var createRolePermissionMap = CreateRolePermissionMapping(map);
                        createRolePermissionMap.ModifiedBy = userName;
                        createRolePermissionMap.ModifiedDate = DateTime.Now;
                        createRolePermissionMap.CreatedBy = userName;
                        createRolePermissionMap.CreatedDate = DateTime.Now;
                        _context.TRolePermissionMappings.Add(createRolePermissionMap);
                        _context.SaveChanges();
                        newRolePermissionMapping.Add(createRolePermissionMap);

                    }
                }
                return GetResponseModel(Constants.httpCodeSuccess, newRolePermissionMapping, "Role Permission Updated.", true);
              
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

      



        private Permission CreatePermission(TPermission permission,string status)
        {
            return new Permission
            {
                Id = permission.Id,
                EntityCategory = permission.EntityCategory,
                EntityName = permission.EntityName,
                Actions = permission.Actions,
                CreatedBy = permission.CreatedBy,
                CreatedDate = permission.CreatedDate,
                ModifiedBy = permission.ModifiedBy,
                ModifiedDate = permission.ModifiedDate,
                Status = status
            };
        }
      
        private TRolePermissionMapping CreateRolePermissionMapping(RolePermissionMapping map)
        {
            
            return new TRolePermissionMapping
            {
                FkRoleId = map.FkRoleId,
                FkPermissionId = map.FkPermissionId,            
                Status = map.Status,
                IsAccess = map.IsAccess,
            };
        }
        private RolePermissionMapping CreateRolePermissions(TRolePermissionMapping rolePermissionMapping)
        {
            return new RolePermissionMapping
            {
                Id = rolePermissionMapping.Id,
                FkRoleId = rolePermissionMapping.FkRoleId,
                FkPermissionId = rolePermissionMapping.FkPermissionId,
                CreatedBy = rolePermissionMapping.CreatedBy,
                CreatedDate = rolePermissionMapping.CreatedDate,
                ModifiedBy = rolePermissionMapping.ModifiedBy,
                ModifiedDate = rolePermissionMapping.ModifiedDate,
                Status = rolePermissionMapping.Status,
                IsAccess = rolePermissionMapping.IsAccess,
            };
        }
        private TRolePermissionMapping MapRolePermisissionProperties
            (TRolePermissionMapping existedRolePermissionMapping, RolePermissionMapping rolePermissionMapping)
        {
            existedRolePermissionMapping.FkRoleId = rolePermissionMapping.FkRoleId;
            existedRolePermissionMapping.FkPermissionId = rolePermissionMapping.FkPermissionId;   
            existedRolePermissionMapping.ModifiedBy = rolePermissionMapping.ModifiedBy;
            existedRolePermissionMapping.ModifiedDate = rolePermissionMapping.ModifiedDate;
            existedRolePermissionMapping.Status = rolePermissionMapping.Status;
            existedRolePermissionMapping.IsAccess = rolePermissionMapping.IsAccess;

            return existedRolePermissionMapping;
        }

        private ResponseModel GetResponseModel
            (int code, object? data, string message, bool status)
        {
            responseModel.code = code;
            responseModel.data = data;
            responseModel.message = message;
            responseModel.status = status;

            return responseModel;
        }
    }
}
