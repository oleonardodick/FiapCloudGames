using FiapCloudGames.API.Entities;

namespace FiapCloudGames.API.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
