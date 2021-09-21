using MinAPIDemo.Models.Security;

namespace MinAPIDemo.Services
{
    public interface ILoginService
    {
         Task<bool> LoginAsync(LoginRequest request);

         Task<LoginResponse> GenerateTokenAsync(LoginRequest request);
    }
}