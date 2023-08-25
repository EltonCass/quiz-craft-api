// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Models;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.Quizzes.Questions;

public interface ISpecificQuestionHandler<T> where T : QuestionForDisplay
{
    Task<OneOf<T, RequestError>> CreateQuestion(
        int quizId, T newQuestion, CancellationToken cancellationToken);
    Task<OneOf<T, RequestError>> UpdateQuestion(
        int quizId, int questionId, T question, CancellationToken cancellationToken);
}
