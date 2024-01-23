using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TbdFriends.WaterDrinkWater.Data.Models;

namespace TbdFriends.WaterDrinkWater.Data.Configurations;

public class ConsumptionConfiguration : IEntityTypeConfiguration<Consumption>
{
    public void Configure(EntityTypeBuilder<Consumption> builder)
    {
        builder.ToTable("Logs");
    }
}