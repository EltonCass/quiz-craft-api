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
using QuizCraft.Persistence.Categories;
using QuizCraft.Persistence.Quizzes;
using QuizCraft.Persistence.Quizzes.Questions;

namespace QuizCraft.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration,
        bool IsDevelopment)
    {
        services.AddDbContext<QuizCraftContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString
                ("QuizAPIConnectionString"))
            .LogTo(
                Console.WriteLine,
                new[] { DbLoggerCategory.Database.Command.Name },
                Microsoft.Extensions.Logging.LogLevel.Information)
            .EnableSensitiveDataLogging(IsDevelopment));

        // Add Repositories
        services.AddScoped<IValidator<Category>, CategoryServerSideValidator>();

        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IMultipleOptionQuestionRepository, MultipleOptionQuestionRepository>();
        services.AddScoped<IFillInBlankQuestionRepository, FillInBlankQuestionRepository>();

        return services;
    }
}
