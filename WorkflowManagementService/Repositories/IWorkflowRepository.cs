using WorkflowManagementService.CommonFiles;

namespace WorkflowManagementService.Repositories
{
    public interface IWorkflowRepository
    {
        ResponseModel GetDetailsRuntime(int tableId, string tableName, string roleName, string userName);
        ResponseModel GetDetailsRuntimeHistory(int tableId, string tableName);
    }
}
