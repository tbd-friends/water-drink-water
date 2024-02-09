using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Time.Testing;
using NSubstitute;
using TbdFriends.WaterDrinkWater.Data.Contexts;
using TbdFriends.WaterDrinkWater.Data.Models;
using TbdFriends.WaterDrinkWater.Data.Repositories;
using Xunit;

namespace data.tests.ConceringConsumptionRepository;

public class when_timezone_offset_difference
{
    [Fact]
    public void and_data_requested_for_current_day_returns_log()
    {
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

        context.Logs.Add(new Consumption
        {
            Id = 1,
            UserId = 1,
            FluidOunces = 12,
            ConsumedOn = timeProvider.GetUtcNow().DateTime
        });

        context.SaveChanges();

        var subject = new ConsumptionRepository(contextFactory, timeProvider);

        // Act 

        var result = subject.GetLogs(1, -5);

        // Assert

        Assert.Equal(1, result.Count());
    }
}