using Microsoft.EntityFrameworkCore;
using TbdFriends.WaterDrinkWater.Data.Contexts;

namespace TbdFriends.WaterDrinkWater.Api.Infrastructure;

public static class MigrationExtension
{
    public static void ApplyMigrations(this IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();

        using var context = factory.CreateDbContext();
        
        context.Database.Migrate();
    }
}