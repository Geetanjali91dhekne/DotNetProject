using LogbookManagementService.CommonFiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SitesManagementService.CommonFiles;
using SitesManagementService.Models;
using SitesManagementService.ViewModels;
using System.Data;

namespace SitesManagementService.Repositories
{
    public class SitesRepository : ISitesRepository
	{
		private readonly FleetManagerContext _szFleetMgrContext;
		private readonly ResponseModel responseModel;

		public SitesRepository(FleetManagerContext szFleetMgrContext)
		{
			_szFleetMgrContext = szFleetMgrContext;
			responseModel = new ResponseModel();
		}

		public ResponseModel GetDetails(UserSiteMapping userSiteMapping)
		{
			try
			{
				if (userSiteMapping.Username == null)
				{
					//whole list
					var response = _szFleetMgrContext.TUserSiteMappings.ToList();

 					if (response.Count == 0)
                    {
                        return GetResponseModel(Constants.httpCodeSuccess, null,
                            "No data available", false);
                    }
					var responseData = CreateTUserSiteMappings(response);

                    return GetResponseModel(Constants.httpCodeSuccess, responseData, "all data", true);
                }
                else
                {
                    var response = _szFleetMgrContext.TUserSiteMappings
                        .Where(u => u.Username == userSiteMapping.Username)
                        .ToList();

                    if(response.Count == 0)
                    {
                        return GetResponseModel(Constants.httpCodeSuccess, null,
                            "User not existed", false);
                    }

					var responseData = CreateTUserSiteMappings(response);

                    return GetResponseModel(Constants.httpCodeSuccess, responseData, "data", true);
                }
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

		public ResponseModel DeleteByUserId(List<UserSiteMapping> userSiteMapping)
		{
			try
			{
				if (userSiteMapping.Count == 0)
				{
					 return GetResponseModel(Constants.httpCodeSuccess, null,
                            "please send data to delete", false);
				}

				var userList = userSiteMapping
					.Select(dto => _szFleetMgrContext.TUserSiteMappings
						.FirstOrDefault(i => i.Id == dto.Id))
					.Where(user => user != null)
					.ToList();

				if (userList.Count == 0)
				{
					return GetResponseModel(Constants.httpCodeSuccess, null,
                            "no data found", false);
				}

				_szFleetMgrContext.TUserSiteMappings.RemoveRange(userList);
				_szFleetMgrContext.SaveChanges();


				userSiteMapping = CreateTUserSiteMappings(userList);
				return GetResponseModel(Constants.httpCodeSuccess, userSiteMapping,
                    "these data deleted successfully", true);

				
			}
			catch (Exception ex)
			{
				return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
			}
		}

		public ResponseModel AddOrUpdateSites(List<UserSiteMapping> userSiteMapping,
			string userName)
		{
			try
			{
				userName = "check";
				if (userSiteMapping.Count == 0)
				{
					return GetResponseModel(Constants.httpCodeSuccess, null, 
                        "please send data to add or update", false);
				}

				//check for --> fkSiteId, fkCountry, fkState, fkArea
				foreach (var user in userSiteMapping)
				{
					var siteDB = _szFleetMgrContext.SiteIds
						.Where(i => i.Id == user.FkSiteId).FirstOrDefault();
					var countryDB = _szFleetMgrContext.Countries
						.Where(i => i.Id == user.FkCountry).FirstOrDefault();
					var stateDB = _szFleetMgrContext.States
						.Where(i => i.Id == user.FkState).FirstOrDefault();
					var areaDB = _szFleetMgrContext.Areas
						.Where(i => i.Id == user.FkArea).FirstOrDefault();

					if (siteDB == null || countryDB == null ||
						stateDB == null || areaDB == null)
					{
						return GetResponseModel(Constants.httpCodeSuccess, null,
                            "please send correct siteId/country/state/area", false);
					}
				}

				//seprating objects by ID 
				var userListWithIDData = userSiteMapping.Where(i => i.Id != 0).ToList();
				var userListWithoutIDData = userSiteMapping.Where(i => i.Id == 0).ToList();

				var userListWithID = CreateTUserSiteMapping(userListWithIDData);
				var userListWithoutID = CreateTUserSiteMapping(userListWithoutIDData);

				var existedUserList = userListWithIDData
					.Select(dto => _szFleetMgrContext.TUserSiteMappings
						.FirstOrDefault(i => i.Id == dto.Id))
					.Where(user => user != null)
					.ToList();
				//now update with new data
				if (existedUserList.Count != 0)
				{
					var updatedUserList = MapUserSiteProperties(existedUserList, userListWithID)
						.Select(user =>
						{
							user.ModifiedBy = userName;
							user.ModifiedDate = DateTime.Now;
							return user;
						})
						.ToList();
					_szFleetMgrContext.TUserSiteMappings.UpdateRange(updatedUserList);
					_szFleetMgrContext.SaveChanges();
				}

				//create new
				if (userListWithoutID.Count != 0)
				{
					userListWithoutID = userListWithoutID.Select(user =>
					{
						user.CreatedBy = userName;
						user.CreatedDate = DateTime.Now;
						user.ModifiedBy = userName;
						user.ModifiedDate = DateTime.Now;
						return user;
					})
					.ToList();
					_szFleetMgrContext.TUserSiteMappings.AddRange(userListWithoutID);
					_szFleetMgrContext.SaveChanges();
				}

				if (existedUserList.Count == 0 && userListWithoutID.Count == 0)
				{
					return GetResponseModel(Constants.httpCodeSuccess, null,
                            "user not avialble", false);
				}

					return GetResponseModel(Constants.httpCodeSuccess, null,
                            "success", true);
			}
			catch (Exception ex)
			{
				return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
			}
		}

		//functions
		private List<UserSiteMapping> CreateTUserSiteMappings
			(List<TUserSiteMapping> userSiteMappings)
		{
			List<UserSiteMapping> dto = userSiteMappings.Select(user => new UserSiteMapping
			{
				Id = user.Id,
				Username = user.Username,
				FkSiteId = user.FkSiteId,
				FkCountry = user.FkCountry,
				FkState = user.FkState,
				FkArea = user.FkArea,
				CreatedBy = user.CreatedBy,
				CreatedDate = user.CreatedDate,
				ModifiedBy = user.ModifiedBy,
				ModifiedDate = user.ModifiedDate,
				Status = user.Status

			}).ToList();

			return dto;
		}
		private List<TUserSiteMapping> CreateTUserSiteMapping
			(List<UserSiteMapping> userSiteMapping)
		{
			List<TUserSiteMapping> userList = userSiteMapping.Select(dto => new TUserSiteMapping
			{
				//Id = dto.Id,
				Username = dto.Username,
				FkSiteId = dto.FkSiteId,
				FkCountry = dto.FkCountry,
				FkState = dto.FkState,
				FkArea = dto.FkArea,
				//CreatedBy = dto.CreatedBy,
				//CreatedDate = dto.CreatedDate,
				//ModifiedBy = dto.ModifiedBy,
				//ModifiedDate = dto.ModifiedDate,
				Status = dto.Status

			}).ToList();

			return userList;
		}
		private List<TUserSiteMapping> MapUserSiteProperties
			(List<TUserSiteMapping> existedUserList, List<TUserSiteMapping> userListWithID)
		{
			List<TUserSiteMapping> userList = new List<TUserSiteMapping>();

			for (int i = 0; i < existedUserList.Count; i++)
			{
				var existedUser = existedUserList[i];
				var newUser = userListWithID[i];

				existedUser.Username = newUser.Username;
				existedUser.FkSiteId = newUser.FkSiteId;
				existedUser.FkCountry = newUser.FkCountry;
				existedUser.FkState = newUser.FkState;
				existedUser.FkArea = newUser.FkArea;
				existedUser.Status = newUser.Status;
				//existedUser.CreatedBy = newUser.CreatedBy;
				//existedUser.CreatedDate = newUser.CreatedDate;
				//existedUser.ModifiedBy = newUser.ModifiedBy;
				//existedUser.ModifiedDate = newUser.ModifiedDate;

				// Add the updated or existing user to the final userList
				userList.Add(existedUser);
			}

            return userList;
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
