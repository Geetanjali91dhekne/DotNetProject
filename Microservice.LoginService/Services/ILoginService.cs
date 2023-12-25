using AuthenticationService.Models;

namespace AuthenticationService.Services
{
    public interface ILoginService
    {
        Task<RespondModel> GetLoginUser(string? userId, string? password);
    }
}
