using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatientPortal.Api.Models;
using PatientPortal.Api.Services;

namespace PatientPortal.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("authenticate")]
        public IActionResult Authenticate(LoginModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            return user == null ? BadRequest(new { message = "Invalid username or password" })
                : Ok(user);
        }

        [HttpPost]
        [Route("list")]
        public ResponseModel ListUsers(IdListModel model)
        {
            IEnumerable<object> returnData;
            try
            {
                returnData = _userService.GetByIds(model.Ids);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new() { Error = e.Message };
            }
            return new() { Data = returnData.ToArray() };
        }

        [HttpPost]
        public async Task<ResponseModel> UpdateUser(UserModel model)
        {
            UserModel data;
            try
            {
                data = await _userService.Update(model);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new() { Error = e.Message };
            }
            return new() { Data = new object[] { data } };
        }

        [HttpPost]
        [Route("delete")]
        public async Task<ResponseModel> DeleteUsers(IdListModel model)
        {
            IEnumerable<Guid> data;
            try
            {
                data = await _userService.DeleteByIds(model.Ids);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new() { Error = e.Message };
            }
            return new() { Data = new object[] { new { DeletedIds = data.ToArray() } } };
        }
    }
}
