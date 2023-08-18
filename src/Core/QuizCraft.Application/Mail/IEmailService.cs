// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Models.Infrastructure;

namespace QuizCraft.Application.Mail;

public interface IEmailService
{
    Task<bool> SendEmail(Email email);
}
