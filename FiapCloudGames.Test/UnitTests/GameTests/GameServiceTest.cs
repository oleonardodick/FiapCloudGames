using FiapCloudGames.API.DTOs.Requests.GameDTO;
using FiapCloudGames.API.Entities;
using FiapCloudGames.API.Exceptions;
using FiapCloudGames.API.Utils;
using FiapCloudGames.Test.Utils;
using Moq;

namespace FiapCloudGames.Test.UnitTests.GameTests
{
    public class GameServiceTest : GameServiceTestBase
    {
        [Theory]
        [InlineData(1, 5)]
        [InlineData(1, 15)]
        [InlineData(2, 15)]
        public async Task GetAll_ShouldGetAllGames(int pageNumber, int qtToGenerate)
        {
            //Arrange
            var pageSize = 10;
            var totalPages = (int)Math.Ceiling(qtToGenerate / (double)pageSize);
            var games = FakeGame.FakeListGames(qtToGenerate);
            var qtGamesInPage = Math.Min(pageSize, qtToGenerate - ((pageNumber - 1) * pageSize));

            var gamesToReturn = games
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            _gameRepository
                .Setup(g => g.GetAll(pageNumber, pageSize))
                .ReturnsAsync((gamesToReturn, qtToGenerate));

            //Act
            var response = await _gameService.GetAll(pageNumber);
            var gamesReturned = response.Games;
            var pagination = response.Pagination;

            //Assert
            Assert.NotNull(pagination);
            Assert.Equal(totalPages, pagination.TotalPages);
            Assert.Equal(qtToGenerate, pagination.TotalItems);
            Assert.Equal(pageNumber, pagination.PageNumber);
            Assert.Equal(pageSize, pagination.PerPage);
            Assert.NotNull(gamesReturned);
            Assert.Equal(qtGamesInPage, gamesReturned.Count);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmptyList()
        {
            //Arrange
            var pageSize = 10;
            var pageNumber = 1;
            var qtToGenerate = 0;
            var totalPages = (int)Math.Ceiling(qtToGenerate / (double)pageSize);

            _gameRepository
                .Setup(g => g.GetAll(pageNumber, pageSize))
                .ReturnsAsync(([], qtToGenerate));

            //Act
            var response = await _gameService.GetAll(pageNumber);
            var gamesReturned = response.Games;
            var pagination = response.Pagination;

            //Assert
            Assert.NotNull(pagination);
            Assert.Equal(totalPages, pagination.TotalPages);
            Assert.Equal(qtToGenerate, pagination.TotalItems);
            Assert.Equal(pageNumber, pagination.PageNumber);
            Assert.Equal(pageSize, pagination.PerPage);
            Assert.Empty(gamesReturned);
        }

        [Fact]
        public async Task GetById_ShouldReturnAGame()
        {
            //Arrange
            var game = FakeGame.FakeSpecificGame();
            _gameRepository
                .Setup(g => g.GetById(game.Id))
                .ReturnsAsync(game);

            //Act
            var response = await _gameService.GetById(game.Id);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(game.Id, response.Id);
            Assert.Equal(game.Name, response.Name);
            Assert.Equal(game.Description, response.Description);
            Assert.Equal(game.Price, response.Price);
            Assert.Equal(game.CreatedAt, response.CreatedAt);
        }

        [Fact]
        public async Task GetById_ShouldReturnGameNotFoundException()
        {
            //Arrange
            _gameRepository
                .Setup(g => g.GetById(It.IsAny<Guid>()))
                .ReturnsAsync((Game?)null);

            //Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _gameService.GetById(It.IsAny<Guid>()));

            Assert.NotNull(exception);
            Assert.Contains(AppMessages.GameNotFoundMessage, exception.GetErrorMessages());
        }

        [Fact]
        public async Task Create_ShouldCreateAGame()
        {
            //Arrange
            _gameRepository
                .Setup(g => g.Create(It.IsAny<Game>()));

            var request = new RequestCreateGameDTO
            {
                Name = "Game test",
                Description = "Game description",
                Price = 10.50
            };

            //Act
            var response = await _gameService.Create(request);

            //Arrange
            Assert.NotNull(response);
            Assert.NotEqual(Guid.Empty, response.Id);
            Assert.Equal(request.Name, response.Name);
            Assert.Equal(request.Description, response.Description);
            Assert.Equal(request.Price, response.Price);
        }

        [Fact]
        public async Task Update_ShouldUpdateAGame()
        {
            //Arrange
            var game = FakeGame.FakeSpecificGame();

            _gameRepository
                .Setup(g => g.GetById(game.Id))
                .ReturnsAsync(game);

            var request = new RequestUpdateGameDTO
            {
                Name = "Updated game"
            };

            //Act
            await _gameService.Update(game.Id, request);

            //Assert
            Assert.Equal(request.Name, game.Name);
            _gameRepository.Verify(r => r.Update(game), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFoundException()
        {
            //Arrange
            var gameId = Guid.NewGuid();
            _gameRepository
                .Setup(g => g.GetById(gameId));

            var request = new RequestUpdateGameDTO
            {
                Name = "Updated game"
            };

            //Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _gameService.Update(gameId, request));

            Assert.NotNull(exception);
            Assert.Contains(AppMessages.GameNotFoundMessage, exception.GetErrorMessages());
        }

        [Fact]
        public async Task Delete_ShouldDeleteTheGame()
        {
            //Arrange
            var game = FakeGame.FakeSpecificGame();

            _gameRepository
                .Setup(g => g.GetById(game.Id))
                .ReturnsAsync(game);

            //Act
            await _gameService.Delete(game.Id);

            //Assert
            _gameRepository.Verify(r => r.Delete(game), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFoundException()
        {
            //Arrange
            var gameId = Guid.NewGuid();
            _gameRepository
                .Setup(g => g.GetById(gameId))
                .ReturnsAsync((Game?)null);

            //Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _gameService.Delete(gameId));

            Assert.NotNull(exception);
            Assert.Contains(AppMessages.GameNotFoundMessage, exception.GetErrorMessages());
        }
    }
}
