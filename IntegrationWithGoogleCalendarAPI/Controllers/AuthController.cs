using Google.Apis.Auth;
using IntegrationWithGoogleCalendarAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IntegrationWithGoogleCalendarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static List<User> UserList = new List<User>();
        private IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("LoginWithGoogle")]
        public async Task<IActionResult> LoginWithGoogle([FromBody] string credential)
        {
            Console.WriteLine(_configuration["google"]);
            Console.WriteLine(_configuration["google:client_id"]);
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["google:client_id"] },
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(credential, settings);

            var user = UserList.Where(x => x.UserName == payload.Email).FirstOrDefault();

            if(user != null)
            {
                return Ok(JWTGenerator(user));
            }

            return BadRequest();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var user = new User { UserName = model.UserName, BirthDay = model.BirthDay, Role = model.Role };
            using (HMACSHA256 hmac = new HMACSHA256())
            {
                user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
            }

            UserList.Add(user);

            return Ok(user);
        }

        private dynamic JWTGenerator(User user)
        {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("d53f4746334584121df7c6617e7b43cfd8e653155af70e0507277e3dc256de2b");

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new[] { new Claim("id", user.UserName), new Claim(ClaimTypes.Role, user.Role), new Claim(ClaimTypes.DateOfBirth, user.BirthDay) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var encriptedToken = tokenHandler.WriteToken(token);

                return new { token = encriptedToken, username = user.UserName };
        }
    }
}
