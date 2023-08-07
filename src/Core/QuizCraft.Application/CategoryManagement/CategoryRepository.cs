// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using OneOf;
using QuizCraft.Application.QuizManagement;
using QuizCraft.Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace QuizCraft.Application.CategoryManagement;

public class CategoryRepository : ICategoryRepository
{
    private readonly IValidator<Category> _validator;
    private const short _DelayInMs = 100;

    public CategoryRepository(IValidator<Category> validator)
    {
        ArgumentNullException.ThrowIfNull(validator);
        _validator = validator;
    }

    public async Task<OneOf<Category, RequestError>> CreateCategory(Category newCategory, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(newCategory);
        await Task.Delay(_DelayInMs, cancellationToken);
        if (result.IsValid)
        {
            Stubs.Categories.Add(newCategory);
            return newCategory;
        }

        return new RequestError(
            HttpStatusCode.UnprocessableEntity,
            result.ToString());
    }

    

    public async Task<OneOf<Category, RequestError>> DeleteCategory(int id, CancellationToken cancellationToken)
    {
        var foundedCategory = Stubs.Categories.FirstOrDefault(c => c.Id == id);

        if (foundedCategory is null)
        {
            return new RequestError(HttpStatusCode.NotFound, "Category does not exist");
        }

        // Delete Category for quizzes
        var quizzesCategories = Stubs.Quizzes
            .Where(q => q.Categories.Select(c => c.Id).Contains(id))
            .SelectMany(q => q.Categories)
            .ToList();

        if (quizzesCategories.Any())
        {
            quizzesCategories.RemoveAll(c => c.Id == id);
        }

        var isSuccessful = Stubs.Categories.Remove(foundedCategory);
        if (!isSuccessful)
        {
            return new RequestError(HttpStatusCode.BadRequest, "Category was not removed");
        }

        await Task.Delay(_DelayInMs , cancellationToken);
        return foundedCategory;
    }

    public async Task<IEnumerable<Category>> RetrieveCategories(CancellationToken cancellationToken)
    {
        await Task.Delay(_DelayInMs, cancellationToken);
        return Stubs.Categories;
    }

    public async Task<OneOf<Category, RequestError>> RetrieveCategory(int id, CancellationToken cancellationToken)
    {
        await Task.Delay(_DelayInMs, cancellationToken);
        var foundedCategory = Stubs.Categories.FirstOrDefault(c => c.Id == id);
        if (foundedCategory is null)
        {
            return new RequestError(HttpStatusCode.NotFound, "Category not found");
        }

        return foundedCategory;
    }

    public async Task<OneOf<Category, RequestError>> UpdateCategory(
        int id,  Category category, CancellationToken cancellationToken)
    {
        var foundedCategory = Stubs.Categories.FirstOrDefault(c => c.Id == id);
        if (foundedCategory is null)
        {
            return new RequestError(HttpStatusCode.NotFound, "Category not found");
        }

        var result = _validator.Validate(category);
        if (!result.IsValid) 
        {
            return new RequestError(
                HttpStatusCode.UnprocessableEntity,
                result.ToString());
        }

        // Update existing categories
        var quizzesCategories = Stubs.Quizzes
            .Where(q => q.Categories.Select(c => c.Id).Contains(id))
            .SelectMany(q => q.Categories)
            .ToList();

        if (quizzesCategories.Any())
        {
            for (int i = 0; i < quizzesCategories.Count - 1; i++)
            {
                quizzesCategories[i] = category;
            }
        }

        foundedCategory = category;
        await Task.Delay(_DelayInMs, cancellationToken);
        return category;
    }
}
