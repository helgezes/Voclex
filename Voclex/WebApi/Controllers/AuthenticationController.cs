using Microsoft.AspNetCore.Mvc;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using SharedLibrary.DataTransferObjects.Authentication;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IAuthTokenService authService;

        public AuthenticationController(IUserService userService, IAuthTokenService authService)
        {
            this.userService = userService;
            this.authService = authService;
        }
        
        [HttpPost(nameof(Login))]
        public async Task<ActionResult> Login(LoginModel model)
        {
            var user = await userService.GetUserByNameAndVerifyPassword(model.Name, model.Password);
            if (user == null) return BadRequest("Bad credentials");

            return Ok(new LoginResult(authService.CreateToken(user)));
        }
        
    }
}
