// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Models;
using QuizCraft.Models.Entities;

namespace QuizCraft.Application.Categories;

public interface ICategoryRepository
{
    Task<OneOf<Category, RequestError>> CreateCategory(
        Category category, CancellationToken cancellationToken);
    Task<OneOf<Category, RequestError>> UpdateCategory(
        Category updatedCategory, CancellationToken cancellationToken);
    Task<OneOf<Category, RequestError>> DeleteCategory(
        int categoryId, CancellationToken cancellationToken);
    Task<ICollection<Category>> GetCategories(CancellationToken cancellationToken);
    Task<OneOf<Category, RequestError>> GetCategory(
        int categoryId, CancellationToken cancellationToken);
    Task<OneOf<ICollection<Category>, RequestError>> AddCategoriesByNames(
        ICollection<Category> categories, CancellationToken cancellationToken);
    Task<(ICollection<Category> MatchingCategories, ICollection<Category> NonMatchingCategories)> 
        GetCategoriesByNames(string[] categories, CancellationToken cancellationToken);
}
