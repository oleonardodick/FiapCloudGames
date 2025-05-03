using FiapCloudGames.API.Modules.Roles.Entities;
using FiapCloudGames.API.Modules.Roles.Repositories.Interfaces;
using FiapCloudGames.API.Modules.Roles.Services.Implementations;
using FiapCloudGames.API.Shared.Utils;
using Moq;

namespace FiapCloudGames.Test.UnitTests.RoleTests
{
    public abstract class RoleServiceTestBase
    {
        protected readonly Mock<IRoleRepository> _roleRepository;
        protected readonly RoleService _roleService;
        protected readonly List<Role> roles;

        protected RoleServiceTestBase()
        {
            _roleRepository = new Mock<IRoleRepository>();
            _roleService = new RoleService(_roleRepository.Object);
            roles = new List<Role>{
                new Role {Id = Guid.NewGuid(), Name = AppRoles.Admin, CreatedAt = DateTime.UtcNow},
                new Role {Id = Guid.NewGuid(), Name = AppRoles.User, CreatedAt = DateTime.UtcNow}
            };
        }
    }
}
