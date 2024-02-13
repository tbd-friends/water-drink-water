using TbdFriends.WaterDrinkWater.Data.Models;
using viewmodels;

namespace TbdFriends.WaterDrinkWater.Data.Contracts;

public interface IConsumptionRepository
{
    void LogConsumption(int userId, int amount);
    IEnumerable<Consumption> GetLogsForToday(int userId, int timezoneOffsetHours);
    PreferencesViewModel GetPreferences(int userId);
    void SetPreferences(int userId, int targetFluidOunces, int timeZoneOffsetHours);
}