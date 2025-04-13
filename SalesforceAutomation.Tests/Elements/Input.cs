using OpenQA.Selenium;
using SalesforceAutomation.Tests.Core;

namespace SalesforceAutomation.Tests.Elements
{
    public class Input : BaseElement
    {
        public Input(By locator) : base(locator)
        {
        }
        
        public void SetValue(string text)
        {
            SendKeys(text);
        }
        
        public string GetValue()
        {
            return GetAttribute("value");
        }
    }
} 