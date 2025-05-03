using FiapCloudGames.API.Database;
using Microsoft.EntityFrameworkCore;
using FiapCloudGames.API.Modules.Users.Repositories.Interfaces;
using FiapCloudGames.API.Shared.Repositories.Implementations;
using FiapCloudGames.API.Modules.Users.Entities;

namespace FiapCloudGames.API.Modules.Users.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(r => r.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
