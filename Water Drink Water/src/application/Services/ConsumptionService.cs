using TbdFriends.WaterDrinkWater.Data.Contracts;

namespace TbdFriends.WaterDrinkWater.Application.Services;

public class ConsumptionService(IConsumptionRepository repository)
{
    public void Log(int userId, int fluidOuncesConsumed)
    {
        repository.LogConsumption(userId, fluidOuncesConsumed);
    }

    public void SetPreferences(int userId,
        int targetFluidOunces)
    {
        repository.SetPreferences(userId, targetFluidOunces);
    }
}