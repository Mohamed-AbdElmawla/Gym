using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.Migrations
{
    public partial class EditedOnMessageModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_coachEnrollments_AspNetUsers_UserId",
                table: "coachEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_SetAttribute_sets_SetId",
                table: "SetAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_sets_Exercises_ExerciseId",
                table: "sets");

            migrationBuilder.DropForeignKey(
                name: "FK_sets_trainingPlans_TrainingId",
                table: "sets");

            migrationBuilder.DropForeignKey(
                name: "FK_trainingPlans_AspNetUsers_UserId",
                table: "trainingPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trainingPlans",
                table: "trainingPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_subscriptions",
                table: "subscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sets",
                table: "sets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_coachEnrollments",
                table: "coachEnrollments");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "trainingPlans",
                newName: "TrainingPlans");

            migrationBuilder.RenameTable(
                name: "subscriptions",
                newName: "Subscriptions");

            migrationBuilder.RenameTable(
                name: "sets",
                newName: "Sets");

            migrationBuilder.RenameTable(
                name: "coachEnrollments",
                newName: "CoachEnrollments");

            migrationBuilder.RenameIndex(
                name: "IX_trainingPlans_UserId",
                table: "TrainingPlans",
                newName: "IX_TrainingPlans_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_sets_TrainingId",
                table: "Sets",
                newName: "IX_Sets_TrainingId");

            migrationBuilder.RenameIndex(
                name: "IX_sets_ExerciseId",
                table: "Sets",
                newName: "IX_Sets_ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_coachEnrollments_UserId",
                table: "CoachEnrollments",
                newName: "IX_CoachEnrollments_UserId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingPlans",
                table: "TrainingPlans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sets",
                table: "Sets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoachEnrollments",
                table: "CoachEnrollments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoachEnrollments_AspNetUsers_UserId",
                table: "CoachEnrollments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SetAttribute_Sets_SetId",
                table: "SetAttribute",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Exercises_ExerciseId",
                table: "Sets",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_TrainingPlans_TrainingId",
                table: "Sets",
                column: "TrainingId",
                principalTable: "TrainingPlans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPlans_AspNetUsers_UserId",
                table: "TrainingPlans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoachEnrollments_AspNetUsers_UserId",
                table: "CoachEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_SetAttribute_Sets_SetId",
                table: "SetAttribute");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Exercises_ExerciseId",
                table: "Sets");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_TrainingPlans_TrainingId",
                table: "Sets");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPlans_AspNetUsers_UserId",
                table: "TrainingPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingPlans",
                table: "TrainingPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subscriptions",
                table: "Subscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sets",
                table: "Sets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoachEnrollments",
                table: "CoachEnrollments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "TrainingPlans",
                newName: "trainingPlans");

            migrationBuilder.RenameTable(
                name: "Subscriptions",
                newName: "subscriptions");

            migrationBuilder.RenameTable(
                name: "Sets",
                newName: "sets");

            migrationBuilder.RenameTable(
                name: "CoachEnrollments",
                newName: "coachEnrollments");

            migrationBuilder.RenameIndex(
                name: "IX_TrainingPlans_UserId",
                table: "trainingPlans",
                newName: "IX_trainingPlans_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Sets_TrainingId",
                table: "sets",
                newName: "IX_sets_TrainingId");

            migrationBuilder.RenameIndex(
                name: "IX_Sets_ExerciseId",
                table: "sets",
                newName: "IX_sets_ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_CoachEnrollments_UserId",
                table: "coachEnrollments",
                newName: "IX_coachEnrollments_UserId");

            migrationBuilder.AddColumn<long>(
                name: "MessageId",
                table: "Messages",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_trainingPlans",
                table: "trainingPlans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_subscriptions",
                table: "subscriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sets",
                table: "sets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "MessageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_coachEnrollments",
                table: "coachEnrollments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_coachEnrollments_AspNetUsers_UserId",
                table: "coachEnrollments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SetAttribute_sets_SetId",
                table: "SetAttribute",
                column: "SetId",
                principalTable: "sets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sets_Exercises_ExerciseId",
                table: "sets",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_sets_trainingPlans_TrainingId",
                table: "sets",
                column: "TrainingId",
                principalTable: "trainingPlans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_trainingPlans_AspNetUsers_UserId",
                table: "trainingPlans",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
