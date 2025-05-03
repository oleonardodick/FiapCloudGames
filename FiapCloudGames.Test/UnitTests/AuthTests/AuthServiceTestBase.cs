using FiapCloudGames.API.Modules.Authentication.Services.Implementations;
using FiapCloudGames.API.Modules.Authentication.Services.Interfaces;
using FiapCloudGames.API.Modules.Encryption.Services.Interfaces;
using FiapCloudGames.API.Modules.Users.Repositories.Interfaces;
using Moq;

namespace FiapCloudGames.Test.UnitTests.AuthTests
{
    public abstract class AuthServiceTestBase
    {
        protected readonly Mock<IUserRepository> _userRepository;
        protected readonly Mock<IJwtService> _jwtService;
        protected readonly Mock<IEncryptionService> _encryptionService;
        protected readonly AuthService _authService;

        protected AuthServiceTestBase()
        {
            _userRepository = new Mock<IUserRepository>();
            _jwtService = new Mock<IJwtService>();
            _encryptionService = new Mock<IEncryptionService>();
            _authService = new AuthService(_userRepository.Object, _jwtService.Object, _encryptionService.Object);
        }
    }
}
