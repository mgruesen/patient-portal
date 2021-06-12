using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PatientPortal.Web.Clients.PatientPortalApi
{
    public interface IApiClient
    {
        Task<UserModel> Login(string username, string password);
        Task<UserModel[]> GetUsersByIds(params Guid[] ids);
        Task<ContactModel[]> GetContactsByIds(params Guid[] ids);
        Task<ProviderModel[]> GetProvidersByIds(params Guid[] ids);
        Task<PatientModel[]> GetPatientsByIds(params Guid[] ids);
        Task<ContactModel> UpsertContact(ContactModel model);
        Task<ProviderModel> UpsertProvider(ProviderModel model);
        Task<PatientModel> UpsertPatient(PatientModel model);
        Task<UserModel> UpdateUser(UserModel model);
    }

    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        public ApiClient(IConfiguration configuration)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri($"http://{configuration["Api:Host"]}:{configuration["Api:Port"]}"),
            };
            _httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var basicAuthHeaderValue = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{configuration["Api:Username"]}:{configuration["Api:Password"]}")
            );

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", basicAuthHeaderValue);
        }

        public async Task<UserModel[]> GetUsersByIds(params Guid[] ids)
        {
            var model = new IdListModel { Ids = ids };
            var response = await _httpClient.PostAsJsonAsync("Users/list", model);

            if (!response.IsSuccessStatusCode)
                return Array.Empty<UserModel>();
            var responseModel = await response.Content.ReadFromJsonAsync<ResponseModel<UserModel>>();
            return responseModel.Data;
        }

        public async Task<ContactModel[]> GetContactsByIds(params Guid[] ids)
        {
            var model = new IdListModel { Ids = ids };
            var response = await _httpClient.PostAsJsonAsync("Contacts/list", model);

            if (!response.IsSuccessStatusCode)
                return Array.Empty<ContactModel>();
            var responseModel = await response.Content.ReadFromJsonAsync<ResponseModel<ContactModel>>();
            return responseModel.Data;
        }

        public async Task<ProviderModel[]> GetProvidersByIds(params Guid[] ids)
        {
            var model = new IdListModel { Ids = ids };
            var response = await _httpClient.PostAsJsonAsync("Providers/list", model);

            if (!response.IsSuccessStatusCode)
                return Array.Empty<ProviderModel>();
            var responseModel = await response.Content.ReadFromJsonAsync<ResponseModel<ProviderModel>>();
            return responseModel.Data;
        }

        public async Task<PatientModel[]> GetPatientsByIds(params Guid[] ids)
        {
            var model = new IdListModel { Ids = ids };
            var response = await _httpClient.PostAsJsonAsync("Patients/list", model);

            if (!response.IsSuccessStatusCode)
                return Array.Empty<PatientModel>();
            var responseModel = await response.Content.ReadFromJsonAsync<ResponseModel<PatientModel>>();
            return responseModel.Data;
        }

        public async Task<UserModel> Login(string username, string password)
        {
            var model = new LoginModel { Username = username, Password = password };
            var response = await _httpClient.PostAsJsonAsync("Users/authenticate", model);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<UserModel>();
        }

        public async Task<ContactModel> UpsertContact(ContactModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("Contacts", model);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseModel = await response.Content.ReadFromJsonAsync<ResponseModel<ContactModel>>();
            return responseModel.Data.SingleOrDefault();
        }

        public async Task<ProviderModel> UpsertProvider(ProviderModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("Providers", model);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseModel = await response.Content.ReadFromJsonAsync<ResponseModel<ProviderModel>>();
            return responseModel.Data.SingleOrDefault();
        }

        public async Task<PatientModel> UpsertPatient(PatientModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("Patients", model);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseModel = await response.Content.ReadFromJsonAsync<ResponseModel<PatientModel>>();
            return responseModel.Data.SingleOrDefault();
        }

        public async Task<UserModel> UpdateUser(UserModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("Users", model);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseModel = await response.Content.ReadFromJsonAsync<ResponseModel<UserModel>>();
            return responseModel.Data.SingleOrDefault();
        }
    }
}
