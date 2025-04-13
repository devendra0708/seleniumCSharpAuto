using OpenQA.Selenium;
using SalesforceAutomation.Tests.Core;

namespace SalesforceAutomation.Tests.Elements
{
    public class Table : BaseElement
    {
        private readonly string _thCss = "thead th:not(.ng-hide)";
        private readonly string _trCss = "tbody tr:not(.ng-hide):not(thead tr)";
        private readonly int _startRowIndex = 1;

        public Table(By locator) : base(locator)
        {
        }

        public IWebElement GetTableHead()
        {
            return GetElement().FindElement(By.CssSelector("thead"));
        }

        public IWebElement GetTableBody()
        {
            return GetElement().FindElement(By.CssSelector("tbody:not(.ng-hide)"));
        }

        public int GetTotalColumnsCount()
        {
            return GetElement().FindElements(By.CssSelector(_thCss)).Count;
        }

        public int GetTotalRowCount()
        {
            return GetTableBody().FindElements(By.CssSelector(_trCss)).Count;
        }

        public List<string> GetColumnHeaders()
        {
            return GetElement().FindElements(By.CssSelector(_thCss))
                         .Select(e => e.Text.Trim())
                         .ToList();
        }

        public IWebElement GetCellElement(int rowNum, int colNum)
        {
            return GetTableBody().FindElement(By.CssSelector($"tr:nth-child({rowNum}) td:nth-child({colNum})"));
        }

        public IWebElement GetCellElement(int rowNum, int colNum, string innerElCss)
        {
            return GetCellElement(rowNum, colNum).FindElement(By.CssSelector(innerElCss));
        }

        public IWebElement GetRowElement(int rowIndex)
        {
            return GetTableBody().FindElement(By.CssSelector($"tr:nth-child({rowIndex}):not(.ng-hide)"));
        }

        public List<string> GetRowValues(int rowIndex)
        {
            return GetRowElement(rowIndex)
                   .FindElements(By.CssSelector("td:not(.ng-hide)"))
                   .Select(e => e.Text.Trim())
                   .ToList();
        }

        public int GetColumnIndex(string columnHeaderText)
        {
            var headers = GetElement().FindElements(By.CssSelector(_thCss));
            for (int i = 0; i < headers.Count; i++)
            {
                if (headers[i].Text.Trim().Equals(columnHeaderText))
                {
                    return i + _startRowIndex;
                }
            }
            throw new Exception($"Column header '{columnHeaderText}' is not present in table");
        }

        public IWebElement GetCellElementByColumnHeader(int rowNum, string columnHeaderText)
        {
            int colNum = GetColumnIndex(columnHeaderText);
            return GetCellElement(rowNum, colNum);
        }

        public IWebElement GetCellElementByColumnHeader(int rowNum, string columnHeaderText, string innerElCss)
        {
            int colNum = GetColumnIndex(columnHeaderText);
            return GetCellElement(rowNum, colNum, innerElCss);
        }

        public int GetRowIndex(string columnHeaderText, string columnValue)
        {
            int columnIndex = GetColumnIndex(columnHeaderText);
            var columnValues = GetElement().FindElements(By.CssSelector($"tbody tr td:nth-child({columnIndex})"));

            for (int i = 0; i < columnValues.Count; i++)
            {
                if (columnValues[i].Text.Trim().Equals(columnValue))
                {
                    return i + _startRowIndex;
                }
            }
            throw new Exception($"Value '{columnValue}' is not present in column '{columnHeaderText}'.");
        }

        public bool IsColumnSortingPresent(string columnHeader)
        {
            return GetColumnHeaderElement(columnHeader)
                   .FindElements(By.CssSelector("span.glyphicon-chevron-up, span.glyphicon-chevron-down"))
                   .Count > 0;
        }

        public void SortColumn(string columnHeader, bool reqAscOrder = true)
        {
            var headerElement = GetColumnHeaderElement(columnHeader);
            if (IsColumnSortingPresent(columnHeader))
            {
                headerElement.Click();
            }

            bool hasAscOrder = headerElement.FindElements(By.CssSelector("span.glyphicon-chevron-up")).Count > 0;
            bool hasDescOrder = headerElement.FindElements(By.CssSelector("span.glyphicon-chevron-down")).Count > 0;

            if (reqAscOrder && hasDescOrder)
            {
                headerElement.Click();
            }
            else if (!reqAscOrder && hasAscOrder)
            {
                headerElement.Click();
            }
        }

        private IWebElement GetColumnHeaderElement(string columnHeaderText)
        {
            var headers = GetElement().FindElements(By.CssSelector(_thCss));
            return headers.FirstOrDefault(h => h.Text.Trim().Equals(columnHeaderText))
                   ?? throw new Exception($"Column header '{columnHeaderText}' not found");
        }
    }
} 