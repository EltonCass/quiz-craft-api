// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Models.Entities;

namespace QuizCraft.Application.CategoryManagement;

public interface ICategoryRepository
{
    Task<Category> CreateCategory(Category category, CancellationToken cancellationToken);
    Task<Category> UpdateCategory(
        int categoryId, Category updatedCategory, CancellationToken cancellationToken);
    Task<Category> DeleteCategory(int categoryId, CancellationToken cancellationToken);
    Task<ICollection<Category>> GetCategories(CancellationToken cancellationToken);
    Task<Category> GetCategory(int categoryId, CancellationToken cancellationToken);
}
