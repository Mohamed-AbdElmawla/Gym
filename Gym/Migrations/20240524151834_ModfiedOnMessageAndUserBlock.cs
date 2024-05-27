using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Migrations
{
    public partial class ModfiedOnMessageAndUserBlock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ApplicationUserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_ApplicationUserId1",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBlocks_AspNetUsers_BlockedId",
                table: "UserBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBlocks_AspNetUsers_BlockerId",
                table: "UserBlocks");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ApplicationUserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ApplicationUserId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "Messages");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBlocks_AspNetUsers_BlockedId",
                table: "UserBlocks",
                column: "BlockedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBlocks_AspNetUsers_BlockerId",
                table: "UserBlocks",
                column: "BlockerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBlocks_AspNetUsers_BlockedId",
                table: "UserBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBlocks_AspNetUsers_BlockerId",
                table: "UserBlocks");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ApplicationUserId",
                table: "Messages",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ApplicationUserId1",
                table: "Messages",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ApplicationUserId",
                table: "Messages",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_ApplicationUserId1",
                table: "Messages",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBlocks_AspNetUsers_BlockedId",
                table: "UserBlocks",
                column: "BlockedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBlocks_AspNetUsers_BlockerId",
                table: "UserBlocks",
                column: "BlockerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
