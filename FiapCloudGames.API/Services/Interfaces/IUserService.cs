using FiapCloudGames.API.DTOs.Requests;
using FiapCloudGames.API.DTOs.Responses;
using FiapCloudGames.API.DTOs.Responses.User;

namespace FiapCloudGames.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseUsersDTO> GetAll(int pageNumber);
        Task<ResponseUserDTO> GetById(Guid userId);
        Task<ResponseAuthDTO> Create(RequestCreateUserDTO request);
        Task Update(Guid userId, RequestUpdateUserDTO request);
        Task Delete(Guid userId);
    }
}
