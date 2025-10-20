using NUnit.Framework;
using NSubstitute;
using MobileChargingStation.Library.Controllers;
using MobileChargingStation.Library.Interfaces;
using MobileChargingStation.Library.Data;



namespace MobileChargingStationApp.Tests
{
    public class DoorTest
    {
        private Door _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Door();
        }

        [Test]
        public void OpenDoor_ShouldSetIsOpenToTrue()
        {
            // Act
            _uut.Lock();

            // Assert
            Assert.That(_uut.IsDoorLocked, Is.True);
        }

        [Test]
        public void CloseDoor_ShouldSetIsOpenToFalse()
        {
            // Act
            _uut.Unlock();

            // Assert
            Assert.That(_uut.IsDoorLocked, Is.False);
        }

        [Test]
        public void SimulateDoorOpen()
        {
            // Act
            _uut.Unlock();

            // Arrange
            _uut.SimulateDoorOpen();

            // Assert
            Assert.That(_uut.IsDoorLocked, Is.False);
        }

        [Test]
        public void SimulateDoorClose()
        {
            // Act
            _uut.Lock();

            // Arrange
            _uut.SimulateDoorClose();

            // Assert
            Assert.That(_uut.IsDoorLocked, Is.True);
        }
    }
}