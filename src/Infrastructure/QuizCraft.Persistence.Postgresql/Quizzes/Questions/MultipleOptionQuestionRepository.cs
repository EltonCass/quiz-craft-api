// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Application.Quizzes.Questions;
using QuizCraft.Models;
using QuizCraft.Models.Entities;

namespace QuizCraft.Persistence.Postgresql.Quizzes.Questions;

public class MultipleOptionQuestionRepository : IMultipleOptionQuestionRepository
{
    public Task<OneOf<MultipleOptionQuestion, RequestError>> CreateQuestion(
        MultipleOptionQuestion question, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<OneOf<MultipleOptionQuestion, RequestError>> GetQuestion(
        int questionId, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<ICollection<MultipleOptionQuestion>> GetQuestions(
        CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<OneOf<MultipleOptionQuestion, RequestError>> UpdateQuestion(
        int questionId, MultipleOptionQuestion updatedquestion, CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}
