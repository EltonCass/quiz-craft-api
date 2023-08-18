// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using QuizCraft.Application.Categories;
using QuizCraft.Application.Quizzes;
using QuizCraft.Application.Quizzes.Questions;
using QuizCraft.Models.DTOs;
using System.Reflection;

namespace QuizCraft.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Mapster configuration, this scans all custom configs
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services
            .AddScoped<ICategoryHandler, CategoryHandler>()
            .AddScoped<IQuizHandler, QuizHandler>()
            .AddScoped<IQuestionHandler, QuestionHandler>()
            .AddScoped<ISpecificQuestionHandler<MultipleOptionQuestionDTO>, MultipleOptionQuestionHandler>()
            .AddScoped<ISpecificQuestionHandler<FillInBlankQuestionDTO>, FillInBlankQuestionHandler>()
            .AddScoped<IValidator<CategoryForUpsert>, CategoryValidator>()
            .AddScoped<IValidator<QuizDTO>, QuizValidator>()
            .AddScoped<IValidator<FillInBlankQuestionDTO>, FillInBlankQuestionValidator>()
            .AddScoped<IValidator<MultipleOptionQuestionDTO>, MultipleOptionQuestionValidator>()
            .AddSingleton(config)
            .AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
