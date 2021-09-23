using MinAPIDemo.Models.Domain;
using MinAPIDemo.Models.Security;

namespace MinAPIDemo.Repositories
{
    public interface IUserRepository
    {
         Task<UserEntity?> GetUserByLoginAsync(string username, string password);
         Task UpdateUserToken(string id, string token, DateTime expiration);         
    }
}