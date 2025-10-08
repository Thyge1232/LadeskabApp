using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileCharginStation.Interfaces;
using MobileCharginStation.Data;


namespace MobileCharginStation.Controllers
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };
        // Her mangler flere member variable
        private LadeskabState _state;
        private IChargeControl _charger;
        private int _oldId;
        private IDoor _door;
        private ILogger _fileLogger;
        private IDisplay _display;
        private IRFIDReader _rfidReader;


        public StationControl(IDoor door, IRfidReader rfidReader, IDisplay display, IChargeControl chargeControl, ILogger logger)
        {
            _door = door;
            _rfidReader = rfidReader;
            _display = display;
            _charger = chargeControl;
            _logger = logger;

            //Abonner  på event
            _rfidReader.RFIDDetectedEvent += HandleRfidDetectedEvent;
            _door.DoorOpenedEvent += HandleDoorOpenedEvent;
            _door.DoorClosedEvent += HandleDoorClosedEvent;
            _charger.ChargingFinishedEvent += HandleChargingFinishedEvent;
            _charger.ChargingErrorEvent += HandleChargingErrorEvent;

            //sæt start tilstand
            _state = LadeskabState.Available;
            _display.ShowInstruction("Indlæs RFID");

        }

        //Event handlers

        void HandleRfidDetectedEvent()


        // Her mangler constructor
        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.Lock();
                        _charger.StartCharge();
                        _oldId = id;

                        _fileLogger.Log($"Skab låst med RFID: {id}");

                        _display.ShowInstruction("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.ShowInstruction("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }
                    break;
                case LadeskabState.DoorOpen:
                    // Ignore
                    break;
                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.Unlock();

                        _fileLogger.Log($"Skab låst med RFID: {id}");

                        _display.ShowInstruction("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.ShowInstruction("Forkert RFID tag");
                    }
                    break;
            }
        }
        // Her mangler de andre trigger handlere
    }
}
