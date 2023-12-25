namespace AuthenticationService.Models
{
	public class RespondModel
	{
		public int code { get; set; }
		public Object data { get; set; }
		public bool status { get; set; }
		
		public string message { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
