using System;
using System.Linq;
using System.Threading.Tasks;
using PatientPortal.Web.Clients.PatientPortalApi;
using PatientPortal.Web.Mappers;
using PatientPortal.Web.Models;

namespace PatientPortal.Web.Services
{
    public interface IUserService
    {
        Task<UserViewModel> LoadUser(Guid userId);
        Task UpdateUser(UserViewModel model, Guid userId);
    }

    public class UserService : IUserService
    {
        private readonly IApiClient _apiClient;
        private readonly IUserViewModelMapper _userViewModelMapper;

        public UserService(IApiClient apiClient, IUserViewModelMapper userViewModelMapper)
        {
            _apiClient = apiClient;
            _userViewModelMapper = userViewModelMapper;
        }

        public async Task<UserViewModel> LoadUser(Guid userId)
        {
            var user = (await _apiClient.GetUsersByIds(userId)).SingleOrDefault();
            var patient = (await _apiClient.GetPatientsByIds(user.PatientId ?? Guid.Empty)).SingleOrDefault();
            var contact = (await _apiClient.GetContactsByIds(user.ContactId ?? Guid.Empty)).SingleOrDefault();
            var provider = (await _apiClient.GetProvidersByIds(user.ProviderId ?? Guid.Empty)).SingleOrDefault();
            return _userViewModelMapper.Compose(user, patient, contact, provider);
        }

        public async Task UpdateUser(UserViewModel viewModel, Guid userId)
        {
            var userModel = (await _apiClient.GetUsersByIds(userId)).SingleOrDefault();
            (var patientModel, var contactModel, var providerModel) = _userViewModelMapper.Decompose(viewModel);
            var patient = await _apiClient.UpsertPatient(patientModel);
            var contact = await _apiClient.UpsertContact(contactModel);
            var provider = await _apiClient.UpsertProvider(providerModel);
            userModel.PatientId = patient.Id;
            userModel.ContactId = contact.Id;
            userModel.ProviderId = provider.Id;
            await _apiClient.UpdateUser(userModel);
        }
    }
}
