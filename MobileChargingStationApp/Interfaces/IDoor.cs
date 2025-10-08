using MobileCharginStation.Data;

namespace MobileCharginStation.Interfaces;

public interface IDoor
{
    void Lock();
    void Unlock();
    event EventHandler DoorOpenedEvent;
    event EventHandler DoorClosedEvent;
}