using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalPlatform.Migrations
{
    /// <inheritdoc />
    public partial class reE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Enrollments_Enrollmentid",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_Enrollmentid",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "Enrollmentid",
                table: "Subjects");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments");

            migrationBuilder.AddColumn<int>(
                name: "Enrollmentid",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Enrollmentid",
                table: "Subjects",
                column: "Enrollmentid");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Enrollments_Enrollmentid",
                table: "Subjects",
                column: "Enrollmentid",
                principalTable: "Enrollments",
                principalColumn: "id");
        }
    }
}
