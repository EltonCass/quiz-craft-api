// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.Quizzes;

internal class QuizValidator : AbstractValidator<QuizDTO>
{
    public QuizValidator()
    {
        RuleFor(q => q.Description).NotEmpty();
    }
}
