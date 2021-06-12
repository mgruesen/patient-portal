using System;
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
        Task<ResponseModel> ListUsers(params Guid[] userIds);
        Task<UserModel[]> GetUsersByIds(params string[] ids);
        Task<ContactModel[]> GetContactsByIds(params string[] ids);
        Task<ProviderModel[]> GetProvidersByIds(params string[] ids);
        Task<PatientModel[]> GetPatientsByIds(params string[] ids);
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

        public async Task<UserModel[]> GetUsersByIds(params string[] ids)
        {
            var model = new IdListModel { Ids = ids.Select(uid => Guid.Parse(uid)).ToArray() };
            var response = await _httpClient.PostAsJsonAsync("Users/list", model);

            if (!response.IsSuccessStatusCode)
                return Array.Empty<UserModel>();
            var responseModel = await response.Content.ReadFromJsonAsync<ResponseModel>();
            return responseModel.Data as UserModel[];
        }

        public async Task<ContactModel[]> GetContactsByIds(params string[] ids)
        {
            var model = new IdListModel { Ids = ids.Select(uid => Guid.Parse(uid)).ToArray() };
            var response = await _httpClient.PostAsJsonAsync("Contacts/list", model);

            if (!response.IsSuccessStatusCode)
                return Array.Empty<ContactModel>();
            var responseModel = await response.Content.ReadFromJsonAsync<ResponseModel>();
            return responseModel.Data as ContactModel[];
        }

        public async Task<ProviderModel[]> GetProvidersByIds(params string[] ids)
        {
            var model = new IdListModel { Ids = ids.Select(uid => Guid.Parse(uid)).ToArray() };
            var response = await _httpClient.PostAsJsonAsync("Providers/list", model);

            if (!response.IsSuccessStatusCode)
                return Array.Empty<ProviderModel>();
            var responseModel = await response.Content.ReadFromJsonAsync<ResponseModel>();
            return responseModel.Data as ProviderModel[];
        }

        public async Task<ResponseModel> ListUsers(params Guid[] userIds)
        {
            throw new NotImplementedException();
        }


        public async Task<PatientModel[]> GetPatientsByIds(params string[] ids)
        {
            var model = new IdListModel { Ids = ids.Select(uid => Guid.Parse(uid)).ToArray() };
            var response = await _httpClient.PostAsJsonAsync("Patients/list", model);

            if (!response.IsSuccessStatusCode)
                return Array.Empty<PatientModel>();
            var responseModel = await response.Content.ReadFromJsonAsync<ResponseModel>();
            return responseModel.Data as PatientModel[];
        }
        public async Task<UserModel> Login(string username, string password)
        {
            var model = new LoginModel { Username = username, Password = password };
            var response = await _httpClient.PostAsJsonAsync("Users/authenticate", model);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<UserModel>();
        }
    }
}
