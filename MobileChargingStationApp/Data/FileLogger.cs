using MobileCharginStation.Interfaces;
using System;


namespace MobileCharginStation.Data;

public class FileLogger : ILogger
{
    public string _logFilePath { get; set; } = "logfile.txt";
    public void Log(string message)
    {
        try
        {
            string logEntry = $"{DateTime.Now:HH:mm:ss}- {message}";

            using (StreamWriter writer = new StreamWriter(_logFilePath, true))
            {//StreamWriter med 'true' for at tilføje til filen
                writer.WriteLine(logEntry);
            }


        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error logging to file: {ex.Message}");
        }
    }
}