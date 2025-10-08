

namespace MobileCharginStation.Models;

public class StationControl
{
    private readonly Door door;

    public void DoorOpened()
    {
        door.Unlock();
    }

    public void DoorClosed()
    {
        door.Lock();
    }
}