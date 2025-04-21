using FiapCloudGames.API.Repositories.Interfaces;
using FiapCloudGames.API.Services.Implementations;
using FiapCloudGames.API.Services.Interfaces;
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
