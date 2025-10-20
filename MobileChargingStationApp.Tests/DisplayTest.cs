using NUnit.Framework;
using NSubstitute;
using MobileChargingStation.Library.Controllers;
using MobileChargingStation.Library.Interfaces;
using MobileChargingStation.Library.Data;



namespace MobileChargingStationApp.Tests
{
    public class DisplayTest
    {
        private Display _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Display();
        }

        [Test]
        public void ShowChargingInProgress()
        {
            // Arrange

            // Act
            _uut.ShowChargingInProgress();

            // Assert
            Assert.That(_uut.ChargeStatus, Is.EqualTo("Opladning i gang"));
        }

        [Test]
        public void ShowFullyCharged()
        {
            // Arrange 

            // Act
            _uut.ShowFullyCharged();

            // Assert
            Assert.That(_uut.ChargeStatus, Is.EqualTo("Telefon fuldt opladet"));
        }

        [Test]
        public void ShowChargingError()
        {
            // Arrange
    
            // Act
            _uut.ShowChargingError();

            // Assert
            Assert.That(_uut.ChargeStatus, Is.EqualTo("Fejl ved opladning"));
        }

        [Test]
        public void ClearChargeStatus()
        {
            // Arrange
            _uut.ShowChargingInProgress();

            // Act
            _uut.ClearChargeStatus();

            // Assert
            Assert.That(_uut.ChargeStatus, Is.EqualTo(""));
        }

        [Test]
        public void ShowInstruction()
        {   
            // Arrange
            _uut.ClearChargeStatus();

            // Act
            _uut.ShowInstruction("Test besked");

            // Assert
            Assert.That(_uut.ChargeStatus, Is.EqualTo(""));
        }

        [Test]
        public void ShowInstruction2()
        {
            // Arrange
            _uut.ShowChargingError();

            // Act
            _uut.ShowInstruction("Test besked 2");

            // Assert
            Assert.That(_uut.ChargeStatus, Is.EqualTo("Fejl ved opladning"));
        }
    }
}
