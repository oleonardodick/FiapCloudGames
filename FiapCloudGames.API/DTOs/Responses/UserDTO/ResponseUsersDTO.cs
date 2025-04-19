namespace FiapCloudGames.API.DTOs.Responses.User
{
    public class ResponseUsersDTO
    {
        public PaginationDTO Pagination { get; set; }
        public List<ResponseUserDTO> Users { get; set; }
    }
}
