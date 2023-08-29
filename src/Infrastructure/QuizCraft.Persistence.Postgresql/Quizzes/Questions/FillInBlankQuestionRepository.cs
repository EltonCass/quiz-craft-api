// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OneOf;
using QuizCraft.Application.Quizzes.Questions;
using QuizCraft.Models;
using QuizCraft.Models.Entities;

namespace QuizCraft.Persistence.Postgresql.Quizzes.Questions;

public class FillInBlankQuestionRepository : IFillInBlankQuestionRepository
{
    private readonly QuizCraftContext _context;

    public FillInBlankQuestionRepository(QuizCraftContext context)
    {
        _context = context;
    }

    public async Task<OneOf<FillInBlankQuestion, RequestError>> CreateQuestion(
        FillInBlankQuestion question, CancellationToken cancellationToken)
    {
        await _context.FillInBlankQuestions
            .AddAsync(question, cancellationToken);

        return question;
    }

    public Task<OneOf<FillInBlankQuestion, RequestError>> GetQuestion(
        int quizId, int questionId, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<ICollection<FillInBlankQuestion>> GetQuestions(
        int quizId, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public Task<OneOf<FillInBlankQuestion, RequestError>> UpdateQuestion(
        int questionId, FillInBlankQuestion updatedquestion, CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}
