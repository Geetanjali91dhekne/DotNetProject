using UsersManagementService.CommonFiles;
using UsersManagementService.Models;
using UsersManagementService.ViewModels;

namespace UsersManagementService.Repositories
{
    public class RoleRepository : IRoleRepository
	{
		private readonly FleetManagerContext _context;
		private readonly ResponseModel responseModel;

		public RoleRepository(FleetManagerContext mntFleetContext)
		{
			_context = mntFleetContext;
			this.responseModel = new ResponseModel();
		}
		
		public ResponseModel GetAllRoles()
		{
			try
			{
				var roleList = _context.TRoleMasters.ToList();
				if (roleList.Count == 0)
				{
					 return GetResponseModel(Constants.httpCodeSuccess, null,
                        "Roles Not Available", false);
				}
				var newRoleList = roleList.Select(role => CreateRoleMaster(role)).ToList();
				return GetResponseModel(Constants.httpCodeSuccess, newRoleList,
                        "data", true);
			}
			catch (Exception ex)
			{
				return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
			}
		
		}

		public ResponseModel GetRoleById(int roleId)
		{
			try
			{
				var role = _context.TRoleMasters.Where(i => i.Id == roleId).FirstOrDefault();
				if (role == null)
				{
					return GetResponseModel(Constants.httpCodeSuccess, null,
                        "Role Not Available", false);
				}
				var roleData = CreateRoleMaster(role);
				return GetResponseModel(Constants.httpCodeSuccess, roleData,
                        "data", true);
			}
			catch (Exception ex)
			{
				return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
			}
		}

		public ResponseModel AddRole(RoleMaster roleMaster, string userName)
		{
			try
			{
				if (_context.TRoleMasters.Any(c => c.RoleName == roleMaster.RoleName))
				{
					return GetResponseModel(Constants.httpCodeSuccess, null,
                        "Role already available", false);

				}

				var createRoleMaster = CreateTRoleMaster(roleMaster);

				createRoleMaster.CreatedBy = userName;
				createRoleMaster.ModifiedBy = userName;
				createRoleMaster.CreatedDate = DateTime.Now;
				createRoleMaster.ModifiedDate = DateTime.Now;

				_context.TRoleMasters.Add(createRoleMaster);
				_context.SaveChanges();

				roleMaster = CreateRoleUser(createRoleMaster, userName);

				return GetResponseModel(Constants.httpCodeSuccess, roleMaster,
                        "Role Added Succesfully", true);
			}
			catch (Exception ex)
			{
				return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
			}
		}

		public ResponseModel UpdateRole(RoleMaster roleMaster, string userName)
		{
			try
			{
				var role = _context.TRoleMasters.SingleOrDefault(c => c.Id == roleMaster.Id);
				if (role == null)
				{
					 return GetResponseModel(Constants.httpCodeSuccess, null,
                        "Roll does not Exists", false);

				}
				if (role.RoleName != roleMaster.RoleName &&
					_context.TRoleMasters.Any(c => c.RoleName == roleMaster.RoleName))
				{
					return GetResponseModel(Constants.httpCodeSuccess, null,
                        "Role already available", false);
				}
				var createRoleMaster = CreateTRoleMaster(roleMaster);
				createRoleMaster.ModifiedBy = userName;
				createRoleMaster.ModifiedDate = DateTime.Now;

				var updatedRole = MapRoleProperties(createRoleMaster, role);
				_context.TRoleMasters.Update(updatedRole);
				_context.SaveChanges();

				//roleMasterDTO.Id = roleId;
				roleMaster = CreateRoleUser(updatedRole, userName);

				return GetResponseModel(Constants.httpCodeSuccess, roleMaster,
                        "Role Updated Succesfully", true);
			}
			catch (Exception ex)
			{
			return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
			}
		}

        public ResponseModel GetDetailsByRoleName(List<string> roleName)
        {
            try
            {
                if (roleName == null || roleName.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null,
                        "RoleName Should Not be Null", false);
                }

                // Initialize a list to store unique role details based on menuName
                List<RoleMenuConfiguration> uniqueRoleDetails = new List<RoleMenuConfiguration>();

                foreach (var role in roleName)
                {
                    var roleDetails = _context.RoleMenuConfigurations
                        .Where(data => data.RoleName == role).OrderBy(d => d.Sequence)
                        .ToList();

                    foreach (var detail in roleDetails)
                    {
                        
                        var existingDetail = uniqueRoleDetails
                            .FirstOrDefault(x => x.MenuName == detail.MenuName
							&& x.Action==detail.Action);

                        if (existingDetail == null)
                        {
                           
                            uniqueRoleDetails.Add(detail);
                        }
                        
                    }
                }

                if (uniqueRoleDetails.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null,
                        "RoleName Does Not Exist", false);
                }

                return GetResponseModel(Constants.httpCodeSuccess, uniqueRoleDetails,
                    "data", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }


        //functions
        private TRoleMaster CreateTRoleMaster(RoleMaster roleMaster)
		{
			return new TRoleMaster
			{
				RoleName = roleMaster.RoleName,
				RoleDescription = roleMaster.RoleDescription,
				Status = roleMaster.Status,
				GroupCode = roleMaster.GroupCode,
				TRolePermissionMappings = new List<TRolePermissionMapping>()
			};
		}
		private RoleMaster CreateRoleMaster(TRoleMaster role)
		{
			return new RoleMaster
			{
				Id = role.Id,
				RoleName = role.RoleName,
				RoleDescription = role.RoleDescription,
				CreatedBy = role.CreatedBy,
				CreatedDate = role.CreatedDate,
				ModifiedBy = role.ModifiedBy,
				ModifiedDate = role.ModifiedDate,
				Status = role.Status,
				GroupCode = role.GroupCode
			};
		}

        private RoleMaster CreateRoleUser(TRoleMaster role,string? userName)
        {
            return new RoleMaster
            {
                Id = role.Id,
                RoleName = role.RoleName,
                RoleDescription = role.RoleDescription,
                CreatedBy = userName,
                CreatedDate = DateTime.Now,
                ModifiedBy = userName,
                ModifiedDate = DateTime.Now,
                Status = role.Status,
                GroupCode = role.GroupCode
            };
        }
        private TRoleMaster MapRoleProperties(TRoleMaster sourceRole, TRoleMaster destinationRole)
		{
			destinationRole.RoleName = sourceRole.RoleName;
			destinationRole.RoleDescription = sourceRole.RoleDescription;
			//destinationRole.CreatedBy = sourceRole.CreatedBy;
			//destinationRole.CreatedDate = sourceRole.CreatedDate;
			destinationRole.ModifiedBy = sourceRole.ModifiedBy;
			destinationRole.ModifiedDate = sourceRole.ModifiedDate;
			destinationRole.Status = sourceRole.Status;
			destinationRole.GroupCode = sourceRole.GroupCode;
			destinationRole.TRolePermissionMappings = sourceRole.TRolePermissionMappings;

            return destinationRole;
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
