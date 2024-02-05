using TbdFriends.WaterDrinkWater.Data.Contracts;
using viewmodels;

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

    public PreferencesViewModel GetPreferences(int userId)
    {
        return repository.GetPreferences(userId);
    }

    public int GetProgress(int userId)
    {
        return 0;
    }
}