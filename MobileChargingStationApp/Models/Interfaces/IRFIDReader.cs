using MobileCharginStation.Models;

namespace MobileCharginStation.Interfaces;

public interface IRFIDReader
{
    public int id { get; set; }
    public event EventHandler<int>? RFIDDetectedEvent;
}

