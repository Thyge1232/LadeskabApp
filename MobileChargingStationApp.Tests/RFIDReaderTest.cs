using NUnit.Framework;
using NSubstitute;
using MobileChargingStation.Library.Controllers;
using MobileChargingStation.Library.Interfaces;
using MobileChargingStation.Library.Data;
using NSubstitute.ReceivedExtensions;


namespace MobileChargingStationApp.Tests
{
    public class RFIDReaderTest
    {
        private RFIDReader _rfidReader;

        [SetUp]
        public void Setup()
        {
            _rfidReader = new RFIDReader();
        }

        [Test]
        public void OnRfidRead()
        {
            // Arrange
            int testRfid = 12345;
            bool eventRaised = false;

            _rfidReader.RFIDDetectedEvent += (sender, args) =>
            {
                eventRaised = true;
            };

            // Act
            _rfidReader.OnRfidRead(testRfid);

            // Assert
            Assert.That(eventRaised, Is.True);
        }
    }
}