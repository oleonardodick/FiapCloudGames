using FiapCloudGames.API.DTOs.Requests;
using FiapCloudGames.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(RequestLoginDTO request)
        {
            return Ok(await _authService.Authenticate(request));
        }
    }
}
