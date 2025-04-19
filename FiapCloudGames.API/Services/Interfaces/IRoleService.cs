using FiapCloudGames.API.DTOs.Responses.RoleDTO;

namespace FiapCloudGames.API.Services.Interfaces
{
    public interface IRoleService
    {
        Task<ResponseRolesDTO> GetAll();
        Task<ResponseRoleDTO> GetById(Guid roleId);
    }
}
