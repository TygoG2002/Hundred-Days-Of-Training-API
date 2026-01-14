using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkoutSessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSessionExercise_WorkoutSessionId",
                table: "WorkoutSessionExercise",
                column: "WorkoutSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSessionExercise_WorkoutSession_WorkoutSessionId",
                table: "WorkoutSessionExercise",
                column: "WorkoutSessionId",
                principalTable: "WorkoutSession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSessionExercise_WorkoutSession_WorkoutSessionId",
                table: "WorkoutSessionExercise");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutSessionExercise_WorkoutSessionId",
                table: "WorkoutSessionExercise");
        }
    }
}
