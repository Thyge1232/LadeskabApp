namespace MobileChargingStation.Library.Data;

public class RFIDEventArgs : EventArgs
{
    public int Rfid { get; set; } // Skal være int ifølge UML [OldId == ID]
}
