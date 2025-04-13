using NUnit.Framework;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using SalesforceAutomation.Tests.Config;
using SalesforceAutomation.Tests.Utils;
using System;
using System.IO;

namespace SalesforceAutomation.Tests.Core
{
    public class BaseTest
    {
        protected IWebDriver Driver;
        protected static ExtentReports ExtentReports;
        protected ExtentTest ExtentTest;
        
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            // Create reports directory
            string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, TestSettings.ReportPath);
            Directory.CreateDirectory(reportPath);
            
            // Initialize Extent Reports
            string reportFile = Path.Combine(reportPath, $"TestReport_{DateUtils.GetTimestamp()}.html");
            ExtentReports = new ExtentReports();
            ExtentReports.AttachReporter(new ExtentSparkReporter(reportFile));
        }
        
        [SetUp]
        public virtual void Setup()
        {
            Driver = WebDriverFactory.Driver;
            Driver.Navigate().GoToUrl(TestSettings.BaseUrl);
            ExtentTest = ExtentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        
        [TearDown]
        public void Cleanup()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            
            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                string errorMessage = TestContext.CurrentContext.Result.Message;
                ExtentTest.Fail(errorMessage);
                
                // Take screenshot on failure
                try
                {
                    string fileName = $"Error_{DateUtils.GetTimestamp()}.png";
                    string screenshotPath = BrowserUtils.TakeScreenshot(fileName);
                    if (!string.IsNullOrEmpty(screenshotPath))
                    {
                        ExtentTest.AddScreenCaptureFromPath(screenshotPath);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error taking screenshot: {ex.Message}");
                }
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                ExtentTest.Pass("Test passed");
            }
            
            WebDriverFactory.QuitDriver();
        }
        
        [OneTimeTearDown]
        public void GlobalCleanup()
        {
            ExtentReports.Flush();
        }
    }
} 