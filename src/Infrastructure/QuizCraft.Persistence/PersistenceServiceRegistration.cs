// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace QuizCraft.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<QuizCraftDbfirstContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString
                ("DbConnectionString")));

        // Add Repositories
        //services.AddScoped();

        return services;
    }
}
