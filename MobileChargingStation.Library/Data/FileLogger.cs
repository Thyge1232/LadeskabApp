using MobileChargingStation.Library.Interfaces;
using System;
using System.IO;

namespace MobileChargingStation.Library.Data;

public class FileLogger : ILogger
{
    public string _logFilePath { get; set; } = "logfile.txt";
    public void Log(string message)
    {
        try
        {
            string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";

            using (StreamWriter writer = new StreamWriter(_logFilePath, true))
            {
                writer.WriteLine(logEntry);
            }


        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error logging to file: {ex.Message}");
        }
    }
}
