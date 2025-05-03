using FiapCloudGames.API.DTOs.Responses;
using FiapCloudGames.API.Exceptions;
using FiapCloudGames.API.Modules.Games.DTOs.Requests;
using FiapCloudGames.API.Modules.Games.DTOs.Responses;
using FiapCloudGames.API.Modules.Games.Services.Interfaces;
using FiapCloudGames.API.Utils;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FiapCloudGames.API.Modules.Games.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ILogger<GameController> _logger;
        private readonly IValidator<RequestCreateGameDTO> _validatorCreate;
        private readonly IValidator<RequestUpdateGameDTO> _validatorUpdate;

        public GameController(IGameService gameService, ILogger<GameController> logger,
            IValidator<RequestCreateGameDTO> validatorCreate, IValidator<RequestUpdateGameDTO> validatorUpdate)
        {
            _gameService = gameService;
            _logger = logger;
            _validatorCreate = validatorCreate;
            _validatorUpdate = validatorUpdate;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseGamesDTO), StatusCodes.Status200OK)]
        [SwaggerOperation(
            Summary = "Lista os jogos com paginação",
            Description = "Retorna uma lista de jogos paginados. Não é necessário login para acessar este recurso."
        )]
        public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? perPage)
        {
            _logger.LogInformation("Rodando o método GetAll do jogo buscando da página {0}", pageNumber ?? 1);
            var page = pageNumber ?? 1;
            var qtdPerPage = perPage ?? 10;
            return Ok(await _gameService.GetAll(page, qtdPerPage));
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseGameDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Retorna o jogo pelo seu ID",
            Description = "Retorna o jogo conforme o ID passado pela rota. Somente usuário logados terão acesso a este recurso."
        )]
        [Route("{gameId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid gameId)
        {
            _logger.LogInformation("Rodando o método GetById do jogo buscando o ID {0}", gameId);
            return Ok(await _gameService.GetById(gameId));
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpPost]
        [ProducesResponseType(typeof(ResponseGameDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status403Forbidden)]
        [SwaggerOperation(
            Summary = "Cria um jogo no sistema",
            Description = "Cria um jogo conforme os dados passados. Somente o administrador do sistema tem acesso a este recurso."
        )]
        public async Task<IActionResult> Create([FromBody] RequestCreateGameDTO request)
        {
            _logger.LogInformation("Rodando o método Create do jogo com os dados {@Request}", request);
            var validationResult = await _validatorCreate.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorException(errors);
            }
            var response = await _gameService.Create(request);

            return Created($"/api/game/{response.Id}", response);
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Atualiza o jogo pelo seu ID",
            Description = "Atualiza um jogo conforme os dados passados, buscando pelo ID passado pela rota. Somente o administrador do sistema tem acesso a este recurso."
        )]
        [Route("{gameId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid gameId, [FromBody] RequestUpdateGameDTO request)
        {
            _logger.LogInformation("Rodando o método Update do jogo buscando o ID {0} e com as informações {@Request}", gameId, request);
            var validationResult = await _validatorUpdate.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorException(errors);
            }
            await _gameService.Update(gameId, request);
            return NoContent();
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Exclui o jogo pelo seu ID",
            Description = "Exclui um jogo conforme o ID passado pela rota. Somente o administrador do sistema tem acesso a este recurso."
        )]
        [Route("{gameId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid gameId)
        {
            _logger.LogInformation("Rodando o método Delete do jogo buscando o ID {0}", gameId);
            await _gameService.Delete(gameId);
            return NoContent();
        }
    }
}
