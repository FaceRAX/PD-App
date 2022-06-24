using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PD_App2.Migrations
{
    public partial class CharsUpdt_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "char_id",
                table: "characters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "char_id",
                table: "characters");
        }
    }
}
