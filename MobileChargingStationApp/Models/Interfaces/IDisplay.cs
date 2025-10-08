using MobileCharginStation.Models;

namespace MobileCharginStation.Interfaces;

public interface IDisplay
{
    void ShowChargingInProgress();
    void ShowFullyCharged();
    void ShowChargingError();
    void ClearChargeStatus();
    void ShowInstructions();
}