using Microsoft.Playwright;
using NUnit.Framework;
using HybridFramework.Tests.Config;
namespace HybridFramework.Tests.Pages

{
    public class LoginPage: BasePage
    {
        private const string userNameInput = "input[name='username']";
        private const string passwordInput = "input[name='password']";
        private const string loginButton = "button[type='submit']";
        private const string dashboardHeader = "h6.oxd-text--h6";

        public LoginPage(IPage page): base(page){}
        public async Task NavigateToLoginPage(){
            await NavigateAsync(ConfigManager.UIBaseUrl + "/web/index.php/auth/login");
        }
        public async Task Login(string username, string password){
            await FillAsync(userNameInput, username);
            await FillAsync(passwordInput, password);
            await ClickAsync(loginButton);
        }
        public async Task<bool> IsDashboardVisible(){
            await WaitForElementAsync(dashboardHeader);
            return await IsVisibleAsync(dashboardHeader);
        }

    }
}