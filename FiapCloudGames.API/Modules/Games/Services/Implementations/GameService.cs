using FiapCloudGames.API.Exceptions;
using FiapCloudGames.API.Modules.Games.DTOs.Requests;
using FiapCloudGames.API.Modules.Games.DTOs.Responses;
using FiapCloudGames.API.Modules.Games.Entities;
using FiapCloudGames.API.Modules.Games.Repositories.Interfaces;
using FiapCloudGames.API.Modules.Games.Services.Interfaces;
using FiapCloudGames.API.Shared.DTOs;
using FiapCloudGames.API.Utils;

namespace FiapCloudGames.API.Modules.Games.Services.Implementations
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ILogger<GameService> _logger;

        public GameService(IGameRepository gameRepository, ILogger<GameService> logger)
        {
            _gameRepository = gameRepository;
            _logger = logger;
        }

        public async Task<ResponseGamesDTO> GetAll(int pageNumber, int perPage)
        {
            _logger.LogInformation("Executando o método GetAll da GameService com o parâmetro pageNumber = {0}", pageNumber);
            var (games, totalItems) = await _gameRepository.GetAll(pageNumber, perPage);

            var response = new ResponseGamesDTO
            {
                Pagination = new PaginationDTO
                {
                    PageNumber = pageNumber,
                    TotalItems = totalItems,
                    TotalPages = (int)Math.Ceiling(totalItems / (double)perPage),
                    PerPage = perPage
                },
                Games = games.Select(game => new ResponseGameDTO
                {
                    Id = game.Id,
                    Name = game.Name,
                    Description = game.Description,
                    Price = game.Price,
                    CreatedAt = game.CreatedAt
                }).ToList()
            };
            _logger.LogInformation("Retorno do método GetAll da GameService: {@Reponse}", response);

            return response;
        }

        public async Task<ResponseGameDTO> GetById(Guid gameId)
        {
            _logger.LogInformation("Executando o método GetById do GameService com o parâmetro gameId como {0}", gameId);
            var game = await _gameRepository.GetById(gameId);
            if (game is null) throw new NotFoundException(AppMessages.GameNotFoundMessage);

            var response = new ResponseGameDTO
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Price = game.Price,
                CreatedAt = game.CreatedAt
            };

            _logger.LogInformation("Retorno do método GetById do GameService: {@Reponse}", response);
            return response;
        }

        public async Task<ResponseGameDTO> Create(RequestCreateGameDTO request)
        {
            _logger.LogInformation("Executando o método Create do GameService com os dados {@Request}", request);
            var game = new Game
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            await _gameRepository.Create(game);

            var response = new ResponseGameDTO
            {
                Id = game.Id,
                CreatedAt = game.CreatedAt,
                Name = game.Name,
                Description = game.Description,
                Price = game.Price
            };

            _logger.LogInformation("Retorno do método Create do GameService: {@Response}", response);
            return response;
        }

        public async Task Update(Guid gameId, RequestUpdateGameDTO request)
        {
            _logger.LogInformation("Executando o método Update do GameService com o ID {0} e os dados {@Request}", gameId, request);
            var game = await _gameRepository.GetById(gameId);
            if (game is null) throw new NotFoundException(AppMessages.GameNotFoundMessage);

            game.Name = request.Name ?? game.Name;
            game.Description = request.Description ?? game.Description;
            game.Price = request.Price ?? game.Price;

            await _gameRepository.Update(game);
        }

        public async Task Delete(Guid gameId)
        {
            _logger.LogInformation("Executando o método Delete do GameService com o ID {0}", gameId);
            var game = await _gameRepository.GetById(gameId);
            if (game is null) throw new NotFoundException(AppMessages.GameNotFoundMessage);

            await _gameRepository.Delete(game);
        }
    }
}
