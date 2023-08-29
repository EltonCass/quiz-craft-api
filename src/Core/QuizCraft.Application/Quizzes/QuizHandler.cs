// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
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
    private readonly IValidator<QuizForUpsert> _validator;
    private readonly IQuizRepository _quizRepository;
    private readonly IMapper _mapper;

    public QuizHandler(
        IValidator<QuizForUpsert> validator,
        IQuizRepository quizRepository,
        IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(validator);
        ArgumentNullException.ThrowIfNull(quizRepository);
        ArgumentNullException.ThrowIfNull(mapper);
        _validator = validator;
        _quizRepository = quizRepository;
        _mapper = mapper;
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

        var quizEntity = _mapper.Map<Quiz>(newQuiz);
        var createdQuizResult = await _quizRepository
            .CreateQuiz(quizEntity, cancellationToken);

        if (createdQuizResult.IsT1)
        {
            return createdQuizResult.AsT1;
        }

        var newQuizDto = _mapper.Map<QuizForDisplay>(createdQuizResult.AsT0);

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

        var quizDto = _mapper
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

        var foundedQuiz = _mapper.Map<QuizForDisplay>(quizResult.AsT0);
        return foundedQuiz;
    }

    public async Task<IEnumerable<QuizForDisplay>> RetrieveQuizzes(
        CancellationToken cancellationToken)
    {
        var quizzesResult = await _quizRepository
            .GetQuizzes(cancellationToken);
        var quizzes = _mapper
            .Map<ICollection<QuizForDisplay>>(quizzesResult);
        return quizzes;
    }

    public async Task<OneOf<QuizForDisplay, RequestError>> UpdateQuiz(
        int id, QuizForUpsert quiz, CancellationToken cancellationToken)
    {
        var result = _validator.Validate(quiz);
        if (!result.IsValid)
        {
            return new RequestError(
                HttpStatusCode.UnprocessableEntity,
                result.ToString());
        }

        var quizEntity = _mapper.Map<Quiz>(quiz);
        quizEntity.Id = id;
        var updatedQuizResult = await _quizRepository
            .UpdateQuiz(quizEntity, cancellationToken);

        if (updatedQuizResult.IsT1)
        {
            return updatedQuizResult.AsT1;
        }

        var quizDto = _mapper.Map<QuizForDisplay>(
            updatedQuizResult.AsT0);

        return quizDto;
    }
}
