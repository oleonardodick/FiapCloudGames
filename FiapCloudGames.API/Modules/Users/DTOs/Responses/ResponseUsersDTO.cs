using FiapCloudGames.API.Shared.DTOs;

namespace FiapCloudGames.API.Modules.Users.DTOs.Responses
{
    public class ResponseUsersDTO
    {
        public PaginationDTO Pagination { get; set; }
        public List<ResponseUserDTO> Users { get; set; }
    }
}
