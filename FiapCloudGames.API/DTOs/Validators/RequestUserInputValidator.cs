using FiapCloudGames.API.DTOs.Requests;
using FiapCloudGames.API.Messages;
using FluentValidation;
namespace FiapCloudGames.API.DTOs.Validators
{
    public class RequestUserInputValidator : AbstractValidator<RequestUserInputDTO>
    {
        public RequestUserInputValidator()
        {
            RuleFor(request => request.Name).NotEmpty().WithMessage(AppMessages.GetRequiredFieldMessage("nome"));
            RuleFor(request => request.Email).EmailAddress().WithMessage(AppMessages.InvalidEmailFormatMessage);
            RuleFor(request => request.Password)
                .MinimumLength(8).WithMessage(AppMessages.InvalidPasswordFormatMessage)
                .Must(HaveUpperCase).WithMessage(AppMessages.InvalidPasswordFormatMessage)
                .Must(HaveLowerCase).WithMessage(AppMessages.InvalidPasswordFormatMessage)
                .Must(HaveNumber).WithMessage(AppMessages.InvalidPasswordFormatMessage)
                .Must(HaveSpecialCharactere).WithMessage(AppMessages.InvalidPasswordFormatMessage);
            RuleFor(request => request.RoleId).NotEmpty().WithMessage(AppMessages.GetRequiredFieldMessage("roleId"));
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
