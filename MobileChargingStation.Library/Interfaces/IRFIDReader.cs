using MobileChargingStation.Library.Data;

namespace MobileChargingStation.Library.Interfaces;

public interface IRFIDReader
{
    public int id { get; set; }
    public event EventHandler<RFIDEventArgs>? RFIDDetectedEvent;
    public void OnRfidRead(int id);
}
