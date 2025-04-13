using NUnit.Framework;
using SalesforceAutomation.Tests.Core;
using SalesforceAutomation.Tests.Pages;
using SalesforceAutomation.Tests.Config;

namespace SalesforceAutomation.Tests.Tests
{
    [TestFixture]
    public class LoginTests : BaseTest
    {
        private LoginPage _loginPage;
        
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            _loginPage = new LoginPage(Driver);
        }
        
        [Test]
        public void ValidLogin_ShouldSucceed()
        {
            // Arrange
            string username = TestSettings.Username;
            string password = TestSettings.Password;
            
            // Act
            _loginPage.Login(username, password);
            
            // Assert
            Assert.That(_loginPage.IsErrorMessageDisplayed(), Is.False, "Error message should not be displayed for valid login");
        }
        
        [Test]
        public void InvalidLogin_ShouldShowError()
        {
            // Arrange
            string username = "invalid@example.com";
            string password = "wrongpassword";
            
            // Act
            _loginPage.Login(username, password);
            
            // Assert
            Assert.That(_loginPage.IsErrorMessageDisplayed(), Is.True, "Error message should be displayed for invalid login");
        }
    }
} 