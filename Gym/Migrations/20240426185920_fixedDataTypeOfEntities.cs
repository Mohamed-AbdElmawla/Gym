using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Migrations
{
    public partial class fixedDataTypeOfEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Set_Exercise_ExerciseId",
                table: "Set");

            migrationBuilder.DropForeignKey(
                name: "FK_Set_TrainingPlan_TrainingId",
                table: "Set");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlan_IdentityUser_UserId",
                table: "TrainingPlan");

            migrationBuilder.DropTable(
                name: "IdentityUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingPlan",
                table: "TrainingPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Set",
                table: "Set");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercise",
                table: "Exercise");

            migrationBuilder.RenameTable(
                name: "TrainingPlan",
                newName: "trainingPlans");

            migrationBuilder.RenameTable(
                name: "Set",
                newName: "sets");

            migrationBuilder.RenameTable(
                name: "Exercise",
                newName: "Exercises");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingPlan_UserId",
                table: "trainingPlans",
                newName: "IX_trainingPlans_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Set_TrainingId",
                table: "sets",
                newName: "IX_sets_TrainingId");

            migrationBuilder.RenameIndex(
                name: "IX_Set_ExerciseId",
                table: "sets",
                newName: "IX_sets_ExerciseId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "trainingPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_trainingPlans",
                table: "trainingPlans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sets",
                table: "sets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscriptions", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_sets_Exercises_ExerciseId",
                table: "sets",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sets_trainingPlans_TrainingId",
                table: "sets",
                column: "TrainingId",
                principalTable: "trainingPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trainingPlans_AspNetUsers_UserId",
                table: "trainingPlans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sets_Exercises_ExerciseId",
                table: "sets");

            migrationBuilder.DropForeignKey(
                name: "FK_sets_trainingPlans_TrainingId",
                table: "sets");

            migrationBuilder.DropForeignKey(
                name: "FK_trainingPlans_AspNetUsers_UserId",
                table: "trainingPlans");

            migrationBuilder.DropTable(
                name: "subscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trainingPlans",
                table: "trainingPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sets",
                table: "sets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Exercises",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "trainingPlans");

            migrationBuilder.RenameTable(
                name: "trainingPlans",
                newName: "TrainingPlan");

            migrationBuilder.RenameTable(
                name: "sets",
                newName: "Set");

            migrationBuilder.RenameTable(
                name: "Exercises",
                newName: "Exercise");

            migrationBuilder.RenameIndex(
                name: "IX_trainingPlans_UserId",
                table: "TrainingPlan",
                newName: "IX_TrainingPlan_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_sets_TrainingId",
                table: "Set",
                newName: "IX_Set_TrainingId");

            migrationBuilder.RenameIndex(
                name: "IX_sets_ExerciseId",
                table: "Set",
                newName: "IX_Set_ExerciseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingPlan",
                table: "TrainingPlan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Set",
                table: "Set",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Exercise",
                table: "Exercise",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Set_Exercise_ExerciseId",
                table: "Set",
                column: "ExerciseId",
                principalTable: "Exercise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Set_TrainingPlan_TrainingId",
                table: "Set",
                column: "TrainingId",
                principalTable: "TrainingPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlan_IdentityUser_UserId",
                table: "TrainingPlan",
                column: "UserId",
                principalTable: "IdentityUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
