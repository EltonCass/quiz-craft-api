// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.Entities;

public record FillInBlankQuestion : BaseQuestion
{
    public int WordPosition { get; init; }
    public FillInBlankQuestion(
        int id, string text, string correctAnswer, int position, int score = 10)
        : base(id, correctAnswer, score, text, QuestionType.FillInBlank)
    {
        WordPosition = position;
    }
}
