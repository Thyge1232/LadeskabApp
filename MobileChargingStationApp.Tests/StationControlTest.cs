using NUnit.Framework;
using NSubstitute;
using MobileChargingStation.Library.Controllers;
using MobileChargingStation.Library.Interfaces;
using MobileChargingStation.Library.Data;


namespace MobileChargingStationApp.Tests
{
    public class StationControlTests
    {
        private StationControl _uut;
        private IDoor _door;
        private IRFIDReader _rfid;
        private IDisplay _display;
        private IChargeControl _charger;
        private ILogger _logger;

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _rfid = Substitute.For<IRFIDReader>();
            _display = Substitute.For<IDisplay>();
            _charger = Substitute.For<IChargeControl>();
            _logger = Substitute.For<ILogger>();

            _uut = new StationControl(_door, _rfid, _display, _charger, _logger);
        }

        [Test]
        public void RfidDetected_AvailableAndConnected_EntersLockedState()
        {
            // Arrange
            _charger.Connected.Returns(true);

            // Act
            _rfid.RFIDDetectedEvent += Raise.EventWith(new RFIDEventArgs { Rfid = 42 });

            // Assert
            _door.Received(1).Lock();
            _charger.Received(1).StartCharge();
            _logger.Received(1).Log("Skab låst med RFID: 42");
            _display.Received().ShowInstruction(
                "Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op."
            );
        }
    }
}
