using FiapCloudGames.API.DTOs.Requests;
using FiapCloudGames.API.Utils;
using FiapCloudGames.API.Validators;
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

        [Fact]
        public void Validate_ShouldReturnRequiredFieldForName()
        {
            //Arrange
            var request = new RequestUpdateUserDTO
            {
                Name = ""
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Name).WithErrorMessage(AppMessages.GetRequiredFieldMessage("name"));
        }

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
        }

        [Fact]
        public void Validate_ShouldReturnRequiredFieldForEmail()
        {
            //Arrange
            var request = new RequestUpdateUserDTO
            {
                Email = ""
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Email).WithErrorMessage(AppMessages.GetRequiredFieldMessage("email"));
        }

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
        }

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
