using System.Security.Claims;

namespace TbdFriends.WaterDrinkWater.Api.Infrastructure;

public static class ClaimExtensions
{
    public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        return int.Parse(
            claimsPrincipal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value.Split("|")[1] ??
            string.Empty
        );
    }
}