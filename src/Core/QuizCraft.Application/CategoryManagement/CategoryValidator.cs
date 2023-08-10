// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.CategoryManagement;

internal class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
    }
}
