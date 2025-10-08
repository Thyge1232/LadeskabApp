using MobileCharginStation.Interfaces;

namespace MobileCharginStation.Data;

public class USBCharger : IUSBCharger
{
    public double CurrentValue { get; }
    public bool Connected { get; }
    public event EventHandler<CurrentEventArgs>? CurrentValueEvent { add { } remove { } }

    public void StartCharge()
    {

    }

    public void StopCharge()
    {
        
    }
}