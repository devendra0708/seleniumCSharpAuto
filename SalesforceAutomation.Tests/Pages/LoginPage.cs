using OpenQA.Selenium;
using SalesforceAutomation.Tests.Core;
using SalesforceAutomation.Tests.Elements;

namespace SalesforceAutomation.Tests.Pages
{
    public class LoginPage : BasePage
    {
        private readonly By _usernameField = By.Id("username");
        private readonly By _passwordField = By.Id("password");
        private readonly By _loginButton = By.Id("Login");
        private readonly By _errorMessage = By.Id("error");
        
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }
        
        public void EnterUsername(string username)
        {
            InputField(_usernameField).SetValue(username);
        }
        
        public void EnterPassword(string password)
        {
            InputField(_passwordField).SetValue(password);
        }
        
        public void ClickLogin()
        {
            Element(_loginButton).Click();
            WaitForPageLoad();
        }
        
        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }
        
        public string GetErrorMessage()
        {
            return Element(_errorMessage).GetText();
        }
        
        public bool IsErrorMessageDisplayed()
        {
            return Element(_errorMessage).IsDisplayed();
        }
    }
} 