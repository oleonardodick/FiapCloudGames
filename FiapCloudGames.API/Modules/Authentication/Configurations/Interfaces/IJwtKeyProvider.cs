using Microsoft.IdentityModel.Tokens;

namespace FiapCloudGames.API.Modules.Authentication.Configurations.Interfaces
{
    public interface IJwtKeyProvider
    {
        SymmetricSecurityKey GetSigninKey();
    }
}
