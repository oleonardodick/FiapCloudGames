using FiapCloudGames.API.DTOs.Requests;
using FiapCloudGames.API.DTOs.Responses.User;
using FiapCloudGames.API.Exceptions;
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
        private readonly IValidator<RequestCreateUserDTO> _validatorCreate;
        private readonly IValidator<RequestUpdateUserDTO> _validatorUpdate;

        public UserController(IUserService userService, IValidator<RequestCreateUserDTO> validatorCreate, IValidator<RequestUpdateUserDTO> validatorUpdate)
        {
            _userService = userService;
            _validatorCreate = validatorCreate;
            _validatorUpdate = validatorUpdate;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseUsersDTO), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetAll([FromQuery]int? pageNumber)
        {
            var page = pageNumber ?? 1;
            return Ok(await _userService.GetAll(page));
        }

        [HttpGet]
        [Route("{userId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid userId)
        {
            return Ok(await _userService.GetById(userId));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RequestCreateUserDTO request)
        {
            var validationResult = await _validatorCreate.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorException(errors);
            }
            return Ok(await _userService.Create(request));
        }

        [HttpPut]
        [Route("{userId:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid userId, [FromBody] RequestUpdateUserDTO request)
        {
            var validationResult = await _validatorUpdate.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationErrorException(errors);
            }
            return Ok(_userService.Update(userId, request));
        }
    }
}
