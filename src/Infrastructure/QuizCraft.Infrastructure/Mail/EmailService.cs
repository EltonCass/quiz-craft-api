// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.Extensions.Options;
using QuizCraft.Application.Mail;
using QuizCraft.Models.Infrastructure;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace QuizCraft.Infrastructure.Mail;

internal class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> mailSettings)
    {
        _emailSettings = mailSettings.Value;
    }

    public async Task<bool> SendEmail(Email email)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);

        var subject = email.Subject;
        var to = new EmailAddress(email.To);
        var emailBody = email.Body;

        var from = new EmailAddress
        {
            Email = _emailSettings.FromAddress,
            Name = _emailSettings.FromName,
        };

        var sendGridMessage = MailHelper
            .CreateSingleEmail(from, to, subject, emailBody, emailBody);
        var response = await client.SendEmailAsync(sendGridMessage);

        return response.StatusCode is HttpStatusCode.Accepted
            or HttpStatusCode.OK;
    }
}
