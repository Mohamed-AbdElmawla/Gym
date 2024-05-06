using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Migrations
{
    public partial class edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SetCount",
                table: "SetAttribute",
                newName: "Reps");

            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "SetAttribute",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reps",
                table: "SetAttribute",
                newName: "SetCount");

            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "SetAttribute",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
