using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalPlatform.Migrations
{
    /// <inheritdoc />
    public partial class updatev8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionCorrectAnswers_Options_OptionId",
                table: "QuestionCorrectAnswers");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionCorrectAnswers_Options_OptionId",
                table: "QuestionCorrectAnswers",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionCorrectAnswers_Options_OptionId",
                table: "QuestionCorrectAnswers");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionCorrectAnswers_Options_OptionId",
                table: "QuestionCorrectAnswers",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
