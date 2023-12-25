using System;
using System.Collections.Generic;

namespace UsersManagementService.Models;

public partial class BulkUploadConfiguration
{
    public int Id { get; set; }

    public string? MasterName { get; set; }

    public string? FieldName { get; set; }

    public int? FieldSequence { get; set; }

    public int? ExcelSequence { get; set; }

    public string? ExcelColumnName { get; set; }

    public string? TableName { get; set; }

    public string? Mandatory { get; set; }

    public string? FeildDataType { get; set; }

    public string? CreatedUserName { get; set; }

    public string? CreatedDateName { get; set; }

    public string? ModifiedUserName { get; set; }

    public string? ModifiedDateName { get; set; }

    public string? RegularExpression { get; set; }

    public string? UploadTable { get; set; }
}
