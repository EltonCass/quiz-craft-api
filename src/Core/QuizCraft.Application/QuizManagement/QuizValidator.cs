// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using QuizCraft.Models.Entities;

namespace QuizCraft.Application.QuizManagement;

internal class QuizValidator : AbstractValidator<Quiz>
{
    public QuizValidator()
    {
        RuleFor(q => q.Description).NotEmpty();
    }
}
