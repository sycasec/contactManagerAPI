using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using contactManagerAPI.Services.NumberServices;
using contactManagerAPI.Models.NumberModels;

namespace contactManagerAPI.Controllers
{
    [Authorize]
    [Route("/api/number")]
    [ApiController]
    public class NumberController : ControllerBase
    {
        private readonly ILogger<NumberController> _logger;
        private readonly INumberService _numberService;

        public NumberController(ILogger<NumberController> logger, INumberService numberService)
        {
            _logger = logger;
            _numberService = numberService;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetNumber([FromBody] NumberModel request)
        {
            try
            {
                var response = await _numberService.GetEntityNumber(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while trying to fetch entity number");
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetNumbers([FromBody] NumberModel request)
        {
            try
            {
                var response = await _numberService.GetEntityNumbers(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while trying to fetch entity numberes");
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateNumber([FromBody] NumberModel request)
        {
            try
            {
                var response = await _numberService.CreateNumber(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while trying to create new number");
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateNumber([FromBody] NumberModel request)
        {
            try
            {
                var response = await _numberService.UpdateNumber(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while trying to update number");
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{NumberID}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeactivateNumber([FromRoute] int NumberID)
        {
            try
            {
                var response = await _numberService.DeactivateNumber(NumberID);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while trying to deactivate number");
                return BadRequest(e.Message);
            }
        }
    }
}
