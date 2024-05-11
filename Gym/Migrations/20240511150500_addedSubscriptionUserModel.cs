using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Migrations
{
    public partial class addedSubscriptionUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Subscriptions",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CoachId",
                table: "Subscriptions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubscriptionUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionUser_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CoachId",
                table: "Subscriptions",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionUser_SubscriptionId",
                table: "SubscriptionUser",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionUser_UserId",
                table: "SubscriptionUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_AspNetUsers_CoachId",
                table: "Subscriptions",
                column: "CoachId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_AspNetUsers_CoachId",
                table: "Subscriptions");

            migrationBuilder.DropTable(
                name: "SubscriptionUser");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_CoachId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "CoachId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
