using FiapCloudGames.API.DTOs;
using FiapCloudGames.API.DTOs.Requests;
using FiapCloudGames.API.DTOs.Responses;
using FiapCloudGames.API.DTOs.Responses.User;
using FiapCloudGames.API.Entities;
using FiapCloudGames.API.Exceptions;
using FiapCloudGames.API.Messages;
using FiapCloudGames.API.Repositories.Interfaces;
using FiapCloudGames.API.Services.Interfaces;

namespace FiapCloudGames.API.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IJwtService _jwtService;
        private const int PAGE_SIZE = 10;

        public UserService(IUserRepository userRepository, IEncryptionService encryptionService, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _jwtService = jwtService;
        }

        public async Task<ResponseUsersDTO> GetAll(RequestAllDataDTO request)
        {
            var (users, totalItems) = await _userRepository.GetAll(request.PageNumber, PAGE_SIZE);

            return new ResponseUsersDTO
            {
                Pagination = new PaginationDTO { 
                    PageNumber = request.PageNumber,
                    TotalItems = totalItems,
                    TotalPages = (int)Math.Ceiling(totalItems/(double)PAGE_SIZE)
                },
                Users = users.Select(user => new ResponseUserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    CreatedAt = user.CreatedAt
                }).ToList()
            };
        }

        public async Task<ResponseUserDTO> GetById(Guid userId)
        {
            var user = await _userRepository.GetById(userId);
            if (user is null) throw new NotFoundException(AppMessages.UserNotFoundMessage);

            return new ResponseUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<ResponseAuthDTO> Create(RequestUserInputDTO request)
        {
            var userEmailAreadyExists = await _userRepository.GetByEmailAsync(request.Email) != null;
            if (userEmailAreadyExists) throw new EmailAlreadyExistsException(AppMessages.EmailAlreadyExistsMessage);

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = _encryptionService.Encrypt(request.Password),
                RoleId = request.RoleId.Value
            };

            var createdUser = await _userRepository.Create(user);

            return new ResponseAuthDTO
            {
                AccessToken = _jwtService.GenerateToken(createdUser.Id)
            };
        }

        public async Task Update(Guid userId, RequestUserInputDTO request)
        {
            var user = await _userRepository.GetById(userId);
            if (user is null) throw new NotFoundException(AppMessages.UserNotFoundMessage);

            user.Name = request.Name ?? user.Name;
            user.Email = request.Email ?? user.Email;
            user.Password = _encryptionService.Encrypt(request.Password) ?? user.Password;
            user.RoleId = request.RoleId ?? user.RoleId;

            _userRepository.Update(user);
        }

        public async Task Delete(Guid userId)
        {
            var user = await _userRepository.GetById(userId);
            if (user is null) throw new NotFoundException(AppMessages.UserNotFoundMessage);

            _userRepository.Delete(user);
        }
    }
}
