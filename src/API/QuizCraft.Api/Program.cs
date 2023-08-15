// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.AspNetCore.Mvc;
using QuizCraft.Api.PromptManagement;
using QuizCraft.Api.QuizManagement;
using QuizCraft.Application;
using QuizCraft.Persistence;
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
            ConfigurePipeline(app);
        }

        private static WebApplicationBuilder ConfigureMiddlewarePipelines(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers(options =>
            {
                options.ReturnHttpNotAcceptable = true;
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                options.Filters.Add(
                    new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
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
            builder.Services.AddHealthChecks();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "QuizCraft API",
                    Version = "v1",
                    Description = "This API lets you access to quizzes and categories.",
                    Contact = new()
                    {
                        Email = "elcassastn50@gmail.com",
                        Name = "Elton Cassas",
                        Url = new Uri("https://www.linkedin.com/in/elton-cassas/")
                    },
                    License = new()
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
                //c.OperationFilter<SecurityOperationFilter>();
            });

            builder.Services.AddScoped<IQuizGeneration, QuizGeneration>();
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApiVersioning(setupAction =>
            {
                setupAction.AssumeDefaultVersionWhenUnspecified = true;
                setupAction.DefaultApiVersion = new ApiVersion(1, 0);
                setupAction.ReportApiVersions = true;
            });

            return builder;
        }

        private static void ConfigurePipeline(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            app.UseSwagger()
                .UseSwaggerUI(c => c.SwaggerEndpoint(
                "/swagger/v1/swagger.json", "QuizCraft Api"));

            app.UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHealthChecks("/health");
                });
            app.Run();
        }
    }
}