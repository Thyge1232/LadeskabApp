namespace MobileChargingStation.Library.Interfaces;

public interface IDisplay
{
    void ShowChargingInProgress();
    void ShowFullyCharged();
    void ShowChargingError();
    void ClearChargeStatus();
    void ShowInstruction(string message);
}
