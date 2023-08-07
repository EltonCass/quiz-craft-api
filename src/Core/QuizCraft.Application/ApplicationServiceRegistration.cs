// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using QuizCraft.Application.CategoryManagement;
using QuizCraft.Application.QuizManagement;
using QuizCraft.Application.QuizManagement.QuestionManagement;
using QuizCraft.Models.Entities;

namespace QuizCraft.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<IQuizRepository, QuizRepository>()
            .AddScoped<IQuestionRepository, QuestionRepository>()
            .AddScoped<IUpsertQuestionRepository<MultipleOptionQuestion>, MultipleOptionQuestionRepository>()
            .AddScoped<IUpsertQuestionRepository<FillInBlankQuestion>, FillInBlankQuestionRepository>()
            .AddScoped<IValidator<Category>, CategoryValidator>()
            .AddScoped<IValidator<Quiz>, QuizValidator>()
            .AddScoped<IValidator<FillInBlankQuestion>, FillInBlankQuestionValidator>()
            .AddScoped<IValidator<MultipleOptionQuestion>, MultipleOptionQuestionValidator>();

        return services;
    }
}
