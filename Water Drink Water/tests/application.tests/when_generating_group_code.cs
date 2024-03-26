using TbdFriends.WaterDrinkWater.Application.Infrastructure;

namespace application.tests;

public class when_generating_group_code
{
    [Fact]
    public void group_code_is_unique()
    {
        // Arrange
        var generator = new CodeGenerator();
        
        var code1 = generator.GenerateCode();
        var code2 = generator.GenerateCode();
        var code3 = generator.GenerateCode();

        // Act
        var result = code1 == code2 && code2 == code3;

        // Assert
        Assert.False(result);
    }
}