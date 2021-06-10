using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientPortal.Api.Models;
using PatientPortal.Api.Services;

namespace PatientPortal.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("authenticate")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Authenticate(UserModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            return user == null ? BadRequest(new { message = "Invalid username or password" })
                : Ok(user);
        }
    }
}
