using NUnit.Framework;
using HybridFramework.Tests.API.Endpoints;

namespace HybridFramework.Tests.Tests.API
{
    [TestFixture]
    public class AuthApiTests
    {
        private AuthApi _authApi;

        [SetUp]
        public void SetUp()
        {
            _authApi = new AuthApi();
        }

        [Test]
        public async Task GetAccessToken_WithValidCredentials_ReturnsToken()
        {
            // Act
            var token = await _authApi.GetAccessTokenAsync();

            // Assert
            Assert.That(token, Is.Not.Null.And.Not.Empty);
            Console.WriteLine($"[TEST] Token: {token}");
        }
    }
}