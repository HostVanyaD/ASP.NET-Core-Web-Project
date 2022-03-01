using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HighPaw.Data.Migrations
{
    public partial class VolunteerTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sex",
                table: "Pets",
                newName: "Gender");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Volunteers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VolunteerId",
                table: "Pets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_UserId",
                table: "Volunteers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_VolunteerId",
                table: "Pets",
                column: "VolunteerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Volunteers_VolunteerId",
                table: "Pets",
                column: "VolunteerId",
                principalTable: "Volunteers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Volunteers_AspNetUsers_UserId",
                table: "Volunteers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Volunteers_VolunteerId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Volunteers_AspNetUsers_UserId",
                table: "Volunteers");

            migrationBuilder.DropIndex(
                name: "IX_Volunteers_UserId",
                table: "Volunteers");

            migrationBuilder.DropIndex(
                name: "IX_Pets_VolunteerId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Volunteers");

            migrationBuilder.DropColumn(
                name: "VolunteerId",
                table: "Pets");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Pets",
                newName: "Sex");
        }
    }
}
