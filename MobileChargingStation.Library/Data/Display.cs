using System;
using MobileChargingStation.Library.Interfaces;

namespace MobileChargingStation.Library.Data;

public class Display : IDisplay
{
    // Status for opladning (område 2)
    private string _chargeStatus = "";

    public void ShowChargingInProgress()
    {
        _chargeStatus = "Opladning i gang";
        UpdateDisplay();
    }
    
    public void ShowFullyCharged()
    {
        _chargeStatus = "Telefon fuldt opladet";
        UpdateDisplay();
    }
    
    public void ShowChargingError()
    {
        _chargeStatus = "Fejl ved opladning";
        UpdateDisplay();
    }
    
    public void ClearChargeStatus()
    {
        _chargeStatus = "";
        UpdateDisplay();
    }
    
    public void ShowInstruction(string message)
    {
        // Område 1: Brugerinstruktioner
        Console.WriteLine($"[Instruktion] {message}");
        // Vis også ladestatus hvis den eksisterer
        if (!string.IsNullOrEmpty(_chargeStatus))
        {
            Console.WriteLine($"[Status] {_chargeStatus}");
        }
    }

    private void UpdateDisplay()
    {
        // Område 2: Opladningsstatus
        if (!string.IsNullOrEmpty(_chargeStatus))
        {
            Console.WriteLine($"[Status] {_chargeStatus}");
        }
    }
}
