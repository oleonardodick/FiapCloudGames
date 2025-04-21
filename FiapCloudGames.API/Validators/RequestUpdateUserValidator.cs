using FiapCloudGames.API.DTOs.Requests.UserDTO;
using FiapCloudGames.API.Utils;
using FluentValidation;

namespace FiapCloudGames.API.Validators
{
    public class RequestUpdateUserValidator : AbstractValidator<RequestUpdateUserDTO>
    {
        public RequestUpdateUserValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage(AppMessages.GetRequiredFieldMessage("name"))
                .When(r => r.Name != null);

            RuleFor(r => r.Email)
                .NotEmpty().WithMessage(AppMessages.GetRequiredFieldMessage("email"))
                .When(r => r.Email != null);

            RuleFor(r => r.Email)
                .EmailAddress().WithMessage(AppMessages.InvalidEmailFormatMessage)
                .When(r => !string.IsNullOrWhiteSpace(r.Email));

            RuleFor(r => r.Password)
                .Length(8, 30).WithMessage(AppMessages.InvalidSizePasswordMessage)
                .When(r => r.Password != null);

            RuleFor(r => r.Password)
                .Must(PasswordValidator.HasValidFormat).WithMessage(AppMessages.InvalidPasswordFormatMessage)
                .When(r => !string.IsNullOrWhiteSpace(r.Password));
        }
    }
}
