// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using QuizCraft.Models.Entities;

namespace QuizCraft.Application.QuizManagement.QuestionManagement
{
    internal class FillInBlankQuestionValidator : AbstractValidator<FillInBlankQuestion>
    {
        public FillInBlankQuestionValidator()
        {
            RuleFor(q => q.CorrectAnswer).NotEmpty();
            RuleFor(q => q.Text).NotEmpty();
            RuleFor(q => q.Score).GreaterThan(0);
            RuleFor(q => q.WordPosition).GreaterThan(0);
        }
    }
}
