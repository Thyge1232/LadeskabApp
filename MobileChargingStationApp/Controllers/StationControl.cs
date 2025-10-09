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
        private ILogger _logger;
        private IDisplay _display;
        private IRFIDReader _rfidReader;


        public StationControl(IDoor door, IRFIDReader rfidReader, IDisplay display, IChargeControl chargeControl, ILogger logger)
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

         private void HandleRfidDetectedEvent(object? sender, RFIDEventArgs e)
        {
            int id = Convert.ToInt32(e.Rfid);
            RfidDetected(id);
        }

        private void HandleDoorOpenedEvent(object? sender, EventArgs e)
        {
            if (_state == LadeskabState.Available)
            {
                 _state = LadeskabState.DoorOpen;
                _display.ShowInstruction("Tilslut telefon");
            }
        }

        private void HandleDoorClosedEvent(object? sender, EventArgs e)
        {
            if (_state == LadeskabState.DoorOpen)
            {
                 _state = LadeskabState.Available;
                _display.ShowInstruction("Indlæs RFID");
            }
        }

        private void HandleChargingFinishedEvent(object? sender, EventArgs e) //Ikke med på STM diagram
        {
            if (_state == LadeskabState.Locked)
            {
               // Fjernet  "_state = LadeskabState.DoorOpen;" - Skabet burde ikke åbne automatisk ifølge STM og kun ved korrekt RFID Skan
                _display.ShowInstruction("Tag telefon");
            }
        }

        private void HandleChargingErrorEvent(object? sender, EventArgs e)
        {
            if (_state == LadeskabState.Locked)
            {
               // Fjernet  "_state = LadeskabState.DoorOpen;" - Skabet burde ikke åbne automatisk ifølge STM og kun ved korrekt RFID Skan
               // Hvis der er en fejl vises fejl, og man skanner RFID og fjerner
                _display.ShowInstruction("Fejl. Fjern telefon");
            }
        }


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

                        _logger.Log($"Skab låst med RFID: {id}");

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

                        _logger.Log($"Skab låst op med RFID: {id}");

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
