using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Domus.Models;
using Domus.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Domus.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class LoginController(NpgSqlContext db, UserService userService, IPasswordHasher<UserCredentialsDto> passwordHasher) : ControllerBase
    {
        private readonly NpgSqlContext _db = db;
        private readonly UserService _userService = userService;
        private readonly IPasswordHasher<UserCredentialsDto> _passwordHasher = passwordHasher;

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] FrontendModels.UserCredentialsDto? userCredentials = null)
        {   if (userCredentials == null) return BadRequest();

            var userId = _userService.ValidateUser(userCredentials);
            HttpContext.Session.SetInt32("userId", userId);
            return Ok($"old - {userId} {HttpContext.Session.Id}");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            var userId = HttpContext.Session.GetInt32("userId");

            HttpContext.Session.Clear();

            return Ok();
        }

        [HttpPost("singup")]
        public async Task<IActionResult> SingUp([FromBody] FrontendModels.CreateUserDto? userCredentials = null)
        {
            if(userCredentials == null) return BadRequest();

            var userId = _userService.CreateUser(userCredentials);
            
            return Ok($"new - {userId} {HttpContext.Session.Id}");
        }

        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var userId = HttpContext.Session.GetInt32("userId");

            if (userId == null)
            {
                return Unauthorized();
            }

            return Ok($"{userId} {HttpContext.Session.Id}");
        }
    }
}
