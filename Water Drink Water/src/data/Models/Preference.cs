namespace TbdFriends.WaterDrinkWater.Data.Models;

public class Preference
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TargetFluidOunces { get; set; }
    public string TimeZoneId { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
}