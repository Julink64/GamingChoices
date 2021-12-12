using Microsoft.EntityFrameworkCore.Migrations;

namespace NewGamingChoices.Data.Migrations
{
    public partial class PlatformPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Game");

            migrationBuilder.AddColumn<bool>(
                name: "IsCrossPlatform",
                table: "Game",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PlatformPrice",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GameID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlatformPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlatformPrice_Game_GameID",
                        column: x => x.GameID,
                        principalTable: "Game",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlatformPrice_GameID",
                table: "PlatformPrice",
                column: "GameID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlatformPrice");

            migrationBuilder.DropColumn(
                name: "IsCrossPlatform",
                table: "Game");

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "Game",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Game",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
