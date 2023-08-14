// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using OneOf;
using QuizCraft.Application.QuizManagement;
using QuizCraft.Models.DTOs;
using System.Net;

namespace QuizCraft.Application.CategoryManagement;

public class CategoryHandler : ICategoryHandler
{
    private readonly IValidator<CategoryDTO> _validator;
    private readonly ICategoryRepository _CategoryRepository;
    private const short _DelayInMs = 100;

    public CategoryHandler(
        IValidator<CategoryDTO> validator,
        ICategoryRepository categoryRepository)
    {
        ArgumentNullException.ThrowIfNull(validator);
        ArgumentNullException.ThrowIfNull(categoryRepository);
        _validator = validator;
        _CategoryRepository = categoryRepository;
    }

    public async Task<OneOf<CategoryDTO, RequestError>> CreateCategory(CategoryDTO newCategory, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(newCategory);
        if (result.IsValid)
        {
            var categoryEntity = CategoryDTO.ToEntity(newCategory);
            var category = await _CategoryRepository
                .CreateCategory(categoryEntity, cancellationToken);
            //Stubs.Categories.Add(newCategory);
            return newCategory;
        }

        return new RequestError(
            HttpStatusCode.UnprocessableEntity,
            result.ToString());
    }

    public async Task<OneOf<CategoryDTO, RequestError>> DeleteCategory(int id, CancellationToken cancellationToken)
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

    public async Task<IEnumerable<CategoryDTO>> RetrieveCategories(CancellationToken cancellationToken)
    {
        await Task.Delay(_DelayInMs, cancellationToken);
        return Stubs.Categories;
    }

    public async Task<OneOf<CategoryDTO, RequestError>> RetrieveCategory(int id, CancellationToken cancellationToken)
    {
        await Task.Delay(_DelayInMs, cancellationToken);
        var foundedCategory = Stubs.Categories.FirstOrDefault(c => c.Id == id);
        if (foundedCategory is null)
        {
            return new RequestError(HttpStatusCode.NotFound, "Category not found");
        }

        return foundedCategory;
    }

    public async Task<OneOf<CategoryDTO, RequestError>> UpdateCategory(
        int id, CategoryDTO category, CancellationToken cancellationToken)
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
