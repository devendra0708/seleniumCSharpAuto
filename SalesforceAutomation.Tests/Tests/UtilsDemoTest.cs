using NUnit.Framework;
using SalesforceAutomation.Tests.Core;
using SalesforceAutomation.Tests.Pages;
using SalesforceAutomation.Tests.Utils;
using System;

namespace SalesforceAutomation.Tests.Tests
{
    [TestFixture]
    public class UtilsDemoTest : BaseTest
    {
        private LoginPage _loginPage;
        
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            _loginPage = new LoginPage(Driver);
        }
        
        [Test]
        public void DemoUtilsTest()
        {
            // Date utils demo
            string currentDate = DateUtils.GetCurrentDate();
            string yesterday = DateUtils.AddDaysToCurrentDate(-1);
            string lastDayOfMonth = DateUtils.GetLastDayOfMonth();
            
            Console.WriteLine($"Current date: {currentDate}");
            Console.WriteLine($"Yesterday: {yesterday}");
            Console.WriteLine($"Last day of month: {lastDayOfMonth}");
            
            // String utils demo
            string randomEmail = StringUtils.GenerateRandomEmail();
            string randomPhoneNumber = StringUtils.GenerateRandomPhoneNumber();
            string truncatedText = StringUtils.Truncate("This is a very long text that should be truncated", 20);
            
            Console.WriteLine($"Random email: {randomEmail}");
            Console.WriteLine($"Random phone: {randomPhoneNumber}");
            Console.WriteLine($"Truncated text: {truncatedText}");
            
            // Data utils demo
            var contactData = DataUtils.GenerateContactData();
            var addressData = DataUtils.GenerateAddressData();
            
            Console.WriteLine($"Contact name: {contactData["FirstName"]} {contactData["LastName"]}");
            Console.WriteLine($"Contact email: {contactData["Email"]}");
            Console.WriteLine($"Contact phone: {contactData["Phone"]}");
            
            Console.WriteLine($"Address: {addressData["Street"]}, {addressData["City"]}, {addressData["State"]} {addressData["PostalCode"]}");
            
            // Create a test file with the generated data
            string content = $"Contact Information:\n" +
                            $"Name: {contactData["FirstName"]} {contactData["LastName"]}\n" +
                            $"Email: {contactData["Email"]}\n" +
                            $"Phone: {contactData["Phone"]}\n\n" +
                            $"Address:\n" +
                            $"{addressData["Street"]}\n" +
                            $"{addressData["City"]}, {addressData["State"]} {addressData["PostalCode"]}";
            
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "contact_info.txt");
            FileUtils.WriteToFile(filePath, content);
            
            Console.WriteLine($"Data saved to: {filePath}");
            
            // Log success message
            ExtentTest.Info("Successfully demonstrated utility classes");
            
            // Visual verification for the test
            Assert.Pass("Utils demo test completed successfully");
        }
    }
} 