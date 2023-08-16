// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OneOf;
using QuizCraft.Application.CategoryManagement;
using QuizCraft.Models;
using QuizCraft.Models.Entities;
using System.Net;
using static QuizCraft.Models.Constants.Constants;

namespace QuizCraft.Persistence.Categories;

public class CategoryRepository : ICategoryRepository
{
    private readonly QuizCraftContext _Context;
    private readonly IValidator<Category> _validator;

    public CategoryRepository(QuizCraftContext context, IValidator<Category> validator)
    {
        _Context = context;
        _validator = validator;
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

        await _Context.Categories
            .AddAsync(category, cancellationToken);
        _Context.ChangeTracker.DetectChanges();
        var debugView = _Context.ChangeTracker.DebugView.ShortView; //TODO Remove once its used
        var result = await _Context.SaveChangesAsync(cancellationToken);

        if (result == 0)
        {
            return new RequestError(
                HttpStatusCode.BadRequest, RequestErrorMessages.NoChanges);
        }

        return category;
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

        _Context.Categories.Remove(foundedCategory.AsT0);
        _Context.ChangeTracker.DetectChanges();
        var debugView = _Context.ChangeTracker.DebugView.ShortView; //TODO Remove once its used
        var result = await _Context.SaveChangesAsync(cancellationToken);

        if (result == 0)
        {
            return new RequestError(
                HttpStatusCode.BadRequest, RequestErrorMessages.NoChanges);
        }

        return foundedCategory;
    }
    
    public async Task<ICollection<Category>> GetCategories(
        CancellationToken cancellationToken)
    {
        return await _Context.Categories
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<OneOf<Category, RequestError>> GetCategory(
        int categoryId, CancellationToken cancellationToken)
    {
        var category = await _Context.Categories
            .Where(x => x.Id == categoryId)
            .FirstOrDefaultAsync(cancellationToken);

        if (category is null)
        {
            return new RequestError(HttpStatusCode.NotFound, RequestErrorMessages.CategoryNotFound);
        }

        return category;
    }

    public async Task<OneOf<Category, RequestError>> UpdateCategory(
        Category updatedCategory, CancellationToken cancellationToken)
    {
        var foundedCategory = await _Context.Categories
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
        _Context.ChangeTracker.DetectChanges();
        var debugView = _Context.ChangeTracker.DebugView.ShortView; //TODO Remove once its used
        var result = await _Context.SaveChangesAsync(cancellationToken);
        if (result == 0)
        {
            return new RequestError(
                HttpStatusCode.BadRequest, RequestErrorMessages.NoChanges);
        }

        return updatedCategory;
    }
}
