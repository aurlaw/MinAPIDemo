using MinAPIDemo.Models.Domain;
using MinAPIDemo.Models.Security;

namespace MinAPIDemo.Services
{
    public interface ILoginService
    {
         Task<Tuple<bool, UserEntity?>> LoginAsync(LoginRequest request);

         Task<LoginResponse> GenerateTokenAsync(UserEntity user);
    }
}