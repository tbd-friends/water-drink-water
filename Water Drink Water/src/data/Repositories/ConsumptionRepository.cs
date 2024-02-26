using Microsoft.EntityFrameworkCore;
using TbdFriends.WaterDrinkWater.Data.Contexts;
using TbdFriends.WaterDrinkWater.Data.Contracts;
using TbdFriends.WaterDrinkWater.Data.Models;
using viewmodels;

namespace TbdFriends.WaterDrinkWater.Data.Repositories;

public class ConsumptionRepository(
    IDbContextFactory<ApplicationDbContext> factory,
    TimeProvider timeProvider) : IConsumptionRepository
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

    public IEnumerable<Consumption> GetLogsForToday(int userId, int timezoneOffsetHours)
    {
        using var context = factory.CreateDbContext();

        var now = timeProvider.GetUtcNow().AddHours(timezoneOffsetHours).Date;

        return context.Logs.Where(l =>
                l.UserId == userId &&
                l.ConsumedOn.AddHours(timezoneOffsetHours).Date == now)
            .OrderByDescending(l => l.ConsumedOn)
            .ToList();
    }

    public PreferencesViewModel GetPreferences(int userId)
    {
        using var context = factory.CreateDbContext();

        var preferences = context.Preferences.FirstOrDefault(p => p.UserId == userId);

        var timeZoneId = !string.IsNullOrEmpty(preferences?.TimeZoneId) ? preferences.TimeZoneId : TimeZoneInfo.Utc.Id;

        var offsetHours = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId)
            .BaseUtcOffset.Hours;

        return new PreferencesViewModel
        {
            TargetFluidOunces = preferences?.TargetFluidOunces ?? 0,
            TimeZoneId = preferences?.TimeZoneId ?? TimeZoneInfo.Utc.Id,
            TimeZoneOffsetHours = offsetHours
        };
    }

    public void SetPreferences(int userId, int targetFluidOunces, string timeZoneId)
    {
        using var context = factory.CreateDbContext();

        var preferences = context.Preferences.FirstOrDefault(p => p.UserId == userId);

        if (preferences == null)
        {
            context.Preferences.Add(new Preference
            {
                UserId = userId,
                TargetFluidOunces = targetFluidOunces,
                TimeZoneId = timeZoneId,
                CreatedOn = DateTime.UtcNow
            });
        }
        else
        {
            preferences.TargetFluidOunces = targetFluidOunces;
            preferences.TimeZoneId = timeZoneId;
            preferences.UpdatedOn = DateTime.UtcNow;
        }

        context.SaveChanges();
    }
}