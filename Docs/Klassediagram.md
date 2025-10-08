```mermaid
classDiagram
    direction TB
    
    %% ========== LAG 1: PRÆSENTATIONSLAG (ØVERST) ==========
    class StationControl {
        -oldId: int
        -door: IDoor
        -rfidReader: IRfidReader
        -display: IDisplay
        -chargeControl: IChargeControl
        -logger: ILogger
        +StationControl(IDoor, IRfidReader, IDisplay, IChargeControl, ILogger)
    }
    
    %% ========== LAG 2: FORRETNINGSLOGIK - INTERFACES  ==========
    class IDoor {
        <<interface>>
        +DoorOpenedEvent event
        +DoorClosedEvent event
        +LockDoor() void
        +UnlockDoor() void
    }
    
    class IRfidReader {
        <<interface>>
        +RfidDetectedEvent event
    }
    
    class IDisplay {
        <<interface>>
        +ShowInstruction(string) void
        +ShowChargingInProgress() void
        +ShowFullyCharged() void
        +ShowChargingError() void
        +ClearChargeStatus() void
    }
    
    class IChargeControl {
        <<interface>>
        +ChargingFinished event
        +ChargingError event
        +StartCharge() void
        +StopCharge() void
    }
    
    class IUsbCharger {
        <<interface>>
        +CurrentValueEvent event
        +CurrentValue: double
        +Connected: bool
        +StartCharge() void
        +StopCharge() void
    }
    
    class ILogger {
        <<interface>>
        +Log(message: string) void
    }
    
    %% ========== LAG 3: DATA IMPLEMENTERINGER  ==========
    class Door {
        +DoorOpenedEvent event
        +DoorClosedEvent event
        +LockDoor() void
        +UnlockDoor() void
    }
    
    class RfidReader {
        +RfidDetectedEvent event
    }
    
    class Display {
        +ShowInstruction(string) void
        +ShowChargingInProgress() void
        +ShowFullyCharged() void
        +ShowChargingError() void
        +ClearChargeStatus() void
    }
    
    class ChargeControl {
        -usbCharger: IUsbCharger
        -display: IDisplay
        +ChargingFinished event
        +ChargingError event
        +ChargeControl(IUsbCharger, IDisplay)
        +StartCharge() void
        +StopCharge() void
    }
    
    class UsbChargerSimulator {
        +CurrentValueEvent event
        +CurrentValue: double
        +Connected: bool
        +StartCharge() void
        +StopCharge() void
    }
    
    class FileLogger {
        +Log(message: string) void
    }
    
    %% ========== RELATIONER ==========
    %% Lag 1 -> Lag 2
    StationControl --o IDoor : "has-a"
    StationControl --o IRfidReader: "has-a"
    StationControl --o IDisplay: "has-a"
    StationControl --o IChargeControl: "has-a"
    StationControl --o ILogger: "has-a"
    
    %% Lag 2 -> Lag 2
    IChargeControl --o IUsbCharger : "has-a"
    IChargeControl --o IDisplay : "has-a"
    
    %% Lag 2 <- Lag 3 (implementations)
    IDoor <|.. Door 
    IRfidReader <|.. RfidReader  
    IDisplay <|.. Display  
    IChargeControl <|.. ChargeControl  
    IUsbCharger <|.. UsbChargerSimulator 
    ILogger <|.. FileLogger 
    
    %% Lag 3 -> Lag 2
    ChargeControl --> IUsbCharger
    ChargeControl --> IDisplay
```

## Kommentarer
Systemet er designet efter en klassisk 3-lags arkitektur.
Øverste lag er Stationkontrol, som fungerer som højniveaumodellen og controlleren for hele systemet. Ved brug af Dependency Injection (DI) er denne klasse uafhængig af de konkrete lavniveau-moduler.
Det midterste lag udgøres af interfaces, som alle er rene. De definerer systemets kontrakter og skaber en klar adskillelse mellem lagene.
Nederste lag består af implementeringerne. Disse klasser opfylder de kontrakter, der er fastlagt af deres respektive interfaces.
Strukturen sikrer samlet set, at systemet er afkoblet, uafhængigt og let at teste.