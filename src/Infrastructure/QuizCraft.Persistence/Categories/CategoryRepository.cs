// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OneOf;
using QuizCraft.Application.Categories;
using QuizCraft.Models;
using QuizCraft.Models.Entities;
using System.Linq;
using System.Net;
using static QuizCraft.Models.Constants.Constants;

namespace QuizCraft.Persistence.Categories;

public class CategoryRepository : ICategoryRepository
{
    private readonly QuizCraftContext _context;
    private readonly IValidator<Category> _validator;

    public CategoryRepository(QuizCraftContext context, IValidator<Category> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<OneOf<ICollection<Category>, RequestError>> AddCategoriesByNames(
        ICollection<Category> categories, CancellationToken cancellationToken)
    {
        var newCategories = new List<Category>();
        foreach (var category in categories)
        {
            var categoryResult = await CreateCategory(
                category, cancellationToken);
            if (categoryResult.IsT1)
            {
                return categoryResult.AsT1;
            }

            newCategories.Add(categoryResult.AsT0);
        }

        return newCategories;
    }

    public async Task<OneOf<Category, RequestError>> CreateCategory(
        Category category, CancellationToken cancellationToken)
    {
        var validationResult = await _validator
            .ValidateAsync(category, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new RequestError(
                HttpStatusCode.UnprocessableEntity, validationResult.ToString());
        }

        await _context.Categories
            .AddAsync(category, cancellationToken);
        var result = await _context.SaveChangesAsync(cancellationToken);

        return result == 0
            ? new RequestError(HttpStatusCode.BadRequest, RequestErrorMessages.NoChanges)
            : category;
    }

    public async Task<OneOf<Category, RequestError>> DeleteCategory(
        int categoryId, CancellationToken cancellationToken)
    {
        var foundedCategory = await GetCategory(
            categoryId, cancellationToken);
        if (foundedCategory.IsT1)
        {
            return foundedCategory;
        }

        _context.Categories.Remove(foundedCategory.AsT0);
        var result = await _context.SaveChangesAsync(cancellationToken);

        return result == 0
            ? new RequestError(
                HttpStatusCode.BadRequest, RequestErrorMessages.NoChanges)
            : foundedCategory;
    }

    public async Task<ICollection<Category>> GetCategories(
        CancellationToken cancellationToken)
    {
        return await _context.Categories
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<(ICollection<Category> MatchingCategories,
        ICollection<Category> NonMatchingCategories)> GetCategoriesByNames(
        string[] categories, CancellationToken cancellationToken)
    {
        var matchingCategories = await _context.Categories.Where(
            c => categories.Contains(c.Name))
            .ToListAsync(cancellationToken);

        var matchingCategoriesNames = matchingCategories
            .Select(c => c.Name).ToArray();
        var nonMatchingCategories = new List<Category>();
        foreach (var category in categories)
        {
            if (!matchingCategoriesNames.Contains(category))
            {
                nonMatchingCategories.Add(
                    new Category() { Name = category });
            }
        }

        return (matchingCategories, nonMatchingCategories);
    }

    public async Task<OneOf<Category, RequestError>> GetCategory(
        int categoryId, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .Where(x => x.Id == categoryId)
            .FirstOrDefaultAsync(cancellationToken);

        return category is null
            ? new RequestError(HttpStatusCode.NotFound, RequestErrorMessages.CategoryNotFound)
            : category;
    }

    public async Task<OneOf<Category, RequestError>> UpdateCategory(
        Category updatedCategory, CancellationToken cancellationToken)
    {
        var foundedCategory = await _context.Categories
            .Where(x => x.Id == updatedCategory.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (foundedCategory is null)
        {
            return new RequestError(
                HttpStatusCode.NotFound, RequestErrorMessages.CategoryNotFound);
        }

        var validationResult = await _validator
            .ValidateAsync(updatedCategory, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new RequestError(
                HttpStatusCode.UnprocessableEntity, validationResult.ToString());
        }

        foundedCategory.Name = updatedCategory.Name;
        foundedCategory.Description = updatedCategory.Description;
        var result = await _context.SaveChangesAsync(cancellationToken);
        return result == 0
            ? new RequestError(
                HttpStatusCode.BadRequest, RequestErrorMessages.NoChanges)
            : updatedCategory;
    }
}
