using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SalesforceAutomation.Tests.Core;
using SalesforceAutomation.Tests.Utils;
using System;
using System.Threading;

namespace SalesforceAutomation.Tests.Pages
{
    public class AccountsPage : BasePage
    {
        // Locators
        private readonly By _accountsTab = By.XPath("//a[@title='Accounts' or contains(@title, 'Account') or contains(@href, '/Account/')]");
        private readonly By _appLauncher = By.XPath("//div[contains(@class,'appLauncher')]//button");
        private readonly By _appSearchBox = By.XPath("//input[contains(@placeholder,'Search apps')]");
        private readonly By _appAccountsItem = By.XPath("//a[.//p[text()='Accounts' or contains(text(), 'Account')]]");
        private readonly By _newButton = By.XPath("//a[@title='New' or contains(@title, 'New') or @aria-label='New' or text()='New']");
        private readonly By _accountNameField = By.XPath("//input[contains(@name,'Name')]");
        private readonly By _phoneField = By.XPath("//input[contains(@name,'Phone')]");
        private readonly By _websiteField = By.XPath("//input[contains(@name,'Website')]");
        private readonly By _industryDropdown = By.XPath("//button[contains(@aria-label,'Industry') or contains(@class, 'industryField')]");
        private readonly By _saveButton = By.XPath("//button[@name='SaveEdit' or text()='Save' or @title='Save']");
        private readonly By _toastMessage = By.XPath("//span[contains(@class,'toastMessage')]");
        
        public AccountsPage(IWebDriver driver) : base(driver)
        {
        }
        
        public void NavigateToAccountsTab()
        {
            try
            {
                // Try direct tab first
                if (IsElementPresent(_accountsTab))
                {
                    WaitAndClick(_accountsTab);
                    WaitForPageLoad();
                    return;
                }
                
                // Use App Launcher
                WaitAndClick(_appLauncher);
                WaitUtils.WaitForElementVisible(Driver, _appSearchBox);
                WaitAndSendKeys(_appSearchBox, "Account");
                Thread.Sleep(1000); // Wait for search results
                WaitAndClick(_appAccountsItem);
                WaitForPageLoad();
            }
            catch (Exception)
            {
                // Use URL navigation as fallback
                Driver.Navigate().GoToUrl(Driver.Url.Split("/lightning")[0] + "/lightning/o/Account/list");
                WaitForPageLoad();
            }
        }
        
        public void ClickNewButton()
        {
            try
            {
                WaitAndClick(_newButton);
                WaitForPageLoad();
            }
            catch (Exception)
            {
                // Try alternative approach if the button is not found
                string jsClick = "document.querySelector('a[title=\"New\"]').click();";
                ((IJavaScriptExecutor)Driver).ExecuteScript(jsClick);
                WaitForPageLoad();
            }
        }
        
        public void EnterAccountName(string accountName)
        {
            WaitAndSendKeys(_accountNameField, accountName);
        }
        
        public void EnterPhone(string phone)
        {
            WaitAndSendKeys(_phoneField, phone);
        }
        
        public void EnterWebsite(string website)
        {
            WaitAndSendKeys(_websiteField, website);
        }
        
        public void SelectIndustry(string industry)
        {
            try 
            {
                WaitAndClick(_industryDropdown);
                
                By industryOption = By.XPath($"//lightning-base-combobox-item[@data-value='{industry}' or contains(.//span, '{industry}')]");
                WaitAndClick(industryOption);
            }
            catch (Exception)
            {
                // Try alternative industry selection approach
                JavaScriptClick(_industryDropdown);
                Thread.Sleep(500);
                
                By genericIndustryOption = By.XPath($"//*[text()='{industry}' or contains(text(), '{industry}')]");
                JavaScriptClick(genericIndustryOption);
            }
        }
        
        public void ClickSaveButton()
        {
            WaitAndClick(_saveButton);
            WaitForPageLoad();
        }
        
        public bool IsAccountCreated()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                return wait.Until(d => {
                    try {
                        var element = d.FindElement(_toastMessage);
                        return element.Displayed && 
                            (element.Text.Contains("Account") || 
                             element.Text.Contains("created") || 
                             element.Text.Contains("success"));
                    }
                    catch {
                        return false;
                    }
                });
            }
            catch
            {
                return false;
            }
        }
        
        public void CreateNewAccount(string accountName, string phone, string website, string industry)
        {
            NavigateToAccountsTab();
            ClickNewButton();
            EnterAccountName(accountName);
            EnterPhone(phone);
            EnterWebsite(website);
            SelectIndustry(industry);
            ClickSaveButton();
        }
    }
} 