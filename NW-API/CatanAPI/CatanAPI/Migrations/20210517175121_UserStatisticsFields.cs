using Microsoft.EntityFrameworkCore.Migrations;

namespace CatanAPI.Migrations
{
    public partial class UserStatisticsFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Roles",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddColumn<int>(
                name: "NoOfGames",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfWonGames",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeOnPlay",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoOfGames",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NoOfWonGames",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TimeOnPlay",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<short>(
                name: "Roles",
                table: "AspNetUsers",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
