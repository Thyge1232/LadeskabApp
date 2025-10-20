using NUnit.Framework;
using NSubstitute;
using MobileChargingStation.Library.Controllers;
using MobileChargingStation.Library.Interfaces;
using MobileChargingStation.Library.Data;
using NSubstitute.ReceivedExtensions;


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
        public void RfidDetected()
        {
            // Arrange
            _charger.Connected.Returns(true);

            // Act
            _rfid.RFIDDetectedEvent += Raise.EventWith(new RFIDEventArgs { Rfid = 42 });

            // Assert
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Locked));
            _door.Received(1).Lock();
            _charger.Received(1).StartCharge();
            _logger.Received(1).Log("Skab låst med RFID: 42");
            _display.Received().ShowInstruction(
                "Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op."
            );
        }

        [Test]
        public void DoorOpenedEvent()
        {
            // Arrange

            // Act
            _door.DoorOpenedEvent += Raise.EventWith(_door, EventArgs.Empty);

            // Assert
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.DoorOpen));
            _display.Received(1).ShowInstruction("Tilslut telefon eller tag telefon");
        }

        [Test]
        public void DoorClosedEvent()
        {
            // Arrange
            _door.DoorOpenedEvent += Raise.EventWith(_door, EventArgs.Empty);

            // Act
            _door.DoorClosedEvent += Raise.EventWith(_door, EventArgs.Empty);

            // Assert
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Available));
            _display.Received(2).ShowInstruction("Indlæs RFID");
        }


        [Test]
        public void ChargingFinished_WhenLocked_ShowsTakePhoneMessage()
        {
            // Arrange - Get to Locked state first
            _charger.Connected.Returns(true);
            _rfid.RFIDDetectedEvent += Raise.EventWith(new RFIDEventArgs { Rfid = 42 });

            // Act
            _charger.ChargingFinishedEvent += Raise.EventWith(_charger, new CurrentEventArgs { Current = 2.5 });

            // Assert
            _display.Received(1).ShowInstruction("Tag telefon");
        }

        [Test]
        public void ChargingError_WhenLocked_ShowsErrorMessage()
        {
            // Arrange - Get to Locked state first
            _charger.Connected.Returns(true);
            _rfid.RFIDDetectedEvent += Raise.EventWith(new RFIDEventArgs { Rfid = 42 });

            // Act
            _charger.ChargingErrorEvent += Raise.EventWith(_charger, new CurrentEventArgs { Current = 600.0 });

            // Assert
            _display.Received(1).ShowInstruction("Fejl. Fjern telefon");
        }


        [Test]
        public void RfidDetected_LockedWithCorrectId_UnlocksAndStopsCharging()
        {
            // Arrange - Lock first with ID 42
            _charger.Connected.Returns(true);
            _rfid.RFIDDetectedEvent += Raise.EventWith(new RFIDEventArgs { Rfid = 42 });

            // Act - Use same ID to unlock
            _rfid.RFIDDetectedEvent += Raise.EventWith(new RFIDEventArgs { Rfid = 42 });

            // Assert
            _charger.Received(1).StopCharge();
            _door.Received(1).Unlock();
            _logger.Received(1).Log("Skab låst op med RFID: 42");
            _display.Received(1).ShowInstruction("Tag din telefon ud af skabet og luk døren");
        }

        [Test]
        public void RfidDetected_LockedWithWrongId_ShowsErrorMessage()
        {
            // Arrange - Lock with ID 42
            _charger.Connected.Returns(true);
            _rfid.RFIDDetectedEvent += Raise.EventWith(new RFIDEventArgs { Rfid = 42 });

            // Act - Try to unlock with different ID
            _rfid.RFIDDetectedEvent += Raise.EventWith(new RFIDEventArgs { Rfid = 99 });

            // Assert
            _display.Received(1).ShowInstruction("Forkert RFID tag");
            _door.DidNotReceive().Unlock();
        }

        [Test]
        public void RfidDetected_AvailableButNotConnected_ShowsConnectionError()
        {
            // Arrange
            _charger.Connected.Returns(false);

            // Act
            _rfid.RFIDDetectedEvent += Raise.EventWith(new RFIDEventArgs { Rfid = 42 });

            // Assert
            _display.Received(1).ShowInstruction("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
            _door.DidNotReceive().Lock();
        }
    }
}
