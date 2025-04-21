namespace FiapCloudGames.API.DTOs.Responses.GameDTO
{
    public class ResponseGamesDTO
    {
        public PaginationDTO Pagination { get; set; }
        public List<ResponseGameDTO> Games { get; set; }
    }
}
