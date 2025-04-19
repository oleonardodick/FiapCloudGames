using FiapCloudGames.API.DTOs.Requests;
using FluentValidation;

namespace FiapCloudGames.API.Services.Validations
{
    public class UserRequestInputValidation
    {
        private readonly IValidator<RequestUserInputDTO> _validator;

        public UserRequestInputValidation(IValidator<RequestUserInputDTO> validator)
        {
            _validator = validator;
        }
    }
}
