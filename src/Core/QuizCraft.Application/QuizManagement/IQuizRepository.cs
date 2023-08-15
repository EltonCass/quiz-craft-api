// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Models.Entities;

namespace QuizCraft.Application.QuizManagement;

public interface IQuizRepository
{
    Task<OneOf<Quiz, RequestError>> CreateQuiz(
        Quiz quiz, CancellationToken cancellationToken);
    Task<OneOf<Quiz, RequestError>> DeleteQuiz(
        int quizId, CancellationToken cancellationToken);
    Task<OneOf<Quiz, RequestError>> GetQuiz(
        int quizId, CancellationToken cancellationToken);
    Task<ICollection<Quiz>> GetQuizzes(CancellationToken cancellationToken);
    Task<OneOf<Quiz, RequestError>> UpdateQuiz(
        int quizId, Quiz updatedQuiz, CancellationToken cancellationToken);
}