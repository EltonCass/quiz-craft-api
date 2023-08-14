// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.EntityFrameworkCore;
using QuizCraft.Application.QuizManagement;
using QuizCraft.Models.Entities;

namespace QuizCraft.Persistence.Quizzes;

public class QuizRepository : IQuizRepository
{
    private readonly QuizCraftContext _Context;

    public QuizRepository(QuizCraftContext context)
    {
        _Context = context;
    }

    public Task<Quiz> CreateQuiz(Quiz quiz, CancellationToken cancellationToken) => throw new NotImplementedException();
    public Task<Quiz> DeleteQuiz(int quizId, CancellationToken cancellationToken) => throw new NotImplementedException();
    public async Task<Quiz> GetQuiz(int quizId, CancellationToken cancellationToken)
    {
        var quiz = await _Context.Quizzes
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
           .Where(x => x.Id == quizId)
           .FirstOrDefaultAsync(cancellationToken) ?? new Quiz();

        var debugView = _Context.ChangeTracker.DebugView.ShortView; //TODO Remove once its used
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

        var debugView = _Context.ChangeTracker.DebugView.ShortView; //TODO Remove once its used
        return quizzes;
    }

    public Task<Quiz> UpdateQuiz(int quizId, Quiz updatedQuiz, CancellationToken cancellationToken) => throw new NotImplementedException();
}
