using MobileChargingStation.Library.Data;

namespace MobileChargingStation.Library.Interfaces;

public interface IChargeControl
{
    bool Connected { get; }
    void StartCharge();
    void StopCharge();
    event EventHandler<CurrentEventArgs>? ChargingFinishedEvent;
    event EventHandler<CurrentEventArgs>? ChargingErrorEvent;
}
