// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.EntityFrameworkCore;
using OneOf;
using QuizCraft.Application.Quizzes.Questions;
using QuizCraft.Models;
using QuizCraft.Models.Constants;
using QuizCraft.Models.Entities;

namespace QuizCraft.Persistence.Postgresql.Quizzes.Questions;

public class QuestionRepository : IQuestionRepository
{
    private readonly QuizCraftContext _context;

    public QuestionRepository(
        QuizCraftContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
    }

    public async Task<OneOf<Question, RequestError>> DeleteQuestion(
        int quizId, int questionId, CancellationToken cancellationToken)
    {
        var foundedQuestion = await _context.Questions
            .FirstOrDefaultAsync(
            q => q.Id == questionId && q.QuizId == quizId, cancellationToken);
        if (foundedQuestion is null)
        {
            return new RequestError(
                System.Net.HttpStatusCode.NotFound,
                Constants.RequestErrorMessages.QuestionNotFound);
        }

        _context.Questions.Remove(foundedQuestion);
        await _context.SaveChangesAsync(cancellationToken);
        return foundedQuestion;
    }

    public async Task<OneOf<Question, RequestError>> GetQuestion(
        int quizId, int questionId, CancellationToken cancellationToken)
    {
        var foundedQuestion = await _context.Questions
            .AsNoTracking()
            .FirstOrDefaultAsync(
            q => q.Id == questionId && q.QuizId == quizId, cancellationToken);
        return foundedQuestion is null
            ? new RequestError(
                System.Net.HttpStatusCode.NotFound,
                Constants.RequestErrorMessages.QuestionNotFound)
            : foundedQuestion;
    }

    public async Task<ICollection<Question>> GetQuestions(
        int quizId, CancellationToken cancellationToken)
    {
        var questions = await _context.Questions
            .AsNoTracking()
            .Where(q => q.QuizId == quizId)
            .ToListAsync(cancellationToken);

        return questions;
    }
}
