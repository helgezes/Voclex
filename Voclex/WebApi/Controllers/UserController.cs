using Application.Exceptions;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DataTransferObjects;
using SharedLibrary.DataTransferObjects.Authentication;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    public sealed class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Register))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register(RegistrationRequest request)
        {
            try
            {
                await userService.Register(request);

                return Ok();
            }
            catch (UserExistsException)
            {
                return BadRequest(
                    "User with this name is already registered. Try another one or login using your credentials.");
            }
            catch (DbUpdateException)
            {
                return BadRequest("Registration failed. Check your model and try again.");
            }

        }
    }
}
