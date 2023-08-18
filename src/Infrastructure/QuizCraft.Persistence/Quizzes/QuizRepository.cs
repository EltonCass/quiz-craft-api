// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.EntityFrameworkCore;
using OneOf;
using QuizCraft.Application.Quizzes;
using QuizCraft.Models;
using QuizCraft.Models.Entities;
using System.Net;
using static QuizCraft.Models.Constants.Constants;

namespace QuizCraft.Persistence.Quizzes;

public class QuizRepository : IQuizRepository
{
    private readonly QuizCraftContext _Context;

    public QuizRepository(QuizCraftContext context)
    {
        _Context = context;
    }

    public Task<OneOf<Quiz, RequestError>> CreateQuiz(Quiz quiz, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<OneOf<Quiz, RequestError>> DeleteQuiz(int quizId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public async Task<OneOf<Quiz, RequestError>> GetQuiz(int quizId, CancellationToken cancellationToken)
    {
        var quiz2 = await _Context.Quizzes
           .AsNoTracking()
           .Where(x => x.Id == quizId)
           //.Include(x => x.Categories)
           //.Include(x => x.Questions)
           //.Include(x => x.CreatedByUser)
           //.ThenInclude(x => x.QuizzesCreatedBy)
           //.Include(x => x.UpdatedByUser)
           .Select(x => new
           {
               Id = x.Id,
               CreatedAt = x.CreatedAt,
               UpdatedAt = x.UpdatedAt,
               Categories = x.Categories,
               Description = x.Description,
               Questions = x.Questions,
               Score = x.Score,
               Title = x.Title,
               CreatedByUser = x.CreatedByUser == null ? "" : x.CreatedByUser.FullName,
               UpdatedByUserEmail = x.UpdatedByUser == null ? "" : x.UpdatedByUser.Email,
           })
           .FirstOrDefaultAsync(cancellationToken);

        var quiz = await _Context.Quizzes
           .AsNoTracking()
           .Where(x => x.Id == quizId)
           //.Include(x => x.Categories)
           //.Include(x => x.Questions)
           //.Include(x => x.CreatedByUser)
           //.ThenInclude(x => x.QuizzesCreatedBy)
           //.Include(x => x.UpdatedByUser)
           .Select(x => new Quiz
           {
               Id = x.Id,
               CreatedAt = x.CreatedAt,
               UpdatedAt = x.UpdatedAt,
               Categories = x.Categories,
               Description = x.Description,
               Questions = x.Questions,
               Score = x.Score,
               Title = x.Title,
               CreatedByUser = x.CreatedByUser,
               UpdatedByUser = x.UpdatedByUser,
           })
           .FirstOrDefaultAsync(cancellationToken);

        if (quiz is null)
        {
            return new RequestError(HttpStatusCode.NotFound, RequestErrorMessages.QuizNotFound);
        }

        return quiz;
    }

    public async Task<ICollection<Quiz>> GetQuizzes(CancellationToken cancellationToken)
    {
        var quizzes = await _Context.Quizzes
            .Include(x => x.Categories)
            .Include(x => x.Questions)
            .Include(x => x.CreatedByUser)
            .Include(x => x.UpdatedByUser)
            .Select(x => new Quiz 
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Categories = x.Categories,
                Description = x.Description,
                Questions = x.Questions,
                Score = x.Score,
                Title = x.Title,
                CreatedByUser = x.CreatedByUser,
                UpdatedByUser = x.UpdatedByUser,
            })
            .ToListAsync(cancellationToken);

        return quizzes;
    }

    public Task<OneOf<Quiz, RequestError>> UpdateQuiz(int quizId, Quiz updatedQuiz, CancellationToken cancellationToken) => throw new NotImplementedException();
}
