using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalPlatform.Migrations
{
    /// <inheritdoc />
    public partial class remove_studentAnswers_and_makeitoneonswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAnswers");

            migrationBuilder.AddColumn<string>(
                name: "StudentAnswer",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentAnswer",
                table: "Answers");

            migrationBuilder.CreateTable(
                name: "StudentAnswers",
                columns: table => new
                {
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    _StudentAnswer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAnswers", x => new { x.AnswerId, x._StudentAnswer });
                    table.ForeignKey(
                        name: "FK_StudentAnswers_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
