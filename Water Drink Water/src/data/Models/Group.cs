namespace TbdFriends.WaterDrinkWater.Data.Models;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public int OwnerId { get; set; }
    public DateTime DateAdded { get; set; }

    public virtual Account Owner { get; set; } = null!;
}