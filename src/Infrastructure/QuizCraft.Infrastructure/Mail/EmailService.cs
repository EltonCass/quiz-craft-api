// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.Extensions.Options;
using QuizCraft.Application.Mail;
using QuizCraft.Models.Infrastructure;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace QuizCraft.Infrastructure.Mail;

internal class EmailService : IEmailService
{
    private readonly EmailSettings _EmailSettings;

    public EmailService(IOptions<EmailSettings> mailSettings)
    {
        _EmailSettings = mailSettings.Value;
    }

    public async Task<bool> SendEmail(Email email)
    {
        var client = new SendGridClient(_EmailSettings.ApiKey);

        var subject = email.Subject;
        var to = new EmailAddress(email.To);
        var emailBody = email.Body;

        var from = new EmailAddress
        {
            Email = _EmailSettings.FromAddress,
            Name = _EmailSettings.FromName
        };

        var sendGridMessage = MailHelper
            .CreateSingleEmail(from, to, subject, emailBody, emailBody);
        var response = await client.SendEmailAsync(sendGridMessage);

        if (response.StatusCode == System.Net.HttpStatusCode.Accepted
            || response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return true;
        }

        return false;
    }
}
