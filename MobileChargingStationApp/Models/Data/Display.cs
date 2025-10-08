using System;
using MobileCharginStation.Interfaces;

namespace MobileCharginStation.Models;

public class Display : IDisplay
{
    public void ShowChargingInProgress()
    {
        Console.WriteLine("Opladning i gang");
    }
    public void ShowFullyCharged()
    {
        Console.WriteLine("Telefon fuldt opladet");
    }
    public void ShowChargingError()
    {
        Console.WriteLine("Fejl ved opladning");
    }
    public void ClearChargeStatus()
    {
        Console.WriteLine("Opladning afsluttet");
    }
    public void ShowInstructions()
    {
        Console.WriteLine("Tilslut telefon");
    }




}