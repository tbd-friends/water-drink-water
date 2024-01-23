namespace TbdFriends.WaterDrinkWater.Data.Models;

public class Consumption
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int FluidOunces { get; set; }
    public DateTime ConsumedOn { get; set; }
}