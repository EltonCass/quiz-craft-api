// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Models.Entities;

namespace QuizCraft.Application.CategoryManagement;

public class CategoryRepository : ICategoryRepository
{
    public Task<Quiz> CreateCategory(Quiz newQuiz, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<Quiz> DeleteCategory(int id, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<IEnumerable<Quiz>> RetrieveCategories(CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<Quiz> RetrieveCategory(int id, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<Quiz> UpdateCategory(Quiz quiz, CancellationToken cancellationToken) => throw new NotImplementedException();
}
