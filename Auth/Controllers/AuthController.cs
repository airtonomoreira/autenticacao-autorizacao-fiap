using Auth.Domain.User.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Auth.Domain.User.Token.Models;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    [ApiController]
    [Route("v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AccessToken>> Authenticate([FromBody] Auth.Domain.User.Models.User user)
        {
            var accessToken = await _userService.AuthenticateUser(user);

            if (accessToken is null)
                return NotFound(new { message = "User or password invalid" });

            return accessToken;
        }

        [HttpGet]
        [Authorize]
        [Route("authenticated")]
        public string Authenticated() => $"Authenticated - {User?.Identity?.Name}";

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("admin")]
        public string AdminOnly() => $"Authenticated as Admin - {User?.Identity?.Name}";
    }
}