using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace SalesforceAutomation.Tests.Utils
{
    public static class FileUtils
    {
        /// <summary>
        /// Create a directory if it doesn't exist
        /// </summary>
        /// <param name="directoryPath">Path to directory</param>
        public static void CreateDirectoryIfNotExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
        
        /// <summary>
        /// Write text to file
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <param name="content">Content to write</param>
        /// <param name="append">Whether to append or overwrite</param>
        public static void WriteToFile(string filePath, string content, bool append = false)
        {
            CreateDirectoryIfNotExists(Path.GetDirectoryName(filePath));
            
            if (append)
            {
                File.AppendAllText(filePath, content);
            }
            else
            {
                File.WriteAllText(filePath, content);
            }
        }
        
        /// <summary>
        /// Read text from file
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>File content as string</returns>
        public static string ReadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }
            
            return File.ReadAllText(filePath);
        }
        
        /// <summary>
        /// Read lines from file
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <returns>Array of lines</returns>
        public static string[] ReadLinesFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }
            
            return File.ReadAllLines(filePath);
        }
        
        /// <summary>
        /// Delete file if it exists
        /// </summary>
        /// <param name="filePath">Path to file</param>
        public static void DeleteFileIfExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        
        /// <summary>
        /// Save CSV data to file
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <param name="headers">CSV headers</param>
        /// <param name="rows">CSV data rows</param>
        public static void SaveCsvFile(string filePath, string[] headers, List<string[]> rows)
        {
            StringBuilder sb = new StringBuilder();
            
            // Add headers
            sb.AppendLine(string.Join(",", headers));
            
            // Add rows
            foreach (var row in rows)
            {
                sb.AppendLine(string.Join(",", row));
            }
            
            WriteToFile(filePath, sb.ToString());
        }
        
        /// <summary>
        /// Get all files in directory with specific extension
        /// </summary>
        /// <param name="directoryPath">Path to directory</param>
        /// <param name="extension">File extension (e.g., ".csv")</param>
        /// <param name="searchSubdirectories">Whether to search subdirectories</param>
        /// <returns>Array of file paths</returns>
        public static string[] GetFilesWithExtension(string directoryPath, string extension, bool searchSubdirectories = false)
        {
            if (!Directory.Exists(directoryPath))
            {
                return new string[0];
            }
            
            SearchOption searchOption = searchSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            return Directory.GetFiles(directoryPath, $"*{extension}", searchOption);
        }
        
        /// <summary>
        /// Get most recent file in directory with specific extension
        /// </summary>
        /// <param name="directoryPath">Path to directory</param>
        /// <param name="extension">File extension (e.g., ".csv")</param>
        /// <returns>Path to most recent file or null if no files found</returns>
        public static string GetMostRecentFile(string directoryPath, string extension)
        {
            string[] files = GetFilesWithExtension(directoryPath, extension);
            
            if (files.Length == 0)
            {
                return null;
            }
            
            return files.OrderByDescending(f => new FileInfo(f).LastWriteTime).First();
        }
    }
} 