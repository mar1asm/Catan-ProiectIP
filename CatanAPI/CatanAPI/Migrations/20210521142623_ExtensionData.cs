using Microsoft.EntityFrameworkCore.Migrations;

namespace CatanAPI.Migrations
{
    public partial class ExtensionData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Extensions",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Extensions");
        }
    }
}
