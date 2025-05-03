using FiapCloudGames.API.Modules.Games.DTOs.Requests;
using FiapCloudGames.API.Shared.Utils;
using FluentValidation;

namespace FiapCloudGames.API.Modules.Games.Validators
{
    public class RequestUpdateGameValidator : AbstractValidator<RequestUpdateGameDTO>
    {
        public RequestUpdateGameValidator()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage(AppMessages.GetNotEmptyFieldMessage("name"))
                .When(r => r.Name != null);

            RuleFor(request => request.Price)
                .GreaterThan(0).WithMessage(AppMessages.PriceGreaterThanZeroMessage)
                .When(r => r.Price != null);
        }
    }
}
