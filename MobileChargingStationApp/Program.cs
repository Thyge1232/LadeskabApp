using System;
using MobileChargingStation.Library.Interfaces;
using MobileChargingStation.Library.Data;
using MobileChargingStation.Library.Controllers;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
class Program
{
    static void Main(string[] args)
    {
        // Assemble your system here from all the classes
        Door door = new Door();
        RFIDReader rfidReader = new RFIDReader();
        Display display = new Display();
        FileLogger logger = new FileLogger();
        UsbChargerSimulator usbCharger = new UsbChargerSimulator();

        // Controller
        ChargeControl chargeControl = new ChargeControl(usbCharger, display);

        // Main controller
        StationControl stationControl = new StationControl(door, rfidReader, display, chargeControl, logger);

        bool finish = false;
        do
        {
            Console.WriteLine("\nIndtast kommando:");
            Console.WriteLine(" O: Åbn dør (Open)");
            Console.WriteLine(" C: Luk dør (Close)");
            Console.WriteLine(" R: Scan RFID (Read)");
            Console.WriteLine(" E: Afslut (Exit)");

            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) continue;

            switch (input[0])
            {
                case 'E':
                case 'e':
                    finish = true;
                    break;

                case 'O':
                case 'o':
                    door.SimulateDoorOpen();
                    break;

                case 'C':
                case 'c':
                    door.SimulateDoorClose();
                    break;

                case 'R':
                case 'r':
                    Console.WriteLine("Indtast RFID id: ");
                    string? idString = Console.ReadLine();

                    if (!string.IsNullOrEmpty(idString))
                    {
                        if (int.TryParse(idString, out int id))
                        {
                            rfidReader.OnRfidRead(id);
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

        } while (!finish);
    }
}