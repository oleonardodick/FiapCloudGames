using FiapCloudGames.API.Exceptions;
using FiapCloudGames.API.Modules.Authentication.Services.Interfaces;
using FiapCloudGames.API.Modules.Encryption.Services.Interfaces;
using FiapCloudGames.API.Modules.Roles.Repositories.Interfaces;
using FiapCloudGames.API.Modules.Users.DTOs.Requests;
using FiapCloudGames.API.Modules.Users.DTOs.Responses;
using FiapCloudGames.API.Modules.Users.Entities;
using FiapCloudGames.API.Modules.Users.Repositories.Interfaces;
using FiapCloudGames.API.Modules.Users.Services.Interfaces;
using FiapCloudGames.API.Shared.DTOs;
using FiapCloudGames.API.Utils;

namespace FiapCloudGames.API.Modules.Users.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IJwtService _jwtService;
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IEncryptionService encryptionService, IJwtService jwtService,
            IRoleRepository roleRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _jwtService = jwtService;
            _roleRepository = roleRepository;
            _logger = logger;
        }

        public async Task<ResponseUsersDTO> GetAll(int pageNumber, int perPage)
        {
            _logger.LogInformation("Executando o método GetAll do UserService com o parâmetro pageNumber = {0}", pageNumber);
            var (users, totalItems) = await _userRepository.GetAll(pageNumber, perPage);

            var response = new ResponseUsersDTO
            {
                Pagination = new PaginationDTO
                {
                    PageNumber = pageNumber,
                    TotalItems = totalItems,
                    TotalPages = (int)Math.Ceiling(totalItems / (double)perPage),
                    PerPage = perPage
                },
                Users = users.Select(user => new ResponseUserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    CreatedAt = user.CreatedAt
                }).ToList()
            };
            _logger.LogInformation("Retorno do método GetAll da UserService: {@Reponse}", response);
            return response;
        }

        public async Task<ResponseUserDTO> GetById(Guid userId)
        {
            _logger.LogInformation("Executando o método GetById do UserService com o parâmetro userId como {0}", userId);
            var user = await _userRepository.GetById(userId);

            if (user is null) throw new NotFoundException(AppMessages.UserNotFoundMessage);

            var response = new ResponseUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
            _logger.LogInformation("Retorno do método GetById da UserService: {@Reponse}", response);
            return response;
        }

        public async Task<ResponseUserDTO> Create(RequestCreateUserDTO request)
        {
            _logger.LogInformation("Executando o método Create do UserService com o request {@Request}", request);
            var userEmailAreadyExists = await _userRepository.GetByEmailAsync(request.Email) != null;
            if (userEmailAreadyExists) throw new EmailAlreadyExistsException(AppMessages.EmailAlreadyExistsMessage);

            var role = await _roleRepository.GetById(request.RoleId);
            if (role is null) throw new NotFoundException(AppMessages.RoleNotFoundMessage);

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = _encryptionService.Encrypt(request.Password),
                RoleId = request.RoleId,
            };

            _logger.LogInformation("Vai criar o usuário");
            await _userRepository.Create(user);

            var response = new ResponseUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                AccessToken = _jwtService.GenerateToken(user.Id, role.Name)
            };

            _logger.LogInformation("Retorno do método create do usuário: {@Response}", response);
            return response;
        }

        public async Task Update(Guid userId, RequestUpdateUserDTO request)
        {
            _logger.LogInformation("Executando o método Update do UserService com o ID {0} e o request {@Request}", userId, request);
            var user = await _userRepository.GetById(userId);
            if (user is null) throw new NotFoundException(AppMessages.UserNotFoundMessage);

            user.Name = request.Name ?? user.Name;
            user.Email = request.Email ?? user.Email;
            user.Password = !string.IsNullOrEmpty(request.Password) ?
                _encryptionService.Encrypt(request.Password)
                : user.Password;
            user.RoleId = request.RoleId ?? user.RoleId;

            _logger.LogInformation("Vai atualizar o usuário {0}", user.Id);
            await _userRepository.Update(user);
        }

        public async Task Delete(Guid userId)
        {
            _logger.LogInformation("Executando o método Delete do UserService com o ID {0}", userId);
            var user = await _userRepository.GetById(userId);
            if (user is null) throw new NotFoundException(AppMessages.UserNotFoundMessage);

            _logger.LogInformation("Vai excluir o usuário {0}", user.Id);
            await _userRepository.Delete(user);
        }
    }
}
