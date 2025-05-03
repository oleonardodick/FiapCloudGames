using FiapCloudGames.API.Modules.Users.DTOs.Requests;
using FiapCloudGames.API.Modules.Users.Validators;
using FiapCloudGames.API.Utils;
using FluentValidation.TestHelper;

namespace FiapCloudGames.Test.UnitTests.UserTests
{
    public class RequestCreateUserValidatorTest
    {
        private readonly RequestCreateUserValidator _validator;

        public RequestCreateUserValidatorTest()
        {
            _validator = new RequestCreateUserValidator();
        }

        #region Field Name Tests
        [Trait("Category", "UnitTest")]
        [Trait("Module", "CreateUserValidator")]
        [Fact]
        public void Validate_ShouldReturnRequiredFieldForName()
        {
            //Arrange
            var request = new RequestCreateUserDTO
            {
                Email = "mail@test.com",
                Password = "@Password123",
                RoleId = Guid.NewGuid()
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Name).WithErrorMessage(AppMessages.GetRequiredFieldMessage("name"));
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "CreateUserValidator")]
        [Fact]
        public void Validate_ShouldReturnNotEmptyFieldForName()
        {
            //Arrange
            var request = new RequestCreateUserDTO
            {
                Name = "",
                Email = "mail@test.com",
                Password = "@Password123",
                RoleId = Guid.NewGuid()
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Name).WithErrorMessage(AppMessages.GetNotEmptyFieldMessage("name"));
        }
        #endregion

        #region Field Email Tests
        [Trait("Category", "UnitTest")]
        [Trait("Module", "CreateUserValidator")]
        [Fact]
        public void Validate_ShouldReturnRequiredFieldForEmail()
        {
            //Arrange
            var request = new RequestCreateUserDTO
            {
                Name = "User test",
                Password = "@Password123",
                RoleId = Guid.NewGuid()
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Email).WithErrorMessage(AppMessages.GetRequiredFieldMessage("email"));
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "CreateUserValidator")]
        [Fact]
        public void Validate_ShouldReturnNotEmptyFieldForEmail()
        {
            //Arrange
            var request = new RequestCreateUserDTO
            {
                Name = "User test",
                Email = "",
                Password = "@Password123",
                RoleId = Guid.NewGuid()
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Email).WithErrorMessage(AppMessages.GetNotEmptyFieldMessage("email"));
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "CreateUserValidator")]
        [Fact]
        public void Validate_ShouldReturnInvalidEmail()
        {
            //Arrange
            var request = new RequestCreateUserDTO
            {
                Name = "User test",
                Email = "invalidMail",
                Password = "@Password123",
                RoleId = Guid.NewGuid()
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Email).WithErrorMessage(AppMessages.InvalidEmailFormatMessage);
        }
        #endregion

        #region Field Password Tests
        [Trait("Category", "UnitTest")]
        [Trait("Module", "CreateUserValidator")]
        [Fact]
        public void Validate_ShouldReturnRequiredFieldForPassword()
        {
            //Arrange
            var request = new RequestCreateUserDTO
            {
                Name = "User test",
                Email = "test@mail.com",
                RoleId = Guid.NewGuid()
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Password).WithErrorMessage(AppMessages.GetRequiredFieldMessage("password"));
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "CreateUserValidator")]
        [Fact]
        public void Validate_ShouldReturnNotEmptyFieldForPassword()
        {
            //Arrange
            var request = new RequestCreateUserDTO
            {
                Name = "User test",
                Email = "mail@test.com",
                Password = "",
                RoleId = Guid.NewGuid()
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Password).WithErrorMessage(AppMessages.GetNotEmptyFieldMessage("password"));
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "CreateUserValidator")]
        [Fact]
        public void Validate_ShouldReturnInvalidPasswordFormat()
        {
            //Arrange
            var request = new RequestCreateUserDTO
            {
                Name = "User test",
                Email = "mail@test.com",
                Password = "password",
                RoleId = Guid.NewGuid()
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Password).WithErrorMessage(AppMessages.InvalidPasswordFormatMessage);
            Assert.Single(result.Errors);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "CreateUserValidator")]
        [Fact]
        public void Validate_ShouldReturnInvalidSizeForPassword()
        {
            //Arrange
            var request = new RequestCreateUserDTO
            {
                Name = "User test",
                Email = "mail@test.com",
                Password = "@Pa12",
                RoleId = Guid.NewGuid()
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Password).WithErrorMessage(AppMessages.InvalidSizePasswordMessage);
        }
        #endregion

        #region Field RoleId Tests
        [Trait("Category", "UnitTest")]
        [Trait("Module", "CreateUserValidator")]
        [Fact]
        public void Validate_ShouldReturnNotEmpyFieldForRoleId()
        {
            //Arrange
            var request = new RequestCreateUserDTO
            {
                Name = "User test",
                Email = "mail@test.com",
                Password = "@Password123",
                RoleId = Guid.Empty
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.RoleId).WithErrorMessage(AppMessages.GetNotEmptyFieldMessage("roleId"));
        }
        #endregion

        [Trait("Category", "UnitTest")]
        [Trait("Module", "CreateUserValidator")]
        [Fact]
        public void Validate_ShouldReturnMoreThanOneError()
        {
            //Arrange
            var request = new RequestCreateUserDTO { };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            Assert.NotEmpty(result.Errors);
            Assert.NotEqual(1, result.Errors.Count);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "CreateUserValidator")]
        [Fact]
        public void Validate_ShouldValidateTheUserData()
        {
            //Arrange
            var request = new RequestCreateUserDTO
            {
                Name = "User test",
                Email = "mail@test.com",
                Password = "@Password123",
                RoleId = Guid.NewGuid()
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            Assert.Empty(result.Errors);
        }
    }
}
