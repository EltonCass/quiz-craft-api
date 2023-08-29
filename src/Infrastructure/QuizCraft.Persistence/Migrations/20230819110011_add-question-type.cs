using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizCraft.Persistence.Migrations;

/// <inheritdoc />
public partial class addquestiontype : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "QuestionType",
            table: "Questions",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.UpdateData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "CreatedAt", "QuestionType" },
            values: new object[] { new DateTime(2023, 8, 19, 7, 0, 10, 975, DateTimeKind.Local).AddTicks(8608), 0 });

        migrationBuilder.UpdateData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "CreatedAt", "QuestionType" },
            values: new object[] { new DateTime(2023, 8, 19, 7, 0, 10, 975, DateTimeKind.Local).AddTicks(8627), 0 });

        migrationBuilder.UpdateData(
            table: "Quizzes",
            keyColumn: "Id",
            keyValue: 1,
            column: "CreatedAt",
            value: new DateTime(2023, 8, 19, 11, 0, 10, 975, DateTimeKind.Utc).AddTicks(8543));

        migrationBuilder.InsertData(
            table: "CategoryQuiz",
            columns: new[] { "CategoriesId", "QuizzesId" },
            values: new object[,]
            {
                { 1, 1 },
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "QuestionType",
            table: "Questions");

        migrationBuilder.UpdateData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 1,
            column: "CreatedAt",
            value: new DateTime(2023, 8, 15, 10, 41, 56, 83, DateTimeKind.Local).AddTicks(2193));

        migrationBuilder.UpdateData(
            table: "Questions",
            keyColumn: "Id",
            keyValue: 2,
            column: "CreatedAt",
            value: new DateTime(2023, 8, 15, 10, 41, 56, 83, DateTimeKind.Local).AddTicks(2208));

        migrationBuilder.UpdateData(
            table: "Quizzes",
            keyColumn: "Id",
            keyValue: 1,
            column: "CreatedAt",
            value: new DateTime(2023, 8, 15, 14, 41, 56, 83, DateTimeKind.Utc).AddTicks(2109));
    }
}
