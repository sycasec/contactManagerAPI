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
    //[Authorize]
    [Route("/api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase { }
}
