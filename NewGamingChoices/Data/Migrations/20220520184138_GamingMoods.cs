using Microsoft.EntityFrameworkCore.Migrations;

namespace NewGamingChoices.Data.Migrations
{
    public partial class GamingMoods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_AspNetUsers_ApplicationUserId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_ApplicationUserId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Game");

            migrationBuilder.CreateTable(
                name: "GamingMoods",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GameID = table.Column<int>(type: "int", nullable: true),
                    ConsoleID = table.Column<int>(type: "int", nullable: true),
                    IsOkToPlay = table.Column<bool>(type: "bit", nullable: false),
                    IsNeverOkToPlay = table.Column<bool>(type: "bit", nullable: false),
                    IsGameDownloadedYet = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamingMoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamingMoods_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GamingMoods_Consoles_ConsoleID",
                        column: x => x.ConsoleID,
                        principalTable: "Consoles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GamingMoods_Game_GameID",
                        column: x => x.GameID,
                        principalTable: "Game",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamingMoods_ApplicationUserId",
                table: "GamingMoods",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GamingMoods_ConsoleID",
                table: "GamingMoods",
                column: "ConsoleID");

            migrationBuilder.CreateIndex(
                name: "IX_GamingMoods_GameID",
                table: "GamingMoods",
                column: "GameID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamingMoods");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Game",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_ApplicationUserId",
                table: "Game",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_AspNetUsers_ApplicationUserId",
                table: "Game",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
