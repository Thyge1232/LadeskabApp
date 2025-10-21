# Ladeskab til Mobiltelefon - Softwaredokumentation


## 1. Design og Arkitektur


### Klassediagram
Systemet er designet efter en klassisk 3-lags arkitektur.
Øverste lag er Stationkontrol, som fungerer som højniveaumodellen og controlleren for hele systemet. Ved brug af Dependency Injection (DI) er denne klasse uafhængig af de konkrete lavniveau-moduler.
Det midterste lag udgøres af interfaces, som alle er rene. De definerer systemets kontrakter og skaber en klar adskillelse og afkobling mellem lagene. 
Nederste lag består af implementeringerne. Disse klasser opfylder de kontrakter, der er fastlagt af deres respektive interfaces.
Strukturen sikrer samlet set, at systemet er afkoblet, uafhængigt og let at teste.

### Sekvensdiagram
Der er identificeret fem komponenter i sekvensdiagrammet. ChargeControl fungerer som controllerklassen i designet, jf. opgavebeskrivelsen. De øvrige komponenter er Display og StationControl samt de to interfaces IUsbCharger og ILogger.

Selve styringslogikken er pakket ind i et loop, som viser, at eventet CurrentValueEvent sendes løbende fra USBCharger til ChargeControl. ChargeControl lytter passivt efter ændringer i strømmen.

De forskellige betingelser er pakket ind i alternatives, og loopet breakes, når en betingelse er opfyldt. For at holde komponenterne afkoblet sender ChargeControl events op til StationControl om, at opladningen er afsluttet eller der er opstået fejl. StationControl videresender disse events til ILogger.

Designet følger SOLID-principperne, med fokus på afkobling, fleksibilitet og skalerbarhed. Dette gør systemet lettere at udvide og vedligeholde.


## 2. Design for Testbarhed


## 3. Refleksioner over Projektet

### Refleksion over Valgte Design

#### Hvorfor blev dette design valgt?


#### Fordele


#### Ulemper og Overvejelser


### Refleksion over Arbejdsproces og Fordeling


### Refleksion over Fælles Repository og Continuous Integration (CI)

#### Observationer


#### Fordele


#### Ulemper og Udfordringer
