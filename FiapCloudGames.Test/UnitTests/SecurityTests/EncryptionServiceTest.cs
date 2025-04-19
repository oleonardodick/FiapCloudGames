using FiapCloudGames.API.Services.Implementations;

namespace FiapCloudGames.Test.UnitTests.SecurityTests
{
    public class EncryptionServiceTest
    {
        private EncryptionService _encryptionService;

        public EncryptionServiceTest()
        {
            _encryptionService = new EncryptionService();
        }

        [Fact]
        public void ShouldEncryptTheText()
        {
            //Arrange
            var originalText = "textToEncrypt";

            //Act
            var encryptedText = _encryptionService.Encrypt(originalText);

            //Assert
            Assert.NotNull(encryptedText);
            Assert.NotEmpty(encryptedText);
            Assert.NotEqual(originalText, encryptedText);
        }

        [Fact]
        public void ShouldDecryptTheText()
        {
            //Arrange
            var originalText = "textToEncrypt";
            var encryptedText = _encryptionService.Encrypt(originalText);

            //Act
            var isSameText = _encryptionService.Decrypt(originalText, encryptedText);

            //Assert
            Assert.True(isSameText);
        }
    }
}
