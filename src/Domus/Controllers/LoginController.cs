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
    public class LoginController : ControllerBase
    {
        private readonly NpgSqlContext _db;
        public LoginController(NpgSqlContext db)
        {
            _db = db;
        }
        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] UserCredentialsDto? userCredentials = null)
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
        public async Task<IActionResult> SingUp([FromBody] UserCredentialsDto? userCredentials = null)
        {
            if(userCredentials == null) return BadRequest();

            var userInDb = await _db.UserCredentials.FirstOrDefaultAsync(user => user.Username == userCredentials.Username);

            if (userInDb != null) return Conflict($"User with login {userCredentials.Username} in DB");

            var newUser = new UserCredentialsDto(userCredentials.Username, userCredentials.Password);
            _db.UserCredentials.Add(newUser);
            _db.SaveChanges();
            HttpContext.Session.SetInt32("userId", newUser.Id);
            
            return Ok($"new - {newUser.Id} {HttpContext.Session.Id}");
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
