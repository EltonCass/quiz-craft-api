// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.Categories;

internal class CategoryValidator : AbstractValidator<CategoryForUpsert>
{
    public CategoryValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(c => c.Description)
            .Length(5, 100)
            .When(c => !string.IsNullOrEmpty(c.Description));
    }
}
