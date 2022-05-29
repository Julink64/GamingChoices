using Microsoft.EntityFrameworkCore.Migrations;

namespace NewGamingChoices.Data.Migrations
{
    public partial class UsersAndConsoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableDiscSpace",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Computer",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComputerPower",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Consoles",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InternetNetworkQuality",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ApplicationUserApplicationUser",
                columns: table => new
                {
                    AskedFriendsListId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FriendsListId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserApplicationUser", x => new { x.AskedFriendsListId, x.FriendsListId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserApplicationUser_AspNetUsers_AskedFriendsListId",
                        column: x => x.AskedFriendsListId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserApplicationUser_AspNetUsers_FriendsListId",
                        column: x => x.FriendsListId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Consoles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consoles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Consoles_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserApplicationUser_FriendsListId",
                table: "ApplicationUserApplicationUser",
                column: "FriendsListId");

            migrationBuilder.CreateIndex(
                name: "IX_Consoles_ApplicationUserId",
                table: "Consoles",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserApplicationUser");

            migrationBuilder.DropTable(
                name: "Consoles");

            migrationBuilder.DropColumn(
                name: "AvailableDiscSpace",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Computer",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ComputerPower",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Consoles",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InternetNetworkQuality",
                table: "AspNetUsers");
        }
    }
}
