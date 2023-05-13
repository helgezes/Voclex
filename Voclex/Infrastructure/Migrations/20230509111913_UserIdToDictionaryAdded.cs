using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class UserIdToDictionaryAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TermsDictionaries",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TermsDictionaries_UserId",
                table: "TermsDictionaries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TermsDictionaries_Users_UserId",
                table: "TermsDictionaries",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TermsDictionaries_Users_UserId",
                table: "TermsDictionaries");

            migrationBuilder.DropIndex(
                name: "IX_TermsDictionaries_UserId",
                table: "TermsDictionaries");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TermsDictionaries");
        }
    }
}
