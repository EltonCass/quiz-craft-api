// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Models;
using QuizCraft.Models.Entities;

namespace QuizCraft.Application.Quizzes;

public interface IQuizRepository
{
    Task<OneOf<Quiz, RequestError>> CreateQuiz(
        Quiz quiz, CancellationToken cancellationToken, bool addNewCategories = false);
    Task<OneOf<Quiz, RequestError>> DeleteQuiz(
        int quizId, CancellationToken cancellationToken);
    Task<OneOf<Quiz, RequestError>> GetQuiz(
        int quizId, CancellationToken cancellationToken, bool useTracking = false);
    Task<ICollection<Quiz>> GetQuizzes(CancellationToken cancellationToken);
    Task<OneOf<Quiz, RequestError>> UpdateQuiz(
        int quizId, Quiz updatedQuiz, CancellationToken cancellationToken);
}