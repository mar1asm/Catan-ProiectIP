using Microsoft.EntityFrameworkCore.Migrations;

namespace CatanAPI.Migrations
{
    public partial class New : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameSessionUser_AspNetUsers_UserId",
                table: "GameSessionUser");

            migrationBuilder.DropForeignKey(
                name: "FK_GameSessionUser_GameSessions_GameSessionId",
                table: "GameSessionUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameSessionUser",
                table: "GameSessionUser");

            migrationBuilder.RenameTable(
                name: "GameSessionUser",
                newName: "GameSessionUsers");

            migrationBuilder.RenameIndex(
                name: "IX_GameSessionUser_GameSessionId",
                table: "GameSessionUsers",
                newName: "IX_GameSessionUsers_GameSessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameSessionUsers",
                table: "GameSessionUsers",
                columns: new[] { "UserId", "GameSessionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessionUsers_AspNetUsers_UserId",
                table: "GameSessionUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessionUsers_GameSessions_GameSessionId",
                table: "GameSessionUsers",
                column: "GameSessionId",
                principalTable: "GameSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameSessionUsers_AspNetUsers_UserId",
                table: "GameSessionUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_GameSessionUsers_GameSessions_GameSessionId",
                table: "GameSessionUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameSessionUsers",
                table: "GameSessionUsers");

            migrationBuilder.RenameTable(
                name: "GameSessionUsers",
                newName: "GameSessionUser");

            migrationBuilder.RenameIndex(
                name: "IX_GameSessionUsers_GameSessionId",
                table: "GameSessionUser",
                newName: "IX_GameSessionUser_GameSessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameSessionUser",
                table: "GameSessionUser",
                columns: new[] { "UserId", "GameSessionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessionUser_AspNetUsers_UserId",
                table: "GameSessionUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessionUser_GameSessions_GameSessionId",
                table: "GameSessionUser",
                column: "GameSessionId",
                principalTable: "GameSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
