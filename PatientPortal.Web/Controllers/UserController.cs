using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PatientPortal.Web.Clients.PatientPortalApi;
using PatientPortal.Web.Mappers;
using PatientPortal.Web.Models;

namespace PatientPortal.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IApiClient _apiClient;
        private readonly IUserViewModelMapper _userViewModelMapper;

        public UserController(ILogger<UserController> logger, IApiClient apiClient, IUserViewModelMapper userViewModelMapper)
        {
            _logger = logger;
            _apiClient = apiClient;
            _userViewModelMapper = userViewModelMapper;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = (await _apiClient.GetUsersByIds(userId)).SingleOrDefault();
            var patient = (await _apiClient.GetPatientsByIds(user.PatientId.ToString())).SingleOrDefault();
            var contact = (await _apiClient.GetContactsByIds(patient.ContactId.ToString())).SingleOrDefault();
            var provider = (await _apiClient.GetProvidersByIds(patient.ProviderId.ToString())).SingleOrDefault();
            var model = _userViewModelMapper.Compose(user, patient, contact, provider);
            return View(model);
        }
    }
}
