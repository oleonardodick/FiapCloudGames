using FiapCloudGames.API.Exceptions;
using FiapCloudGames.API.Modules.Authentication.DTOs.Requests;
using FiapCloudGames.API.Modules.Authentication.DTOs.Responses;
using FiapCloudGames.API.Modules.Authentication.Services.Interfaces;
using FiapCloudGames.API.Modules.Encryption.Services.Interfaces;
using FiapCloudGames.API.Modules.Users.Repositories.Interfaces;

namespace FiapCloudGames.API.Modules.Authentication.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IEncryptionService _encryptionService;

        public AuthService(IUserRepository userRepository, IJwtService jwtService, IEncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _encryptionService = encryptionService;
        }

        public async Task<ResponseLoginDTO> Authenticate(RequestLoginDTO request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user is null) throw new InvalidLoginException();

            var passwordValid = _encryptionService.Decrypt(request.Password, user.Password);
            if (!passwordValid) throw new InvalidLoginException();

            return new ResponseLoginDTO
            {
                AccessToken = _jwtService.GenerateToken(user.Id, user.Role.Name)
            };
        }
    }
}
