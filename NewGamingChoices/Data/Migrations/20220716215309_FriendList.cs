using Microsoft.EntityFrameworkCore.Migrations;

namespace NewGamingChoices.Data.Migrations
{
    public partial class FriendList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserApplicationUser");

            migrationBuilder.AddColumn<string>(
                name: "SerializedAskedFriendsList",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerializedFriendsList",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SerializedAskedFriendsList",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SerializedFriendsList",
                table: "AspNetUsers");

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

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserApplicationUser_FriendsListId",
                table: "ApplicationUserApplicationUser",
                column: "FriendsListId");
        }
    }
}
