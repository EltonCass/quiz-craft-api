// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Api.ApiDocumentation;
using QuizCraft.Api.Middlewares;
using QuizCraft.Api.QuizManagement;

namespace QuizCraft.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "QuizCraft Api",
                    Version = "v1",
                });
                c.OperationFilter<SecurityOperationFilter>();
            });
            builder.Services.AddScoped<IQuizGeneration, QuizGeneration>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint(
                "/swagger/v1/swagger.json", "QuizCraft Api"));

            app.UseHttpsRedirection();

            app.UseMiddleware<SecurityMiddleware>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}