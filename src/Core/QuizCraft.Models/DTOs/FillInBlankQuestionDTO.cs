using QuizCraft.Models.Constants;

namespace QuizCraft.Models.DTOs
{
    public record FillInBlankQuestionDTO : QuestionDTO
    {
        public int WordPosition { get; init; }
        public FillInBlankQuestionDTO(
            int Id, int QuizId, string Text, string CorrectAnswer, int Position, int? Score = null, short? Order = null)
            : base(Id, QuizId, CorrectAnswer, Text, QuestionType.FillInBlank, Score, Order)
        {
            WordPosition = Position;
        }
    }
}
