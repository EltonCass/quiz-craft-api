// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Models.Constants;

namespace QuizCraft.Models.DTOs;

public record MultipleOptionQuestion : BaseQuestion
{
    public IReadOnlyList<string> Options { get; }

    public MultipleOptionQuestion(
        int Id, int QuizId, string Text, IEnumerable<string> Options, string CorrectAnswer, int? Score = null, short? Order = null)
        : base(Id, QuizId, CorrectAnswer, Text, QuestionType.MultipleOption, Score, Order)
    {
        this.Options = new List<string>(Options);
    }
}
