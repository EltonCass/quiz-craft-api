// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Models.Constants;

namespace QuizCraft.Models.Entities;

public record MultipleOptionQuestion : BaseQuestion
{
    public IReadOnlyList<string> Options { get; }

    public MultipleOptionQuestion(
        int id, string text, IEnumerable<string> options, string correctAnswer, int score = 10)
        : base(id, correctAnswer, score, text, QuestionType.MultipleOption)
    {
        Options = new List<string>(options);
    }
}
