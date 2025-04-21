using FiapCloudGames.API.DTOs.Requests.GameDTO;
using FiapCloudGames.API.DTOs.Responses.GameDTO;

namespace FiapCloudGames.API.Services.Interfaces
{
    public interface IGameService
    {
        Task<ResponseGamesDTO> GetAll(int pageNumber, int perPage);
        Task<ResponseGameDTO> GetById(Guid gameId);
        Task<ResponseGameDTO> Create(RequestCreateGameDTO request);
        Task Update(Guid gameId, RequestUpdateGameDTO request);
        Task Delete(Guid gameId);
    }
}
