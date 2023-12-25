using WorkflowManagementService.CommonFiles;
using WorkflowManagementService.Repositories;

namespace WorkflowManagementService.Services
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WorkflowService(IWorkflowRepository workflowRepository, IHttpContextAccessor httpContextAccessor)
        {
            _workflowRepository = workflowRepository;
            _httpContextAccessor = httpContextAccessor;
        }

       
        public ResponseModel GetDetailsRuntime(int tableId, string tableName, string roleName, string userName)
        {
            return _workflowRepository.GetDetailsRuntime(tableId, tableName, roleName, userName);
        }
        public ResponseModel GetDetailsRuntimeHistory(int tableId, string tableName)
        {
            return _workflowRepository.GetDetailsRuntimeHistory(tableId, tableName);
        }
    }
}
