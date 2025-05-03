namespace FiapCloudGames.API.Modules.Authentication.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string role);
    }
}
