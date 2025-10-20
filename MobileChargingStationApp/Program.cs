using System;
using MobileChargingStation.Library.Interfaces;
using MobileChargingStation.Library.Data;
using MobileChargingStation.Library.Controllers;


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
            string? input;
                Console.WriteLine("\nIndtast kommando:");
                Console.WriteLine(" O: Åbn dør (Open)");
                Console.WriteLine(" C: Luk dør (Close)");
                Console.WriteLine(" R: Scan RFID (Read)");
                Console.WriteLine(" E: Afslut (Exit)");
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) continue;

            switch (input[0])
            {
                case 'E':
                    finish = true;
                    break;

                case 'O':
                    door.SimulateDoorOpen();
                    break;

                case 'C':
                    door.SimulateDoorClose();
                    break;

                case 'R':
                    System.Console.WriteLine("Indtast RFID id: ");
                    string? idString = System.Console.ReadLine();

                    int id = Convert.ToInt32(idString);
                    rfidReader.OnRfidRead(id);
                    break;

                default:
                    break;
            }

        } while (!finish);
    }
}
