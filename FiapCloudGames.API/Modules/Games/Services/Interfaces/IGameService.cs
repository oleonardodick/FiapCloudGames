using FiapCloudGames.API.Modules.Games.DTOs.Requests;
using FiapCloudGames.API.Modules.Games.DTOs.Responses;

namespace FiapCloudGames.API.Modules.Games.Services.Interfaces
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
