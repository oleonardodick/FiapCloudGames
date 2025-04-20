using FiapCloudGames.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _roleService.GetAll());
        }

        [HttpGet]
        [Route("{roleId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid roleId)
        {
            return Ok(await _roleService.GetById(roleId));
        }
    }
}
