using FiapCloudGames.API.DTOs.Requests.UserDTO;
using FiapCloudGames.API.Utils;
using FluentValidation;

namespace FiapCloudGames.API.Validators.UserValidator
{
    public class RequestUpdateUserValidator : AbstractValidator<RequestUpdateUserDTO>
    {
        public RequestUpdateUserValidator()
        {
            #region Name Rules
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage(AppMessages.GetNotEmptyFieldMessage("name"))
                .When(r => r.Name != null);
            #endregion

            #region Email Rules
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage(AppMessages.GetNotEmptyFieldMessage("email"))
                .When(r => r.Email != null);

            RuleFor(r => r.Email)
                .EmailAddress().WithMessage(AppMessages.InvalidEmailFormatMessage)
                .When(r => !string.IsNullOrWhiteSpace(r.Email));
            #endregion

            #region Password Rules
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage(AppMessages.GetNotEmptyFieldMessage("password"))
                .When(r => r.Password != null);

            RuleFor(r => r.Password)
                .Length(8, 30).WithMessage(AppMessages.InvalidSizePasswordMessage)
                .When(r => !string.IsNullOrEmpty(r.Password));

            RuleFor(r => r.Password!)
                .Must(PasswordValidator.HasValidFormat).WithMessage(AppMessages.InvalidPasswordFormatMessage)
                .When(r => !string.IsNullOrWhiteSpace(r.Password));
            #endregion

            #region RoleId Rules
            RuleFor(r => r.RoleId)
                .Must(roleId => roleId != Guid.Empty).WithMessage(AppMessages.GetNotEmptyFieldMessage("roleId"));
            #endregion
        }
    }
}
