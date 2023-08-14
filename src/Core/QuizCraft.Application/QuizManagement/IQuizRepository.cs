// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Models.Entities;

namespace QuizCraft.Application.QuizManagement;

public interface IQuizRepository
{
    Task<Quiz> CreateQuiz(Quiz quiz, CancellationToken cancellationToken);
    Task<Quiz> DeleteQuiz(int quizId, CancellationToken cancellationToken);
    Task<Quiz> GetQuiz(int quizId, CancellationToken cancellationToken);
    Task<ICollection<Quiz>> GetQuizzes(CancellationToken cancellationToken);
    Task<Quiz> UpdateQuiz(int quizId, Quiz updatedQuiz, CancellationToken cancellationToken);
}