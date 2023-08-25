// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Models;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.Quizzes.Questions;

public interface IQuestionHandler
{
    Task<ICollection<QuestionForDisplay>> RetrieveQuestions(
        int quizId, CancellationToken cancellationToken);
    Task<OneOf<QuestionForDisplay, RequestError>> RetrieveQuestion(
        int quizId, int id, CancellationToken cancellationToken);
    Task<OneOf<QuestionForDisplay, RequestError>> DeleteQuestion(
        int quizId, int id, CancellationToken cancellationToken);
}
