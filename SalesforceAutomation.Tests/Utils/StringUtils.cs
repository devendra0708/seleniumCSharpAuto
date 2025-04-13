using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SalesforceAutomation.Tests.Utils
{
    public static class StringUtils
    {
        private static readonly Random Random = new Random();
        
        /// <summary>
        /// Generate random string of specified length
        /// </summary>
        /// <param name="length">Length of string to generate</param>
        /// <param name="includeSpecialChars">Whether to include special characters</param>
        /// <returns>Random string</returns>
        public static string GenerateRandomString(int length, bool includeSpecialChars = false)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const string specialChars = "!@#$%^&*()_-+=<>?";
            
            string allowedChars = includeSpecialChars ? chars + specialChars : chars;
            
            return new string(Enumerable.Repeat(allowedChars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
        
        /// <summary>
        /// Generate random email address
        /// </summary>
        /// <param name="domainName">Domain name (default: "example.com")</param>
        /// <returns>Random email address</returns>
        public static string GenerateRandomEmail(string domainName = "example.com")
        {
            string username = GenerateRandomString(8).ToLower();
            return $"{username}@{domainName}";
        }
        
        /// <summary>
        /// Generate random phone number
        /// </summary>
        /// <param name="format">Phone number format (default: "###-###-####")</param>
        /// <returns>Random phone number</returns>
        public static string GenerateRandomPhoneNumber(string format = "###-###-####")
        {
            string result = format;
            
            for (int i = 0; i < format.Length; i++)
            {
                if (format[i] == '#')
                {
                    result = result.Remove(i, 1).Insert(i, Random.Next(10).ToString());
                }
            }
            
            return result;
        }
        
        /// <summary>
        /// Extract numbers from a string
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>String with only numbers</returns>
        public static string ExtractNumbers(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            
            return new string(input.Where(char.IsDigit).ToArray());
        }
        
        /// <summary>
        /// Extract text between two strings
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="startText">Start text</param>
        /// <param name="endText">End text</param>
        /// <returns>Extracted text</returns>
        public static string ExtractBetween(string input, string startText, string endText)
        {
            int startIndex = input.IndexOf(startText);
            if (startIndex < 0)
                return string.Empty;
                
            startIndex += startText.Length;
            
            int endIndex = input.IndexOf(endText, startIndex);
            if (endIndex < 0)
                return string.Empty;
                
            return input.Substring(startIndex, endIndex - startIndex);
        }
        
        /// <summary>
        /// Truncate string to specified length
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="maxLength">Maximum length</param>
        /// <param name="addEllipsis">Whether to add ellipsis</param>
        /// <returns>Truncated string</returns>
        public static string Truncate(string input, int maxLength, bool addEllipsis = true)
        {
            if (string.IsNullOrEmpty(input) || input.Length <= maxLength)
                return input;
                
            string result = input.Substring(0, maxLength);
            
            if (addEllipsis)
                result += "...";
                
            return result;
        }
        
        /// <summary>
        /// Remove HTML tags from a string
        /// </summary>
        /// <param name="input">Input string with HTML</param>
        /// <returns>Plain text</returns>
        public static string RemoveHtmlTags(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
                
            return Regex.Replace(input, "<.*?>", string.Empty);
        }
    }
} 