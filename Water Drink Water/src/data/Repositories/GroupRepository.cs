using Microsoft.EntityFrameworkCore;
using TbdFriends.WaterDrinkWater.Data.Contexts;
using TbdFriends.WaterDrinkWater.Data.Contracts;
using TbdFriends.WaterDrinkWater.Data.Models;

namespace TbdFriends.WaterDrinkWater.Data.Repositories;

public class GroupRepository(IDbContextFactory<ApplicationDbContext> factory) : IGroupRepository
{
    public void Add(Group group)
    {
        using var context = factory.CreateDbContext();

        context.Groups.Add(group);

        context.SaveChanges();
    }

    public Group? GetByName(string name, int userId)
    {
        using var context = factory.CreateDbContext();

        return context.Groups.FirstOrDefault(g => g.Name == name && g.OwnerId == userId);
    }
}