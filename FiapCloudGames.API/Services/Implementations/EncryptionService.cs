using FiapCloudGames.API.Services.Interfaces;

namespace FiapCloudGames.API.Services.Implementations
{
    public class EncryptionService : IEncryptionService
    {
        public bool Decrypt(string value, string hashedValue)
        {
            return BCrypt.Net.BCrypt.Verify(value, hashedValue);
        }

        public string Encrypt(string value)
        {
            return BCrypt.Net.BCrypt.HashPassword(value);
        }
    }
}
