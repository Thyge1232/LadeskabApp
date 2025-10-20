using NUnit.Framework;
using NSubstitute;
using MobileChargingStation.Library.Controllers;
using MobileChargingStation.Library.Interfaces;
using MobileChargingStation.Library.Data;


namespace MobileChargingStationApp.Tests
{
    public class ChargeControlTest
    {
        private ChargeControl _uut;
        private IDisplay _display;
        private IUSBCharger _charger;

        [SetUp]
        public void Setup()
        {
            _display = Substitute.For<IDisplay>();
            _charger = Substitute.For<IUSBCharger>();


            _uut = new ChargeControl(_charger, _display);
        }


        [Test]
        public void StartChargeCalled()
        {
            // Arrange 

            // Act
            _uut.StartCharge();

            // Assert
            _charger.Received(1).StartCharge();
        }

        [Test]
        public void StopChargeCalled()
        {
            // Arrange 

            // Act
            _uut.StopCharge();

            // Assert
            _charger.Received(1).StopCharge();
        }

        [Test]
        public void ChargerIsConnected()
        {
            // Arrange
            _charger.Connected.Returns(true);

            // Act
            var result = _uut.Connected;

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void HandleCurrentValue_250mA()
        {
            // Arrange
            var currentArgs = new CurrentEventArgs { Current = 250.0 };

            // Act
            _charger.CurrentValueEvent += Raise.EventWith(_charger, currentArgs);

            // Assert
            _display.Received(1).ShowChargingInProgress();
        }

        [Test]
        public void HandleCurrentValue_600mA()
        {
            // Arrange
            var currentArgs = new CurrentEventArgs { Current = 600.0 };

            // Act
            _charger.CurrentValueEvent += Raise.EventWith(_charger, currentArgs);

            // Assert
            _display.Received(1).ShowChargingError();
            _charger.Received(1).StopCharge(); 
        }

        [Test]
        public void HandleCurrentValue_3mA_RaisesChargingFinishedEvent()
        {
            // Arrange
            var currentArgs = new CurrentEventArgs { Current = 3.0 };
            bool eventWasRaised = false;

            _uut.ChargingFinishedEvent += (sender, args) => eventWasRaised = true;

            // Act
            _charger.CurrentValueEvent += Raise.EventWith(_charger, currentArgs);

            // Assert
            Assert.That(eventWasRaised, Is.True);
        }


        [Test]
        public void HandleCurrentValue_5mA_ShowsFullyCharged()
        {
            // Arrange
            var currentArgs = new CurrentEventArgs { Current = 5.0 };

            // Act
            _charger.CurrentValueEvent += Raise.EventWith(_charger, currentArgs);

            // Assert
            _display.Received(1).ShowFullyCharged();
        }

        [Test]
        public void HandleCurrentValue_5point1mA_ShowsChargingInProgress()
        {
            // Arrange
            var currentArgs = new CurrentEventArgs { Current = 5.1 };

            // Act
            _charger.CurrentValueEvent += Raise.EventWith(_charger, currentArgs);

            // Assert
            _display.Received(1).ShowChargingInProgress();
        }
    }
}
