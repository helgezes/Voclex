using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dictionaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DictionaryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DictionaryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<string>(type: "TEXT", maxLength: 600, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DictionaryItems_Dictionaries_DictionaryId",
                        column: x => x.DictionaryId,
                        principalTable: "Dictionaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Definitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DictionaryItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Definitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Definitions_DictionaryItems_DictionaryItemId",
                        column: x => x.DictionaryItemId,
                        principalTable: "DictionaryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DictionaryItemProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    DictionaryItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    GuessedTimesCount = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryItemProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DictionaryItemProgresses_DictionaryItems_DictionaryItemId",
                        column: x => x.DictionaryItemId,
                        principalTable: "DictionaryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DictionaryItemProgresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Definitions_DictionaryItemId",
                table: "Definitions",
                column: "DictionaryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DictionaryItemProgresses_DictionaryItemId",
                table: "DictionaryItemProgresses",
                column: "DictionaryItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DictionaryItemProgresses_UserId",
                table: "DictionaryItemProgresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DictionaryItems_DictionaryId",
                table: "DictionaryItems",
                column: "DictionaryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Definitions");

            migrationBuilder.DropTable(
                name: "DictionaryItemProgresses");

            migrationBuilder.DropTable(
                name: "DictionaryItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Dictionaries");
        }
    }
}
