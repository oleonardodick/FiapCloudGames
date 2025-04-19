using FiapCloudGames.API.DTOs.Requests;
using FiapCloudGames.API.DTOs.Responses.User;
using FiapCloudGames.API.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace FiapCloudGames.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<RequestUserInputDTO> _validator;

        public UserController(IUserService userService, IValidator<RequestUserInputDTO> validator)
        {
            _userService = userService;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseUsersDTO), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAll([FromQuery]int? pageNumber)
        {
            var page = pageNumber ?? 1;
            return Ok(await _userService.GetAll(page));
        }
    }
}
