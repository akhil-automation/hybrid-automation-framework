using RestSharp;
using HybridFramework.Tests.Config;

namespace HybridFramework.Tests.API.Client
{
    public class ApiClient
    {
        private readonly RestClient _client;

        public ApiClient()
        {
            _client = new RestClient(ConfigManager.APIBaseUrl);
        }

        public async Task<RestResponse<T>> ExecuteAsync<T>(RestRequest request)
        {
            var response = await _client.ExecuteAsync<T>(request);
            Console.WriteLine($"[API] {request.Method} → {(int)response.StatusCode}");
            return response;
        }

        public void AddAuthToken(RestRequest request, string token)
        {
            request.AddHeader("Authorization", $"Bearer {token}");
        }
    }
}