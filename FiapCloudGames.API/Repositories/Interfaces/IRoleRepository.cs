using FiapCloudGames.API.Entities;

namespace FiapCloudGames.API.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<IList<Role>> GetAll();
        Task<Role?> GetById(Guid roleId);
    }
}
