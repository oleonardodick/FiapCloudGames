using FiapCloudGames.API.DTOs.Responses;
using FiapCloudGames.API.Exceptions;

namespace FiapCloudGames.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(FiapCloudGamesException ex)
            {
                context.Response.StatusCode = (int)ex.GetStatusCode();
                await context.Response.WriteAsJsonAsync(new ResponseErrorMessagesDTO
                {
                    Errors = ex.GetErrorMessages()
                });
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new ResponseErrorMessagesDTO
                {
                    Errors = ["Erro desconhecido."]
                });
            }
        }
    }
}
