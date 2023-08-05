// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.Entities;

public record MultipleOptionQuestion : BaseQuestion
{
    public IReadOnlyList<string> Options { get; }

    public MultipleOptionQuestion(
        int id, string text, IEnumerable<string> options, string correctAnswer, int score = 10)
        : base(id, correctAnswer, score, text)
    {
        Options = new List<string>(options);
    }
}
