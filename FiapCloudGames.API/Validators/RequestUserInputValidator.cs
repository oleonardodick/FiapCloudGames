using FiapCloudGames.API.DTOs.Requests;
using FiapCloudGames.API.Messages;
using FluentValidation;
namespace FiapCloudGames.API.Validators
{
    public class RequestUserInputValidator : AbstractValidator<RequestUserInputDTO>
    {
        public RequestUserInputValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty().WithMessage(AppMessages.GetRequiredFieldMessage("name"));
            RuleFor(request => request.Email)
                .NotEmpty().WithMessage(AppMessages.GetRequiredFieldMessage("email"))
                .EmailAddress().WithMessage(AppMessages.InvalidEmailFormatMessage);
            RuleFor(request => request.Password)
                .NotEmpty().WithMessage(AppMessages.InvalidSizePasswordMessage)
                .Length(8, 30).WithMessage(AppMessages.InvalidSizePasswordMessage);

            When(request => !string.IsNullOrEmpty(request.Password) &&
                request.Password.Length >= 8 &&
                request.Password.Length <= 30, () =>
                {
                    RuleFor(request => request.Password)
                    .Must(PasswordHasRequiredCharacteres)
                    .WithMessage(AppMessages.InvalidPasswordFormatMessage);
                });

            RuleFor(request => request.RoleId)
                .NotEmpty().WithMessage(AppMessages.GetRequiredFieldMessage("roleId"));
        }

        private bool PasswordHasRequiredCharacteres(string password)
        {
            return HaveUpperCase(password) && HaveLowerCase(password) && HaveNumber(password) && HaveSpecialCharactere(password);
        }

        private bool HaveUpperCase(string password) => password.Any(char.IsUpper);
        private bool HaveLowerCase(string password) => password.Any(char.IsLower);
        private bool HaveNumber(string password) => password.Any(char.IsDigit);
        private bool HaveSpecialCharactere(string password)
        {
            var specialCharacteres = "!@#$%&*()-_<>?:;^~][{}";
            return password.Any(c => specialCharacteres.Contains(c));
        }

    }
}
