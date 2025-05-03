using FiapCloudGames.API.Modules.Authentication.DTOs.Requests;
using FiapCloudGames.API.Modules.Authentication.DTOs.Responses;

namespace FiapCloudGames.API.Modules.Authentication.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseLoginDTO> Authenticate(RequestLoginDTO request);
    }
}
