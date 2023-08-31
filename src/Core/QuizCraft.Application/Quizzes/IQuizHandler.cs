// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Models;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.Quizzes;

public interface IQuizHandler
{
    Task<IEnumerable<QuizForDisplay>> RetrieveQuizzes(
        CancellationToken cancellationToken);

    Task<OneOf<QuizForDisplay, RequestError>> RetrieveQuiz(
        int id, CancellationToken cancellationToken);

    Task<OneOf<QuizForDisplay, RequestError>> CreateQuiz(
        QuizForUpsert newQuiz, CancellationToken cancellationToken);

    Task<OneOf<QuizForDisplay, RequestError>> UpdateQuiz(
        int id, QuizForUpsert quiz, CancellationToken cancellationToken);

    Task<OneOf<QuizForDisplay, RequestError>> DeleteQuiz(
        int id, CancellationToken cancellationToken);
}
