using Microsoft.Extensions.DependencyInjection;
using QuizCraft.Application.CategoryManagement;
using QuizCraft.Application.QuizManagement;
using System.Reflection;

namespace QuizCraft.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IQuizRepository, QuizRepository>();
            return services;
        }
    }
}
