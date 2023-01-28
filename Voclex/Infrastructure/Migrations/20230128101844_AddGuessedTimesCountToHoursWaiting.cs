using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddGuessedTimesCountToHoursWaiting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastGuessDateTime",
                table: "TermProgresses",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "GuessedTimesCountToHoursWaiting",
                columns: table => new
                {
                    GuessedTimesCount = table.Column<byte>(type: "INTEGER", nullable: false),
                    HoursWaiting = table.Column<ushort>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuessedTimesCountToHoursWaiting", x => x.GuessedTimesCount);
                });

            migrationBuilder.InsertData(
                table: "GuessedTimesCountToHoursWaiting",
                columns: new[] { "GuessedTimesCount", "HoursWaiting" },
                values: new object[] { (byte)0, (ushort)0 });

            migrationBuilder.InsertData(
                table: "GuessedTimesCountToHoursWaiting",
                columns: new[] { "GuessedTimesCount", "HoursWaiting" },
                values: new object[] { (byte)1, (ushort)12 });

            migrationBuilder.InsertData(
                table: "GuessedTimesCountToHoursWaiting",
                columns: new[] { "GuessedTimesCount", "HoursWaiting" },
                values: new object[] { (byte)2, (ushort)24 });

            migrationBuilder.InsertData(
                table: "GuessedTimesCountToHoursWaiting",
                columns: new[] { "GuessedTimesCount", "HoursWaiting" },
                values: new object[] { (byte)3, (ushort)120 });

            migrationBuilder.InsertData(
                table: "GuessedTimesCountToHoursWaiting",
                columns: new[] { "GuessedTimesCount", "HoursWaiting" },
                values: new object[] { (byte)4, (ushort)336 });

            migrationBuilder.InsertData(
                table: "GuessedTimesCountToHoursWaiting",
                columns: new[] { "GuessedTimesCount", "HoursWaiting" },
                values: new object[] { (byte)5, (ushort)1080 });

            migrationBuilder.InsertData(
                table: "GuessedTimesCountToHoursWaiting",
                columns: new[] { "GuessedTimesCount", "HoursWaiting" },
                values: new object[] { (byte)6, (ushort)2880 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuessedTimesCountToHoursWaiting");

            migrationBuilder.DropColumn(
                name: "LastGuessDateTime",
                table: "TermProgresses");
        }
    }
}
