using FiapCloudGames.API.Shared.DTOs;

namespace FiapCloudGames.API.Modules.Games.DTOs.Responses
{
    public class ResponseGamesDTO
    {
        public PaginationDTO Pagination { get; set; }
        public List<ResponseGameDTO> Games { get; set; }
    }
}
