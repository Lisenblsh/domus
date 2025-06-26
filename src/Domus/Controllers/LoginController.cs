using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Domus.Models;
using Domus.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Domus.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class LoginController(NpgSqlContext db, UserService userService) : ControllerBase
    {
        private readonly NpgSqlContext _db = db;
        private readonly UserService _userService = userService;

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] FrontendModels.UserCredentialsDto? userCredentials = null)
        {   if (userCredentials == null) return BadRequest();

            var user = await _db.UserCredentials.FirstOrDefaultAsync(user => user.Username == userCredentials.Username
                                                && user.Password == userCredentials.Password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("userId", user.Id);
                return Ok($"old - {user.Id} {HttpContext.Session.Id}");
            }
            
            return Unauthorized();
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

            if (_userService.CheckUser(userCredentials.Username))
                return Conflict($"User with login {userCredentials.Username} in DB");

            var userId = _userService.Add(new UserDto
            {
                Name = userCredentials.Name,
                Credentials = new UserCredentialsDto(userCredentials.Username, userCredentials.Password)
            });
            
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
