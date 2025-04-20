using FiapCloudGames.API.Services.Configurations.JwtConfigurations;
using FiapCloudGames.API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FiapCloudGames.API.Services.Implementations
{
    public class JwtService : IJwtService
    {
        private readonly IJwtKeyProvider _jwtKeyProvider;

        public JwtService(IJwtKeyProvider jwtKeyProvider)
        {
            _jwtKeyProvider = jwtKeyProvider;
        }

        public string GenerateToken(Guid userId, string role)
        {
            var key = _jwtKeyProvider.GetSigninKey();
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
