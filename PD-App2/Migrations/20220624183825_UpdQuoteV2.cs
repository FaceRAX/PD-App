using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PD_App2.Migrations
{
    public partial class UpdQuoteV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "quoteid",
                table: "quotes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quoteid",
                table: "quotes");
        }
    }
}
