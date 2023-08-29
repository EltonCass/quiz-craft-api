// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizCraft.Application.Categories;
using QuizCraft.Application.Quizzes;
using QuizCraft.Application.Quizzes.Questions;
using QuizCraft.Models.Entities;
using QuizCraft.Persistence.Postgresql.Categories;
using QuizCraft.Persistence.Postgresql.Quizzes;
using QuizCraft.Persistence.Postgresql.Quizzes.Questions;

namespace QuizCraft.Persistence.Postgresql;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection AddPostgreSqlPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration,
        bool isDevelopment)
    {
        services.AddDbContext<QuizCraftContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("QuizAPIConnectionString"))
            .LogTo(
                Console.WriteLine,
                new[] { DbLoggerCategory.Database.Command.Name },
                Microsoft.Extensions.Logging.LogLevel.Information)
            .EnableSensitiveDataLogging(isDevelopment));

        // Add Repositories
        services.AddScoped<IValidator<Category>, CategoryServerSideValidator>();

        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IMultipleOptionQuestionRepository, MultipleOptionQuestionRepository>();
        services.AddScoped<IFillInBlankQuestionRepository, FillInBlankQuestionRepository>();

        return services;
    }
}
