using FiapCloudGames.API.DTOs.Responses.RoleDTO;
using FiapCloudGames.API.Exceptions;
using FiapCloudGames.API.Utils;
using FiapCloudGames.API.Repositories.Interfaces;
using FiapCloudGames.API.Services.Interfaces;

namespace FiapCloudGames.API.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ResponseRolesDTO> GetAll()
        {
            var roles = await _roleRepository.GetAll();

            return new ResponseRolesDTO
            {
                Roles = roles.Select(role => new ResponseRoleDTO
                {
                    Id = role.Id,
                    Name = role.Name,
                    CreatedAt = role.CreatedAt
                }).ToList()
            };
        }

        public async Task<ResponseRoleDTO> GetById(Guid roleId)
        {
            var role = await _roleRepository.GetById(roleId);

            if(role is null) throw new NotFoundException(AppMessages.RoleNotFoundMessage);

            return new ResponseRoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                CreatedAt = role.CreatedAt
            };
        }
    }
}
