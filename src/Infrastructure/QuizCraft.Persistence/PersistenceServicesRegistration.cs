// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizCraft.Application.CategoryManagement;
using QuizCraft.Application.QuizManagement;
using QuizCraft.Persistence.Categories;
using QuizCraft.Persistence.Quizzes;

namespace QuizCraft.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        var env= Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        services.AddDbContext<QuizCraftContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString
                ("QuizAPIConnectionString"))
            .LogTo(
                Console.WriteLine,
                new[] { DbLoggerCategory.Database.Command.Name },
                Microsoft.Extensions.Logging.LogLevel.Information)
            .EnableSensitiveDataLogging(
                env == "Development"));

        // Add Repositories
        services.AddScoped<IQuizRepository, QuizRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}
