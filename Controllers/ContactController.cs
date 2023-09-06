using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using contactManagerAPI.Services.ContactServices;
using contactManagerAPI.Services.UserServices;
using contactManagerAPI.Models.ContactModels;

namespace contactManagerAPI.Controllers
{
    [Authorize]
    [Route("/api/contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContactService _contactService;
        private readonly IUserService _userService;

        public ContactController(
            ILogger<ContactController> logger,
            IContactService contactService,
            IUserService userService
        )
        {
            _logger = logger;
            _contactService = contactService;
            _userService = userService;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserContacts()
        {
            try
            {
                var user = await _userService.GetUserByToken();
                var res = await _contactService.GetUserContacts(user.ID);
                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while attempting get user contacts");
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{ContactID}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserContact([FromRoute] int ContactID)
        {
            try
            {
                var user = await _userService.GetUserByToken();
                var res = await _contactService.GetUserContact(ContactID);
                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError(
                    e,
                    $"An error occured while attempting to get user contact ID = {ContactID}"
                );
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateUserContact([FromBody] UpsertContactModel req)
        {
            try
            {
                var user = await _userService.GetUserByToken();
                var res = await _contactService.CreateContact(user.ID, req);
                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occurred while attempting to create new contact");
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{ContactID}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateUserContact(
            [FromRoute] int ContactID,
            [FromBody] UpsertContactModel req
        )
        {
            try
            {
                var user = await _userService.GetUserByToken();
                var res = await _contactService.UpdateContact(ContactID, req);
                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while attempting to update contact");
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{ContactID}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteUserContact([FromRoute] int ContactID)
        {
            try
            {
                var user = await _userService.GetUserByToken();
                var res = await _contactService.DeactivateContact(ContactID);
                return Ok(res);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while attempting to delete contact");
                return BadRequest(e.Message);
            }
        }
    }
}
