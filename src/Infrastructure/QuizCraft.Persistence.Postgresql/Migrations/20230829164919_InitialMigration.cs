using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuizCraft.Persistence.Postgresql.Migrations;

/// <inheritdoc />
public partial class InitialMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Categories",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Categories_Id", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                FullName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users_Id", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Quizzes",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                Score = table.Column<int>(type: "integer", nullable: true),
                CreatedByUserId = table.Column<int>(type: "integer", nullable: true),
                UpdatedByUserId = table.Column<int>(type: "integer", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Quizzes_Id", x => x.Id);
                table.ForeignKey(
                    name: "FK_Quizzes_CreatedByUserId_Users_Id",
                    column: x => x.CreatedByUserId,
                    principalTable: "Users",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Quizzes_UpdatedByUserId_Users_Id",
                    column: x => x.UpdatedByUserId,
                    principalTable: "Users",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "CategoryQuiz",
            columns: table => new
            {
                CategoriesId = table.Column<int>(type: "integer", nullable: false),
                QuizzesId = table.Column<int>(type: "integer", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CategoryQuiz", x => new { x.CategoriesId, x.QuizzesId });
                table.ForeignKey(
                    name: "FK_CategoryQuiz_Categories_CategoriesId",
                    column: x => x.CategoriesId,
                    principalTable: "Categories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_CategoryQuiz_Quizzes_QuizzesId",
                    column: x => x.QuizzesId,
                    principalTable: "Quizzes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Questions",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                QuizId = table.Column<int>(type: "integer", nullable: false),
                QuestionType = table.Column<int>(type: "integer", nullable: false),
                CorrectAnswer = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                Text = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                Score = table.Column<int>(type: "integer", nullable: true),
                PlacementOrder = table.Column<int>(type: "integer", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Questions_Id", x => x.Id);
                table.ForeignKey(
                    name: "FK_Questions_QuizId_Quizzes_Id",
                    column: x => x.QuizId,
                    principalTable: "Quizzes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "FillInBlankQuestions",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                WordPosition = table.Column<int>(type: "integer", nullable: false),
                QuestionId = table.Column<int>(type: "integer", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FillInBlankQuestions_Id", x => x.Id);
                table.ForeignKey(
                    name: "FK_FillInBlankQuestions_QuestionId_Questions_Id",
                    column: x => x.QuestionId,
                    principalTable: "Questions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "MultipleOptionQuestions",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                QuestionId = table.Column<int>(type: "integer", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MultipleOptionQuestions_Id", x => x.Id);
                table.ForeignKey(
                    name: "FK_MultipleOptionQuestions_QuestionId_Questions_Id",
                    column: x => x.QuestionId,
                    principalTable: "Questions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Options",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                MultipleOptionQuestionId = table.Column<int>(type: "integer", nullable: false),
                Text = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Options_Id", x => x.Id);
                table.ForeignKey(
                    name: "FK_Options_MultipleOptionQuestionId_MultipleOptionQuestions_Id",
                    column: x => x.MultipleOptionQuestionId,
                    principalTable: "MultipleOptionQuestions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "Categories",
            columns: new[] { "Id", "Description", "Name" },
            values: new object[] { 1, "Quizzes Related to C# Programming", "C# Programming" });

        migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "Id", "Email", "FullName" },
            values: new object[] { 1, "admin@gmail.com", "Admin" });

        migrationBuilder.InsertData(
            table: "Quizzes",
            columns: new[] { "Id", "CreatedAt", "CreatedByUserId", "Description", "Score", "Title", "UpdatedAt", "UpdatedByUserId" },
            values: new object[] { 1, new DateTime(2023, 8, 29, 16, 49, 19, 79, DateTimeKind.Utc).AddTicks(9816), 1, "This Quiz contains basic questions about C#.", null, "C# Fundamentals", null, null });

        migrationBuilder.InsertData(
            table: "Questions",
            columns: new[] { "Id", "CorrectAnswer", "CreatedAt", "PlacementOrder", "QuestionType", "QuizId", "Score", "Text", "UpdatedAt" },
            values: new object[,]
            {
                { 1, "Common Language Runtime", new DateTime(2023, 8, 29, 16, 49, 19, 79, DateTimeKind.Utc).AddTicks(9851), null, 0, 1, null, "What does the acronym \"CLR\" stand for in the context of C# and .NET?", null },
                { 2, "property", new DateTime(2023, 8, 29, 16, 49, 19, 79, DateTimeKind.Utc).AddTicks(9854), null, 0, 1, null, "In C#, a ______ is a class member that encapsulates a get accessor and an optional set accessor to provide controlled access to an object's state", null },
            });

        migrationBuilder.InsertData(
            table: "FillInBlankQuestions",
            columns: new[] { "Id", "QuestionId", "WordPosition" },
            values: new object[] { 1, 2, 4 });

        migrationBuilder.InsertData(
            table: "MultipleOptionQuestions",
            columns: new[] { "Id", "QuestionId" },
            values: new object[] { 1, 1 });

        migrationBuilder.InsertData(
            table: "Options",
            columns: new[] { "Id", "MultipleOptionQuestionId", "Text" },
            values: new object[,]
            {
                { 1, 1, "Common Language Runtime" },
                { 2, 1, "Core Language Runtime" },
                { 3, 1, "Compiled Language Runtime" },
            });

        migrationBuilder.CreateIndex(
            name: "UQ_Categories_Name",
            table: "Categories",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_CategoryQuiz_QuizzesId",
            table: "CategoryQuiz",
            column: "QuizzesId");

        migrationBuilder.CreateIndex(
            name: "IX_FillInBlankQuestions_QuestionId",
            table: "FillInBlankQuestions",
            column: "QuestionId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_MultipleOptionQuestions_QuestionId",
            table: "MultipleOptionQuestions",
            column: "QuestionId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Options_MultipleOptionQuestionId",
            table: "Options",
            column: "MultipleOptionQuestionId");

        migrationBuilder.CreateIndex(
            name: "IX_Questions_QuizId",
            table: "Questions",
            column: "QuizId");

        migrationBuilder.CreateIndex(
            name: "IX_Quizzes_CreatedByUserId",
            table: "Quizzes",
            column: "CreatedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_Quizzes_UpdatedByUserId",
            table: "Quizzes",
            column: "UpdatedByUserId");

        migrationBuilder.CreateIndex(
            name: "UQ_Users_Email",
            table: "Users",
            column: "Email",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CategoryQuiz");

        migrationBuilder.DropTable(
            name: "FillInBlankQuestions");

        migrationBuilder.DropTable(
            name: "Options");

        migrationBuilder.DropTable(
            name: "Categories");

        migrationBuilder.DropTable(
            name: "MultipleOptionQuestions");

        migrationBuilder.DropTable(
            name: "Questions");

        migrationBuilder.DropTable(
            name: "Quizzes");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
