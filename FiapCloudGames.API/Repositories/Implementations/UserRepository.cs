using FiapCloudGames.API.Database;
using FiapCloudGames.API.Entities;
using FiapCloudGames.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.API.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
