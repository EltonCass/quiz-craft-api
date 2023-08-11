using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace QuizCraft.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<QuizCraftContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString
                ("DbConnectionString")));

        // Add Repositories
        //services.AddScoped();

        return services;
    }
}
