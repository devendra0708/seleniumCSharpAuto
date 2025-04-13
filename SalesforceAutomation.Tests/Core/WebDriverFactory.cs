using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using SalesforceAutomation.Tests.Config;
using OpenQA.Selenium.Support.UI;

namespace SalesforceAutomation.Tests.Core
{
    public class WebDriverFactory
    {
        private static readonly ThreadLocal<IWebDriver?> _driver = new ThreadLocal<IWebDriver?>();

        public static IWebDriver Driver
        {
            get
            {
                if (_driver.Value == null)
                {
                    _driver.Value = CreateDriver();
                    _driver.Value.Manage().Window.Size = new System.Drawing.Size(1920, 1080);
                }
                return _driver.Value!;
            }
        }

        // Static utility method for page operations
        public static void WaitForPageLoad()
        {
            var wait = new WebDriverWait(WebDriverFactory.Driver, TimeSpan.FromSeconds(TestSettings.ExplicitWait));
            wait.Until(driver => ((IJavaScriptExecutor)driver)
                .ExecuteScript("return document.readyState")?.ToString() == "complete");
        }

        public static IWebDriver CreateDriver()
        {
            switch (TestSettings.Browser.ToLower())
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    return new ChromeDriver();
                
                case "chromefortesting":
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    var options = new ChromeOptions();
                    options.AddArgument("--no-sandbox");
                    options.AddArgument("--disable-dev-shm-usage");
                    return new ChromeDriver(options);
                
                case "firefox":
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    return new FirefoxDriver();
                
                case "edge":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    return new EdgeDriver();
                
                default:
                    throw new ArgumentException($"Browser type {TestSettings.Browser} is not supported.");
            }
        }

        public static void QuitDriver()
        {
            if (_driver.Value != null)
            {
                _driver.Value.Quit();
                _driver.Value = null;
            }
        }
    }
} 