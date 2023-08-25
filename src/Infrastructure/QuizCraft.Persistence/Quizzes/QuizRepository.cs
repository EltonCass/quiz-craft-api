// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using OneOf;
using OneOf.Types;
using QuizCraft.Application.Categories;
using QuizCraft.Application.Quizzes;
using QuizCraft.Models;
using QuizCraft.Models.Entities;
using System.Net;
using System.Transactions;
using static QuizCraft.Models.Constants.Constants;

namespace QuizCraft.Persistence.Quizzes;

public class QuizRepository : IQuizRepository
{
    private readonly QuizCraftContext _Context;
    private readonly ICategoryRepository _CategoryRepository;

    public QuizRepository(
        QuizCraftContext context,
        ICategoryRepository categoryRepository)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));
        ArgumentNullException.ThrowIfNull(categoryRepository, nameof(categoryRepository));
        _Context = context;
        _CategoryRepository = categoryRepository;
    }

    public async Task<OneOf<Quiz, RequestError>> CreateQuiz(
        Quiz quiz, CancellationToken cancellationToken, bool addNewCategories = false)
    {
        if (!quiz.Categories.Any())
        {
            await _Context.Quizzes.AddAsync(quiz, cancellationToken);
            await _Context.SaveChangesAsync(cancellationToken);
            return quiz;
        }

        using var transaction = await _Context.Database
            .BeginTransactionAsync(cancellationToken);
        // Adding Categories
        try
        {
            var (existingCategories, newCategories) =
                await _CategoryRepository.GetCategoriesByNames(
                quiz.Categories.Select(c => c.Name).ToArray(), cancellationToken);

            if (!addNewCategories)
            {
                return new RequestError(
                    HttpStatusCode.BadRequest,
                    $"Quiz contains non existing categories:" +
                    $" {string.Join(',', newCategories)}");
            }

            quiz.Categories.Clear();
            quiz.Categories = existingCategories.ToList();

            if (newCategories.Any())
            {
                var result = await _CategoryRepository
                    .AddCategoriesByNames(newCategories.ToArray(), cancellationToken);

                if (result.IsT1)
                {
                    return result.AsT1;
                }

                quiz.Categories.AddRange(result.AsT0);
            }

            await _Context.Quizzes.AddAsync(quiz, cancellationToken);
            await _Context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return quiz;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<OneOf<Quiz, RequestError>> DeleteQuiz(
        int quizId, CancellationToken cancellationToken)
    {
        var quizResult = await GetQuiz(
            quizId, cancellationToken, true);
        if (quizResult.IsT1)
        {
            return quizResult;
        }

        _Context.Quizzes.Remove(quizResult.AsT0);
        var result = await _Context.SaveChangesAsync(cancellationToken);

        if (result == 0)
        {
            return new RequestError(
                HttpStatusCode.BadRequest, RequestErrorMessages.NoChanges);
        }

        return quizResult.AsT0;
    }

    public async Task<OneOf<Quiz, RequestError>> GetQuiz(
        int quizId, CancellationToken cancellationToken, bool useTracking = false)
    {
        var quizQuery = _Context.Quizzes
            .Where(x => x.Id == quizId);

        if (!useTracking)
        {
            quizQuery = quizQuery.AsNoTracking();
        }

        var quiz = await quizQuery
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

    public async Task<ICollection<Quiz>> GetQuizzes(
        CancellationToken cancellationToken)
    {
        return await _Context.Quizzes
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
    }

    public Task<OneOf<Quiz, RequestError>> UpdateQuiz(
        Quiz updatedQuiz, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}
