using Bogus;
using System;
using System.Collections.Generic;

namespace SalesforceAutomation.Tests.Utils
{
    public static class FakerDataFactory
    {
        private static readonly Faker _faker = new Faker("en");
        
        /// <summary>
        /// Generate fake account data
        /// </summary>
        /// <param name="includeTimestamp">Include timestamp in account name</param>
        /// <returns>Dictionary with account data</returns>
        public static Dictionary<string, string> GenerateAccountData(bool includeTimestamp = true)
        {
            var company = _faker.Company;
            string accountName = includeTimestamp 
                ? $"{company.CompanyName()} {DateUtils.GetTimestamp()}" 
                : company.CompanyName();
                
            return new Dictionary<string, string>
            {
                { "AccountName", accountName },
                { "Phone", _faker.Phone.PhoneNumber("###-###-####") },
                { "Website", _faker.Internet.Url() },
                { "Industry", GetRandomIndustry() },
                { "Description", _faker.Lorem.Paragraph() },
                { "BillingStreet", _faker.Address.StreetAddress() },
                { "BillingCity", _faker.Address.City() },
                { "BillingState", _faker.Address.StateAbbr() },
                { "BillingZip", _faker.Address.ZipCode() },
                { "BillingCountry", _faker.Address.Country() }
            };
        }
        
        /// <summary>
        /// Generate fake contact data
        /// </summary>
        /// <returns>Dictionary with contact data</returns>
        public static Dictionary<string, string> GenerateContactData()
        {
            var firstName = _faker.Name.FirstName();
            var lastName = _faker.Name.LastName();
            
            return new Dictionary<string, string>
            {
                { "FirstName", firstName },
                { "LastName", lastName },
                { "Email", _faker.Internet.Email(firstName, lastName) },
                { "Phone", _faker.Phone.PhoneNumber("###-###-####") },
                { "MobilePhone", _faker.Phone.PhoneNumber("###-###-####") },
                { "Title", _faker.Name.JobTitle() },
                { "Department", _faker.Commerce.Department() },
                { "MailingStreet", _faker.Address.StreetAddress() },
                { "MailingCity", _faker.Address.City() },
                { "MailingState", _faker.Address.StateAbbr() },
                { "MailingZip", _faker.Address.ZipCode() },
                { "MailingCountry", _faker.Address.Country() }
            };
        }
        
        /// <summary>
        /// Generate fake opportunity data
        /// </summary>
        /// <returns>Dictionary with opportunity data</returns>
        public static Dictionary<string, string> GenerateOpportunityData()
        {
            return new Dictionary<string, string>
            {
                { "OpportunityName", $"{_faker.Commerce.ProductName()} Opportunity {DateUtils.GetTimestamp()}" },
                { "Amount", _faker.Finance.Amount(1000, 100000, 2).ToString() },
                { "CloseDate", _faker.Date.Future(1).ToString("MM/dd/yyyy") },
                { "Stage", GetRandomSalesStage() },
                { "Type", GetRandomOpportunityType() },
                { "LeadSource", GetRandomLeadSource() },
                { "Description", _faker.Lorem.Paragraph() }
            };
        }
        
        /// <summary>
        /// Get a random Salesforce industry
        /// </summary>
        /// <returns>Random industry</returns>
        public static string GetRandomIndustry()
        {
            string[] industries = 
            {
                "Agriculture", "Apparel", "Banking", "Biotechnology", "Chemicals", 
                "Communications", "Construction", "Consulting", "Education", "Electronics", 
                "Energy", "Engineering", "Entertainment", "Environmental", "Finance", 
                "Food & Beverage", "Government", "Healthcare", "Hospitality", "Insurance", 
                "Machinery", "Manufacturing", "Media", "Not For Profit", "Recreation", 
                "Retail", "Shipping", "Technology", "Telecommunications", "Transportation", 
                "Utilities"
            };
            
            return _faker.PickRandom(industries);
        }
        
        /// <summary>
        /// Get a random Salesforce sales stage
        /// </summary>
        /// <returns>Random sales stage</returns>
        public static string GetRandomSalesStage()
        {
            string[] stages = 
            {
                "Prospecting", "Qualification", "Needs Analysis", "Value Proposition", 
                "Id. Decision Makers", "Perception Analysis", "Proposal/Price Quote", 
                "Negotiation/Review", "Closed Won", "Closed Lost"
            };
            
            return _faker.PickRandom(stages);
        }
        
        /// <summary>
        /// Get a random Salesforce opportunity type
        /// </summary>
        /// <returns>Random opportunity type</returns>
        public static string GetRandomOpportunityType()
        {
            string[] types = { "Existing Customer - Upgrade", "Existing Customer - Replacement", 
                              "Existing Customer - Downgrade", "New Customer" };
            
            return _faker.PickRandom(types);
        }
        
        /// <summary>
        /// Get a random Salesforce lead source
        /// </summary>
        /// <returns>Random lead source</returns>
        public static string GetRandomLeadSource()
        {
            string[] sources = { "Web", "Phone Inquiry", "Partner Referral", "Purchased List", 
                                "Other" };
            
            return _faker.PickRandom(sources);
        }
    }
} 