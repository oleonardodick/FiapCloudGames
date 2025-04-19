using FiapCloudGames.API.Entities;
using FiapCloudGames.API.Exceptions;
using FiapCloudGames.API.Messages;
using Moq;

namespace FiapCloudGames.Test.UnitTests.RoleTests
{
    public class RoleServiceTest : RoleServiceTestBase
    {
        [Fact]
        public async Task GetAll_ShouldGetAllRoles()
        {
            //Arrange
            _roleRepository
                .Setup(r => r.GetAll())
                .ReturnsAsync(roles);

            //Act
            var response = await _roleService.GetAll();

            //Assert
            Assert.NotNull(response.Roles);
            Assert.Equal(roles.Count, response.Roles.Count);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmptyList()
        {
            //Arrange
            _roleRepository
                .Setup(r => r.GetAll())
                .ReturnsAsync([]);

            //Act
            var response = await _roleService.GetAll();

            //Assert
            Assert.NotNull(response.Roles);
            Assert.Empty(response.Roles);
        }

        [Fact]
        public async Task GetById_ShouldReturnARoleByItsId()
        {
            //Arrange
            _roleRepository
                .Setup(r => r.GetById(roles[0].Id))
                .ReturnsAsync(roles[0]);

            //Act
            var response = await _roleService.GetById(roles[0].Id);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(response.Name, roles[0].Name);
            Assert.Equal(response.Id, roles[0].Id);
            Assert.Equal(response.CreatedAt, roles[0].CreatedAt);
        }

        [Fact]
        public async Task GetById_ShouldReturnRoleNotFound()
        {
            //Arrange
            var roleId = Guid.NewGuid();
            _roleRepository
                .Setup(r => r.GetById(roleId))
                .ReturnsAsync((Role?)null);

            //Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _roleService.GetById(roleId));

            //Assert
            Assert.NotNull(exception);
            Assert.Contains(AppMessages.RoleNotFoundMessage, exception.GetErrorMessages());
        }
    }
}
