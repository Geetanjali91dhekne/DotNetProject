using AuthenticationService.Models;

namespace AuthenticationService.Repositories
{
    public interface ILoginRepository
    {
        Task<RespondModel>  GetLoginUser(string? userId, string? password);
    }
}
