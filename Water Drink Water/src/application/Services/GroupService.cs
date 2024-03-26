using Ardalis.Result;
using TbdFriends.WaterDrinkWater.Application.Contracts;
using TbdFriends.WaterDrinkWater.Data.Contracts;
using TbdFriends.WaterDrinkWater.Data.Models;

namespace TbdFriends.WaterDrinkWater.Application.Services;

public class GroupService(IGroupRepository repository, ICodeGenerator codeGenerator)
{
    public Result AddNewGroup(string name, int userId)
    {
        var group = repository.GetByName(name, userId);

        if (group is not null)
        {
            return Result.Conflict();
        }

        repository.Add(new Group
        {
            Name = name,
            OwnerId = userId,
            Code = codeGenerator.GenerateCode(),
            DateAdded = DateTime.UtcNow
        });

        return Result.Success();
    }
}