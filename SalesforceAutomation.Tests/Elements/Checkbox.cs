using OpenQA.Selenium;
using SalesforceAutomation.Tests.Core;

namespace SalesforceAutomation.Tests.Elements
{
    public class Checkbox : BaseElement
    {
        public Checkbox(By locator) : base(locator)
        {
        }

        public bool IsChecked()
        {
            string classAttribute = GetAttribute("class") ?? string.Empty;
            string checkedAttribute = GetAttribute("checked") ?? string.Empty;
            
            return classAttribute.Contains("checked") || 
                   checkedAttribute.Equals("true") || 
                   checkedAttribute.Equals("checked");
        }

        public void Check()
        {
            if (!IsChecked())
            {
                Click();
                Thread.Sleep(300); // Short debounce
            }
        }

        public void Uncheck()
        {
            if (IsChecked())
            {
                Click();
                Thread.Sleep(300); // Short debounce
            }
        }

        public void SetChecked(bool shouldBeChecked)
        {
            if (shouldBeChecked)
            {
                Check();
            }
            else
            {
                Uncheck();
            }
        }
    }
} 