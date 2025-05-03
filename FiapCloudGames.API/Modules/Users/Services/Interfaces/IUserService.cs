using FiapCloudGames.API.Modules.Users.DTOs.Requests;
using FiapCloudGames.API.Modules.Users.DTOs.Responses;

namespace FiapCloudGames.API.Modules.Users.Services.Interfaces
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
