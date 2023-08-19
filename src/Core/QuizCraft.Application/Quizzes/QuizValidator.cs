// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.Quizzes;

internal class QuizValidator : AbstractValidator<QuizForUpsert>
{
    public QuizValidator()
    {
        RuleFor(q => q.Title)
            .NotEmpty()
            .Length(2, 200);
        RuleFor(q => q.Description)
            .NotEmpty()
            .Length(5, 200);
        RuleFor(q => q.Categories)
            .Must(c => c
                .GroupBy(x => x.Name)
                .All(g => g.Count() == 1))
            .WithMessage("Cannot have categories with the same name.")
            .Must(c => c.Count <= 5)
            .WithMessage("Cannot have more than 5 categories")
            .When(c => c.Categories.Any());
    }
}
