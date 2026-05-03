using Newtonsoft.Json;
namespace HybridFramework.Tests.API.Models
{
    public class LoginRequest{
        [JsonProperty("username")]
        public string? Username {get;set;}
        [JsonProperty("password")]
        public string? Password {get;set;}
        
    }
}