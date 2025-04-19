using FiapCloudGames.API.Repositories.Interfaces;
using FiapCloudGames.API.Services.Implementations;
using FiapCloudGames.API.Services.Interfaces;
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
