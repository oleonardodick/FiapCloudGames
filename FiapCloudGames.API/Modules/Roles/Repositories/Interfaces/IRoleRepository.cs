using FiapCloudGames.API.Modules.Roles.Entities;

namespace FiapCloudGames.API.Modules.Roles.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<IList<Role>> GetAll();
        Task<Role?> GetById(Guid roleId);
    }
}
