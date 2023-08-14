using QuizCraft.Models.Constants;

namespace QuizCraft.Models.DTOs;

public record MultipleOptionQuestionDTO : QuestionDTO
{
    public IReadOnlyList<string> Options { get; }

    public MultipleOptionQuestionDTO(
        int Id, int QuizId, string Text, IEnumerable<string> Options, string CorrectAnswer, int? Score = null, short? Order = null)
        : base(Id, QuizId, CorrectAnswer, Text, QuestionType.MultipleOption, Score, Order)
    {
        this.Options = new List<string>(Options);
    }
}
