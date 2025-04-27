using FiapCloudGames.API.DTOs.Requests.GameDTO;
using FiapCloudGames.API.Utils;
using FluentValidation;

namespace FiapCloudGames.API.Validators.GameValidator
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
