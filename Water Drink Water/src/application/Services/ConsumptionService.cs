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

    public int GetProgressPercentage(int userId)
    {
        var preferences = repository.GetPreferences(userId);
        
        if ( preferences.TargetFluidOunces <= 0) return 0;
        
        var logs = repository.GetLogs(userId, DateTime.UtcNow);
        
        var totalConsumed = logs.Sum(l => l.FluidOunces);
        
        var progress = (int) (totalConsumed / (double) preferences.TargetFluidOunces * 100);
        
        return progress;

    }
}