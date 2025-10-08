using MobileCharginStation.Interfaces;

namespace MobileCharginStation.Models;

public class RFIDReader : IRFIDReader
{
    public int id { get; set; }
    public event EventHandler<int>? RFIDDetectedEvent{ add { } remove { } }
}