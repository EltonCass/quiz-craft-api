// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.Entities;

public record FillInBlankQuestion : BaseQuestion
{
    public FillInBlankQuestion(
        int id, string text, string correctAnswer, int score = 10) : base(id, correctAnswer, score, text)
    { }
}
