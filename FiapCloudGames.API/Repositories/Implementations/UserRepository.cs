using FiapCloudGames.API.Entities;
using FiapCloudGames.API.Repositories.Interfaces;

namespace FiapCloudGames.API.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public Task<User?> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
