using DataSyncHub.Modules.Users.Core.Models;
using System.Text.Json;

namespace DataSyncHub.Modules.Users.Core.Services
{
    internal class NinjasApiService : INinjasApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NinjasApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<RandomUser> GetRandomUserAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("api-ninjas");

            var response = await httpClient.GetAsync("randomuser");

            var user = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<RandomUser>(user);
        }
    }
}
