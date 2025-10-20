using System;
using MobileChargingStation.Library.Interfaces;

namespace MobileChargingStation.Library.Data;

public class Door : IDoor
{
    public event EventHandler? DoorOpenedEvent;
    public event EventHandler? DoorClosedEvent;

    public bool IsDoorLocked { get; private set; }

    public void Lock()
    {
        IsDoorLocked = true;
        Console.WriteLine("Døren er nu låst.");
    }

    public void Unlock()
    {
        IsDoorLocked = false;
        Console.WriteLine("Døren er nu låst op.");
    }

    // Simulerer at en bruger åbner døren
    public void SimulateDoorOpen()
    {
        if (!IsDoorLocked)
        {
            Console.WriteLine("Bruger åbner døren...");
            DoorOpenedEvent?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Console.WriteLine("Kan ikke åbne, døren er låst!");
        }
    }

    // Simulerer at en bruger lukker døren
    public void SimulateDoorClose()
    {
        Console.WriteLine("Bruger lukker døren...");
        DoorClosedEvent?.Invoke(this, EventArgs.Empty);
    }
}
