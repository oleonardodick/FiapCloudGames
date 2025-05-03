using FiapCloudGames.API.DTOs.Responses;
using FiapCloudGames.API.Exceptions;

namespace FiapCloudGames.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(FiapCloudGamesException ex)
            {
                _logger.LogInformation("Apresentou uma excessão tratada ao rodar o endpoint {0}", context.GetEndpoint());
                _logger.LogError(string.Join(" | ", ex.GetErrorMessages()));
                context.Response.StatusCode = (int)ex.GetStatusCode();
                await context.Response.WriteAsJsonAsync(new ResponseErrorMessagesDTO
                {
                    Errors = ex.GetErrorMessages(),
                    StatusCode = context.Response.StatusCode
                });
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Apresentou uma excessão não tratada ao rodar o endpoint {0}", context.GetEndpoint());
                _logger.LogError(ex.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new ResponseErrorMessagesDTO
                {
                    Errors = ["Erro desconhecido."],
                    StatusCode = context.Response.StatusCode
                });
            }
        }
    }
}
