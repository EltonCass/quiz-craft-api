using QuizCraft.Models.Constants;

namespace QuizCraft.Models.DTOs;

public record QuestionForDisplay(
    int Id,
    int QuizId,
    string CorrectAnswer,
    string Text,
    QuestionType Type,
    int? Score = null,
    short? Order = null);
