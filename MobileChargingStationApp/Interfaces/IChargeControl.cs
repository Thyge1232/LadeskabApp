using MobileCharginStation.Data;

namespace MobileCharginStation.Interfaces;

public interface IChargeControl
{
    bool Connected { get; }
    void StartCharge();
    void StopCharge();
    event EventHandler<CurrentEventArgs>? ChargingFinishedEvent;
    event EventHandler<CurrentEventArgs>? ChargingErrorEvent;
}