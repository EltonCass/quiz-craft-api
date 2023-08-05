// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Models.Entities;

namespace QuizCraft.Application.CategoryManagement;

public interface ICategoryRepository
{
    Task<IEnumerable<Quiz>> RetrieveCategories(CancellationToken cancellationToken);
    Task<Quiz> RetrieveCategory(int id, CancellationToken cancellationToken);
    Task<Quiz> CreateCategory(Quiz newQuiz, CancellationToken cancellationToken);
    Task<Quiz> UpdateCategory(Quiz quiz, CancellationToken cancellationToken);
    Task<Quiz> DeleteCategory(int id, CancellationToken cancellationToken);
}
