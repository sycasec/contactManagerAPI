using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using contactManagerAPI.Services.UserServices;
using contactManagerAPI.Models.UserModels;
using contactManagerAPI.Models.AuthModels;

namespace contactManagerAPI.Controllers
{
    [Authorize]
    [Route("/api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        private UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Get User details.
        /// </summary>
        /// <returns>The user details.</returns>
        /// <remarks>
        /// Sample Request:
        ///
        ///     GET /api/user
        ///
        /// </remarks>
        /// <response code="200">Returns the user's profile.</response>
        /// <response code="400">Bad Request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserDetails()
        {
            try
            {
                var res = await _userService.GetUserDetails();
                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while trying to fetch user details.");
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateUserDetails([FromBody] UpdateUserModel request)
        {
            try
            {
                var res = await _userService.UpdateUser(request);
                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while trying to update user details.");
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeactivateUser([FromBody] LoginModel request)
        {
            try
            {
                var res = await _userService.DeactivateUser(request);
                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while trying to deactivate user account");
                return BadRequest(e.Message);
            }
        }
    }
}
