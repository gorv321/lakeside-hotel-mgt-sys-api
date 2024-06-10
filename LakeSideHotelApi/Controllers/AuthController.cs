using LakeSideHotelApi.Models.DTO;
using LakeSideHotelApi.Models.Entities;
using LakeSideHotelApi.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LakeSideHotelApi.Controllers
{
    public class AuthController : Controller
    {
        private IUserRepository _userRepository;

        public AuthController(IConfiguration configuration,IUserRepository _userRepository)
        {
            Configuration = configuration;
            this._userRepository = _userRepository;
        }

        public IConfiguration Configuration { get; }
      


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var serverSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:ServerSecret"]));
            var isValidUser = _userRepository.FindByEmailAndPassword(loginDto.Email, loginDto.Password);

            if (isValidUser != null)
            {
                // Assuming you will generate a JWT token or some form of authentication token
                var token = GenerateToken(serverSecret,isValidUser); // Implement this method to generate a JWT token
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized("Invalid email or password");
            }
        }

        private string GenerateToken(SecurityKey key, User user)
        {
            var now = DateTime.UtcNow;
            var issuer = Configuration["JWT:Issuer"];
            var audience = Configuration["JWT:Audience"];
            var identity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                });
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateJwtSecurityToken(issuer, audience, identity,
            now, now.Add(TimeSpan.FromHours(1)), now, signingCredentials);
            var encodedJwt = handler.WriteToken(token);
            return encodedJwt;
        }


        [HttpPost("register")]
        public ActionResult<User> AddUser([FromBody] RegisterUserDto registerUserDto)
        {
            return Ok(_userRepository.AddUser(registerUserDto));
        }

    }
}

