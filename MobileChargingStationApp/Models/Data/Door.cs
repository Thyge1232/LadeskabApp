

namespace MobileCharginStation.Models;

public class Door
{
    public event EventHandler? DoorOpenedEvent;
    public event EventHandler? DoorClosedEvent;
    
    private bool IsDoorOpen;
    
}