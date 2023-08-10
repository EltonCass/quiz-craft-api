// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.QuizManagement.QuestionManagement;

public interface IQuestionRepository
{
    Task<OneOf<IEnumerable<BaseQuestion>, RequestError>> RetrieveQuestions(
        int quizId, CancellationToken cancellationToken);
    Task<OneOf<BaseQuestion, RequestError>> RetrieveQuestion(
        int quizId, int id, CancellationToken cancellationToken);
    Task<OneOf<BaseQuestion, RequestError>> DeleteQuestion(
        int quizId, int id, CancellationToken cancellationToken);
}
