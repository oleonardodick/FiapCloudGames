using FiapCloudGames.API.Modules.Users.DTOs.Requests;
using FiapCloudGames.API.Modules.Users.Validators;
using FiapCloudGames.API.Shared.Utils;
using FluentValidation.TestHelper;

namespace FiapCloudGames.Test.UnitTests.UserTests
{
    public class RequestUpdateUserValidatorTest
    {
        private readonly RequestUpdateUserValidator _validator;

        public RequestUpdateUserValidatorTest()
        {
            _validator = new RequestUpdateUserValidator();
        }

        #region Field Name Tests
        [Trait("Category", "UnitTest")]
        [Trait("Module", "UpdateUserValidation")]
        [Fact]
        public void Validate_ShouldReturnNotEmptyFieldForName()
        {
            //Arrange
            var request = new RequestUpdateUserDTO
            {
                Name = ""
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Name).WithErrorMessage(AppMessages.GetNotEmptyFieldMessage("name"));
            Assert.Single(result.Errors);
        }
        #endregion

        #region Field Email Tests
        [Trait("Category", "UnitTest")]
        [Trait("Module", "UpdateUserValidation")]
        [Fact]
        public void Validate_ShouldReturnNotEmptyFieldForEmail()
        {
            //Arrange
            var request = new RequestUpdateUserDTO
            {
                Email = ""
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Email).WithErrorMessage(AppMessages.GetNotEmptyFieldMessage("email"));
            Assert.Single(result.Errors);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UpdateUserValidation")]
        [Fact]
        public void Validate_ShouldReturnInvalidEmail()
        {
            //Arrange
            var request = new RequestUpdateUserDTO
            {
                Email = "invalidMail"
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Email).WithErrorMessage(AppMessages.InvalidEmailFormatMessage);
            Assert.Single(result.Errors);
        }
        #endregion

        #region Field Password Tests
        [Trait("Category", "UnitTest")]
        [Trait("Module", "UpdateUserValidation")]
        [Fact]
        public void Validate_ShouldReturnNotEmptyFieldForPassword()
        {
            //Arrange
            var request = new RequestUpdateUserDTO
            {
                Password = ""
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Password).WithErrorMessage(AppMessages.GetNotEmptyFieldMessage("password"));
            Assert.Single(result.Errors);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UpdateUserValidation")]
        [Fact]
        public void Validate_ShouldReturnInvalidPasswordFormat()
        {
            //Arrange
            var request = new RequestUpdateUserDTO
            {
                Password = "password"
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Password).WithErrorMessage(AppMessages.InvalidPasswordFormatMessage);
            Assert.Single(result.Errors);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UpdateUserValidation")]
        [Fact]
        public void Validate_ShouldReturnInvalidSizeForPassword()
        {
            //Arrange
            var request = new RequestUpdateUserDTO
            {
                Password = "@Pa12"
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Password).WithErrorMessage(AppMessages.InvalidSizePasswordMessage);
            Assert.Single(result.Errors);
        }
        #endregion

        #region Field RoleId Tests
        [Trait("Category", "UnitTest")]
        [Trait("Module", "UpdateUserValidation")]
        [Fact]
        public void Validate_ShouldReturnNotEmpyFieldForRoleId()
        {
            //Arrange
            var request = new RequestUpdateUserDTO
            {
                RoleId = Guid.Empty
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.RoleId).WithErrorMessage(AppMessages.GetNotEmptyFieldMessage("roleId"));
        }
        #endregion

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UpdateUserValidation")]
        [Fact]
        public void Validate_ShouldReturnMoreThanOneError()
        {
            //Arrange
            var request = new RequestUpdateUserDTO 
            {
                Name = "",
                Email = ""
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            Assert.NotEmpty(result.Errors);
            Assert.NotEqual(1, result.Errors.Count);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UpdateUserValidation")]
        [Fact]
        public void Validate_ShouldValidateTheUserData()
        {
            //Arrange
            var request = new RequestUpdateUserDTO
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
