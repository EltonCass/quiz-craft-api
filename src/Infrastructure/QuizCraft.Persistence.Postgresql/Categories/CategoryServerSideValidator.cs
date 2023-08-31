// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using QuizCraft.Models.Entities;
using static QuizCraft.Models.Constants.Constants;

namespace QuizCraft.Persistence.Postgresql.Categories;

internal class CategoryServerSideValidator : AbstractValidator<Category>
{
    private readonly QuizCraftContext _context;

    public CategoryServerSideValidator(QuizCraftContext context)
    {
        _context = context;

        RuleFor(c => c.Name)
            .MustAsync(async (c, cn, cancellationToken) =>
            {
                var res = await _context.Categories
                    .AllAsync(x => x.Name != cn || x.Id == c.Id, cancellationToken);
                return res;
            })
            .WithMessage(ValidationMessages.NameNotUnique);
    }
}
