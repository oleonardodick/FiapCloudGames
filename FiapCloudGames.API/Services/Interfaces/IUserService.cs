using FiapCloudGames.API.DTOs.Requests.UserDTO;
using FiapCloudGames.API.DTOs.Responses.User;

namespace FiapCloudGames.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseUsersDTO> GetAll(int pageNumber, int perPage);
        Task<ResponseUserDTO> GetById(Guid userId);
        Task<ResponseUserDTO> Create(RequestCreateUserDTO request);
        Task Update(Guid userId, RequestUpdateUserDTO request);
        Task Delete(Guid userId);
    }
}
