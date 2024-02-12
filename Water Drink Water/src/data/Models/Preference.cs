namespace TbdFriends.WaterDrinkWater.Data.Models;

public class Preference
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TargetFluidOunces { get; set; }
    public int TimeZoneOffsetHours { get; set; } 
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
}