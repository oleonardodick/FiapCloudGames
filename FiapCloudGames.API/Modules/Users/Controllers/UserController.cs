using FiapCloudGames.API.Exceptions;
using FiapCloudGames.API.Modules.Users.DTOs.Requests;
using FiapCloudGames.API.Modules.Users.DTOs.Responses;
using FiapCloudGames.API.Modules.Users.Services.Interfaces;
using FiapCloudGames.API.Shared.DTOs.Responses;
using FiapCloudGames.API.Shared.Helpers;
using FiapCloudGames.API.Shared.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace FiapCloudGames.API.Modules.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
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
        [ProducesResponseType(typeof(ResponseUsersDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status403Forbidden)]
        [SwaggerOperation(
            Summary = "Lista usuários com paginação",
            Description = "Retorna uma lista de usuários paginados. Somente o administrador do sistema tem acesso a este recurso."
        )]
        public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? perPage)
        {
            _logger.LogInformation("Rodando o método GetAll do usuário buscando da página {0}", pageNumber ?? 1);
            var page = pageNumber ?? 1;
            var qtdPerPage = perPage ?? 10;
            return Ok(await _userService.GetAll(page, qtdPerPage));
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpGet]
        [Route("{userId:guid}")]
        [ProducesResponseType(typeof(ResponseUserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status403Forbidden)]
        [SwaggerOperation(
            Summary = "Retorna o usuário pelo seu ID",
            Description = "Retorna um usuário conforme o ID passado na rota. Somente o administrador do sistema tem acesso a este recurso."
        )]
        public async Task<IActionResult> GetById([FromRoute] Guid userId)
        {
            _logger.LogInformation("Rodando o método GetById do usuário buscando o ID {0}", userId);
            return Ok(await _userService.GetById(userId));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseUserDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Cria um novo usuário",
            Description = "Cria um novo usuário na plataforma. Todos possuem acesso a este recurso."
        )]
        public async Task<IActionResult> Create([FromBody] RequestCreateUserDTO request)
        {
            _logger.LogInformation("Rodando o método Create do usuário com os dados {@Request}", request);
            
            await ValidationHelper.ValidateAsync(_validatorCreate, request);
            
            var response = await _userService.Create(request);

            return Created($"/api/user/{response.Id}", response);
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpPut]
        [Route("{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Atualiza o usuário pelo ID",
            Description = "Atualiza os dados do usuário conforme os dados passados, buscando pelo ID passado pela rota. Somente o administrador do sistema tem acesso a este recurso."
        )]
        public async Task<IActionResult> Update([FromRoute] Guid userId, [FromBody] RequestUpdateUserDTO request)
        {
            _logger.LogInformation("Rodando o método Update do usuário buscando o ID {0} e com as informações {@Request}", userId, request);

            await ValidationHelper.ValidateAsync(_validatorUpdate, request);

            await _userService.Update(userId, request);
            return NoContent();
        }

        [Authorize]
        [HttpPut("update-self")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Atualiza os próprios dados",
            Description = "Atualiza os próprios dados, com base no ID do usuário logado. Não é necessário login para acessar este recurso."
        )]
        public async Task<IActionResult> UpdateSelf([FromBody] RequestUpdateUserDTO request)
        {
            Guid userId = Guid.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)!.Value);
            _logger.LogInformation("Rodando o método UpdateSelf do usuário buscando o ID {0} e com as informações {@Request}", userId, request);
            var validationResult = await _validatorUpdate.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorException(errors);
            }
            await _userService.Update(userId, request);
            return NoContent();
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpDelete]
        [Route("{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Exclui o usuário pelo seu ID",
            Description = "Exclui um usuário conforme o ID passado pela rota. Somente o administrador do sistema tem acesso a este recurso."
        )]
        public async Task<IActionResult> Delete([FromRoute] Guid userId)
        {
            _logger.LogInformation("Rodando o método Delete do usuário buscando o ID {0}", userId);
            await _userService.Delete(userId);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("delete-self")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Exclui o próprio usuário",
            Description = "Exclui o usuário com base no ID do usuário logado. Todos os usuários logados no sistema possuem acesso a este recurso."
        )]
        public async Task<IActionResult> DeleteSelf()
        {
            Guid userId = Guid.Parse(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier)!.Value);
            _logger.LogInformation("Rodando o método DeleteSelf do usuário buscando o ID {0}", userId);
            await _userService.Delete(userId);
            return NoContent();
        }
    }
}
