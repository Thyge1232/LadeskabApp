using MobileCharginStation.Interfaces;

namespace MobileCharginStation.Data;

public class ChargeControl : IChargeControl
{
    private readonly IUSBCharger _usbCharger;
    private readonly IDisplay _display;


    public event EventHandler<CurrentEventArgs>? ChargingFinishedEvent;
    public event EventHandler<CurrentEventArgs>? ChargingErrorEvent;

    public bool Connected => _usbCharger.Connected;

    public ChargeControl(IUSBCharger usbCharger, IDisplay display)
    {
        _usbCharger = usbCharger;
        _display = display;
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
        if (e.Current > 500.0) // Fejl i opladning, stop ladning
        {
            _display.ShowChargingError(); // Viser fejl på display
            _usbCharger.StopCharge();
            ChargingErrorEvent?.Invoke(this, e);
            return;
        }

        if (e.Current > 0.0 && e.Current <= 5.0) // Fuld opladet, stop ladning
        {
            _display.ShowFullyCharged(); // Viser fuld opladning på display
            _usbCharger.StopCharge();
            ChargingFinishedEvent?.Invoke(this, e);
            return;
        }

        if (e.Current > 5.0 && e.Current <= 500.0) // Opladning foregår normalt - display viser det
        {
            _display.ShowChargingInProgress(); // Viser opladning på display
            return;
        }

        if (e.Current == 0.0) // Ingen forbindelse til nogen tlf, eller ladning ikke startet
        {
            _display.ClearChargeStatus(); // Rydder display
        }


    }
}