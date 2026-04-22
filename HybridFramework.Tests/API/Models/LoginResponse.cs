using Newtonsoft.Json;
namespace HybridFramework.Tests.API.Models
{
    public class LoginResponse{
        [JsonProperty("token")]
        public string Token {get;set;}
    }
}