// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using QuizCraft.Models.Infrastructure;
using QuizCraft.Application.Mail;
using QuizCraft.Infrastructure.Mail;

namespace QuizCraft.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<EmailSettings>()
            .Bind(configuration.GetSection(nameof(EmailSettings)));
        //.Validate(a =>
        //{
        //    var result = a.Validate(new ValidationContext(a));
        //    return string.IsNullOrEmpty(result.FirstOrDefault()?.ErrorMessage);
        //});

        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
