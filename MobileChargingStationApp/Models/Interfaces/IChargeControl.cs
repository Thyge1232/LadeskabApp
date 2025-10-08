using MobileCharginStation.Models;

namespace MobileCharginStation.Interfaces;

public interface IChargeControl
{
    void StartCharge();
    void StopCharge();
    event EventHandler<CurrentEventArgs> ChargingFinishedEvent;
    event EventHandler<CurrentEventArgs> ChargingErrorEvent;
}