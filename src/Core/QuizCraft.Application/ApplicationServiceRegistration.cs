// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using QuizCraft.Application.CategoryManagement;
using QuizCraft.Application.QuizManagement;
using QuizCraft.Application.QuizManagement.QuestionManagement;
using QuizCraft.Models.DTOs;
using QuizCraft.Models.Entities;

namespace QuizCraft.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services
            .AddScoped<ICategoryHandler, CategoryHandler>()
            .AddScoped<IQuizHandler, QuizHandler>()
            .AddScoped<IQuestionHandler, QuestionHandler>()
            .AddScoped<ISpecificQuestionHandler<MultipleOptionQuestionDTO>, MultipleOptionQuestionHandler>()
            .AddScoped<ISpecificQuestionHandler<FillInBlankQuestionDTO>, FillInBlankQuestionHandler>()
            .AddScoped<IValidator<CategoryForUpsert>, CategoryValidator>()
            .AddScoped<IValidator<QuizDTO>, QuizValidator>()
            .AddScoped<IValidator<FillInBlankQuestionDTO>, FillInBlankQuestionValidator>()
            .AddScoped<IValidator<MultipleOptionQuestionDTO>, MultipleOptionQuestionValidator>();

        return services;
    }
}
