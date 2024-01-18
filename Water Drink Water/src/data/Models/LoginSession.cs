namespace TbdFriends.WaterDrinkWater.Data.Models;

public class LoginSession
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public DateTime? Expiration { get; set; }
    public DateTime? Revoked { get; set; }
}