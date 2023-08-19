// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.Quizzes.Questions;

internal class FillInBlankQuestionValidator : AbstractValidator<FillInBlankQuestionDTO>
{
    public FillInBlankQuestionValidator()
    {
        RuleFor(q => q.CorrectAnswer).NotEmpty();
        RuleFor(q => q.Text).NotEmpty();
        RuleFor(q => q.Score).GreaterThan(0);
        RuleFor(q => q.WordPosition).GreaterThan(0);
    }
}
