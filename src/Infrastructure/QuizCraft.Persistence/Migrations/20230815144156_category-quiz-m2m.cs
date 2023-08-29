using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuizCraft.Persistence.Migrations;

/// <inheritdoc />
public partial class categoryquizm2m : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_FillInBlankQuestions_QuestionId_Questions_Id",
            table: "Questions");

        migrationBuilder.DropForeignKey(
            name: "FK_MultipleOptionQuestions_QuestionId_Questions_Id",
            table: "Questions");

        migrationBuilder.DropForeignKey(
            name: "FK_Quizzes_Categories_CategoryId",
            table: "Quizzes");

        migrationBuilder.DropForeignKey(
            name: "FK_Quizzes_Quizzes_QuizId",
            table: "Quizzes");

        migrationBuilder.DropIndex(
            name: "IX_Quizzes_CategoryId",
            table: "Quizzes");

        migrationBuilder.DropIndex(
            name: "IX_Quizzes_QuizId",
            table: "Quizzes");

        migrationBuilder.DropColumn(
            name: "CategoryId",
            table: "Quizzes");

        migrationBuilder.DropColumn(
            name: "QuizId",
            table: "Quizzes");

        migrationBuilder.DropForeignKey(
            name: "FK_Questions_QuizId_Quizzes_Id",
            table: "Questions");

        migrationBuilder.DropPrimaryKey(
            "PK_Questions_Id",
            "Questions");

        migrationBuilder.DropColumn(
            name: "Id",
            table: "Questions");

        migrationBuilder.AddColumn<int>(
            name: "Id",
            table: "Questions",
            type: "int",
            nullable: false)
            .Annotation("SqlServer:Identity", "1, 1");

        migrationBuilder.AddPrimaryKey(
           name: "PK_Questions_Id",
           table: "Questions",
           column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Questions_QuizId_Quizzes_Id",
            table: "Questions",
            column: "QuizId",
            principalTable: "Quizzes",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.CreateTable(
            name: "CategoryQuiz",
            columns: table => new
            {
                CategoriesId = table.Column<int>(type: "int", nullable: false),
                QuizzesId = table.Column<int>(type: "int", nullable: false),
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

        migrationBuilder.InsertData(
            table: "Questions",
            columns: new[] { "Id", "CorrectAnswer", "CreatedAt", "PlacementOrder", "QuizId", "Score", "Text", "UpdatedAt" },
            values: new object[,]
            {
                { 1, "Common Language Runtime", new DateTime(2023, 8, 15, 10, 41, 56, 83, DateTimeKind.Local).AddTicks(2193), null, 1, null, "What does the acronym \"CLR\" stand for in the context of C# and .NET?", null },
                { 2, "property", new DateTime(2023, 8, 15, 10, 41, 56, 83, DateTimeKind.Local).AddTicks(2208), null, 1, null, "In C#, a ______ is a class member that encapsulates a get accessor and an optional set accessor to provide controlled access to an object's state", null },
            });

        migrationBuilder.UpdateData(
            table: "Quizzes",
            keyColumn: "Id",
            keyValue: 1,
            column: "CreatedAt",
            value: new DateTime(2023, 8, 15, 14, 41, 56, 83, DateTimeKind.Utc).AddTicks(2109));

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
            name: "IX_MultipleOptionQuestions_QuestionId",
            table: "MultipleOptionQuestions",
            column: "QuestionId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_FillInBlankQuestions_QuestionId",
            table: "FillInBlankQuestions",
            column: "QuestionId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_CategoryQuiz_QuizzesId",
            table: "CategoryQuiz",
            column: "QuizzesId");

        migrationBuilder.AddForeignKey(
            name: "FK_FillInBlankQuestions_QuestionId_Questions_Id",
            table: "FillInBlankQuestions",
            column: "QuestionId",
            principalTable: "Questions",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_MultipleOptionQuestions_QuestionId_Questions_Id",
            table: "MultipleOptionQuestions",
            column: "QuestionId",
            principalTable: "Questions",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_FillInBlankQuestions_QuestionId_Questions_Id",
            table: "FillInBlankQuestions");

        migrationBuilder.DropForeignKey(
            name: "FK_MultipleOptionQuestions_QuestionId_Questions_Id",
            table: "MultipleOptionQuestions");

        migrationBuilder.DropTable(
            name: "CategoryQuiz");

        migrationBuilder.DropIndex(
            name: "IX_MultipleOptionQuestions_QuestionId",
            table: "MultipleOptionQuestions");

        migrationBuilder.DropIndex(
            name: "IX_FillInBlankQuestions_QuestionId",
            table: "FillInBlankQuestions");

        migrationBuilder.DeleteData(
            table: "FillInBlankQuestions",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Options",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Options",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "Options",
            keyColumn: "Id",
            keyValue: 3);

        migrationBuilder.DeleteData(
            table: "MultipleOptionQuestions",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 2);

        migrationBuilder.DeleteData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 1);

        migrationBuilder.AddColumn<int>(
            name: "CategoryId",
            table: "Quizzes",
            type: "int",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            name: "QuizId",
            table: "Quizzes",
            type: "int",
            nullable: true);

        migrationBuilder.AlterColumn<int>(
            name: "Id",
            table: "Questions",
            type: "int",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "int")
            .OldAnnotation("SqlServer:Identity", "1, 1");

        migrationBuilder.UpdateData(
            table: "Quizzes",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CategoryId", "CreatedAt", "QuizId" },
            values: new object[] { null, new DateTime(2023, 8, 14, 21, 3, 41, 341, DateTimeKind.Utc).AddTicks(7687), null });

        migrationBuilder.CreateIndex(
            name: "IX_Quizzes_CategoryId",
            table: "Quizzes",
            column: "CategoryId");

        migrationBuilder.CreateIndex(
            name: "IX_Quizzes_QuizId",
            table: "Quizzes",
            column: "QuizId");

        migrationBuilder.AddForeignKey(
            name: "FK_FillInBlankQuestions_QuestionId_Questions_Id",
            table: "Questions",
            column: "Id",
            principalTable: "FillInBlankQuestions",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_MultipleOptionQuestions_QuestionId_Questions_Id",
            table: "Questions",
            column: "Id",
            principalTable: "MultipleOptionQuestions",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Quizzes_Categories_CategoryId",
            table: "Quizzes",
            column: "CategoryId",
            principalTable: "Categories",
            principalColumn: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_Quizzes_Quizzes_QuizId",
            table: "Quizzes",
            column: "QuizId",
            principalTable: "Quizzes",
            principalColumn: "Id");
    }
}
