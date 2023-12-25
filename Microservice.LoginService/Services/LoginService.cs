using AuthenticationService.Models;
using AuthenticationService.Repositories;
using System.Runtime.CompilerServices;

namespace AuthenticationService.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _repo;
        public LoginService(ILoginRepository repo)
        {
            _repo = repo;
        }
        public async Task<RespondModel>  GetLoginUser(string? userId, string? password)
        {
            return (await _repo.GetLoginUser(userId,password));
        }
    }
}
