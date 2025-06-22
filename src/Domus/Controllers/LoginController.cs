using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Domus.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet("login")]
        public IActionResult Login([FromQuery] string? username = null)
        {
            if (username == "tom")
            {
                HttpContext.Session.SetString("userId", "42");
                return Ok($"42 {HttpContext.Session.Id}");
            }
            return Unauthorized();
        }

        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var userId = HttpContext.Session.GetString("userId");

            if (userId == null)
            {
                return Unauthorized();
            }

            return Ok($"{userId} {HttpContext.Session.Id}");
        }
    }
}
