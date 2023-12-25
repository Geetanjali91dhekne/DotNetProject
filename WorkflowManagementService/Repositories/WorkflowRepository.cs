using WorkflowManagementService.CommonFiles;
using WorkflowManagementService.Models;

namespace WorkflowManagementService.Repositories
{
    public class WorkflowRepository : IWorkflowRepository
    {
        private readonly FleetManagerContext _context;
        private readonly ResponseModel responseModel;

        public WorkflowRepository(FleetManagerContext szFleetMgrContext)
        {
            _context = szFleetMgrContext;
            responseModel = new ResponseModel();
        }

        public ResponseModel GetDetailsRuntime(int tableId, string tableName, string roleName, string userName)
        {
            try
            {
                if(tableId == 0 || tableName == null || roleName == null || userName == null) 
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, 
                        "either of the field entered is wrong", false); 
                }
                var data = _context.WorkflowRuntimes
                        .Where(i => i.TableId == tableId)
                        .Where(i => i.TableName == tableName)
                        .Where(i => i.RoleName == roleName)
                        .Where(i => i.UserName == userName)
                        .ToList();

                if (data.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, 
                        "data is not available", false);
                }

                return GetResponseModel(Constants.httpCodeSuccess, data, "data", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel GetDetailsRuntimeHistory(int tableId, string tableName)
        {
            try
            {
                if (tableId == 0 || tableName == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null,
                        "either of the field entered is wrong", false);
                }

                var data = _context.WorkflowRuntimes
                        .Where(i => i.TableId == tableId)
                        .Where(i => i.TableName == tableName)
                        .ToList();

                if (data.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null,
                        "data is not available", false);
                }

                return GetResponseModel(Constants.httpCodeSuccess, data, "data", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
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
