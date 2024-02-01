namespace TbdFriends.WaterDrinkWater.Data.Contracts;

public interface IConsumptionRepository
{
    void LogConsumption(int userId, int amount);
    void SetPreferences(int userId, int targetFluidOunces);
    int? GetPreferences(int userId);
}