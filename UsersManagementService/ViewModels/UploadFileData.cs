using UsersManagementService.Models;

namespace UsersManagementService.ViewModels
{
    public class UploadFileData
    {
        public int? TaskId { get; set; }
        public List<IFormFile> file { get; set; }
    }
}
