// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using MapsterMapper;
using OneOf;
using QuizCraft.Models;
using QuizCraft.Models.DTOs;
using QuizCraft.Models.Entities;
using Serilog;
using Serilog.Core;
using System.Net;

namespace QuizCraft.Application.Categories;

public class CategoryHandler : ICategoryHandler
{
    private readonly IValidator<CategoryForUpsert> _validator;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _Logger;

    public CategoryHandler(
        IValidator<CategoryForUpsert> validator,
        ICategoryRepository categoryRepository,
        IMapper mapper,
        ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(validator);
        ArgumentNullException.ThrowIfNull(categoryRepository);
        ArgumentNullException.ThrowIfNull(mapper);
        _validator = validator;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _Logger = logger;
    }

    public async Task<OneOf<CategoryForDisplay, RequestError>> CreateCategory(
        CategoryForUpsert newCategory, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(newCategory);
        if (!result.IsValid)
        {
            return new RequestError(
                HttpStatusCode.UnprocessableEntity,
                result.ToString());
        }

        var createdCategory = await _categoryRepository
                .CreateCategory(_mapper.Map<Category>(newCategory), cancellationToken);
        if (createdCategory.IsT1)
        {
            return createdCategory.AsT1;
        }

        var createdCategoryDto = _mapper
            .Map<CategoryForDisplay>(createdCategory.AsT0);
        return createdCategoryDto;
    }

    public async Task<OneOf<CategoryForDisplay, RequestError>> DeleteCategory(
        int id, CancellationToken cancellationToken)
    {
        var categoryResult = await _categoryRepository
            .DeleteCategory(id, cancellationToken);

        if (categoryResult.IsT1)
        {
            return categoryResult.AsT1;
        }

        var categoryDto = _mapper
            .Map<CategoryForDisplay>(categoryResult.AsT0);
        return categoryDto;
    }

    public async Task<IEnumerable<CategoryForDisplay>> RetrieveCategories(
        CancellationToken cancellationToken)
    {
        _Logger.Information("Retrieving Categories");
        var categories = await _categoryRepository
            .GetCategories(cancellationToken);

        _Logger.Information("{0} categories were retrieved", categories.Count);
        var categoriesDtos = _mapper
            .Map<ICollection<CategoryForDisplay>>(categories);
        return categoriesDtos;
    }

    public async Task<OneOf<CategoryForDisplay, RequestError>> RetrieveCategory(
        int id, CancellationToken cancellationToken)
    {
        var categoryResult = await _categoryRepository
            .GetCategory(id, cancellationToken);
        if (categoryResult.IsT1)
        {
            return categoryResult.AsT1;
        }

        var foundedCategory = _mapper
            .Map<CategoryForDisplay>(categoryResult.AsT0);
        return foundedCategory;
    }

    public async Task<OneOf<CategoryForDisplay, RequestError>> UpdateCategory(
        int id, CategoryForUpsert category, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(category);
        if (!result.IsValid)
        {
            return new RequestError(
                HttpStatusCode.UnprocessableEntity,
                result.ToString());
        }

        var categoryEntity = _mapper.Map<Category>(category);
        categoryEntity.Id = id;
        var categoryResult = await _categoryRepository
            .UpdateCategory(categoryEntity, cancellationToken);
        if (categoryResult.IsT1)
        {
            return categoryResult.AsT1;
        }

        var updatedCategory = _mapper
            .Map<CategoryForDisplay>(categoryResult.AsT0);
        return updatedCategory;
    }
}
