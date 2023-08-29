// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.Quizzes.Questions;

internal class FillInBlankQuestionValidator : AbstractValidator<FillInBlankQuestionDTO>
{
    public FillInBlankQuestionValidator()
    {
        RuleFor(q => q.CorrectAnswer)
            .NotEmpty()
            .Length(1, 400);
        RuleFor(q => q.Text)
            .NotEmpty()
            .Length(5, 500);
        RuleFor(q => q.Score)
            .GreaterThan(0)
            .When(q => q.Score is not null);
        RuleFor(q => q.WordPosition)
            .GreaterThanOrEqualTo(0);
    }
}
