using System;
using System.Globalization;

namespace SalesforceAutomation.Tests.Utils
{
    public static class DateUtils
    {
        /// <summary>
        /// Get current date formatted as string
        /// </summary>
        /// <param name="format">Date format (default: "MM/dd/yyyy")</param>
        /// <returns>Formatted date string</returns>
        public static string GetCurrentDate(string format = "MM/dd/yyyy")
        {
            return DateTime.Now.ToString(format);
        }
        
        /// <summary>
        /// Get current date and time formatted as string
        /// </summary>
        /// <param name="format">DateTime format (default: "MM/dd/yyyy HH:mm:ss")</param>
        /// <returns>Formatted date time string</returns>
        public static string GetCurrentDateTime(string format = "MM/dd/yyyy HH:mm:ss")
        {
            return DateTime.Now.ToString(format);
        }
        
        /// <summary>
        /// Get timestamp for use in filenames
        /// </summary>
        /// <returns>Timestamp string (yyyyMMdd_HHmmss)</returns>
        public static string GetTimestamp()
        {
            return DateTime.Now.ToString("yyyyMMdd_HHmmss");
        }
        
        /// <summary>
        /// Add days to current date
        /// </summary>
        /// <param name="days">Number of days to add (can be negative)</param>
        /// <param name="format">Date format (default: "MM/dd/yyyy")</param>
        /// <returns>Formatted date string after adding days</returns>
        public static string AddDaysToCurrentDate(int days, string format = "MM/dd/yyyy")
        {
            return DateTime.Now.AddDays(days).ToString(format);
        }
        
        /// <summary>
        /// Parse date string to DateTime object
        /// </summary>
        /// <param name="dateString">Date string to parse</param>
        /// <param name="format">Date format (default: "MM/dd/yyyy")</param>
        /// <returns>DateTime object</returns>
        public static DateTime ParseDate(string dateString, string format = "MM/dd/yyyy")
        {
            return DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
        }
        
        /// <summary>
        /// Calculate the difference in days between two dates
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Number of days between dates</returns>
        public static int GetDaysBetween(DateTime startDate, DateTime endDate)
        {
            return (int)(endDate - startDate).TotalDays;
        }
        
        /// <summary>
        /// Get the first day of the current month
        /// </summary>
        /// <param name="format">Date format (default: "MM/dd/yyyy")</param>
        /// <returns>Formatted date string</returns>
        public static string GetFirstDayOfMonth(string format = "MM/dd/yyyy")
        {
            DateTime now = DateTime.Now;
            return new DateTime(now.Year, now.Month, 1).ToString(format);
        }
        
        /// <summary>
        /// Get the last day of the current month
        /// </summary>
        /// <param name="format">Date format (default: "MM/dd/yyyy")</param>
        /// <returns>Formatted date string</returns>
        public static string GetLastDayOfMonth(string format = "MM/dd/yyyy")
        {
            DateTime now = DateTime.Now;
            return new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month)).ToString(format);
        }
    }
} 