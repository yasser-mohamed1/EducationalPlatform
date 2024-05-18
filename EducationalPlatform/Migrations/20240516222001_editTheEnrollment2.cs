using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalPlatform.Migrations
{
    /// <inheritdoc />
    public partial class editTheEnrollment2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfMonthes",
                table: "Enrollments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfMonthes",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
