using Microsoft.EntityFrameworkCore.Migrations;

namespace CatanAPI.Migrations
{
    public partial class ExtensionGameSessions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Extensions_GameSessions_GameSessionId",
                table: "Extensions");

            migrationBuilder.DropIndex(
                name: "IX_Extensions_GameSessionId",
                table: "Extensions");

            migrationBuilder.DropColumn(
                name: "GameSessionId",
                table: "Extensions");

            migrationBuilder.CreateTable(
                name: "ExtensionGameSession",
                columns: table => new
                {
                    ExtensionsId = table.Column<int>(type: "integer", nullable: false),
                    GameSessionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtensionGameSession", x => new { x.ExtensionsId, x.GameSessionsId });
                    table.ForeignKey(
                        name: "FK_ExtensionGameSession_Extensions_ExtensionsId",
                        column: x => x.ExtensionsId,
                        principalTable: "Extensions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExtensionGameSession_GameSessions_GameSessionsId",
                        column: x => x.GameSessionsId,
                        principalTable: "GameSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExtensionGameSession_GameSessionsId",
                table: "ExtensionGameSession",
                column: "GameSessionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExtensionGameSession");

            migrationBuilder.AddColumn<int>(
                name: "GameSessionId",
                table: "Extensions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Extensions_GameSessionId",
                table: "Extensions",
                column: "GameSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Extensions_GameSessions_GameSessionId",
                table: "Extensions",
                column: "GameSessionId",
                principalTable: "GameSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
