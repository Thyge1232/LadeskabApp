using MobileCharginStation.Interfaces;

namespace MobileCharginStation.Models;

public class Door : IDoor
{
    public event EventHandler? DoorOpenedEvent;
    public event EventHandler? DoorClosedEvent;

    private bool IsDoorLocked;

    public void Lock()
    {
        IsDoorLocked = true;
        DoorOpenedEvent?.Invoke(this, EventArgs.Empty);
    }
    
    public void Unlock()
    {
        IsDoorLocked = false;
        DoorClosedEvent?.Invoke(this, EventArgs.Empty);
    }

}
