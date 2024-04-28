using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Migrations
{
    public partial class fixedSomeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "sets");

            migrationBuilder.DropColumn(
                name: "SetCount",
                table: "sets");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "sets");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "trainingPlans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "SetAttribute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SetId = table.Column<int>(type: "int", nullable: false),
                    SetCount = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetAttribute_sets_SetId",
                        column: x => x.SetId,
                        principalTable: "sets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SetAttribute_SetId",
                table: "SetAttribute",
                column: "SetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SetAttribute");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "trainingPlans");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "sets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SetCount",
                table: "sets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "sets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
