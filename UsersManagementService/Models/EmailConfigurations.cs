using System.ComponentModel.DataAnnotations;

namespace UsersManagementService.Models
{
    public class EmailConfigurations
    {
        [Key]
        public int Id { get; set; }
        public string? From { get; set; }
        public string? FromName { get; set; }
        public string? HostName { get; set; }
        public bool EnableSsl { get; set; }
        public int Port { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
        public string? url { get; set; }
        public string? Apikey { get; set; }
    }
}
