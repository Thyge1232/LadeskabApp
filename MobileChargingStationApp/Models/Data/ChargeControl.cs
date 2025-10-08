using MobileCharginStation.Interfaces;

namespace MobileCharginStation.Models;

public class ChargeControl : IChargeControl
{
    private readonly IUSBCharger _usbCharger;

    public event EventHandler<CurrentEventArgs> ChargingFinishedEvent;
    public event EventHandler<CurrentEventArgs> ChargingErrorEvent;



    public void StartCharge()
    {
        _usbCharger.StartCharge();
    }

    public void StopCharge()
    {
        _usbCharger.StopCharge();
    }

}