using MobileChargingStation.Library.Data;

namespace MobileChargingStation.Library.Interfaces;

public interface IUSBCharger
{
    event EventHandler<CurrentEventArgs>? CurrentValueEvent;
    double CurrentValue { get; }
    bool Connected { get; }
    void StartCharge();
    void StopCharge();
    void SimulateConnected(bool connected);
    void SimulateOverload(bool overload);
}
