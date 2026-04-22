using RestSharp;
using HybridFramework.Tests.API.Client;
using HybridFramework.Tests.API.Models;
using HybridFramework.Tests.Config;

namespace HybridFramework.Tests.API.Endpoints
{
    public class AuthApi
    {
        private readonly ApiClient _apiClient;

        public AuthApi()
        {
            _apiClient = new ApiClient();
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var request = new RestRequest("/auth", Method.Post);

            request.AddJsonBody(new LoginRequest
            {
                Username = ConfigManager.APIUsername,
                Password = ConfigManager.APIPassword
            });

            var response = await _apiClient.ExecuteAsync<LoginResponse>(request);

            if (response.Data?.Token == null)
                throw new Exception(
                    $"Token fetch failed. Status: {response.StatusCode}, " +
                    $"Content: {response.Content}"
                );

            Console.WriteLine("[AUTH] Token retrieved successfully");
            return response.Data.Token;
        }
    }
}