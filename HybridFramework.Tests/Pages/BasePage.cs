using Microsoft.Playwright;
namespace HybridFramework.Tests.Pages{
    public class BasePage{
        protected readonly IPage Page;
        public BasePage(IPage page){
            Page = page;
        }

        protected async Task NavigateAsync(string url){
            await Page.GotoAsync(url);
        }
        protected async Task ClickAsync(string locator){
            await Page.Locator(locator).ClickAsync();
        }
        protected async Task FillAsync(string locator, string value){
            await Page.Locator(locator).FillAsync(value);
        }
        protected async Task<string> GetTextAsync(string locator){
            return await Page.Locator(locator).InnerTextAsync();
        }
        protected async Task<bool> IsVisibleAsync(string locator){
            return await Page.Locator(locator).IsVisibleAsync();
        }
        protected async Task WaitForElementAsync(string locator){
            await Page.Locator(locator).WaitForAsync();
        }

    }
}