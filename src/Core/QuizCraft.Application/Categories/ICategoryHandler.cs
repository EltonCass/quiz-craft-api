// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Models;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.Categories;

public interface ICategoryHandler
{
    Task<IEnumerable<CategoryForDisplay>> RetrieveCategories(
        CancellationToken cancellationToken);
    Task<OneOf<CategoryForDisplay, RequestError>> RetrieveCategory(
        int id, CancellationToken cancellationToken);
    Task<OneOf<CategoryForDisplay, RequestError>> CreateCategory(
        CategoryForUpsert newCategory, CancellationToken cancellationToken);
    Task<OneOf<CategoryForDisplay, RequestError>> UpdateCategory(
        int id, CategoryForUpsert category, CancellationToken cancellationToken);
    Task<OneOf<CategoryForDisplay, RequestError>> DeleteCategory(
        int id, CancellationToken cancellationToken);
}
