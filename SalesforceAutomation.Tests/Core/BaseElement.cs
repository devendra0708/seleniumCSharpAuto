using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SalesforceAutomation.Tests.Config;

namespace SalesforceAutomation.Tests.Core
{
    public class BaseElement
    {
        protected readonly IWebDriver Driver;
        protected readonly WebDriverWait Wait;
        protected readonly By? Locator;
        
        public BaseElement(By locator)
        {
            Driver = WebDriverFactory.Driver;
            Locator = locator ?? throw new ArgumentNullException(nameof(locator));
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestSettings.ExplicitWait));
        }
        
        protected BaseElement(IWebElement element)
        {
            Driver = WebDriverFactory.Driver;
            Element = element;
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestSettings.ExplicitWait));
        }
        
        public IWebElement? Element { get; private set; }
        
        // Initialize element if not already set
        protected IWebElement GetElement() 
        {
            if (Element == null)
            {
                Element ??= WaitUntilVisible();
            }
            return Element;
        }
        
        public bool IsDisplayed()
        {
            try
            {
                return GetElement().Displayed;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public void Click()
        {
            try
            {
                GetElement().Click();
            }
            catch (ElementClickInterceptedException)
            {
                ScrollIntoView();
                GetElement().Click();
            }
        }
        
        public void SendKeys(string text)
        {
            GetElement().Clear();
            GetElement().SendKeys(text);
        }
        
        public string GetText()
        {
            return GetElement().Text;
        }
        
        public string GetAttribute(string attributeName)
        {
            return GetElement().GetAttribute(attributeName) ?? string.Empty;
        }
        
        public void WaitUntilClickable()
        {
            if (Locator != null)
            {
                Wait.Until(ExpectedConditions.ElementToBeClickable(Locator));
            }
            else
            {
                Wait.Until(ExpectedConditions.ElementToBeClickable(Element));
            }
        }
        
        // Support for element chaining
        public BaseElement FindElement(By by)
        {
            return new BaseElement(GetElement().FindElement(by));
        }
        
        public List<BaseElement> FindElements(By by)
        {
            return GetElement().FindElements(by)
                .Select(e => new BaseElement(e))
                .ToList();
        }
        
        // Static utility method for page operations
        public static void WaitForPageLoad()
        {
            var wait = new WebDriverWait(WebDriverFactory.Driver, TimeSpan.FromSeconds(TestSettings.ExplicitWait));
            wait.Until(driver => ((IJavaScriptExecutor)driver)
                .ExecuteScript("return document.readyState")?.ToString() == "complete");
        }
        
        protected IWebElement WaitUntilVisible()
        {
            if (Locator != null)
            {
                return Wait.Until(ExpectedConditions.ElementIsVisible(Locator));
            }
            throw new InvalidOperationException("Cannot wait for visibility - no locator provided");
        }
        
        protected void ScrollIntoView()
        {
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", GetElement());
        }
    }
} 