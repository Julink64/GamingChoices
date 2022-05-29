using Microsoft.EntityFrameworkCore.Migrations;

namespace NewGamingChoices.Data.Migrations
{
    public partial class UsersAndConsoles2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consoles_AspNetUsers_ApplicationUserId",
                table: "Consoles");

            migrationBuilder.DropIndex(
                name: "IX_Consoles_ApplicationUserId",
                table: "Consoles");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Consoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Consoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consoles_ApplicationUserId",
                table: "Consoles",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consoles_AspNetUsers_ApplicationUserId",
                table: "Consoles",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
