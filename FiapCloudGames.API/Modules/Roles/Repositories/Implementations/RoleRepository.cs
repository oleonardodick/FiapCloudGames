using FiapCloudGames.API.Database;
using FiapCloudGames.API.Modules.Roles.Entities;
using FiapCloudGames.API.Modules.Roles.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiapCloudGames.API.Modules.Roles.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetById(Guid roleId)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
        }
    }
}
