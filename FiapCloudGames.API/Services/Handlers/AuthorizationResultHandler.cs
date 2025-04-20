using FiapCloudGames.API.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace FiapCloudGames.API.Services.Handlers
{
    public class AuthorizationResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new AuthorizationMiddlewareResultHandler();
        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Forbidden) throw new ForbiddenException();

            if (authorizeResult.Challenged) throw new InvalidTokenException();

            await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}
