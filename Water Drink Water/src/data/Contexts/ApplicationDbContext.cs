using Microsoft.EntityFrameworkCore;
using TbdFriends.WaterDrinkWater.Data.Models;

namespace TbdFriends.WaterDrinkWater.Data.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Consumption> Logs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}