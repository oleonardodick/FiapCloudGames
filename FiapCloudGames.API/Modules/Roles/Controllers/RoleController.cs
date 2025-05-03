using FiapCloudGames.API.DTOs.Responses;
using FiapCloudGames.API.Modules.Roles.DTOs.Responses;
using FiapCloudGames.API.Modules.Roles.Services.Interfaces;
using FiapCloudGames.API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FiapCloudGames.API.Modules.Roles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRolesDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status403Forbidden)]
        [SwaggerOperation(
            Summary = "Lista todas as roles do sistema",
            Description = "Retorna uma lista com as roles do sistema. Somente o administrador do sistema tem acesso a este recurso."
        )]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _roleService.GetAll());
        }

        [Authorize(Roles = AppRoles.Admin)]
        [HttpGet]
        [ProducesResponseType(typeof(ResponseRoleDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ResponseErrorMessagesDTO), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Retorna a role pelo seu ID",
            Description = "Retorna a role conforme o ID passado na rota. Somente o administrador do sistema tem acesso a este recurso."
        )]
        [Route("{roleId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid roleId)
        {
            return Ok(await _roleService.GetById(roleId));
        }
    }
}
