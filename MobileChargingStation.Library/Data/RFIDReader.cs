using MobileChargingStation.Library.Interfaces;

namespace MobileChargingStation.Library.Data;

public class RFIDReader : IRFIDReader
{
    public int id { get; set; }
    public event EventHandler<RFIDEventArgs>? RFIDDetectedEvent;

    public void OnRfidRead(int id)
    {
        this.id = id;
        RFIDDetectedEvent?.Invoke(this, new RFIDEventArgs { Rfid = id});
    }
}
