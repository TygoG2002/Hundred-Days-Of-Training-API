using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkoutPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalDays = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlans", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkoutTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutTemplate", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkoutDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DayNumber = table.Column<int>(type: "int", nullable: false),
                    WorkoutPlanId = table.Column<int>(type: "int", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Completed = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutDays_WorkoutPlans_WorkoutPlanId",
                        column: x => x.WorkoutPlanId,
                        principalTable: "WorkoutPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TemplateExercise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sets = table.Column<int>(type: "int", nullable: false),
                    Reps = table.Column<int>(type: "int", nullable: false),
                    RestSeconds = table.Column<int>(type: "int", nullable: false),
                    WorkoutTemplateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateExercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateExercise_WorkoutTemplate_WorkoutTemplateId",
                        column: x => x.WorkoutTemplateId,
                        principalTable: "WorkoutTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkoutTemplateScheduledDay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    WorkoutTemplateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutTemplateScheduledDay", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutTemplateScheduledDay_WorkoutTemplate_WorkoutTemplateId",
                        column: x => x.WorkoutTemplateId,
                        principalTable: "WorkoutTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkoutSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Reps = table.Column<int>(type: "int", nullable: false),
                    Completed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    WorkoutDayId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutSets_WorkoutDays_WorkoutDayId",
                        column: x => x.WorkoutDayId,
                        principalTable: "WorkoutDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateExercise_WorkoutTemplateId",
                table: "TemplateExercise",
                column: "WorkoutTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutDays_WorkoutPlanId",
                table: "WorkoutDays",
                column: "WorkoutPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSets_WorkoutDayId",
                table: "WorkoutSets",
                column: "WorkoutDayId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutTemplateScheduledDay_WorkoutTemplateId",
                table: "WorkoutTemplateScheduledDay",
                column: "WorkoutTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemplateExercise");

            migrationBuilder.DropTable(
                name: "WorkoutSets");

            migrationBuilder.DropTable(
                name: "WorkoutTemplateScheduledDay");

            migrationBuilder.DropTable(
                name: "WorkoutDays");

            migrationBuilder.DropTable(
                name: "WorkoutTemplate");

            migrationBuilder.DropTable(
                name: "WorkoutPlans");
        }
    }
}
