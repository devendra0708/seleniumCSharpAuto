using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SalesforceAutomation.Tests.Core;

namespace SalesforceAutomation.Tests.Elements
{
    public class SelectOption : BaseElement
    {
        private readonly SelectElement _select;

        public SelectOption(By locator) : base(locator)
        {
            _select = new SelectElement(GetElement());
        }

        public void SelectByText(string text)
        {
            _select.SelectByText(text);
        }

        public void SelectByValue(string value)
        {
            _select.SelectByValue(value);
        }

        public void SelectByIndex(int index)
        {
            _select.SelectByIndex(index);
        }

        public string GetSelectedText()
        {
            return _select.SelectedOption.Text;
        }

        public string GetSelectedValue()
        {
            return _select.SelectedOption.GetAttribute("value") ?? string.Empty;
        }

        public List<string> GetAllOptions()
        {
            return _select.Options.Select(option => option.Text).ToList();
        }

        public bool HasOption(string optionText)
        {
            return _select.Options.Any(option => option.Text.Equals(optionText));
        }

        public bool HasOptionContaining(string partialText)
        {
            return _select.Options.Any(option => option.Text.Contains(partialText));
        }
    }
} 