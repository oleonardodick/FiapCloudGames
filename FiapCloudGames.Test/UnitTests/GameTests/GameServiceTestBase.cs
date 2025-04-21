using FiapCloudGames.API.Repositories.Interfaces;
using FiapCloudGames.API.Services.Implementations;
using Microsoft.Extensions.Logging;
using Moq;

namespace FiapCloudGames.Test.UnitTests.GameTests
{
    public abstract class GameServiceTestBase
    {
        protected readonly Mock<IGameRepository> _gameRepository;
        protected readonly Mock<ILogger<GameService>> _logger;
        protected readonly GameService _gameService;

        protected GameServiceTestBase()
        {
            _gameRepository = new Mock<IGameRepository>();
            _logger = new Mock<ILogger<GameService>>();
            _gameService = new GameService(_gameRepository.Object, _logger.Object);
        }
    }
}
