using TbdFriends.WaterDrinkWater.Application.Contracts;

namespace TbdFriends.WaterDrinkWater.Application.Infrastructure;

public class CodeGenerator : ICodeGenerator
{
    private static readonly string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public string GenerateCode()
    {
        Random random = new Random();

        var characters = Characters.ToArray();

        random.Shuffle(characters);

        return new string(characters.Take(6).ToArray());
    }
}