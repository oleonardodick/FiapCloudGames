using FiapCloudGames.API.Exceptions;
using FiapCloudGames.API.Modules.Authentication.DTOs.Requests;
using FiapCloudGames.API.Modules.Roles.Entities;
using FiapCloudGames.API.Modules.Users.Entities;
using FiapCloudGames.API.Shared.Utils;
using FiapCloudGames.Test.Utils;
using Moq;

namespace FiapCloudGames.Test.UnitTests.AuthTests
{
    public class AuthServiceTest : AuthServiceTestBase
    {
        [Trait("Category", "UnitTest")]
        [Trait("Module", "AuthService")]
        [Fact]
        public async Task InvalidEmail_ShouldThrownInvalidLoginException()
        {
            //Arrange
            var request = new RequestLoginDTO
            {
                Email = "invalidMail@mail.com",
                Password = "@Password123"
            };

            _userRepository
                .Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync((User?)null);

            //Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidLoginException>(() => _authService.Authenticate(request));

            Assert.NotNull(exception);
            Assert.Contains(AppMessages.InvalidLoginMessage, exception.GetErrorMessages());

        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "AuthService")]
        [Fact]
        public async Task InvalidPassword_ShouldThrownInvalidLoginException()
        {
            //Arrange
            var user = FakeUser.FakeSpecificUser();

            var request = new RequestLoginDTO
            {
                Email = user.Email,
                Password = "invalidPassword"
            };

            _userRepository
                .Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync(user);

            _encryptionService
                .Setup(x => x.Decrypt(request.Password, user.Password))
                .Returns(false);

            //Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidLoginException>(() => _authService.Authenticate(request));

            Assert.NotNull(exception);
            Assert.Contains(AppMessages.InvalidLoginMessage, exception.GetErrorMessages());
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "AuthService")]
        [Fact]
        public async Task ValidLogin_ShouldReturnTheAccessToken()
        {
            //Arrange
            var user = FakeUser.FakeSpecificUser();
            var role = new Role { Name = AppRoles.Admin };
            user.Role = role;
            var accessToken = "accessToken";

            var request = new RequestLoginDTO
            {
                Email = user.Email,
                Password = user.Password
            };

            _userRepository
                .Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync(user);

            _encryptionService
                .Setup(x => x.Decrypt(request.Password, user.Password))
                .Returns(true);

            _jwtService
                .Setup(x => x.GenerateToken(user.Id, user.Role.Name))
                .Returns(accessToken);

            //Act
            var response = await _authService.Authenticate(request);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(accessToken, response.AccessToken);
        }
    }
}
