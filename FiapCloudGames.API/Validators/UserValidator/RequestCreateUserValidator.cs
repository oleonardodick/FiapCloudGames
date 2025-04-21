using FiapCloudGames.API.DTOs.Requests.UserDTO;
using FiapCloudGames.API.Utils;
using FluentValidation;
namespace FiapCloudGames.API.Validators.UserValidator
{
    public class RequestCreateUserValidator : AbstractValidator<RequestCreateUserDTO>
    {
        public RequestCreateUserValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty().WithMessage(AppMessages.GetRequiredFieldMessage("name"));

            RuleFor(request => request.Email)
                .EmailAddress().WithMessage(AppMessages.InvalidEmailFormatMessage)
                .When(r => !string.IsNullOrEmpty(r.Email));

            RuleFor(request => request.Email)
                .NotEmpty().WithMessage(AppMessages.GetRequiredFieldMessage("email"));


            RuleFor(request => request.Password)
                .Must(PasswordValidator.HasValidFormat).WithMessage(AppMessages.InvalidPasswordFormatMessage)
                .When(r => !string.IsNullOrEmpty(r.Password) && r.Password.Length >= 8 && r.Password.Length <= 30);

            RuleFor(request => request.Password)
                .Length(8, 30).WithMessage(AppMessages.InvalidSizePasswordMessage);

            RuleFor(request => request.RoleId)
                .NotEmpty().WithMessage(AppMessages.GetRequiredFieldMessage("roleId"));
        }
    }
}
