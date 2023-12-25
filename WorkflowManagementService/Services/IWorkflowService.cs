using WorkflowManagementService.CommonFiles;

namespace WorkflowManagementService.Services
{
    public interface IWorkflowService
    {
        ResponseModel GetDetailsRuntime(int tableId, string tableName, string roleName, string userName);
        ResponseModel GetDetailsRuntimeHistory(int tableId, string tableName);
       
    }
}
