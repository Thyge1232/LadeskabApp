```mermaid
sequenceDiagram
    autonumber
    participant LOG as ILogger
    participant SC as StationControl
    participant CC as ChargeControl
    participant DP as Display
    participant USB as IUsbCharger

    SC->>CC: StartCharge()
    activate CC
    
    CC->>USB: StartCharge()
    activate USB
    
    SC->>LOG: Log(OldId, StartCharging)

    loop Mens opladning er aktiv
        %% USB sender løbende events med strømværdien
        USB-->>CC: CurrentValueEvent(current)

        alt Normal opladning [5 < current <= 500]
            CC->>DP: ShowChargingInProgress()

        else Fuldt opladet [0 < current <= 5]
            CC->>DP: ShowFullyCharged()
            CC->>USB: StopCharge()
            CC-->>SC: ChargingFinished()
            SC->>LOG: Log(OldId, EndCharging)
            Note over CC,SC: Loop afsluttes

        else Fejl/kortslutning [current > 500]
            CC->>USB: StopCharge()
            CC->>DP: ShowChargingError()
            CC-->>SC: ChargingError()
            SC->>LOG: Log(OldId, EndCharging)
            Note over CC,SC: Loop afsluttes

        else Ingen forbindelse [current == 0]
            CC->>DP: ClearChargeStatus()
        end
    end

    deactivate USB
    deactivate CC

```

## Kommentarer: 
Der er identificeret fem komponenter i sekvensdiagrammet. ChargeControl fungerer som controllerklassen i designet, jf. opgavebeskrivelsen. De øvrige komponenter er Display og StationControl samt de to interfaces IUsbCharger og ILogger.

Selve styringslogikken er pakket ind i et loop, som viser, at eventet CurrentValueEvent sendes løbende fra USBCharger til ChargeControl. ChargeControl lytter passivt efter ændringer i strømmen.

De forskellige betingelser er pakket ind i alternatives, og loopet breakes, når en betingelse er opfyldt. For at holde komponenterne afkoblet sender ChargeControl events op til StationControl om, at opladningen er afsluttet eller der er opstået fejl. StationControl videresender disse events til ILogger.

Designet følger SOLID-principperne, med fokus på afkobling, fleksibilitet og skalerbarhed. Dette gør systemet lettere at udvide og vedligeholde.