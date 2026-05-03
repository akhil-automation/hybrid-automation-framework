using NUnit.Framework;
using Microsoft.Playwright;
using HybridFramework.Tests.API.Endpoints;
using HybridFramework.Tests.API.Models;
using HybridFramework.Tests.Config;

namespace HybridFramework.Tests.Tests.Hybrid
{
    [TestFixture]
    public class HybridTests
    {
        private AuthApi _authApi;
        private BookingApi _bookingApi;
        private string _token;
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage _page;

        [SetUp]
        public async Task SetUp()
        {
            // API Setup
            _authApi = new AuthApi();
            _token = await _authApi.GetAccessTokenAsync();
            _bookingApi = new BookingApi(_token);

            // UI Setup
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = ConfigManager.Headless
            });
            _page = await _browser.NewPageAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }

        [Test]
        public async Task CreateBookingViaAPI_ThenVerifyViaUI()
        {
            // STEP 1 — Create booking via API
            var booking = new BookingRequest
            {
                FirstName = "Hybrid",
                LastName = "Test",
                TotalPrice = 999,
                DepositPaid = true,
                BookingDates = new BookingDates
                {
                    CheckIn = "2026-06-01",
                    CheckOut = "2026-06-05"
                },
                AdditionalNeeds = "Breakfast"
            };

            var response = await _bookingApi.CreateBookingAsync(booking);
            int bookingId = response.Data.BookingId;

            Assert.That(bookingId, Is.GreaterThan(0));
            Console.WriteLine($"[HYBRID] Booking created via API. ID: {bookingId}");

            // STEP 2 — Verify via UI (OrangeHRM login as UI verification)
            await _page.GotoAsync(ConfigManager.UIBaseUrl + "/web/index.php/auth/login");
            await _page.Locator("input[name='username']").FillAsync(ConfigManager.AdminUsername);
            await _page.Locator("input[name='password']").FillAsync(ConfigManager.AdminPassword);
            await _page.Locator("button[type='submit']").ClickAsync();

            // Verify dashboard loaded
            await _page.WaitForSelectorAsync("h6.oxd-text--h6");
            bool isDashboardVisible = await _page.Locator("h6.oxd-text--h6").IsVisibleAsync();

            Assert.That(isDashboardVisible, Is.True);
            Console.WriteLine($"[HYBRID] UI verification passed. Dashboard visible.");
            Console.WriteLine($"[HYBRID] Test complete — API booking ID {bookingId} created, UI login verified.");
        }
    }
}