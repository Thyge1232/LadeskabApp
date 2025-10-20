using NUnit.Framework;
using MobileChargingStation.Library.Data;
using System.IO;

namespace MobileChargingStationApp.Tests
{
    public class FileLoggerTest
    {
        private FileLogger _uut;
        private string _testLogFile;

        [SetUp]
        public void Setup()
        {
            _uut = new FileLogger();
        }

        [Test]
        public void Testlog()
        {
            // Arrange
            var message = "Dette er en test logbesked.";
            // Act
            _uut.Log(message);

            // Assert
            Assert.That(File.ReadAllText("logfile.txt"), Does.Contain(message));
        }

        [Test]
        public void TestlogFailure()
        {
            // Arrange
            using var fileStream = new FileStream("logfile.txt", FileMode.Create, FileAccess.Write, FileShare.None);
            var message = "Test message";
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            _uut.Log(message);

            // Assert
            var consoleOutput = sw.ToString();
            Assert.That(consoleOutput, Does.Contain("Error logging to file"));
            
            Console.SetOut(Console.Out);
        }


    }
}