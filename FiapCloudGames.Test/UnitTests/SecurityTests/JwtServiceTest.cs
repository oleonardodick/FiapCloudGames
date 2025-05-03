using FiapCloudGames.API.Modules.Authentication.Configurations.Interfaces;
using FiapCloudGames.API.Modules.Authentication.Services.Implementations;
using FiapCloudGames.API.Utils;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FiapCloudGames.Test.UnitTests.SecurityTests
{
    public class JwtServiceTest
    {
        private readonly Mock<IJwtKeyProvider> _jwtKeyProvider;
        private readonly JwtService _jwtService;

        public JwtServiceTest()
        {
            _jwtKeyProvider = new Mock<IJwtKeyProvider>();
            _jwtService = new JwtService(_jwtKeyProvider.Object);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "JwtService")]
        [Fact]
        public void ShouldGenerateJwtToken()
        {
            //Arrange
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("zpkMJohnerYXfjJ2YWaxzdJSlq2Xc5Y0"));

            _jwtKeyProvider
                .Setup(k => k.GetSigninKey())
                .Returns(key);

            var userId = Guid.NewGuid();
            var role = AppRoles.Admin;

            //Act
            var token = _jwtService.GenerateToken(userId, role);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var claimUserId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var claimRole = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

            //Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
            Assert.NotNull(jwtToken);
            Assert.NotNull(claimUserId);
            Assert.Equal(userId.ToString(), claimUserId.Value);
            Assert.NotNull(claimRole);
            Assert.Equal(role, claimRole.Value);
        }
    }
}
