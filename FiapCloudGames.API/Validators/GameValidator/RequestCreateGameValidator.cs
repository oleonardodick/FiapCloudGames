using FiapCloudGames.API.DTOs.Requests.GameDTO;
using FiapCloudGames.API.Utils;
using FluentValidation;

namespace FiapCloudGames.API.Validators.GameValidator
{
    public class RequestCreateGameValidator : AbstractValidator<RequestCreateGameDTO>
    {
        public RequestCreateGameValidator()
        {
            #region Name Field Rules
            RuleFor(r => r.Name)
                .NotNull().WithMessage(AppMessages.GetRequiredFieldMessage("name"));

            RuleFor(r => r.Name)
                .NotEmpty().WithMessage(AppMessages.GetNotEmptyFieldMessage("name"))
                .When(r => r.Name != null);
            #endregion

            #region Price Field Rules
            RuleFor(request => request.Price)
                .GreaterThan(0).WithMessage(AppMessages.PriceGreaterThanZeroMessage);
            #endregion
        }
    }
}
