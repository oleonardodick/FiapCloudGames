using FiapCloudGames.API.DTOs.Requests;
using FiapCloudGames.API.Entities;
using FiapCloudGames.API.Exceptions;
using FiapCloudGames.API.Messages;
using FiapCloudGames.Test.Utils;
using Moq;

namespace FiapCloudGames.Test.UnitTests.UserTests
{
    public class UserServiceTest : UserServiceTestBase
    {
        [Fact]
        public async Task GetAll_ShouldReturnAllUsers()
        {
            //Arrange
            var qtToGenerate = 5;
            var users = FakeUser.FakeListUsers(qtToGenerate);
            var pageSize = 10;
            var totalPages = (int)Math.Ceiling(qtToGenerate / (double)pageSize);
            var pageNumber = 1;

            _userRepository
                .Setup(r => r.GetAll(pageNumber, pageSize))
                .ReturnsAsync((users, qtToGenerate));

            //Act
            var response = await _userSevice.GetAll(pageNumber);
            var resultUsers = response.Users;
            var pagination = response.Pagination;

            //Assert
            Assert.NotNull(resultUsers);
            Assert.Equal(qtToGenerate, resultUsers.Count);
            Assert.NotNull(pagination);
            Assert.Equal(pageNumber, pagination.PageNumber);
            Assert.Equal(totalPages, pagination.TotalPages);
            Assert.Equal(qtToGenerate, pagination.TotalItems);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmptyList()
        {
            //Arrange
            var qtToGenerate = 0;
            var pageSize = 10;
            var totalPages = (int)Math.Ceiling(qtToGenerate / (double)pageSize);
            var pageNumber = 1;

            _userRepository
                .Setup(r => r.GetAll(pageNumber, pageSize))
                .ReturnsAsync(([], qtToGenerate));

            //Act
            var response = await _userSevice.GetAll(pageNumber);
            var resultUsers = response.Users;
            var pagination = response.Pagination;

            //Assert
            Assert.NotNull(resultUsers);
            Assert.Empty(resultUsers);
            Assert.NotNull(pagination);
            Assert.Equal(pageNumber, pagination.PageNumber);
            Assert.Equal(totalPages, pagination.TotalPages);
            Assert.Equal(qtToGenerate, pagination.TotalItems);
        }

        [Fact]
        public async Task GetById_ShouldReturnUserNotFound()
        {
            //Arrange
            var userId = Guid.NewGuid();

            _userRepository
                .Setup(u => u.GetById(userId))
                .ReturnsAsync((User?)null);

            //Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _userSevice.GetById(userId));

            Assert.NotNull(exception);
            Assert.Contains(AppMessages.UserNotFoundMessage, exception.GetErrorMessages());
        }

        [Fact]
        public async Task GetById_ShouldReturnTheUser()
        {
            //Arrange
            var user = FakeUser.FakeSpecificUser();

            _userRepository
                .Setup(u => u.GetById(user.Id))
                .ReturnsAsync(user);

            //Act
            var response = await _userSevice.GetById(user.Id);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(response.Id, user.Id);
            Assert.Equal(response.Name, user.Name);
            Assert.Equal(response.Email, user.Email);
            Assert.Equal(response.CreatedAt, user.CreatedAt);
        }

        [Fact]
        public async Task Create_ShouldCreateAnUser()
        {
            //Arrange
            var user = FakeUser.FakeSpecificUser();
            var generatedToken = "generatedToken";

            var request = new RequestCreateUserDTO
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId
            };

            _userRepository
                .Setup(u => u.GetByEmailAsync(user.Email))
                .ReturnsAsync((User?)null);

            _userRepository
                .Setup(u => u.Create(It.IsAny<User>()))
                .ReturnsAsync(user);

            _jwtService
                .Setup(j => j.GenerateToken(user.Id))
                .Returns(generatedToken);

            //Act
            var response = await _userSevice.Create(request);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(generatedToken, response.AccessToken);

        }

        [Fact]
        public async Task Create_ShouldReturnEmailAlreadyExistsException()
        {
            //Arrange
            var user = FakeUser.FakeSpecificUser();
            var request = new RequestCreateUserDTO
            {
                Name = "user test",
                Email = user.Email,
                Password = "@Password123",
                RoleId = Guid.NewGuid()
            };

            _userRepository
                .Setup(u => u.GetByEmailAsync(request.Email))
                .ReturnsAsync(user);

            //Act & Assert
            var exception = await Assert.ThrowsAsync<EmailAlreadyExistsException>(() => _userSevice.Create(request));

            Assert.NotNull(exception);
            Assert.Contains(AppMessages.EmailAlreadyExistsMessage, exception.GetErrorMessages());
        }

        [Fact]
        public async Task Update_ShouldUpdateTheUser()
        {
            //Arrange
            var user = FakeUser.FakeSpecificUser();

            var request = new RequestUpdateUserDTO
            {
                Name = "Nome atualizado"
            };

            _userRepository
                .Setup(u => u.GetById(user.Id))
                .ReturnsAsync(user);

            //Act
            await _userSevice.Update(user.Id, request);

            //Assert
            Assert.Equal(request.Name, user.Name);
            _userRepository.Verify(r => r.Update(It.Is<User>(u => u == user)), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldReturnUserNotFound()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var request = new RequestUpdateUserDTO
            {
                Name = "Nome atualizado"
            };

            _userRepository
                .Setup(u => u.GetById(userId))
                .ReturnsAsync((User?)null);

            //Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _userSevice.Update(userId, request));

            Assert.NotNull(exception);
            Assert.Contains(AppMessages.UserNotFoundMessage, exception.GetErrorMessages());
        }

        [Fact]
        public async Task Delete_ShouldDeleteTheUser()
        {
            //Arrange
            var user = FakeUser.FakeSpecificUser();

            _userRepository
                .Setup(u => u.GetById(user.Id))
                .ReturnsAsync(user);

            //Act
            await _userSevice.Delete(user.Id);

            //Assert
            _userRepository.Verify(r => r.Delete(user), Times.Once);
        }

        [Fact]
        public async Task Delete_ShouldReturnUserNotFound()
        {
            //Arrange
            var userId = Guid.NewGuid();
            _userRepository
                .Setup(u => u.GetById(userId))
                .ReturnsAsync((User?)null);

            //Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _userSevice.Delete(userId));

            Assert.NotNull(exception);
            Assert.Contains(AppMessages.UserNotFoundMessage, exception.GetErrorMessages());
        }
    }
}
