using Microsoft.Playwright;
using HybridFramework.Tests.Pages;
using Reqnroll;
using NUnit.Framework;

namespace HybridFramework.Tests.StepDefinitions{
    [Binding]
    public class LoginStepDef{
        private readonly LoginPage _loginPage;
        public LoginStepDef(IPage page){
            _loginPage = new LoginPage(page);
        }
        [Given("I navigate to the OrangeHRM login page")]
        public async Task GivenINavigateToLoginPage(){
            await _loginPage.NavigateToLoginPage();
        }
        [When("I login with username {string} and password {string}")]
        public async Task WhenLogin(string username, string password){
            await _loginPage.Login(username, password);
        }
        [Then(@"Verify whether landed on Dashboard page or not")]
        public async Task ThenVerifyDashboardPage(){
            var isVisible = await _loginPage.IsDashboardVisible();
            
            Assert.That(isVisible, Is.True, "Dashboard not visible after login");
        }
    }
}