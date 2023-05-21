using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    [HandleServiceException]
    [ValidateModel]
    public class AuthController  : Controller
    {
        private readonly Services.IAuthorizationService _service;

        public AuthController(Services.IAuthorizationService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserVm userVm)
        {
            if (await _service.IsExist(userVm.Login))
            {
                return BadRequest("User with this login already exists.");
            }

            await _service.AddUser(userVm.Login, userVm.Password);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login(UserVm userVm)
        {
            var token = await _service.CreateJwtToken(userVm.Login, userVm.Password);
            return token;
        }
    }
}
