using MobileCharginStation.Interfaces;

namespace MobileCharginStation.Data;

public class RFIDEventArgs : EventArgs
{
    public int Rfid { get; set; } // Skal være int ifølge UML [OldId == ID]
}

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