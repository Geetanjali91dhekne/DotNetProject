namespace LogbookManagementService.CommonFiles
{
    public class ResponseModel
    {
        public int code { get; set; }
        public object data { get; set; }
        public string message { get; set; }
        public bool status { get; set; }
    }
    public class ResponseModelLogbook
    {
        public int code { get; set; }
        public object data { get; set; }
        public string message { get; set; }
        public bool status { get; set; }
        public bool Validation { get; set; }

        public int? countWtg { get; set; }
    }
}
