using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalPlatform.Migrations
{
    /// <inheritdoc />
    public partial class updatev7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CorrectAnswer",
                table: "QuestionCorrectAnswers",
                newName: "OptionId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Quizzes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_QuestionCorrectAnswers_OptionId",
                table: "QuestionCorrectAnswers",
                column: "OptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionCorrectAnswers_QuestionId",
                table: "QuestionCorrectAnswers",
                column: "QuestionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionCorrectAnswers_Options_OptionId",
                table: "QuestionCorrectAnswers",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionCorrectAnswers_Options_OptionId",
                table: "QuestionCorrectAnswers");

            migrationBuilder.DropIndex(
                name: "IX_QuestionCorrectAnswers_OptionId",
                table: "QuestionCorrectAnswers");

            migrationBuilder.DropIndex(
                name: "IX_QuestionCorrectAnswers_QuestionId",
                table: "QuestionCorrectAnswers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Quizzes");

            migrationBuilder.RenameColumn(
                name: "OptionId",
                table: "QuestionCorrectAnswers",
                newName: "CorrectAnswer");
        }
    }
}
