using Ardalis.Result;
using TbdFriends.WaterDrinkWater.Application.Contracts;
using TbdFriends.WaterDrinkWater.Data.Contracts;
using TbdFriends.WaterDrinkWater.Data.Models;
using viewmodels;

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

    public IEnumerable<GroupViewModel> GetGroups(int userId)
    {
        var groups = repository.GetGroups(userId);

        return groups.Select(g => new GroupViewModel
        {
            Id = g.Id,
            Name = g.Name,
            Code = g.Code
        });
    }
}