using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationalPlatform.Migrations
{
    /// <inheritdoc />
    public partial class add_options_And_CorrectAnswer_in_question : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionCorrectAnswers");

            migrationBuilder.DropTable(
                name: "QuizQuestions");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedDate",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswer",
                table: "Questiones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Option1",
                table: "Questiones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Option2",
                table: "Questiones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Option3",
                table: "Questiones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Option4",
                table: "Questiones",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "QuizId",
                table: "Questiones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Questiones_QuizId",
                table: "Questiones",
                column: "QuizId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questiones_Quizzes_QuizId",
                table: "Questiones",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questiones_Quizzes_QuizId",
                table: "Questiones");

            migrationBuilder.DropIndex(
                name: "IX_Questiones_QuizId",
                table: "Questiones");

            migrationBuilder.DropColumn(
                name: "CorrectAnswer",
                table: "Questiones");

            migrationBuilder.DropColumn(
                name: "Option1",
                table: "Questiones");

            migrationBuilder.DropColumn(
                name: "Option2",
                table: "Questiones");

            migrationBuilder.DropColumn(
                name: "Option3",
                table: "Questiones");

            migrationBuilder.DropColumn(
                name: "Option4",
                table: "Questiones");

            migrationBuilder.DropColumn(
                name: "QuizId",
                table: "Questiones");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Quizzes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_Questiones_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    QuizId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestions", x => new { x.QuestionId, x.QuizId });
                    table.ForeignKey(
                        name: "FK_QuizQuestions_Questiones_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizQuestions_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionCorrectAnswers",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    OptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionCorrectAnswers", x => new { x.QuestionId, x.OptionId });
                    table.ForeignKey(
                        name: "FK_QuestionCorrectAnswers_Options_OptionId",
                        column: x => x.OptionId,
                        principalTable: "Options",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuestionCorrectAnswers_Questiones_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionId",
                table: "Options",
                column: "QuestionId");

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

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_QuizId",
                table: "QuizQuestions",
                column: "QuizId");
        }
    }
}
