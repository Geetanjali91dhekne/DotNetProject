﻿using KpiAnalyzerService.CommonFiles;
using KpiAnalyzerService.Models;
using KpiAnalyzerService.ViewModels;

namespace KpiAnalyzerService.Services
{
    public interface IAnalyzerService
    {
        public ResponseModel GetTableAndViewNames();
        public ResponseModel GetTableOrViewColumns(string tableName);
        public ResponseModel GetDataFilterConditions(string tableName, string? filterColumns = null);
        public ResponseModel AddOrUpdateDataFilterCondition(ResponseDataFilterCondition conditions);
        public ResponseModel GetColumnXYMapping(string tableName);
        public ResponseModel AddOrUpdateColumnXYMapping(ResponseColumnXYMapping columns);
    }
}
