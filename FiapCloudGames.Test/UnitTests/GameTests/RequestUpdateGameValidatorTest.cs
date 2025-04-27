using FiapCloudGames.API.DTOs.Requests.GameDTO;
using FiapCloudGames.API.Utils;
using FiapCloudGames.API.Validators.GameValidator;
using FluentValidation.TestHelper;

namespace FiapCloudGames.Test.UnitTests.GameTests
{
    public class RequestUpdateGameValidatorTest
    {
        private readonly RequestUpdateGameValidator _validator;

        public RequestUpdateGameValidatorTest()
        {
            _validator = new RequestUpdateGameValidator();
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UpdateGameValidator")]
        [Fact]
        public void Validate_ShouldReturnNotEmptyFieldForName()
        {
            //Arrange
            var request = new RequestUpdateGameDTO
            {
                Name = "",
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Name).WithErrorMessage(AppMessages.GetNotEmptyFieldMessage("name"));
            Assert.Single(result.Errors);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UpdateGameValidator")]
        [Fact]
        public void Validate_ShouldReturnPriceGreaterThanZeroValidation()
        {
            //Arrange
            var request = new RequestUpdateGameDTO
            {
                Price = 0
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            result.ShouldHaveValidationErrorFor(c => c.Price).WithErrorMessage(AppMessages.PriceGreaterThanZeroMessage);
            Assert.Single(result.Errors);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UpdateGameValidator")]
        [Fact]
        public void Validate_ShouldReturnMoreThanOneError()
        {
            //Arrange
            var request = new RequestUpdateGameDTO 
            {
                Name = "",
                Price = 0
            };

            //Act
            var result = _validator.TestValidate(request);

            //Assert
            Assert.NotEmpty(result.Errors);
            Assert.NotEqual(1, result.Errors.Count);
        }

        [Trait("Category", "UnitTest")]
        [Trait("Module", "UpdateGameValidator")]
        [Fact]
        public void Validate_ShouldValidateTheGameData()
        {
            //Arrange
            var request = new RequestUpdateGameDTO
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
