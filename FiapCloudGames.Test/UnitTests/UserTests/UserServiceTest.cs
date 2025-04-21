using FiapCloudGames.API.DTOs.Requests.UserDTO;
using FiapCloudGames.API.Entities;
using FiapCloudGames.API.Exceptions;
using FiapCloudGames.API.Utils;
using FiapCloudGames.Test.Utils;
using Moq;

namespace FiapCloudGames.Test.UnitTests.UserTests
{
    public class UserServiceTest : UserServiceTestBase
    {
        [Theory]
        [InlineData(1, 5)]
        [InlineData(1, 15)]
        [InlineData(2, 15)]
        public async Task GetAll_ShouldReturnAllUsersFromPage(int pageNumber, int qtToGenerate)
        {
            //Arrange
            var users = FakeUser.FakeListUsers(qtToGenerate);
            var pageSize = 10;
            var totalPages = (int)Math.Ceiling(qtToGenerate / (double)pageSize);
            var qtUsersInPage = Math.Min(pageSize, qtToGenerate - ((pageNumber - 1) * pageSize));

            var usersToReturn = users
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            _userRepository
                .Setup(r => r.GetAll(pageNumber, pageSize))
                .ReturnsAsync((usersToReturn, qtToGenerate));

            //Act
            var response = await _userSevice.GetAll(pageNumber, pageSize);
            var resultUsers = response.Users;
            var pagination = response.Pagination;

            //Assert
            Assert.NotNull(resultUsers);
            Assert.NotNull(pagination);
            Assert.Equal(pageNumber, pagination.PageNumber);
            Assert.Equal(totalPages, pagination.TotalPages);
            Assert.Equal(qtToGenerate, pagination.TotalItems);
            Assert.Equal(pageSize, pagination.PerPage);
            Assert.Equal(qtUsersInPage, resultUsers.Count);
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
            var response = await _userSevice.GetAll(pageNumber, pageSize);
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
            var generatedToken = "generatedToken";
            var role = new Role { Name = AppRoles.Admin };

            var request = new RequestCreateUserDTO
            {
                Name = "test User",
                Email = "teste@mail.com",
                Password = "@Password123",
                RoleId = Guid.NewGuid()
            };

            _userRepository
                .Setup(u => u.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User?)null);

            _roleRepository
                .Setup(u => u.GetById(request.RoleId))
                .ReturnsAsync(role);

            _userRepository
                .Setup(u => u.Create(It.IsAny<User>()));

            _jwtService
                .Setup(j => j.GenerateToken(It.IsAny<Guid>(), It.IsAny<string>()))
                .Returns(generatedToken);

            //Act
            var response = await _userSevice.Create(request);

            //Assert
            Assert.NotNull(response);
            Assert.NotEqual(Guid.Empty, response.Id);
            Assert.Equal(request.Name, response.Name);
            Assert.Equal(request.Email, response.Email);
            Assert.Equal(generatedToken, response.AccessToken);
            _userRepository.Verify(r => r.Create(It.IsAny<User>()), Times.Once);

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
