using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TbdFriends.WaterDrinkWater.Data.Migrations
{
    /// <inheritdoc />
    public partial class drop_offset_save_timezone_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeZoneOffsetHours",
                table: "Preferences");

            migrationBuilder.AddColumn<string>(
                name: "TimeZoneId",
                table: "Preferences",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeZoneId",
                table: "Preferences");

            migrationBuilder.AddColumn<int>(
                name: "TimeZoneOffsetHours",
                table: "Preferences",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
