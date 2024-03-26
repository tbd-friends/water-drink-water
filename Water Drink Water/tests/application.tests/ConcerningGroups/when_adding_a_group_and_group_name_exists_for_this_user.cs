using Ardalis.Result;
using TbdFriends.WaterDrinkWater.Application.Contracts;

namespace application.tests.ConcerningGroups;

public class when_adding_a_group_and_group_name_exists_for_this_user
{
    [Fact]
    public void group_is_not_added()
    {
        // Arrange
        const int UserId = 10001;
        const string GroupName = "Existing Group Name";

        var repository = Substitute.For<IGroupRepository>();

        repository
            .GetByName(GroupName, UserId)
            .Returns(new Group() { OwnerId = UserId, Name = GroupName });

        var generator = Substitute.For<ICodeGenerator>();

        var service = new GroupService(repository, generator);

        // Act

        var result = service.AddNewGroup(GroupName, UserId);

        // Assert

        repository
            .DidNotReceive()
            .Add(Arg.Any<Group>());

        generator
            .DidNotReceive()
            .GenerateCode();

        Assert.Equal(ResultStatus.Conflict, result.Status);
    }
}