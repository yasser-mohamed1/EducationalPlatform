using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalPlatform.Migrations
{
    /// <inheritdoc />
    public partial class re : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Subjects_SubjectId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "Enrollments",
                newName: "SubjectIdd");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_SubjectId",
                table: "Enrollments",
                newName: "IX_Enrollments_SubjectIdd");

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
                name: "FK_Enrollments_Subjects_SubjectIdd",
                table: "Enrollments",
                column: "SubjectIdd",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Enrollments_Enrollmentid",
                table: "Subjects",
                column: "Enrollmentid",
                principalTable: "Enrollments",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Subjects_SubjectIdd",
                table: "Enrollments");

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

            migrationBuilder.RenameColumn(
                name: "SubjectIdd",
                table: "Enrollments",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_SubjectIdd",
                table: "Enrollments",
                newName: "IX_Enrollments_SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Subjects_SubjectId",
                table: "Enrollments",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
