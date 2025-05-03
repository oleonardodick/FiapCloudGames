using FiapCloudGames.API.Modules.Authentication.Configurations.Interfaces;
using FiapCloudGames.API.Utils;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FiapCloudGames.API.Modules.Authentication.Configurations.Implementations
{
    public class JwtKeyProvider : IJwtKeyProvider
    {
        private readonly IConfiguration _configuration;

        public JwtKeyProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SymmetricSecurityKey GetSigninKey()
        {
            var jwtSettingsSection = _configuration.GetSection("JwtSettings");
            if (!jwtSettingsSection.Exists())
                throw new InvalidOperationException(AppMessages.JwtSectionNotConfigured);

            var secretKey = jwtSettingsSection.GetValue<string>("SecretKey");
            if (string.IsNullOrEmpty(secretKey))
                throw new InvalidOperationException(AppMessages.SecretKeyNotConfigured);

            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        }
    }
}
