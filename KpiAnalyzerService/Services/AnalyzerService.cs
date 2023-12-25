using KpiAnalyzerService.CommonFiles;
using KpiAnalyzerService.Models;
using KpiAnalyzerService.Repositories;
using KpiAnalyzerService.ViewModels;

namespace KpiAnalyzerService.Services
{
    public class AnalyzerService : IAnalyzerService
    {
        private readonly IAnalyzerRepository analyzerRepository;

        public AnalyzerService(IAnalyzerRepository analyzerRepository)
        {
            this.analyzerRepository = analyzerRepository;
        }

        public ResponseModel GetTableAndViewNames()
        {
            return analyzerRepository.GetTableAndViewNames();
        }
        public ResponseModel GetTableOrViewColumns(string tableName)
        {
            return analyzerRepository.GetTableOrViewColumns(tableName);
        }
        public ResponseModel GetDataFilterConditions(string tableName)
        {
            return analyzerRepository.GetDataFilterConditions(tableName);
        }
        public ResponseModel AddOrUpdateDataFilterCondition(ResponseDataFilterCondition conditions)
        {
            return analyzerRepository.AddOrUpdateDataFilterCondition(conditions);
        }
        public ResponseModel GetColumnXYMapping(string tableName)
        {
            return analyzerRepository.GetColumnXYMapping(tableName);
        }
        public ResponseModel AddOrUpdateColumnXYMapping(ResponseColumnXYMapping columns)
        {
            return analyzerRepository.AddOrUpdateColumnXYMapping(columns);
        }
    }
}
