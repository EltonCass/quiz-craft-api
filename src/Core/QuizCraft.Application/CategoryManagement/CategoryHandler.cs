// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using Mapster;
using OneOf;
using QuizCraft.Application.QuizManagement;
using QuizCraft.Models.DTOs;
using QuizCraft.Models.Entities;
using System.Net;

namespace QuizCraft.Application.CategoryManagement;

public class CategoryHandler : ICategoryHandler
{
    private readonly IValidator<CategoryDTO> _validator;
    private readonly ICategoryRepository _CategoryRepository;

    public CategoryHandler(
        IValidator<CategoryDTO> validator,
        ICategoryRepository categoryRepository)
    {
        ArgumentNullException.ThrowIfNull(validator);
        ArgumentNullException.ThrowIfNull(categoryRepository);
        _validator = validator;
        _CategoryRepository = categoryRepository;
    }

    public async Task<OneOf<CategoryDTO, RequestError>> CreateCategory(
        CategoryDTO newCategoryDto, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(newCategoryDto);
        if (!result.IsValid)
        {
            return new RequestError(
            HttpStatusCode.UnprocessableEntity,
            result.ToString());
        }

        var createdCategory = await _CategoryRepository
                .CreateCategory(newCategoryDto.Adapt<Category>(), cancellationToken);
        if (createdCategory.IsT1)
        {
            return createdCategory.AsT1;
        }

        var createdCategoryDto = createdCategory.AsT0.Adapt<CategoryDTO>();
        return createdCategoryDto;
    }

    public async Task<OneOf<CategoryDTO, RequestError>> DeleteCategory(int id, CancellationToken cancellationToken)
    {
        var categoryResult = await _CategoryRepository
            .DeleteCategory(id, cancellationToken);

        if (categoryResult.IsT1)
        {
            return categoryResult.AsT1;
        }

        var categoryDto = categoryResult.AsT0.Adapt<CategoryDTO>();
        return categoryDto;
    }

    public async Task<IEnumerable<CategoryDTO>> RetrieveCategories(CancellationToken cancellationToken)
    {
        var categories = await _CategoryRepository
            .GetCategories(cancellationToken);

        var categoriesDtos = categories.Adapt<ICollection<CategoryDTO>>();
        return categoriesDtos;
    }

    public async Task<OneOf<CategoryDTO, RequestError>> RetrieveCategory(int id, CancellationToken cancellationToken)
    {
        var categoryResult = await _CategoryRepository
            .GetCategory(id, cancellationToken);
        if (categoryResult.IsT1)
        {
            return categoryResult.AsT1;
        }

        var foundedCategory = categoryResult.AsT0.Adapt<CategoryDTO>();
        return foundedCategory;
    }

    public async Task<OneOf<CategoryDTO, RequestError>> UpdateCategory(
        int id, CategoryDTO category, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(category);
        if (!result.IsValid)
        {
            return new RequestError(
                HttpStatusCode.UnprocessableEntity,
                result.ToString());
        }

        var categoryEntity = category.Adapt<Category>();
        categoryEntity.Id = id;
        var categoryResult = await _CategoryRepository
            .UpdateCategory(categoryEntity, cancellationToken);
        if (categoryResult.IsT1)
        {
            return categoryResult.AsT1;
        }

        var updatedCategory = categoryResult.AsT0.Adapt<CategoryDTO>();
        return updatedCategory;

        //var quizzesCategories = Stubs.Quizzes
        //    .Where(q => q.Categories.Select(c => c.Id).Contains(id))
        //    .SelectMany(q => q.Categories)
        //    .ToList();

        //if (quizzesCategories.Any())
        //{
        //    for (int i = 0; i < quizzesCategories.Count - 1; i++)
        //    {
        //        quizzesCategories[i] = category;
        //    }
        //}

        //foundedCategory = category;
    }
}
