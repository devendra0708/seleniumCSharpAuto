using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SalesforceAutomation.Tests.Config;
using SalesforceAutomation.Tests.Elements;
using SalesforceAutomation.Tests.Utils;

namespace SalesforceAutomation.Tests.Core
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly WebDriverWait Wait;
        
        protected BasePage(IWebDriver driver)
        {
            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestSettings.ExplicitWait));
        }
        
        protected BaseElement Element(By locator)
        {
            return new BaseElement(locator);
        }
        
        protected Input InputField(By locator)
        {
            return new Input(locator);
        }
        
        protected Dropdown DropdownField(By locator)
        {
            return new Dropdown(locator);
        }
        
        protected IWebElement WaitAndFindElement(By by)
        {
            return WaitUtils.WaitForElementVisible(Driver, by);
        }
        
        protected void WaitAndClick(By by)
        {
            WaitUtils.WaitForElementClickable(Driver, by).Click();
        }
        
        protected void WaitAndSendKeys(By by, string text)
        {
            WaitUtils.WaitForElementVisible(Driver, by).SendKeys(text);
        }
        
        protected bool IsElementPresent(By by)
        {
            try
            {
                return WaitUtils.WaitForElementVisible(Driver, by, 5) != null;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
        
        protected void WaitForPageLoad()
        {
            WaitUtils.WaitForPageLoad(Driver);
        }
        
        protected string GetText(By by)
        {
            return WaitUtils.WaitForElementVisible(Driver, by).Text;
        }
        
        protected string GetAttribute(By by, string attributeName)
        {
            return WaitUtils.WaitForElementVisible(Driver, by).GetAttribute(attributeName);
        }
        
        protected void ScrollToElement(By by)
        {
            var element = WaitUtils.WaitForElementExists(Driver, by);
            BrowserUtils.ScrollIntoView(element);
        }
        
        protected void JavaScriptClick(By by)
        {
            var element = WaitUtils.WaitForElementExists(Driver, by);
            BrowserUtils.ClickWithJavaScript(element);
        }
    }
} 