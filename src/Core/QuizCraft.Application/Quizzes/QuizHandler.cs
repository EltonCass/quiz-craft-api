// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using Mapster;
using MapsterMapper;
using OneOf;
using QuizCraft.Application.Categories;
using QuizCraft.Application.Quizzes.Questions;
using QuizCraft.Models;
using QuizCraft.Models.DTOs;
using QuizCraft.Models.Entities;
using System.Net;

namespace QuizCraft.Application.Quizzes;

public class QuizHandler : IQuizHandler
{
    private readonly IValidator<QuizForUpsert> _validator;
    private readonly ISpecificQuestionHandler<MultipleOptionQuestionDTO> _multipleOptionHandler;
    private readonly ISpecificQuestionHandler<FillInBlankQuestionDTO> _fillInBlankHandler;
    private readonly IQuizRepository _quizRepository;
    private readonly IMapper _Mapper;

    public QuizHandler(
        IValidator<QuizForUpsert> validator,
        ISpecificQuestionHandler<MultipleOptionQuestionDTO> multipleOptionRepository,
        ISpecificQuestionHandler<FillInBlankQuestionDTO> fillInBlankRepository,
        IQuizRepository quizRepository,
        IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(validator);
        ArgumentNullException.ThrowIfNull(multipleOptionRepository);
        ArgumentNullException.ThrowIfNull(fillInBlankRepository);
        ArgumentNullException.ThrowIfNull(quizRepository);
        ArgumentNullException.ThrowIfNull(mapper);
        _validator = validator;
        _multipleOptionHandler = multipleOptionRepository;
        _fillInBlankHandler = fillInBlankRepository;
        _quizRepository = quizRepository;
        _Mapper = mapper;
    }

    public async Task<OneOf<QuizForDisplay, RequestError>> CreateQuiz(
        QuizForUpsert newQuiz, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(newQuiz);
        if (!result.IsValid)
        {
            return new RequestError(
                HttpStatusCode.UnprocessableEntity,
                result.ToString());
        }

        var quizEntity = _Mapper.Map<Quiz>(newQuiz);
        var createdQuizResult = await _quizRepository
            .CreateQuiz(quizEntity, cancellationToken);

        if (createdQuizResult.IsT1)
        {
            return createdQuizResult.AsT1;
        }

        var newQuizDto = _Mapper.Map<QuizForDisplay>(createdQuizResult.AsT0);

        //foreach (var question in newQuiz.Questions)
        //{
        //    if (question.Type is Models.Constants.QuestionType.MultipleOption)
        //    {
        //        await _multipleOptionHandler.CreateQuestion(
        //            newQuiz.Id, (MultipleOptionQuestionDTO)question, cancellationToken);
        //    }
        //    else if (question.Type is Models.Constants.QuestionType.FillInBlank)
        //    {
        //        await _fillInBlankHandler.CreateQuestion(
        //            newQuiz.Id, (FillInBlankQuestionDTO)question, cancellationToken);
        //    }
        //}

        return newQuizDto;
    }

    public async Task<OneOf<QuizForDisplay, RequestError>> DeleteQuiz(
        int id, CancellationToken cancellationToken)
    {
        var quizResult = await _quizRepository
            .DeleteQuiz(id, cancellationToken);

        if (quizResult.IsT1)
        {
            return quizResult.AsT1;
        }

        var quizDto = _Mapper
            .Map<QuizForDisplay>(quizResult.AsT0);
        return quizDto;
    }

    public async Task<OneOf<QuizForDisplay, RequestError>> RetrieveQuiz(
        int id, CancellationToken cancellationToken)
    {
        var quizResult = await _quizRepository
            .GetQuiz(id, cancellationToken);
        if (quizResult.IsT1)
        {
            return new RequestError(
                HttpStatusCode.NotFound, Constants.RequestErrorMessages.QuizNotFound);
        }

        var foundedQuiz = _Mapper.Map<QuizForDisplay>(quizResult.AsT0);
        return foundedQuiz;
    }

    public async Task<IEnumerable<QuizForDisplay>> RetrieveQuizzes(
        CancellationToken cancellationToken)
    {
        var quizzesResult = await _quizRepository
            .GetQuizzes(cancellationToken);
        var quizzes = _Mapper
            .Map<ICollection<QuizForDisplay>>(quizzesResult);
        return quizzes;
    }

    public Task<OneOf<QuizForDisplay, RequestError>> UpdateQuiz(
        QuizForDisplay quiz, CancellationToken cancellationToken) => throw new NotImplementedException();
}
