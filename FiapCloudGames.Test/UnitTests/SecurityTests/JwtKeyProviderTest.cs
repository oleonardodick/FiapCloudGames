using FiapCloudGames.API.Modules.Authentication.Configurations.Implementations;
using FiapCloudGames.API.Shared.Utils;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace FiapCloudGames.Test.UnitTests.SecurityTests
{
    public class JwtKeyProviderTest
    {
        private JwtKeyProvider _jwtKeyProvider;

        [Trait("Category", "UnitTest")]
        [Trait("Module", "JwtProvider")]
        [Fact]
        public void SectionNotConfigured_ShouldThrownInvalidOperationException()
        {
            //Arrange
            var inMemorySettings = new Dictionary<string, string?> { };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _jwtKeyProvider = new JwtKeyProvider(configuration);

            //Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _jwtKeyProvider.GetSigninKey());
            Assert.Equal(AppMessages.JwtSectionNotConfigured, exception.Message);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "JwtProvider")]
        [Fact]
        public void SecretKeyEmpty_ShouldThrownInvalidOperationException()
        {
            //Arrange
            var inMemorySettings = new Dictionary<string, string?> 
            { 
                {"JwtSettings:SecretKey", "" },
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _jwtKeyProvider = new JwtKeyProvider(configuration);

            //Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _jwtKeyProvider.GetSigninKey());
            Assert.Equal(AppMessages.SecretKeyNotConfigured, exception.Message);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "JwtProvider")]
        [Fact]
        public void SecretKeyNull_ShouldThrownInvalidOperationException()
        {
            //Arrange
            var inMemorySettings = new Dictionary<string, string?>
            {
                {"JwtSettings:OtherKey", "otherValue" },
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _jwtKeyProvider = new JwtKeyProvider(configuration);

            //Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _jwtKeyProvider.GetSigninKey());
            Assert.Equal(AppMessages.SecretKeyNotConfigured, exception.Message);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "JwtProvider")]
        [Fact]
        public void KeyConfigured_ShouldReturnTheKeyConfigured()
        {
            //Arrange
            var key = "zpkMJohnerYXfjJ2YWaxzdJSlq2Xc5Y0";

            var inMemorySettings = new Dictionary<string, string?>
            {
                {"JwtSettings:SecretKey", key },
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _jwtKeyProvider = new JwtKeyProvider(configuration);

            //Act
            var keyReturned = _jwtKeyProvider.GetSigninKey();
            var keyConverted = Encoding.UTF8.GetString(keyReturned.Key);

            //Assert
            Assert.NotNull(keyConverted);
            Assert.Equal(keyConverted, key);
        }
    }
}
