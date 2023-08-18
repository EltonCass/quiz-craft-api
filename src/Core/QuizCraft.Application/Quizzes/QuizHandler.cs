// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using Mapster;
using MapsterMapper;
using OneOf;
using QuizCraft.Application.Quizzes.Questions;
using QuizCraft.Models;
using QuizCraft.Models.DTOs;
using QuizCraft.Models.Entities;
using System.Net;

namespace QuizCraft.Application.Quizzes;

public class QuizHandler : IQuizHandler
{
    private readonly IValidator<QuizDTO> _validator;
    private readonly ISpecificQuestionHandler<MultipleOptionQuestionDTO> _multipleOptionHandler;
    private readonly ISpecificQuestionHandler<FillInBlankQuestionDTO> _fillInBlankHandler;
    private readonly IQuizRepository _quizRepository;
    private readonly IMapper _Mapper;

    public QuizHandler(
        IValidator<QuizDTO> validator,
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

    public async Task<OneOf<QuizDTO, RequestError>> CreateQuiz(
        QuizDTO newQuiz, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(newQuiz);
        await Task.Delay(100, cancellationToken);
        if (!result.IsValid)
        {
            return new RequestError(
                HttpStatusCode.UnprocessableEntity,
                result.ToString());
        }


        foreach (var question in newQuiz.Questions)
        {
            if (question.Type is Models.Constants.QuestionType.MultipleOption)
            {
                await _multipleOptionHandler.CreateQuestion(
                    newQuiz.Id, (MultipleOptionQuestionDTO)question, cancellationToken);
            }
            else if (question.Type is Models.Constants.QuestionType.FillInBlank)
            {
                await _fillInBlankHandler.CreateQuestion(
                    newQuiz.Id, (FillInBlankQuestionDTO)question, cancellationToken);
            }
        }

        Stubs.Quizzes.Add(newQuiz);
        return newQuiz;
    }

    public async Task<OneOf<QuizDTO, RequestError>> DeleteQuiz(
        int id, CancellationToken cancellationToken)
    {
        var foundedQuiz = Stubs.Quizzes.FirstOrDefault(q => q.Id == id);
        await Task.Delay(100, cancellationToken);
        if (foundedQuiz is null)
        {
            return new RequestError(HttpStatusCode.NotFound, Constants.RequestErrorMessages.QuizNotFound);
        }

        Stubs.Quizzes.Remove(foundedQuiz);
        return foundedQuiz;
    }

    public async Task<OneOf<QuizDTO, RequestError>> RetrieveQuiz(
        int id, CancellationToken cancellationToken)
    {
        var quizResult = await _quizRepository
            .GetQuiz(id, cancellationToken);
        if (quizResult.IsT1)
        {
            return new RequestError(
                HttpStatusCode.NotFound, Constants.RequestErrorMessages.QuizNotFound);
        }

        var foundedQuiz = _Mapper.Map<QuizDTO>(quizResult.AsT0);
        return foundedQuiz;
    }

    public async Task<IEnumerable<QuizDTO>> RetrieveQuizzes(
        CancellationToken cancellationToken)
    {
        var quizzesResult = await _quizRepository
            .GetQuizzes(cancellationToken);
        var quizzes = _Mapper
            .Map<ICollection<QuizDTO>>(quizzesResult);
        return quizzes;
    }

    public Task<OneOf<QuizDTO, RequestError>> UpdateQuiz(
        QuizDTO quiz, CancellationToken cancellationToken) => throw new NotImplementedException();
}
