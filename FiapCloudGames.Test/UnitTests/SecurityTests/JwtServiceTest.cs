using FiapCloudGames.API.Services.Configurations.JwtConfigurations;
using FiapCloudGames.API.Services.Implementations;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
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

        [Fact]
        public void ShouldGenerateJwtToken()
        {
            //Arrange
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("zpkMJohnerYXfjJ2YWaxzdJSlq2Xc5Y0"));

            _jwtKeyProvider
                .Setup(k => k.GetSigninKey())
                .Returns(key);

            var userId = Guid.NewGuid();

            //Act
            var token = _jwtService.GenerateToken(userId);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var claim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);

            //Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
            Assert.NotNull(jwtToken);
            Assert.NotNull(claim);
            Assert.Equal(userId.ToString(), claim.Value);
        }
    }
}
