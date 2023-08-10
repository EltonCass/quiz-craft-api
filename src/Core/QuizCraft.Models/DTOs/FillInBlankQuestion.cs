// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Models.Constants;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Models.DTOs;

public record FillInBlankQuestion : BaseQuestion
{
    public int WordPosition { get; init; }
    public FillInBlankQuestion(
        int Id, int QuizId, string Text, string CorrectAnswer, int Position, int? Score = null, short? Order = null)
        : base(Id, QuizId, CorrectAnswer, Text, QuestionType.FillInBlank, Score, Order)
    {
        WordPosition = Position;
    }
}
