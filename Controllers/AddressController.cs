using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using contactManagerAPI.Services.AddressServices;
using contactManagerAPI.Models.AddressModels;

namespace contactManagerAPI.Controllers
{
    [Authorize]
    [Route("/api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private readonly IAddressService _addressService;

        public AddressController(ILogger<AddressController> logger, IAddressService addressService)
        {
            _logger = logger;
            _addressService = addressService;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAddress([FromBody] AddressModel request)
        {
            try
            {
                var response = await _addressService.GetEntityAddress(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while trying to fetch entity address");
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAddresses([FromBody] AddressModel request)
        {
            try
            {
                var response = await _addressService.GetEntityAddresses(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while trying to fetch entity addresses");
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateAddress([FromBody] AddressModel request)
        {
            try
            {
                var response = await _addressService.CreateAddress(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while trying to create new address");
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateAddress([FromBody] AddressModel request)
        {
            try
            {
                var response = await _addressService.UpdateAddress(request);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while trying to update address");
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{AddressID}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeactivateAddress([FromRoute] int AddressID)
        {
            try
            {
                var response = await _addressService.DeactivateAddress(AddressID);
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occured while trying to deactivate address");
                return BadRequest(e.Message);
            }
        }
    }
}
