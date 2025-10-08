using MobileCharginStation.Data;

namespace MobileCharginStation.Interfaces;

public class CurrentEventArgs : EventArgs
{
    public double Current { get; set; }
}

public interface IUSBCharger
{
    event EventHandler<CurrentEventArgs>? CurrentValueEvent;
    double CurrentValue { get; }
    bool Connected { get; }
    void StartCharge();
    void StopCharge();
}