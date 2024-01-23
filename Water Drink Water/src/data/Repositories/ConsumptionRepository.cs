using Microsoft.EntityFrameworkCore;
using TbdFriends.WaterDrinkWater.Data.Contexts;
using TbdFriends.WaterDrinkWater.Data.Contracts;
using TbdFriends.WaterDrinkWater.Data.Models;

namespace TbdFriends.WaterDrinkWater.Data.Repositories;

public class ConsumptionRepository(IDbContextFactory<ApplicationDbContext> factory) : IConsumptionRepository
{
    public void LogConsumption(int userId, int amount)
    {
        using var context = factory.CreateDbContext();

        context.Logs.Add(new Consumption
        {
            UserId = userId,
            FluidOunces = amount,
            ConsumedOn = DateTime.UtcNow
        });

        context.SaveChanges();
    }
}