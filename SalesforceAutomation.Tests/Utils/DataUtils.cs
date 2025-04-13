
namespace SalesforceAutomation.Tests.Utils
{
    public static class DataUtils
    {
        private static readonly Random Random = new Random();
        
        /// <summary>
        /// Generate a random integer between min and max (inclusive)
        /// </summary>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>Random integer</returns>
        public static int GetRandomInt(int min, int max)
        {
            return Random.Next(min, max + 1);
        }
        
        /// <summary>
        /// Generate a random decimal between min and max
        /// </summary>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <param name="decimalPlaces">Number of decimal places</param>
        /// <returns>Random decimal</returns>
        public static decimal GetRandomDecimal(decimal min, decimal max, int decimalPlaces = 2)
        {
            decimal range = max - min;
            decimal randomValue = (decimal)Random.NextDouble() * range + min;
            return Math.Round(randomValue, decimalPlaces);
        }
        
        /// <summary>
        /// Get a random item from an array
        /// </summary>
        /// <typeparam name="T">Type of array items</typeparam>
        /// <param name="items">Array of items</param>
        /// <returns>Random item</returns>
        public static T GetRandomItem<T>(T[] items)
        {
            if (items == null || items.Length == 0)
            {
                throw new ArgumentException("Array cannot be null or empty");
            }
            
            return items[Random.Next(items.Length)];
        }
        
        /// <summary>
        /// Get a random item from a list
        /// </summary>
        /// <typeparam name="T">Type of list items</typeparam>
        /// <param name="items">List of items</param>
        /// <returns>Random item</returns>
        public static T GetRandomItem<T>(List<T> items)
        {
            if (items == null || items.Count == 0)
            {
                throw new ArgumentException("List cannot be null or empty");
            }
            
            return items[Random.Next(items.Count)];
        }
        
        /// <summary>
        /// Generate a random boolean value
        /// </summary>
        /// <returns>Random boolean</returns>
        public static bool GetRandomBoolean()
        {
            return Random.Next(2) == 1;
        }
        
        /// <summary>
        /// Generate a random date within a range
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Random date</returns>
        public static DateTime GetRandomDate(DateTime startDate, DateTime endDate)
        {
            TimeSpan timeSpan = endDate - startDate;
            TimeSpan randomTimeSpan = new TimeSpan((long)(Random.NextDouble() * timeSpan.Ticks));
            return startDate + randomTimeSpan;
        }
        
        /// <summary>
        /// Generate random test data for a contact
        /// </summary>
        /// <returns>Dictionary with contact data</returns>
        public static Dictionary<string, string> GenerateContactData()
        {
            string[] firstNames = { "John", "Jane", "Michael", "Emily", "David", "Sarah", "Robert", "Lisa" };
            string[] lastNames = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson" };
            
            string firstName = GetRandomItem(firstNames);
            string lastName = GetRandomItem(lastNames);
            
            return new Dictionary<string, string>
            {
                { "FirstName", firstName },
                { "LastName", lastName },
                { "Email", StringUtils.GenerateRandomEmail($"{firstName.ToLower()}{lastName.ToLower()}.com") },
                { "Phone", StringUtils.GenerateRandomPhoneNumber() },
                { "Title", GetRandomJobTitle() }
            };
        }
        
        /// <summary>
        /// Generate a random job title
        /// </summary>
        /// <returns>Random job title</returns>
        public static string GetRandomJobTitle()
        {
            string[] titles = {
                "Manager",
                "Director",
                "Consultant",
                "Analyst",
                "Engineer",
                "Developer",
                "Specialist",
                "Coordinator",
                "Administrator",
                "Executive"
            };
            
            return GetRandomItem(titles);
        }
        
        /// <summary>
        /// Generate random address data
        /// </summary>
        /// <returns>Dictionary with address data</returns>
        public static Dictionary<string, string> GenerateAddressData()
        {
            string[] streets = { "Main St", "Oak Ave", "Maple Rd", "Cedar Ln", "Pine Dr", "Elm St", "Washington Ave" };
            string[] cities = { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia", "San Antonio" };
            string[] states = { "NY", "CA", "IL", "TX", "AZ", "PA", "FL", "OH", "GA", "NC" };
            
            return new Dictionary<string, string>
            {
                { "Street", $"{GetRandomInt(100, 9999)} {GetRandomItem(streets)}" },
                { "City", GetRandomItem(cities) },
                { "State", GetRandomItem(states) },
                { "PostalCode", GetRandomInt(10000, 99999).ToString() }
            };
        }
    }
} 