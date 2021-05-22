using Microsoft.EntityFrameworkCore.Migrations;

namespace CatanAPI.Migrations
{
    public partial class RemovedUserNotificationId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserNotifications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserNotifications",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
