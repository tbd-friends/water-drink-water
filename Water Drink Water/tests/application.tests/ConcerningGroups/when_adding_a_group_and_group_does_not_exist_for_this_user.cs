using Ardalis.Result;
using TbdFriends.WaterDrinkWater.Application.Contracts;

namespace application.tests.ConcerningGroups;

public class when_adding_a_group_and_group_does_not_exist_for_this_user
{
    [Fact]
    public void group_is_added()
    {
        // Arrange
        const int UserId = 10001;
        const string GroupName = "New Group Name";
        const string GeneratedCode = "ABC123";

        var repository = Substitute.For<IGroupRepository>();
        var generator = Substitute.For<ICodeGenerator>();

        generator
            .GenerateCode()
            .Returns(GeneratedCode);

        var service = new GroupService(repository, generator);

        // Act
        var result = service.AddNewGroup(GroupName, UserId);

        // Assert
        repository
            .Received()
            .Add(Arg.Is<Group>(g =>
                g.Name == GroupName &&
                g.OwnerId == UserId &&
                g.Code == GeneratedCode &&
                g.DateAdded > DateTime.UtcNow.AddSeconds(-10)));

        Assert.Equal(ResultStatus.Ok, result.Status);
    }
}