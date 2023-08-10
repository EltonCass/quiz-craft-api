// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.QuizManagement;

public interface IQuizRepository
{
    Task<IEnumerable<Quiz>> RetrieveQuizzes(CancellationToken cancellationToken);
    Task<OneOf<Quiz, RequestError>> RetrieveQuiz(int id, CancellationToken cancellationToken);
    Task<OneOf<Quiz, RequestError>> CreateQuiz(Quiz newQuiz, CancellationToken cancellationToken);
    Task<OneOf<Quiz, RequestError>> UpdateQuiz(Quiz quiz, CancellationToken cancellationToken);
    Task<OneOf<Quiz, RequestError>> DeleteQuiz(int id, CancellationToken cancellationToken);
}
