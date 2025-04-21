using FiapCloudGames.API.DTOs.Requests.GameDTO;
using FiapCloudGames.API.DTOs.Requests.UserDTO;
using FiapCloudGames.API.Exceptions;
using FiapCloudGames.API.Services.Implementations;
using FiapCloudGames.API.Services.Interfaces;
using FiapCloudGames.API.Utils;
using FiapCloudGames.API.Validators.GameValidator;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> GetAll([FromQuery] int? pageNumber, [FromQuery] int? perPage)
        {
            _logger.LogInformation("Rodando o método GetAll do jogo buscando da página {0}", pageNumber ?? 1);
            var page = pageNumber ?? 1;
            var qtdPerPage = perPage ?? 10;
            return Ok(await _gameService.GetAll(page, qtdPerPage));
        }

        [Authorize]
        [HttpGet]
        [Route("{gameId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid gameId)
        {
            _logger.LogInformation("Rodando o método GetById do jogo buscando o ID {0}", gameId);
            return Ok(await _gameService.GetById(gameId));
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RequestCreateGameDTO request)
        {
            _logger.LogInformation("Rodando o método Create do jogo com os dados {@Request}", request);
            var validationResult = await _validatorCreate.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorException(errors);
            }
            return Ok(await _gameService.Create(request));
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpPut]
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
            return Ok();
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpDelete]
        [Route("{gameId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid gameId)
        {
            _logger.LogInformation("Rodando o método Delete do jogo buscando o ID {0}", gameId);
            await _gameService.Delete(gameId);
            return Ok();
        }
    }
}
