using System;
using MobileChargingStation.Library.Data;
using MobileChargingStation.Library.Controllers;
using MobileChargingStation.Library.Interfaces;

namespace MobileChargingStation.Library;

public class ChargingStationApp
{
    private readonly Door _door;
    private readonly RFIDReader _rfidReader;
    private readonly Display _display;
    private readonly StationControl _stationControl;

    public ChargingStationApp()
    {
        // Assemble your system here from all the classes
        _door = new Door();
        _rfidReader = new RFIDReader();
        _display = new Display();
        var logger = new FileLogger();
        var usbCharger = new UsbChargerSimulator();

        // Controller
        var chargeControl = new ChargeControl(usbCharger, _display);

        // Main controller
        _stationControl = new StationControl(_door, _rfidReader, _display, chargeControl, logger);
    }

    public void Run()
    {
        bool finish = false;
        do
        {
            Console.WriteLine("\nIndtast kommando:");
            Console.WriteLine(" O: Åbn dør (Open)");
            Console.WriteLine(" C: Luk dør (Close)");
            Console.WriteLine(" R: Scan RFID (Read)");
            Console.WriteLine(" E: Afslut (Exit)");
            
            string? input = Console.ReadLine();
            finish = ProcessInput(input);
        } while (!finish);
    }

    public bool ProcessInput(string? input)
    {
        if (string.IsNullOrEmpty(input)) return false;

        switch (input[0])
        {
            case 'E':
            case 'e':
                return true;

            case 'O':
            case 'o':
                _door.SimulateDoorOpen();
                break;

            case 'C':
            case 'c':
                _door.SimulateDoorClose();
                break;

            case 'R':
            case 'r':
                Console.WriteLine("Indtast RFID id: ");
                string? idString = Console.ReadLine();

                if (!string.IsNullOrEmpty(idString))
                {
                    if (int.TryParse(idString, out int id))
                    {
                        _rfidReader.OnRfidRead(id);
                    }
                    else
                    {
                        Console.WriteLine("Ugyldigt RFID id. Indtast et tal.");
                    }
                }
                break;

            default:
                Console.WriteLine("Ugyldig kommando. Prøv igen.");
                break;
        }
        return false;
    }
}