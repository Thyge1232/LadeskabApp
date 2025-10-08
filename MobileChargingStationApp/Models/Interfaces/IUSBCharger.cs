using MobileCharginStation.Models;

namespace MobileCharginStation.Interfaces;

public class CurrentEventArgs : EventArgs
{
    public double Current { get; set; }
}

public interface IUSBCharger
{
    event EventHandler<CurrentEventArgs> CurrentValueEvent;
    void StartCharge();
    void StopCharge();
}