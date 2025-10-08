using MobileCharginStation.Data;

namespace MobileCharginStation.Interfaces;

public interface IRFIDReader
{
    public int id { get; set; }
    public event EventHandler<RFIDEventArgs>? RFIDDetectedEvent;
    public void OnRfidRead(int id);
}

