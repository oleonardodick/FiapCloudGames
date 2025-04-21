using FiapCloudGames.API.DTOs.Requests.GameDTO;
using FiapCloudGames.API.Utils;
using FluentValidation;

namespace FiapCloudGames.API.Validators.GameValidator
{
    public class RequestCreateGameValidator : AbstractValidator<RequestCreateGameDTO>
    {
        public RequestCreateGameValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty().WithMessage(AppMessages.GetRequiredFieldMessage("name"));

            RuleFor(request => request.Price)
                .NotEmpty().WithMessage(AppMessages.GetRequiredFieldMessage("price"));

            RuleFor(request => request.Price)
                .GreaterThan(0).WithMessage(AppMessages.PriceGreaterThanZeroMessage);
        }
    }
}
