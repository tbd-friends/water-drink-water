using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TbdFriends.WaterDrinkWater.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_timezone_offset_hours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeZoneOffsetHours",
                table: "Preferences",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeZoneOffsetHours",
                table: "Preferences");
        }
    }
}
