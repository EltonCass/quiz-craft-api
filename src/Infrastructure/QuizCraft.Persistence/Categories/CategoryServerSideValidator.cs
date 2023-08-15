// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using QuizCraft.Models.Entities;
using static QuizCraft.Models.Constants.Constants;

namespace QuizCraft.Persistence.Categories;

internal class CategoryServerSideValidator : AbstractValidator<Category>
{
    private readonly QuizCraftContext _Context;

    public CategoryServerSideValidator(QuizCraftContext context)
    {
        _Context = context;

        RuleFor(c => c.Name)
            .MustAsync((c, cn,  cancellationToken) =>
            {
                return _Context.Categories
                    .AnyAsync(x => x.Name != cn || x.Id == c.Id, cancellationToken);
            })
            .WithMessage(ValidationMessages.NameNotUnique);

    }
}
