using viewmodels;

namespace TbdFriends.WaterDrinkWater.Data.Contracts;

public interface IConsumptionRepository
{
    void LogConsumption(int userId, int amount);
    PreferencesViewModel GetPreferences(int userId);
    void SetPreferences(int userId, int targetFluidOunces);
}