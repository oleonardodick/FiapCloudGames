using Microsoft.IdentityModel.Tokens;

namespace FiapCloudGames.API.Services.Configurations.JwtConfigurations
{
    public interface IJwtKeyProvider
    {
        SymmetricSecurityKey GetSigninKey();
    }
}
