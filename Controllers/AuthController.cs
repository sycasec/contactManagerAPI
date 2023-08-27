using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using contactManagerAPI.Services.AuthServices;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace contactManagerAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;

        public AuthController(
            ILogger<AuthController> logger,
            IAuthService aSvc,
            IConfiguration config
        )
        {
            _logger = logger;
            _authService = aSvc;
            _config = config;
        }

        private string GenerateToken(UserAuthDTO user)
        {
            List<Claim> claims = new List<Claim>
            {
                user.EmailAddress is not null
                    ? new Claim(ClaimTypes.Email, user.EmailAddress)
                    : new Claim(ClaimTypes.Name, user.Username!)
            };

            SymmetricSecurityKey key =
                new(Encoding.UTF8.GetBytes(_config.GetSection("JwtSettings:Key").Value!));

            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token =
                new(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials
                );

            string JWTtoken = new JwtSecurityTokenHandler().WriteToken(token);
            return JWTtoken;
        }

        [HttpGet("All")]
        public async Task<ActionResult<SvcResponse<IEnumerable<UserDTO>>>> GetAllUsers()
        {
            var res = await _authService.GetAllUsers();
            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<ActionResult<SvcResponse<string>>> Login(UserAuthDTO req)
        {
            var res = await _authService.AuthenticateUser(req);
            if (res.Success)
            {
                string token = GenerateToken(req);
                res.Data = token;
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpPost("register")]
        public async Task<ActionResult<SvcResponse<string>>> Register(UserDTO req)
        {
            var res = await _authService.CreateUser(req);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpPut("update")]
        public async Task<ActionResult<SvcResponse<string>>> Update(UserDTO req)
        {
            var res = await _authService.UpdateUser(req);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<SvcResponse<string>>> Deactivate(UserAuthDTO req)
        {
            var res = await _authService.DeactivateUser(req);
            if (res.Success)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }
    }
}
