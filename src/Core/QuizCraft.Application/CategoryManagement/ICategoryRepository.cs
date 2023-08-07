// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Application.QuizManagement;
using QuizCraft.Models.Entities;

namespace QuizCraft.Application.CategoryManagement;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> RetrieveCategories(CancellationToken cancellationToken);
    Task<OneOf<Category, RequestError>> RetrieveCategory(int id, CancellationToken cancellationToken);
    Task<OneOf<Category, RequestError>> CreateCategory(Category newCategory, CancellationToken cancellationToken);
    Task<OneOf<Category, RequestError>> UpdateCategory(int id, Category category, CancellationToken cancellationToken);
    Task<OneOf<Category, RequestError>> DeleteCategory(int id, CancellationToken cancellationToken);
}
