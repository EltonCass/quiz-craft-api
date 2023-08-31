// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizCraft.Application.Mail;
using QuizCraft.Infrastructure.Mail;
using QuizCraft.Models.Infrastructure;

namespace QuizCraft.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<EmailSettings>()
            .Bind(configuration.GetSection(nameof(EmailSettings)));

        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
