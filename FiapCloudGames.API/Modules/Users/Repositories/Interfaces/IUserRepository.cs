using FiapCloudGames.API.Modules.Users.Entities;
using FiapCloudGames.API.Shared.Repositories.Interfaces;

namespace FiapCloudGames.API.Modules.Users.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
