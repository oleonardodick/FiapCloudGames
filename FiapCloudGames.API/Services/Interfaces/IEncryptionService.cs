namespace FiapCloudGames.API.Services.Interfaces
{
    public interface IEncryptionService
    {
        string Encrypt(string value);
        bool Decrypt(string value, string hashedValue);
    }
}
