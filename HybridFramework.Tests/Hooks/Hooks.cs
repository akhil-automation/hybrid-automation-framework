using Reqnroll;
using Reqnroll.BoDi;
using Microsoft.Playwright;
using HybridFramework.Tests.Config;

namespace HybridFramework.Tests.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _container;
        private IPlaywright _playwright;
        private IBrowser _browser;

        public Hooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            _playwright = await Playwright.CreateAsync();

            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = ConfigManager.Headless,
                SlowMo = ConfigManager.SlowMo,
                Channel = "msedge",
                ExecutablePath = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe"
            });

            var context = await _browser.NewContextAsync();
            var page = await context.NewPageAsync();

            // Register page in SpecFlow's DI container
            // so StepDefinitions can access it
            _container.RegisterInstanceAs(page);
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}
