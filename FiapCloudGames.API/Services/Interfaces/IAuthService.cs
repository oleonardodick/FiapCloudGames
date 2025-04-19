using FiapCloudGames.API.DTOs.Requests;
using FiapCloudGames.API.DTOs.Responses;

namespace FiapCloudGames.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseAuthDTO> Authenticate(RequestLoginDTO request);
    }
}
