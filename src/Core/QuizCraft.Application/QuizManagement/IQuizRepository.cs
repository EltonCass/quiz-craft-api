// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Models.Entities;

namespace QuizCraft.Application.QuizManagement;

public interface IQuizRepository
{
    Task<IEnumerable<Quiz>> RetrieveAllQuizzes(CancellationToken cancellationToken);
    Task<Quiz> RetrieveQuiz(int id, CancellationToken cancellationToken);
    Task<Quiz> CreateQuiz(Quiz newQuiz, CancellationToken cancellationToken);
    Task<Quiz> UpdateQuiz(Quiz quiz, CancellationToken cancellationToken);
    Task<Quiz> DeleteQuiz(int id, CancellationToken cancellationToken);
}
