using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Migrations
{
    public partial class UpdateSetAttribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetAttribute_Sets_SetId",
                table: "SetAttribute");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SetAttribute",
                table: "SetAttribute");

            migrationBuilder.RenameTable(
                name: "SetAttribute",
                newName: "SetAttributes");

            migrationBuilder.RenameIndex(
                name: "IX_SetAttribute_SetId",
                table: "SetAttributes",
                newName: "IX_SetAttributes_SetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SetAttributes",
                table: "SetAttributes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SetAttributes_Sets_SetId",
                table: "SetAttributes",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetAttributes_Sets_SetId",
                table: "SetAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SetAttributes",
                table: "SetAttributes");

            migrationBuilder.RenameTable(
                name: "SetAttributes",
                newName: "SetAttribute");

            migrationBuilder.RenameIndex(
                name: "IX_SetAttributes_SetId",
                table: "SetAttribute",
                newName: "IX_SetAttribute_SetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SetAttribute",
                table: "SetAttribute",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SetAttribute_Sets_SetId",
                table: "SetAttribute",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
