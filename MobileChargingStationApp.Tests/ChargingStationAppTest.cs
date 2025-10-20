using NUnit.Framework;
using MobileChargingStation.Library;

namespace MobileChargingStationApp.Tests
{
    public class ChargingStationAppTest
    {
        private ChargingStationApp _app;

        [SetUp]
        public void Setup()
        {
            _app = new ChargingStationApp();
        }

        [Test]
        public void ProcessInput_E_ShouldReturnTrue()
        {
            // Act
            var result = _app.ProcessInput("E");

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ProcessInput_e_ShouldReturnTrue()
        {
            // Act
            var result = _app.ProcessInput("e");

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ProcessInput_O_ShouldReturnFalse()
        {
            // Act
            var result = _app.ProcessInput("O");

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ProcessInput_C_ShouldReturnFalse()
        {
            // Act
            var result = _app.ProcessInput("C");

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ProcessInput_R_ShouldReturnFalse()
        {
            // Act
            var result = _app.ProcessInput("R");

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ProcessInput_EmptyString_ShouldReturnFalse()
        {
            // Act
            var result = _app.ProcessInput("");

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ProcessInput_Null_ShouldReturnFalse()
        {
            // Act
            var result = _app.ProcessInput(null);

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ProcessInput_InvalidCommand_ShouldReturnFalse()
        {
            // Act
            var result = _app.ProcessInput("X");

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void ProcessInput_LowerCase_ShouldWork()
        {
            // Act & Assert
            Assert.That(_app.ProcessInput("o"), Is.False);
            Assert.That(_app.ProcessInput("c"), Is.False);
            Assert.That(_app.ProcessInput("r"), Is.False);
            Assert.That(_app.ProcessInput("e"), Is.True);
        }
    }
}