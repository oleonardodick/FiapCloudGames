namespace FiapCloudGames.API.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId);
    }
}
