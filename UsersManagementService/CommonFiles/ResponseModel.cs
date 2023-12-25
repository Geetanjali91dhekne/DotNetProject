namespace UsersManagementService.CommonFiles
{
    public class ResponseModel
    {
        public int code { get; set; }
        public object data { get; set; }
        public string message { get; set; }
        public bool status { get; set; }
        public int? totalPageCount { get; set; }
        public int? totalRecords { get; set; }

    }
}
