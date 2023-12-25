using KpiAnalyzerService.CommonFiles;
using KpiAnalyzerService.Models;
using KpiAnalyzerService.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpiAnalyzerService.Repositories
{
    public class AnalyzerRepository : IAnalyzerRepository
    {
        private readonly FleetManagerContext fleetManagerContext;
        private readonly ResponseModel responseModel;

        public AnalyzerRepository(FleetManagerContext fleetManagerContext)
        {
            this.fleetManagerContext = fleetManagerContext;
            this.responseModel = new ResponseModel();
        }

        public ResponseModel GetTableAndViewNames()
        {
            try
            {
                using (var connection = fleetManagerContext.Database.GetDbConnection())
                {
                    connection.Open();

                    // Retrieve table names
                    var tableNames = GetSchemaObjectNames(connection, "Tables");

                    // Retrieve view names in "System Views" schema
                    var systemViews = GetSchemaObjectNames(connection, "Views", "System Views");

                    // Combine table and view names
                    var allObjectNames = tableNames.Concat(systemViews).ToList();

                    return GetResponseModel(Constants.httpCodeSuccess, allObjectNames, "Table and view names retrieved successfully", true);
                }
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, $"An error occurred: {ex.Message}", false);
            }
        }
        public ResponseModel GetTableOrViewColumns(string tableName)
        {
            try
            {
                using (var connection = fleetManagerContext.Database.GetDbConnection())
                {
                    connection.Open();

                    // Retrieve columns for the specified table or view
                    var columns = GetTableOrViewColumns(connection, tableName);

                    return GetResponseModel(Constants.httpCodeSuccess, columns, "Columns retrieved successfully", true);
                }
            }
            catch (Exception ex)
            {
                // Log the error - Example: Log.Error($"An error occurred: {ex.Message}", ex);

                return GetResponseModel(Constants.httpCodeFailure, null, $"An error occurred: {ex.Message}", false);
            }
        }
        public ResponseModel GetDataFilterConditions(string tableName, string? filterColumns = null)
        {
            try
            {
                var filterColumnsList = !string.IsNullOrEmpty(filterColumns)
                    ? filterColumns.Split(',').Select(x => x.Trim()).ToList()
                    : new List<string>();

                var filterConditions = fleetManagerContext.DataFilterConditions
                    .Where(t => t.TableName == tableName)
                    .ToList();

                if (filterConditions.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "No Filter Conditions Found", false);
                }

                var columnsData = new List<dynamic>();
                var conditionStrings = new Dictionary<string, string>();
                var filterList = new List<dynamic>();

                foreach (var condition in filterConditions)
                {
                    dynamic columnData = new
                    {
                        column_name = condition.ColumnName,
                        condition = condition.Condition,
                        value1 = condition.Value1,
                        value2 = condition.Value2
                    };

                    columnsData.Add(columnData);

                    if (!conditionStrings.ContainsKey(condition.ColumnName))
                    {
                        conditionStrings[condition.ColumnName] = "";
                    }

                    var currentConditionString = conditionStrings[condition.ColumnName];

                    if (condition.Condition == "is_null")
                    {
                        currentConditionString += $" WHEN {condition.ColumnName} IS NULL THEN '{condition.Value1}' ";
                    }
                    else if (condition.Condition == "replace_by")
                    {
                        currentConditionString += $" WHEN {condition.ColumnName} = '{condition.Value1}' THEN '{condition.Value2}' ";
                    }

                    conditionStrings[condition.ColumnName] = currentConditionString;

                    if (filterColumnsList.Contains(condition.ColumnName))
                    {
                        var filterItem = new
                        {
                            filterColumn = condition.ColumnName,
                            columnQuery = $"{currentConditionString} ELSE {condition.ColumnName} END AS {condition.ColumnName}"
                        };

                        var filterListCopy = new List<dynamic>(filterList);

                        foreach (var filter in filterListCopy)
                        {
                            if (filter.filterColumn == condition.ColumnName)
                            {
                                filterList.Remove(filter);
                            }
                        }
                        filterList.Add(filterItem);
                    }
                }

                var finalConditionString = "";
                foreach (var columnCondition in conditionStrings)
                {
                    var columnName = columnCondition.Key;
                    var conditionString = columnCondition.Value;

                    if (!string.IsNullOrEmpty(conditionString))
                    {
                        conditionString = $"CASE {conditionString} ELSE {columnName} END AS {columnName}, ";
                        finalConditionString += conditionString;
                    }
                }

                if (!string.IsNullOrEmpty(finalConditionString))
                {
                    finalConditionString = finalConditionString.Remove(finalConditionString.Length - 2);
                }

                var responseData = new
                {
                    table_name = tableName,
                    columns = columnsData,
                    condition_string = finalConditionString,
                    Filter = filterList
                };

                return GetResponseModel(Constants.httpCodeSuccess, responseData, "All Columns Listed", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }

        public ResponseModel AddOrUpdateDataFilterCondition(ResponseDataFilterCondition conditions)
        {
            try
            {
                if (conditions == null || conditions.columns == null || conditions.columns.Length == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "Condition Not Updated", false);
                }

                // Get existing conditions for the given table name
                var existingConditions = fleetManagerContext.DataFilterConditions.Where(t => t.TableName == conditions.table_name).ToList();

                // Delete existing conditions for the given table
                fleetManagerContext.DataFilterConditions.RemoveRange(existingConditions);

                // Add new conditions
                foreach (var data in conditions.columns)
                {
                    var newCondition = CreateDataFilterCondition(conditions.table_name, data);
                    fleetManagerContext.DataFilterConditions.Add(newCondition);
                }

                fleetManagerContext.SaveChanges();
                return GetResponseModel(Constants.httpCodeSuccess, conditions, "Data Updated Successfully", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, $"An error occurred: {ex.Message}", false);
            }
        }

        private DataFilterCondition CreateDataFilterCondition(string tableName, DataFilterConditionView dataFilterCondition)
        {
            return new DataFilterCondition
            {
                TableName = tableName,
                ColumnName = dataFilterCondition.column_name,
                Condition = dataFilterCondition.condition,
                Value1 = dataFilterCondition.value1,
                Value2 = dataFilterCondition.value2
            };
        }

        private void MapDataFilterCondition(DataFilterCondition existingCondition, DataFilterConditionView updateConditions)
        {
            existingCondition.Condition = updateConditions.condition;
            existingCondition.Value1 = updateConditions.value1;
            existingCondition.Value2 = updateConditions.value2;
        }

        public ResponseModel GetColumnXYMapping(string tableName)
        {
            try
            {
                var columns = fleetManagerContext.ColumnXymappings.Where(t => t.TableName == tableName)
                .Select(t => new { t.TableName, t.ColumnName, t.Axis })
                .ToList();

                if (columns.Count == 0)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "Columns Not Found", false);
                }

                var xColumns = columns.Where(c => c.Axis == "X").Select(c => c.ColumnName).ToArray();
                var yColumns = columns.Where(c => c.Axis == "Y").Select(c => c.ColumnName).ToArray();

                var responseData = new
                {
                    table_name = tableName,
                    xaxis = xColumns,
                    yaxis = yColumns
                };

                return GetResponseModel(Constants.httpCodeSuccess, responseData, "All Columns Listed", true);
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, ex.Message, false);
            }
        }
        public ResponseModel AddOrUpdateColumnXYMapping(ResponseColumnXYMapping columns)
        {
            try
            {
                ColumnXymapping columnXymapping = new ColumnXymapping();

                //List<ColumnXymapping>  columnXymappings = new List<ColumnXymapping>();
                if (columns == null)
                {
                    return GetResponseModel(Constants.httpCodeSuccess, null, "Data Is Empty", true);
                }
                List<ColumnXymapping> allData = new List<ColumnXymapping>();
                var existingColumns = fleetManagerContext.ColumnXymappings.Where(t => t.TableName == columns.table_name).ToList();
                if (existingColumns.Count == 0)
                {
                    foreach (var item in columns.Xaxis)
                    {                        
                            var createColumnX = new ColumnXymapping
                            {
                                TableName = columns.table_name,
                                ColumnName = item,
                                Axis = "X"
                            };
                            allData.Add(createColumnX);
                            fleetManagerContext.ColumnXymappings.Add(createColumnX);                     
                    }
                    foreach (var item in columns.Yaxis)
                    {
                        var createColumnY = new ColumnXymapping
                        {
                            TableName = columns.table_name,
                            ColumnName = item,
                            Axis = "Y"
                        };
                        allData.Add(createColumnY);
                        fleetManagerContext.ColumnXymappings.Add(createColumnY);
                    }
                }
                else
                {
                    fleetManagerContext.ColumnXymappings.RemoveRange(existingColumns);
                    fleetManagerContext.SaveChanges();
                    foreach (var item in columns.Xaxis)
                    {
                        var updateColumnX = new ColumnXymapping
                        {
                            TableName = columns.table_name,
                            ColumnName = item,
                            Axis = "X"
                        };
                        allData.Add(updateColumnX);
                        fleetManagerContext.ColumnXymappings.Add(updateColumnX);                     
                    }
                    foreach (var item in columns.Yaxis)
                    {
                        var updateColumnY = new ColumnXymapping
                        {
                            TableName = columns.table_name,
                            ColumnName = item,
                            Axis = "Y"
                        };
                        allData.Add(updateColumnY);
                        fleetManagerContext.ColumnXymappings.Add(updateColumnY);
                    }
                }
                fleetManagerContext.SaveChanges();
                return GetResponseModel(Constants.httpCodeSuccess, allData, "Columns Updated Successfully", true);                
            }
            catch (Exception ex)
            {
                return GetResponseModel(Constants.httpCodeFailure, null, $"An error occurred: {ex.Message}", false);
            }
        }
        private ColumnXymapping CreateColumnXYMapping(ColumnXYMappingView columnMapping)
        {
            ColumnXymapping newColumnXymapping = new ColumnXymapping();
            newColumnXymapping.TableName = columnMapping.table_name;
            newColumnXymapping.ColumnName = columnMapping.column_name;
            newColumnXymapping.Axis = columnMapping.axis;
            return newColumnXymapping;
        }
        private ColumnXymapping MapColumnXYMapping(ColumnXymapping existingMapping, ColumnXYMappingView updatedMapping)
        {
            existingMapping.Id = updatedMapping.Id;
            existingMapping.TableName = updatedMapping.table_name;
            existingMapping.ColumnName = updatedMapping.column_name;
            existingMapping.Axis = updatedMapping.axis;
            return existingMapping;
        }
        private List<string> GetSchemaObjectNames(IDbConnection connection, string schemaType, string schemaName = "dbo")
        {
            var command = connection.CreateCommand();
            command.CommandText = $"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.{schemaType} WHERE TABLE_SCHEMA = '{schemaName}'";

            var objectNames = new List<string>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var objectName = reader["TABLE_NAME"].ToString();
                    objectNames.Add(objectName);
                }
            }

            return objectNames;
        }
        private List<ColumnInfo> GetTableOrViewColumns(IDbConnection connection, string tableName)
        {
            var columns = new List<ColumnInfo>();

            var command = connection.CreateCommand();
            command.CommandText = $"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var columnName = reader["COLUMN_NAME"].ToString();
                    var dataType = reader["DATA_TYPE"].ToString();

                    columns.Add(new ColumnInfo { column_name = columnName, data_type = dataType });
                }
            }

            return columns;
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
