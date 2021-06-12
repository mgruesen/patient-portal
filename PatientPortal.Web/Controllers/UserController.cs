using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatientPortal.Web.Clients.PatientPortalApi;
using PatientPortal.Web.Mappers;
using PatientPortal.Web.Models;
using PatientPortal.Web.Services;

namespace PatientPortal.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _userService.LoadUser(GetCurrentUserId());
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var model = await _userService.LoadUser(GetCurrentUserId());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserViewModel model)
        {
            await _userService.UpdateUser(model, GetCurrentUserId());
            return RedirectToAction("Index");
        }

        private Guid GetCurrentUserId() =>
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}
