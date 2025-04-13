using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.IO;
using SalesforceAutomation.Tests.Core;
using SalesforceAutomation.Tests.Config;

namespace SalesforceAutomation.Tests.Utils
{
    public static class BrowserUtils
    {
        /// <summary>
        /// Execute JavaScript in the browser
        /// </summary>
        /// <param name="script">JavaScript to execute</param>
        /// <param name="args">Arguments for the script</param>
        /// <returns>Result from the JavaScript execution</returns>
        public static object ExecuteJavaScript(string script, params object[] args)
        {
            return ((IJavaScriptExecutor)WebDriverFactory.Driver).ExecuteScript(script, args);
        }
        
        /// <summary>
        /// Scroll element into view
        /// </summary>
        /// <param name="element">Element to scroll to</param>
        public static void ScrollIntoView(IWebElement element)
        {
            ExecuteJavaScript("arguments[0].scrollIntoView(true);", element);
        }
        
        /// <summary>
        /// Scroll to top of page
        /// </summary>
        public static void ScrollToTop()
        {
            ExecuteJavaScript("window.scrollTo(0, 0)");
        }
        
        /// <summary>
        /// Scroll to bottom of page
        /// </summary>
        public static void ScrollToBottom()
        {
            ExecuteJavaScript("window.scrollTo(0, document.body.scrollHeight)");
        }
        
        /// <summary>
        /// Click element using JavaScript
        /// </summary>
        /// <param name="element">Element to click</param>
        public static void ClickWithJavaScript(IWebElement element)
        {
            ExecuteJavaScript("arguments[0].click();", element);
        }
        
        /// <summary>
        /// Set element value using JavaScript
        /// </summary>
        /// <param name="element">Element to set value for</param>
        /// <param name="value">Value to set</param>
        public static void SetValueWithJavaScript(IWebElement element, string value)
        {
            ExecuteJavaScript("arguments[0].value = arguments[1];", element, value);
        }
        
        /// <summary>
        /// Get browser console logs
        /// </summary>
        /// <returns>List of console logs</returns>
        public static IList<string> GetConsoleLogs()
        {
            try
            {
                var logs = WebDriverFactory.Driver.Manage().Logs.GetLog(LogType.Browser);
                var result = new List<string>();
                
                foreach (var log in logs)
                {
                    result.Add($"[{log.Timestamp}] [{log.Level}] {log.Message}");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                return new List<string> { $"Error getting console logs: {ex.Message}" };
            }
        }
        
        /// <summary>
        /// Take a screenshot and save it to file
        /// </summary>
        /// <param name="fileName">Name of the screenshot file</param>
        /// <returns>Path to saved screenshot</returns>
        public static string TakeScreenshot(string fileName)
        {
            try
            {
                // Create screenshots directory
                string screenshotDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
                    TestSettings.ScreenshotPath);
                Directory.CreateDirectory(screenshotDir);
                
                // Full path to the screenshot
                string fullPath = Path.Combine(screenshotDir, fileName);
                
                // Take and save screenshot
                var screenshot = ((ITakesScreenshot)WebDriverFactory.Driver).GetScreenshot();
                screenshot.SaveAsFile(fullPath);
                
                return fullPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error taking screenshot: {ex.Message}");
                return null;
            }
        }
        
        /// <summary>
        /// Clear browser cookies
        /// </summary>
        public static void ClearCookies()
        {
            WebDriverFactory.Driver.Manage().Cookies.DeleteAllCookies();
        }
        
        /// <summary>
        /// Get element text even if it's not directly visible
        /// </summary>
        /// <param name="element">Element to get text from</param>
        /// <returns>Element text content</returns>
        public static string GetTextContent(IWebElement element)
        {
            return element?.GetAttribute("textContent") ?? string.Empty;
        }
    }
} 