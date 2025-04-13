using OpenQA.Selenium;
using SalesforceAutomation.Tests.Core;

namespace SalesforceAutomation.Tests.Elements
{
    public class Dropdown : BaseElement
    {
        private readonly string _listSelector = "ul";
        private readonly string _optionSelector = "li";

        public Dropdown(By locator) : base(locator)
        {
        }

        public void SelectByText(string text)
        {
            OpenDropdown();
            SelectOption(text);
        }

        public void SelectByTextContaining(string partialText)
        {
            OpenDropdown();
            IList<IWebElement> options = GetDropdownOptions();
            foreach (var option in options)
            {
                if (option.Text.Contains(partialText))
                {
                    option.Click();
                    return;
                }
            }
            throw new NoSuchElementException($"No option containing '{partialText}' found");
        }

        public void SelectByValue(string value)
        {
            OpenDropdown();
            SelectOptionByValue(value);
        }

        public void SelectByIndex(int index)
        {
            OpenDropdown();
            GetDropdownOptions()[index].Click();
        }

        public string GetSelectedText()
        {
            try
            {
                return GetElement().FindElement(By.CssSelector(".ui-select-match span, .selected-option")).Text;
            }
            catch
            {
                return GetElement().Text;
            }
        }

        public string GetSelectedValue()
        {
            try
            {
                return GetElement().FindElement(By.CssSelector(".selected-value")).GetAttribute("data-value") ?? string.Empty;
            }
            catch
            {
                return GetElement().GetAttribute("data-value") ?? string.Empty;
            }
        }

        public List<string> GetAllOptions()
        {
            OpenDropdown();
            var options = GetDropdownOptions();
            return options.Select(option => option.Text).ToList();
        }

        private void OpenDropdown()
        {
            Click();
            Thread.Sleep(300); // Allow dropdown to open
        }

        private IList<IWebElement> GetDropdownOptions()
        {
            try
            {
                // Try finding options in a list that appears after clicking
                var listElement = GetElement().FindElement(By.CssSelector(_listSelector));
                return listElement.FindElements(By.CssSelector(_optionSelector));
            }
            catch
            {
                // If no separate list appears, try finding options within the dropdown element
                return GetElement().FindElements(By.CssSelector(_optionSelector));
            }
        }

        private void SelectOption(string text)
        {
            var options = GetDropdownOptions();
            foreach (var option in options)
            {
                if (option.Text.Equals(text))
                {
                    option.Click();
                    return;
                }
            }
            throw new NoSuchElementException($"Option '{text}' not found");
        }

        private void SelectOptionByValue(string value)
        {
            var options = GetDropdownOptions();
            foreach (var option in options)
            {
                if (option.GetAttribute("value")?.Equals(value) == true || 
                    option.GetAttribute("data-value")?.Equals(value) == true)
                {
                    option.Click();
                    return;
                }
            }
            throw new NoSuchElementException($"Option with value '{value}' not found");
        }
    }
} 