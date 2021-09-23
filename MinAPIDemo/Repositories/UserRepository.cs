using Microsoft.EntityFrameworkCore;
using MinAPIDemo.Data;
using MinAPIDemo.Models.Domain;

namespace MinAPIDemo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<EfContext> _dbContext;

        public UserRepository(IDbContextFactory<EfContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserEntity?> GetUserByLoginAsync(string username, string password)
        {
            await using var dbContext = _dbContext.CreateDbContext();
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Username.Equals(username) && u.Password == password);
        }

        public async Task UpdateUserToken(string id, string token, DateTime expiration)
        {
            await using var dbContext = _dbContext.CreateDbContext();
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if(user is not null)
            {
                user.Token = token;
                user.TokenExpiration = expiration;
                dbContext.Update(user);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}