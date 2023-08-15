// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using OneOf;
using QuizCraft.Models.DTOs;
using System.Net;

namespace QuizCraft.Application.QuizManagement.QuestionManagement;

public class FillInBlankQuestionHandler : ISpecificQuestionHandler<FillInBlankQuestionDTO>
{
    private readonly IValidator<FillInBlankQuestionDTO> _validator;

    public FillInBlankQuestionHandler(IValidator<FillInBlankQuestionDTO> validator)
    {
        ArgumentNullException.ThrowIfNull(validator);
        _validator = validator;
    }

    public async Task<OneOf<FillInBlankQuestionDTO, RequestError>> CreateQuestion(
        int quizId, FillInBlankQuestionDTO newQuestion, CancellationToken cancellationToken)
    {
        var foundedQuiz = Stubs.Quizzes.FirstOrDefault(q => q.Id == quizId);
        await Task.Delay(100, cancellationToken);
        if (foundedQuiz is null)
        {
            return new RequestError(HttpStatusCode.NotFound, Constants.RequestErrorMessages.QuizNotFound);
        }

        var result = _validator.Validate(newQuestion);
        if (result.IsValid)
        {
            foundedQuiz.Questions.Add(newQuestion);
            return newQuestion;
        }

        return new RequestError(
            HttpStatusCode.UnprocessableEntity,
            result.ToString());
    }

    public async Task<OneOf<FillInBlankQuestionDTO, RequestError>> UpdateQuestion(
        int quizId, int questionId, FillInBlankQuestionDTO question, CancellationToken cancellationToken)
    {
        var foundedQuiz = Stubs.Quizzes.FirstOrDefault(q => q.Id == quizId);
        await Task.Delay(100, cancellationToken);
        if (foundedQuiz is null)
        {
            return new RequestError(HttpStatusCode.NotFound, Constants.RequestErrorMessages.QuizNotFound);
        }

        var foundedQuestion = foundedQuiz.Questions.FirstOrDefault(q => q.Id == questionId);
        if (foundedQuestion is null)
        {
            return new RequestError(HttpStatusCode.NotFound, Constants.RequestErrorMessages.QuestionNotFound);
        }

        var result = _validator.Validate(question);
        if (result.IsValid)
        {
            foundedQuestion = question;
            return question;
        }

        return new RequestError(
            HttpStatusCode.UnprocessableEntity,
            result.ToString());
    }
}
