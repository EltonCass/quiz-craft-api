// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Application.QuizManagement;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.CategoryManagement;

public interface ICategoryHandler
{
    Task<IEnumerable<CategoryDTO>> RetrieveCategories(CancellationToken cancellationToken);
    Task<OneOf<CategoryDTO, RequestError>> RetrieveCategory(int id, CancellationToken cancellationToken);
    Task<OneOf<CategoryDTO, RequestError>> CreateCategory(CategoryDTO newCategory, CancellationToken cancellationToken);
    Task<OneOf<CategoryDTO, RequestError>> UpdateCategory(int id, CategoryDTO category, CancellationToken cancellationToken);
    Task<OneOf<CategoryDTO, RequestError>> DeleteCategory(int id, CancellationToken cancellationToken);
}
