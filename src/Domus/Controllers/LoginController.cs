using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Domus.Models;
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
        [HttpPost("singin")]
        public async Task<IActionResult> SingIn([FromBody] UserCredentials? userCredentials = null)
        {   if (userCredentials == null) return Unauthorized();

            var user = await _db.Users.FirstOrDefaultAsync(user => user.Username == userCredentials.Username
                                                && user.Password == userCredentials.Password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("userId", user.Id);
                return Ok($"old - {user.Id} {HttpContext.Session.Id}");
            }
            else
            {
                var newUser = new UserCredentials(userCredentials.Username, userCredentials.Password);
                _db.Users.Add(newUser);
                _db.SaveChanges();
                HttpContext.Session.SetInt32("userId", newUser.Id);
                return Ok($"new - {newUser.Id} {HttpContext.Session.Id}");
            }
            return Unauthorized();
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SingUp([FromBody] UserCredentials? userCredentials = null)
        {
            
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
