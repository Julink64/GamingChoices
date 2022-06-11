using Microsoft.EntityFrameworkCore.Migrations;

namespace NewGamingChoices.Data.Migrations
{
    public partial class GamingMoods2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNeverOkToPlay",
                table: "GamingMoods");

            migrationBuilder.AddColumn<bool>(
                name: "IsFavAndNotBlacklisted",
                table: "GamingMoods",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavAndNotBlacklisted",
                table: "GamingMoods");

            migrationBuilder.AddColumn<bool>(
                name: "IsNeverOkToPlay",
                table: "GamingMoods",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
