using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediFlow.Modules.Scheduling.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "scheduling");

            migrationBuilder.CreateTable(
                name: "PractitionerAvailabilities",
                schema: "scheduling",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PractitionerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PractitionerAvailabilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchedulingPractitionerReadModels",
                schema: "scheduling",
                columns: table => new
                {
                    PractitionerId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulingPractitionerReadModels", x => x.PractitionerId);
                });

            migrationBuilder.CreateTable(
                name: "WeeklyAvailabilityRules",
                schema: "scheduling",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    PractitionerAvailabilityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyAvailabilityRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeeklyAvailabilityRules_PractitionerAvailabilities_Practiti~",
                        column: x => x.PractitionerAvailabilityId,
                        principalSchema: "scheduling",
                        principalTable: "PractitionerAvailabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyAvailabilityRules_PractitionerAvailabilityId",
                schema: "scheduling",
                table: "WeeklyAvailabilityRules",
                column: "PractitionerAvailabilityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchedulingPractitionerReadModels",
                schema: "scheduling");

            migrationBuilder.DropTable(
                name: "WeeklyAvailabilityRules",
                schema: "scheduling");

            migrationBuilder.DropTable(
                name: "PractitionerAvailabilities",
                schema: "scheduling");
        }
    }
}
