using viewmodels;

namespace application.tests.ConcerningGoals;

public class when_requesting_progress_and_no_goal_set
{
    [Fact]
    public void result_is_zero()
    {
        var userId = 1;
        var repository = Substitute.For<IConsumptionRepository>();

        repository.GetPreferences(Arg.Is(userId))
            .Returns(new PreferencesViewModel());

        var subject = new ConsumptionService(repository);

        var result = subject.GetProgressPercentage(userId);

        Assert.Equal(0, result);
    }
}