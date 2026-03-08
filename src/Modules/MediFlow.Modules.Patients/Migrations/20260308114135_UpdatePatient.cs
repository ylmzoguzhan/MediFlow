using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediFlow.Modules.Patients.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "patients",
                table: "Patients",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Email",
                schema: "patients",
                table: "Patients",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_Email",
                schema: "patients",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "patients",
                table: "Patients");
        }
    }
}
