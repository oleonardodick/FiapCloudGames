using FiapCloudGames.API.Modules.Users.DTOs.Requests;
using FiapCloudGames.API.Utils;
using FluentValidation;

namespace FiapCloudGames.API.Modules.Users.Validators
{
    public class RequestCreateUserValidator : AbstractValidator<RequestCreateUserDTO>
    {
        public RequestCreateUserValidator()
        {
            #region Name rules
            RuleFor(r => r.Name)
                .NotNull().WithMessage(AppMessages.GetRequiredFieldMessage("name"));

            RuleFor(r => r.Name)
                .NotEmpty().WithMessage(AppMessages.GetNotEmptyFieldMessage("name"))
                .When(r => r.Name != null);
            #endregion

            #region Email rules
            RuleFor(r => r.Email)
                .NotNull().WithMessage(AppMessages.GetRequiredFieldMessage("email"));

            RuleFor(r => r.Email)
                .NotEmpty().WithMessage(AppMessages.GetNotEmptyFieldMessage("email"))
                .When(r => r.Email != null);

            RuleFor(r => r.Email)
                .EmailAddress().WithMessage(AppMessages.InvalidEmailFormatMessage)
                .When(r => !string.IsNullOrEmpty(r.Email));
            #endregion

            #region Password rules
            RuleFor(r => r.Password)
                .NotNull().WithMessage(AppMessages.GetRequiredFieldMessage("password"));

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage(AppMessages.GetNotEmptyFieldMessage("password"))
                .When(r => r.Password != null);

            RuleFor(r => r.Password)
                .Length(8, 30).WithMessage(AppMessages.InvalidSizePasswordMessage)
                .When(r => !string.IsNullOrEmpty(r.Password));

            RuleFor(r => r.Password)
                .Must(PasswordValidator.HasValidFormat).WithMessage(AppMessages.InvalidPasswordFormatMessage)
                .When(r => !string.IsNullOrEmpty(r.Password) && r.Password.Length >= 8 && r.Password.Length <= 30);
            #endregion

            #region Role rules
            RuleFor(r => r.RoleId)
                .NotEmpty().WithMessage(AppMessages.GetNotEmptyFieldMessage("roleId"))
                .When(r => r.RoleId.ToString() != null);
            #endregion
        }
    }
}
