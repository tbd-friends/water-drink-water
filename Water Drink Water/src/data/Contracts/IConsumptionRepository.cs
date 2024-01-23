namespace TbdFriends.WaterDrinkWater.Data.Contracts;

public interface IConsumptionRepository
{
    void LogConsumption(int userId, int amount);
}