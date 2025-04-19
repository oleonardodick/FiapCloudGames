using FiapCloudGames.API.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FiapCloudGames.API.Services.Configurations.JwtConfigurations
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

            options.Events = new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    throw new InvalidTokenException();
                }
            };
        }
    }
}
