using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using contactManagerAPI.Services.AuthServices;

namespace contactManagerAPI.Controllers {
    [Route("/api/[controller]")]
    [ApiController]
    public class  AuthController : ControllerBase {

        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;
        private readonly IConfiguration _config;

        public AuthController(ILogger<AuthController> logger, IAuthService aSvc, IConfiguration config){
            _logger = logger;
            _authService = aSvc;
            _config = config;
        }

        [HttpGet("All")]
        public async Task<ActionResult<SvcResponse<IEnumerable<UserDTO>>>> GetAllUsers() {
            var res = await _authService.GetAllUsers();
            return res;
        }
    }
}