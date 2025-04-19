using FiapCloudGames.API.DTOs.Requests;
using FiapCloudGames.API.DTOs.Responses;
using FiapCloudGames.API.DTOs.Responses.User;

namespace FiapCloudGames.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseUsersDTO> GetAll(RequestAllDataDTO request);
        Task<ResponseUserDTO> GetById(Guid userId);
        Task<ResponseAuthDTO> Create(RequestUserInputDTO request);
        Task Update(Guid userId, RequestUserInputDTO request);
        Task Delete(Guid userId);
    }
}
