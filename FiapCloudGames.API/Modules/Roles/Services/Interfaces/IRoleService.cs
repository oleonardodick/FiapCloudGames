using FiapCloudGames.API.Modules.Roles.DTOs.Responses;

namespace FiapCloudGames.API.Modules.Roles.Services.Interfaces
{
    public interface IRoleService
    {
        Task<ResponseRolesDTO> GetAll();
        Task<ResponseRoleDTO> GetById(Guid roleId);
    }
}
