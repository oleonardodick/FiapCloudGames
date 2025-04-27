using FiapCloudGames.API.DTOs.Requests;
using FiapCloudGames.API.DTOs.Responses;
using FiapCloudGames.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FiapCloudGames.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseAuthDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status401Unauthorized)]
        [SwaggerOperation(
            Summary = "Login do sistema",
            Description = "Realiza o login no sistema, retornando o token que deve ser enviados nas requisições."
        )]
        public async Task<IActionResult> Login(RequestLoginDTO request)
        {
            return Ok(await _authService.Authenticate(request));
        }
    }
}
