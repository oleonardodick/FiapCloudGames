using FiapCloudGames.API.DTOs.Requests;
using FiapCloudGames.API.DTOs.Responses.User;
using FiapCloudGames.API.Exceptions;
using FiapCloudGames.API.Services.Interfaces;
using FiapCloudGames.API.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace FiapCloudGames.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IValidator<RequestCreateUserDTO> _validatorCreate;
        private readonly IValidator<RequestUpdateUserDTO> _validatorUpdate;

        public UserController(IUserService userService, IValidator<RequestCreateUserDTO> validatorCreate, IValidator<RequestUpdateUserDTO> validatorUpdate, ILogger<UserController> logger)
        {
            _userService = userService;
            _validatorCreate = validatorCreate;
            _validatorUpdate = validatorUpdate;
            _logger = logger;
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseUsersDTO), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAll([FromQuery]int? pageNumber)
        {
            _logger.LogInformation("Rodando o método GetAll do usuário buscando da página {0}", pageNumber ?? 1);
            var page = pageNumber ?? 1;
            return Ok(await _userService.GetAll(page));
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpGet]
        [Route("{userId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid userId)
        {
            _logger.LogInformation("Rodando o método GetById do usuário buscando o ID {0}", userId);
            return Ok(await _userService.GetById(userId));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RequestCreateUserDTO request)
        {
            _logger.LogInformation("Rodando o método Create do usuário com os dados {@Request}", request);
            var validationResult = await _validatorCreate.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorException(errors);
            }
            return Ok(await _userService.Create(request));
        }

        [Authorize]
        [HttpPut]
        [Route("{userId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid userId, [FromBody] RequestUpdateUserDTO request)
        {
            _logger.LogInformation("Rodando o método Update do usuário buscando o ID {0} e com as informações {@Request}", userId, request.ToString());
            var validationResult = await _validatorUpdate.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorException(errors);
            }
            await _userService.Update(userId, request);
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route("{userId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid userId)
        {
            _logger.LogInformation("Rodando o método Delete do usuário buscando o ID {0}", userId);
            await _userService.Delete(userId);
            return Ok();
        }
    }
}
