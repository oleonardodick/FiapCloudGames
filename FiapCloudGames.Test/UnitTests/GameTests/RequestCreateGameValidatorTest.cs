using FiapCloudGames.API.Modules.Games.DTOs.Requests;
using FiapCloudGames.API.Modules.Games.Validators;
using FiapCloudGames.API.Shared.Utils;
using FluentValidation.TestHelper;

namespace FiapCloudGames.Test.UnitTests.GameTests
{
    public class RequestCreateGameValidatorTest
    {
        private readonly RequestCreateGameValidator _validator;

        public RequestCreateGameValidatorTest()
        {
            _validator = new RequestCreateGameValidator();
        }

        #region Name Field Tests
        [Trait("Category", "UnitTest")]
        [Trait("Module", "CreateGameValidator")]
        [Fact]
        public void Validate_ShouldReturnRequiredFieldForName()
        {
            //Arrange
            var request = new RequestCreateGameDTO
            {
                Description = "Game description",
                Price = 10
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Name).WithErrorMessage(AppMessages.GetRequiredFieldMessage("name"));
            Assert.Single(result.Errors);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "CreateGameValidator")]
        [Fact]
        public void Validate_ShouldReturnNotEmptyFieldForName()
        {
            //Arrange
            var request = new RequestCreateGameDTO
            {
                Name = "",
                Description = "Game description",
                Price = 10
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Name).WithErrorMessage(AppMessages.GetNotEmptyFieldMessage("name"));
            Assert.Single(result.Errors);
        }
        #endregion

        #region Price Field Tests
        [Trait("Category", "UnitTest")]
        [Trait("Module", "CreateGameValidator")]
        [Fact]
        public void Validate_ShouldReturnPriceGreaterThanZeroValidation()
        {
            //Arrange
            var request = new RequestCreateGameDTO
            {
                Name = "Game name",
                Description = "Game description",
                Price = 0
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Price).WithErrorMessage(AppMessages.PriceGreaterThanZeroMessage);
            Assert.Single(result.Errors);
        }
        #endregion

        [Trait("Category", "UnitTest")]
        [Trait("Module", "CreateGameValidator")]
        [Fact]
        public void Validate_ShouldReturnMoreThanOneError()
        {
            //Arrange
            var request = new RequestCreateGameDTO { };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            Assert.NotEmpty(result.Errors);
            Assert.NotEqual(1, result.Errors.Count);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "CreateGameValidator")]
        [Fact]
        public void Validate_ShouldValidateTheGameData()
        {
            //Arrange
            var request = new RequestCreateGameDTO
            {
                Name = "Game test",
                Price = 10.0
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            Assert.Empty(result.Errors);
        }
    }
}
