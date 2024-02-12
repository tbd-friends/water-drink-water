using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Time.Testing;
using NSubstitute;
using TbdFriends.WaterDrinkWater.Data.Contexts;
using TbdFriends.WaterDrinkWater.Data.Models;
using TbdFriends.WaterDrinkWater.Data.Repositories;
using Xunit;

namespace data.tests.ConceringConsumptionRepository;

public class when_user_timezone_not_in_utc
{
    [Fact]
    public void and_logs_for_today_requested_only_logs_for_current_user_day_are_returned()
    {
        int ExpectedConsumptionId = 1004;
        int UserId = 9009;

        // Arrange
        var contextFactory = Substitute.For<IDbContextFactory<ApplicationDbContext>>();
        var context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("fakeDb")
            .Options);
        var timeProvider = new FakeTimeProvider();

        timeProvider.SetLocalTimeZone(TimeZoneInfo.Local);
        timeProvider.SetUtcNow(new DateTime(2024, 7, 8, 0, 5, 0, DateTimeKind.Utc));

        contextFactory
            .CreateDbContext()
            .Returns(context);

        context.Logs.AddRange(
            new Consumption()
            {
                Id = 1,
                UserId = UserId,
                FluidOunces = 100,
                ConsumedOn = timeProvider.GetUtcNow().AddDays(-1).DateTime
            },
            new Consumption
            {
                Id = ExpectedConsumptionId,
                UserId = UserId,
                FluidOunces = 12,
                ConsumedOn = timeProvider.GetUtcNow().DateTime
            },
            new Consumption()
            {
                Id = 2,
                UserId = UserId,
                FluidOunces = 120,
                ConsumedOn = timeProvider.GetUtcNow().AddDays(1).DateTime
            }
        );

        context.SaveChanges();

        var subject = new ConsumptionRepository(contextFactory, timeProvider);

        // Act 

        var result = subject.GetLogsForToday(UserId, -5);

        // Assert

        Assert.Single(result, p => p.Id == ExpectedConsumptionId);
    }
}