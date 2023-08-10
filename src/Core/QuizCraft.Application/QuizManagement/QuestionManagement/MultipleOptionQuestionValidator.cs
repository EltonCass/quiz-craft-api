// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.QuizManagement.QuestionManagement;

internal class MultipleOptionQuestionValidator : AbstractValidator<MultipleOptionQuestion>
{
    public MultipleOptionQuestionValidator()
    {
        RuleFor(q => q.CorrectAnswer).NotEmpty();
        RuleFor(q => q.Text).NotEmpty();
        RuleFor(q => q.Score).GreaterThan(0);
        RuleFor(q => q.Options.Count).GreaterThan(2);
    }
}
