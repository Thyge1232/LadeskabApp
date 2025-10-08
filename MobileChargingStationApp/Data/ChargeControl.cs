using MobileCharginStation.Interfaces;

namespace MobileCharginStation.Data;

public class ChargeControl : IChargeControl
{
    private readonly IUSBCharger _usbCharger;

    public event EventHandler<CurrentEventArgs>? ChargingFinishedEvent;
    public event EventHandler<CurrentEventArgs>? ChargingErrorEvent;

    public bool Connected => _usbCharger.Connected;

    public ChargeControl(IUSBCharger usbCharger)
    {
        _usbCharger = usbCharger;
        _usbCharger.CurrentValueEvent += HandleCurrentValue;
    }

    public void StartCharge()
    {
        _usbCharger.StartCharge();
    }

    public void StopCharge()
    {
        _usbCharger.StopCharge();
    }

    private void HandleCurrentValue(object? sender, CurrentEventArgs e)
    {
        // Logik for at håndtere strømværdier
        if (e.Current == 0.0)
        {
            // Opladning færdig
            ChargingFinishedEvent?.Invoke(this, e);
        }
        else if (e.Current > 500.0) // Eksempel på fejlgrænse
        {
            // Fejl i opladning
            ChargingErrorEvent?.Invoke(this, e);
        }
    }
}