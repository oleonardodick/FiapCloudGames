using FiapCloudGames.API.Repositories.Interfaces;
using FiapCloudGames.API.Services.Implementations;
using FiapCloudGames.API.Services.Interfaces;
using Moq;

namespace FiapCloudGames.Test.UnitTests.UserTests
{
    public abstract class UserServiceTestBase
    {
        protected readonly Mock<IUserRepository> _userRepository;
        protected readonly Mock<IEncryptionService> _encryptionService;
        protected readonly Mock<IJwtService> _jwtService;
        protected readonly UserService _userSevice;

        protected UserServiceTestBase()
        {
            _userRepository = new Mock<IUserRepository>();
            _encryptionService = new Mock<IEncryptionService>();
            _jwtService = new Mock<IJwtService>();
            _userSevice = new UserService(_userRepository.Object, _encryptionService.Object, _jwtService.Object);
        }
    }
}
