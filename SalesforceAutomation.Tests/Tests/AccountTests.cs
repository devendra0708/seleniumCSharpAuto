using NUnit.Framework;
using SalesforceAutomation.Tests.Core;
using SalesforceAutomation.Tests.Pages;
using SalesforceAutomation.Tests.Utils;
using SalesforceAutomation.Tests.Config;
using Bogus;

namespace SalesforceAutomation.Tests.Tests
{
    [TestFixture]
    public class AccountTests : BaseTest
    {
        private LoginPage _loginPage;
        private AccountsPage _accountsPage;
        
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            _loginPage = new LoginPage(Driver);
            _accountsPage = new AccountsPage(Driver);
            
            // Login before each test
            _loginPage.Login(TestSettings.Username, TestSettings.Password);
        }
        
        [Test]
        public void CreateAccount_WithValidData_ShouldSucceed()
        {
            // Arrange - Generate test data
            var accountData = FakerDataFactory.GenerateAccountData();
            string industry = "Technology";
            
            // Act - Create the account
            _accountsPage.CreateNewAccount(
                accountData["AccountName"],
                accountData["Phone"],
                accountData["Website"],
                industry);
            
            // Assert - Verify success
            Assert.That(_accountsPage.IsAccountCreated(), Is.True, "Account creation failed");
        }
        
        [Test]
        public void CreateMultipleAccounts_WithFakerData_ShouldSucceed()
        {
            // Create multiple accounts with different industries
            string[] industries = { "Technology", "Healthcare", "Financial" };
            
            // Create first account
            var account1Data = FakerDataFactory.GenerateAccountData();
            _accountsPage.CreateNewAccount(
                account1Data["AccountName"],
                account1Data["Phone"],
                account1Data["Website"],
                industries[0]);
            Assert.That(_accountsPage.IsAccountCreated(), Is.True, "First account creation failed");
            
            // Navigate back to home page and create second account
            Driver.Navigate().GoToUrl(TestSettings.BaseUrl);
            var account2Data = FakerDataFactory.GenerateAccountData();
            _accountsPage.CreateNewAccount(
                account2Data["AccountName"],
                account2Data["Phone"],
                account2Data["Website"],
                industries[1]);
            Assert.That(_accountsPage.IsAccountCreated(), Is.True, "Second account creation failed");
        }
    }
} 