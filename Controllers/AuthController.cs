using Microsoft.AspNetCore.Mvc;
using contactManagerAPI.Models.AuthModels;

namespace contactManagerAPI.Controllers
{
    [Route("/api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IAuthService aSvc)
        {
            _logger = logger;
            _authService = aSvc;
        }

        [HttpPost("login")]
        [Produces("application/json")]
        public async Task<ActionResult> Login([FromBody] LoginModel req)
        {
            try
            {
                var res = await _authService.UserLogin(req);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while attempting to sign in.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        [Produces("application/json")]
        public async Task<ActionResult> Register([FromBody] RegisterModel req)
        {
            try
            {
                var res = await _authService.UserRegister(req);
                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while attempting to register user.");
                return Problem(e.Message);
            }
        }
    }
}
