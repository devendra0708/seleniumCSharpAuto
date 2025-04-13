using Microsoft.Extensions.Configuration;

namespace SalesforceAutomation.Tests.Config
{
    public static class TestSettings
    {
        private static IConfiguration? _configuration;

        private static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .Build();
                }
                return _configuration;
            }
        }

        public static string BaseUrl => Configuration["AppSettings:BaseUrl"] ?? "https://login.salesforce.com/";
        public static string Username => Configuration["AppSettings:Username"] ?? string.Empty;
        public static string Password => Configuration["AppSettings:Password"] ?? string.Empty;
        public static string Browser => Configuration["AppSettings:Browser"] ?? "Chrome";
        public static int ImplicitWait => int.Parse(Configuration["AppSettings:ImplicitWait"] ?? "10");
        public static int ExplicitWait => int.Parse(Configuration["AppSettings:ExplicitWait"] ?? "30");
        public static int PageLoadTimeout => int.Parse(Configuration["AppSettings:PageLoadTimeout"] ?? "60");
        public static string ReportPath => Configuration["AppSettings:ReportPath"] ?? "TestResults/Reports";
        public static string ScreenshotPath => Configuration["AppSettings:ScreenshotPath"] ?? "TestResults/Screenshots";
    }
} 