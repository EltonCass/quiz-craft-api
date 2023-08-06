// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Models.Entities;

namespace QuizCraft.Application.QuizManagement;

public class QuizRepository : IQuizRepository
{
    public Task<Quiz> CreateQuiz(Quiz newQuiz, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<Quiz> DeleteQuiz(int id, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<IEnumerable<Quiz>> RetrieveAllQuizzes(CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<Quiz> RetrieveQuiz(int id, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<Quiz> UpdateQuiz(Quiz quiz, CancellationToken cancellationToken) => throw new NotImplementedException();
}
