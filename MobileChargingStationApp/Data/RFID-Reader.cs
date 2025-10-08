using MobileCharginStation.Interfaces;

namespace MobileCharginStation.Data;

public class RFIDEventArgs : EventArgs
{
    public string Rfid { get; set; } = string.Empty;
}

public class RFIDReader : IRFIDReader
{
    public int id { get; set; }
    public event EventHandler<RFIDEventArgs>? RFIDDetectedEvent;

    public void OnRfidRead(int id)
    {
        this.id = id;
        RFIDDetectedEvent?.Invoke(this, new RFIDEventArgs { Rfid = id.ToString() });
    }
}