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
            _testLogFile = "test_logfile.txt";
            _uut = new FileLogger();
        }

        [Test]
        public void Testlog()
        {
            Assert.Pass();
        }

    }
}