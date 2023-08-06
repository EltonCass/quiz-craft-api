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
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IValidator<Category>, CategoryValidator>();
        services.AddScoped<IValidator<Quiz>, QuizValidator>();
        services.AddScoped<IValidator<FillInBlankQuestion>, FillInBlankQuestionValidator>();
        services.AddScoped<IValidator<MultipleOptionQuestion>, MultipleOptionQuestionValidator>();

        return services;
    }
}
