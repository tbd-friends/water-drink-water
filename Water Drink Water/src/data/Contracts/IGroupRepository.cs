using TbdFriends.WaterDrinkWater.Data.Models;

namespace TbdFriends.WaterDrinkWater.Data.Contracts;

public interface IGroupRepository
{
    void Add(Group group);
    Group? GetByName(string name, int userId);
}