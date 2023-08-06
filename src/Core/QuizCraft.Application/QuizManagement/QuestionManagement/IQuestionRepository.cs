// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Models.Entities;

namespace QuizCraft.Application.QuizManagement.QuestionManagement;

public interface IQuestionRepository
{
    Task<OneOf<IEnumerable<BaseQuestion>, RequestError>> RetrieveQuestions(int quizId, CancellationToken cancellationToken);
    Task<OneOf<BaseQuestion, RequestError>> RetrieveQuestion(int quizId, int id, CancellationToken cancellationToken);
    Task<OneOf<MultipleOptionQuestion, RequestError>> CreateMultipleOptionQuestion(
        int quizId, MultipleOptionQuestion newQuestion, CancellationToken cancellationToken);
    Task<OneOf<FillInBlankQuestion, RequestError>> CreateFillInBlankQuestion(
        int quizId, FillInBlankQuestion newQuestion, CancellationToken cancellationToken);
    Task<OneOf<MultipleOptionQuestion, RequestError>> UpdateMultipleOptionQuestion(
        int quizId, MultipleOptionQuestion newQuestion, CancellationToken cancellationToken);
    Task<OneOf<FillInBlankQuestion, RequestError>> UpdateFillInBlankQuestion(
        int quizId, FillInBlankQuestion newQuestion, CancellationToken cancellationToken);
    Task<OneOf<BaseQuestion, RequestError>> DeleteQuestions(int quizId, int id, CancellationToken cancellationToken);
}
