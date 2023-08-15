// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Models.Entities;

namespace QuizCraft.Application.QuizManagement.QuestionManagement;

public interface IMultipleOptionQuestionRepository
{
    Task<OneOf<MultipleOptionQuestion, RequestError>> CreateQuestion(
        MultipleOptionQuestion question, CancellationToken cancellationToken);
    Task<OneOf<MultipleOptionQuestion, RequestError>> DeleteQuestion(
        int questionId, CancellationToken cancellationToken);
    Task<OneOf<MultipleOptionQuestion, RequestError>> GetQuestion(
        int questionId, CancellationToken cancellationToken);
    Task<ICollection<MultipleOptionQuestion>> GetQuestions(
        CancellationToken cancellationToken);
    Task<OneOf<MultipleOptionQuestion, RequestError>> UpdateQuestion(
        int questionId, MultipleOptionQuestion updatedquestion, CancellationToken cancellationToken);
}
