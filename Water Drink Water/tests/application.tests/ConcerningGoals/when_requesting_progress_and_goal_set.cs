using viewmodels;

namespace application.tests.ConcerningGoals;

public class when_requesting_progress_and_goal_set
{
    [Fact]
    public void and_no_logs_have_been_entered_progress_is_zero()
    {
        // Arrange
        var userId = 1;
        var repository = Substitute.For<IConsumptionRepository>();

        repository.GetPreferences(Arg.Is(userId))
            .Returns(new PreferencesViewModel());

        var subject = new ConsumptionService(repository);

        // Act
        var result = subject.GetProgressPercentage(userId);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void and_a_log_has_been_made_should_return_percentage_of_goal()
    {
        int expectedProgress = 10;

        // Assert
        var userId = 1;
        var repository = Substitute.For<IConsumptionRepository>();

        repository.GetPreferences(Arg.Is(userId))
            .Returns(new PreferencesViewModel { TargetFluidOunces = 120 });

        repository.GetLogs(Arg.Is(userId), 0)
            .Returns(new List<Consumption>
            {
                new()
                {
                    UserId = userId,
                    FluidOunces = 12,
                    ConsumedOn = DateTime.UtcNow
                }
            });

        var subject = new ConsumptionService(repository);

        // Act  
        var result = subject.GetProgressPercentage(userId);

        // Assert

        Assert.Equal(expectedProgress, result);
    }
}