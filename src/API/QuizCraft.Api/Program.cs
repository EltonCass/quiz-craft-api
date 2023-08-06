// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Api.Middlewares;
using QuizCraft.Api.QuizManagement;
using QuizCraft.Application;
using System.Text.Json;

namespace QuizCraft.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication
                .CreateBuilder(args);
            builder = ConfigureMiddlewarePipelines(builder);
            var app = builder.Build();
            ConfigureMiddleware(app);
        }

        private static WebApplicationBuilder ConfigureMiddlewarePipelines(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;
            }).AddJsonOptions(options =>
            {
                // Set the JSON serializer options here
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                // Add other options as needed...
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            /*
             .AddOptions<ConfigurationOptions>()
            .Bind(builder.Configuration.GetSection(ConfigurationOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();
             */
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "QuizCraft Api",
                    Version = "v1",
                });
                //c.OperationFilter<SecurityOperationFilter>();
            });

            builder.Services.AddScoped<IQuizGeneration, QuizGeneration>();
            builder.Services.AddApplicationServices();
            builder.Services.AddApiVersioning(setupAction =>
            {
                setupAction.AssumeDefaultVersionWhenUnspecified = true;
                setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                setupAction.ReportApiVersions = true;
            });

            return builder;
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint(
                "/swagger/v1/swagger.json", "QuizCraft Api"));

            app.UseMiddleware<SecurityMiddleware>();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}