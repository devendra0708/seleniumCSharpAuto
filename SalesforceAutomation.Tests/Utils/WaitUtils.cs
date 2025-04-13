using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SalesforceAutomation.Tests.Config;
using SeleniumExtras.WaitHelpers;

namespace SalesforceAutomation.Tests.Utils
{
    public static class WaitUtils
    {
        /// <summary>
        /// Wait for element to be visible
        /// </summary>
        /// <param name="driver">WebDriver instance</param>
        /// <param name="locator">Element locator</param>
        /// <param name="timeoutInSeconds">Timeout in seconds (default: from TestSettings)</param>
        /// <returns>The WebElement once it is visible</returns>
        public static IWebElement WaitForElementVisible(IWebDriver driver, By locator, int? timeoutInSeconds = null)
        {
            int timeout = timeoutInSeconds ?? TestSettings.ExplicitWait;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }
        
        /// <summary>
        /// Wait for element to be clickable
        /// </summary>
        /// <param name="driver">WebDriver instance</param>
        /// <param name="locator">Element locator</param>
        /// <param name="timeoutInSeconds">Timeout in seconds (default: from TestSettings)</param>
        /// <returns>The WebElement once it is clickable</returns>
        public static IWebElement WaitForElementClickable(IWebDriver driver, By locator, int? timeoutInSeconds = null)
        {
            int timeout = timeoutInSeconds ?? TestSettings.ExplicitWait;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }
        
        /// <summary>
        /// Wait for element to exist in the DOM
        /// </summary>
        /// <param name="driver">WebDriver instance</param>
        /// <param name="locator">Element locator</param>
        /// <param name="timeoutInSeconds">Timeout in seconds (default: from TestSettings)</param>
        /// <returns>The WebElement once it exists</returns>
        public static IWebElement WaitForElementExists(IWebDriver driver, By locator, int? timeoutInSeconds = null)
        {
            int timeout = timeoutInSeconds ?? TestSettings.ExplicitWait;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.ElementExists(locator));
        }
        
        /// <summary>
        /// Wait for element to be invisible
        /// </summary>
        /// <param name="driver">WebDriver instance</param>
        /// <param name="locator">Element locator</param>
        /// <param name="timeoutInSeconds">Timeout in seconds (default: from TestSettings)</param>
        /// <returns>True if the element is invisible</returns>
        public static bool WaitForElementInvisible(IWebDriver driver, By locator, int? timeoutInSeconds = null)
        {
            int timeout = timeoutInSeconds ?? TestSettings.ExplicitWait;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
        }
        
        /// <summary>
        /// Wait for page to load completely
        /// </summary>
        /// <param name="driver">WebDriver instance</param>
        /// <param name="timeoutInSeconds">Timeout in seconds (default: from TestSettings)</param>
        public static void WaitForPageLoad(IWebDriver driver, int? timeoutInSeconds = null)
        {
            int timeout = timeoutInSeconds ?? TestSettings.PageLoadTimeout;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        }
        
        /// <summary>
        /// Wait for text to be present in element
        /// </summary>
        /// <param name="driver">WebDriver instance</param>
        /// <param name="locator">Element locator</param>
        /// <param name="text">Text to wait for</param>
        /// <param name="timeoutInSeconds">Timeout in seconds (default: from TestSettings)</param>
        /// <returns>True if text is present in the element</returns>
        public static bool WaitForTextPresent(IWebDriver driver, By locator, string text, int? timeoutInSeconds = null)
        {
            int timeout = timeoutInSeconds ?? TestSettings.ExplicitWait;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.TextToBePresentInElementLocated(locator, text));
        }
        
        /// <summary>
        /// Wait for an alert to be present
        /// </summary>
        /// <param name="driver">WebDriver instance</param>
        /// <param name="timeoutInSeconds">Timeout in seconds (default: from TestSettings)</param>
        /// <returns>The IAlert instance</returns>
        public static IAlert WaitForAlertPresent(IWebDriver driver, int? timeoutInSeconds = null)
        {
            int timeout = timeoutInSeconds ?? TestSettings.ExplicitWait;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(ExpectedConditions.AlertIsPresent());
        }
        
        /// <summary>
        /// Wait with a specific condition
        /// </summary>
        /// <typeparam name="T">Type of result from the wait</typeparam>
        /// <param name="driver">WebDriver instance</param>
        /// <param name="condition">Wait condition function</param>
        /// <param name="timeoutInSeconds">Timeout in seconds (default: from TestSettings)</param>
        /// <returns>Result from the wait condition</returns>
        public static T WaitUntil<T>(IWebDriver driver, Func<IWebDriver, T> condition, int? timeoutInSeconds = null)
        {
            int timeout = timeoutInSeconds ?? TestSettings.ExplicitWait;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(condition);
        }
    }
} 