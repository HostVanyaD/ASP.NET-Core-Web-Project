using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HighPaw.Data.Migrations
{
    public partial class PetModelUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FoundDate",
                table: "Pets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FoundLocation",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoundDate",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "FoundLocation",
                table: "Pets");
        }
    }
}
