// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using MapsterMapper;
using OneOf;
using QuizCraft.Models;
using QuizCraft.Models.DTOs;
using QuizCraft.Models.Entities;
using System.Net;

namespace QuizCraft.Application.Categories;

public class CategoryHandler : ICategoryHandler
{
    private readonly IValidator<CategoryForUpsert> _validator;
    private readonly ICategoryRepository _CategoryRepository;
    private readonly IMapper _Mapper;

    public CategoryHandler(
        IValidator<CategoryForUpsert> validator,
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(validator);
        ArgumentNullException.ThrowIfNull(categoryRepository);
        ArgumentNullException.ThrowIfNull(mapper);
        _validator = validator;
        _CategoryRepository = categoryRepository;
        _Mapper = mapper;
    }

    public async Task<OneOf<CategoryForDisplay, RequestError>> CreateCategory(
        CategoryForUpsert newCategoryDto, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(newCategoryDto);
        if (!result.IsValid)
        {
            return new RequestError(
                HttpStatusCode.UnprocessableEntity,
                result.ToString());
        }

        var createdCategory = await _CategoryRepository
                .CreateCategory(_Mapper.Map<Category>(newCategoryDto), cancellationToken);
        if (createdCategory.IsT1)
        {
            return createdCategory.AsT1;
        }

        var createdCategoryDto = _Mapper
            .Map<CategoryForDisplay>(createdCategory.AsT0);
        return createdCategoryDto;
    }

    public async Task<OneOf<CategoryForDisplay, RequestError>> DeleteCategory(
        int id, CancellationToken cancellationToken)
    {
        var categoryResult = await _CategoryRepository
            .DeleteCategory(id, cancellationToken);

        if (categoryResult.IsT1)
        {
            return categoryResult.AsT1;
        }

        var categoryDto = _Mapper
            .Map<CategoryForDisplay>(categoryResult.AsT0);
        return categoryDto;
    }

    public async Task<IEnumerable<CategoryForDisplay>> RetrieveCategories(
        CancellationToken cancellationToken)
    {
        var categories = await _CategoryRepository
            .GetCategories(cancellationToken);


        var categoriesDtos = _Mapper
            .Map<ICollection<CategoryForDisplay>>(categories);
        return categoriesDtos;
    }

    public async Task<OneOf<CategoryForDisplay, RequestError>> RetrieveCategory(
        int id, CancellationToken cancellationToken)
    {
        var categoryResult = await _CategoryRepository
            .GetCategory(id, cancellationToken);
        if (categoryResult.IsT1)
        {
            return categoryResult.AsT1;
        }

        var foundedCategory = _Mapper
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

        var categoryEntity = _Mapper.Map<Category>(category);
        categoryEntity.Id = id;
        var categoryResult = await _CategoryRepository
            .UpdateCategory(categoryEntity, cancellationToken);
        if (categoryResult.IsT1)
        {
            return categoryResult.AsT1;
        }

        var updatedCategory = _Mapper
            .Map<CategoryForDisplay>(categoryResult.AsT0);
        return updatedCategory;
    }
}
