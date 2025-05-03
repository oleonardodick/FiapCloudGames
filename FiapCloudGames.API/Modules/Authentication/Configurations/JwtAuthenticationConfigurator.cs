using FiapCloudGames.API.Modules.Authentication.Configurations.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FiapCloudGames.API.Modules.Authentication.Configurations
{
    public class JwtAuthenticationConfigurator
    {
        private readonly IJwtKeyProvider _jwtKeyProvider;

        public JwtAuthenticationConfigurator(IJwtKeyProvider jwtKeyProvider)
        {
            _jwtKeyProvider = jwtKeyProvider;
        }

        // Método para configurar as opções do JWT
        public void ConfigureJwtBearerOptions(JwtBearerOptions options)
        {
            var key = _jwtKeyProvider.GetSigninKey();
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key
            };
        }
    }
}
