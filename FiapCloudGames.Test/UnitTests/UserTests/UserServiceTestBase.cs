using FiapCloudGames.API.Modules.Authentication.Services.Interfaces;
using FiapCloudGames.API.Modules.Encryption.Services.Interfaces;
using FiapCloudGames.API.Modules.Roles.Repositories.Interfaces;
using FiapCloudGames.API.Modules.Users.Repositories.Interfaces;
using FiapCloudGames.API.Modules.Users.Services.Implementations;
using Microsoft.Extensions.Logging;
using Moq;

namespace FiapCloudGames.Test.UnitTests.UserTests
{
    public abstract class UserServiceTestBase
    {
        protected readonly Mock<IUserRepository> _userRepository;
        protected readonly Mock<IEncryptionService> _encryptionService;
        protected readonly Mock<IJwtService> _jwtService;
        protected readonly Mock<IRoleRepository> _roleRepository;
        protected readonly Mock<ILogger<UserService>> _logger;
        protected readonly UserService _userSevice;

        protected UserServiceTestBase()
        {
            _userRepository = new Mock<IUserRepository>();
            _encryptionService = new Mock<IEncryptionService>();
            _jwtService = new Mock<IJwtService>();
            _roleRepository = new Mock<IRoleRepository>();
            _logger = new Mock<ILogger<UserService>>();
            _userSevice = new UserService(_userRepository.Object, _encryptionService.Object, _jwtService.Object, _roleRepository.Object, _logger.Object);
        }
    }
}
