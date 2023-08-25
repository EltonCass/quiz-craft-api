// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using MapsterMapper;
using OneOf;
using QuizCraft.Models;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.Quizzes.Questions;

public class QuestionHandler : IQuestionHandler
{
    private readonly IQuestionRepository _QuestionRepository;
    private readonly IMapper _Mapper;

    public QuestionHandler(
        IQuestionRepository questionRepository,
        IMapper mapper)
    {
        _QuestionRepository = questionRepository;
        _Mapper = mapper;
    }

    public async Task<OneOf<QuestionForDisplay, RequestError>> DeleteQuestion(
        int quizId, int id, CancellationToken cancellationToken)
    {
        var questionResult = await _QuestionRepository
            .DeleteQuestion(quizId, id, cancellationToken);

        if (questionResult.IsT1)
        {
            return questionResult.AsT1;
        }

        var question = _Mapper.Map<QuestionForDisplay>(questionResult.AsT0);
        return question;
    }

    public async Task<OneOf<QuestionForDisplay, RequestError>> RetrieveQuestion(
        int quizId, int id, CancellationToken cancellationToken)
    {
        var questionResult = await _QuestionRepository
            .GetQuestion(quizId, id, cancellationToken);

        if (questionResult.IsT1)
        {
            return questionResult.AsT1;
        }

        var question = _Mapper.Map<QuestionForDisplay>(questionResult.AsT0);
        return question;
    }

    public async Task<ICollection<QuestionForDisplay>> RetrieveQuestions(
        int quizId, CancellationToken cancellationToken)
    {
        var questionResult = await _QuestionRepository
            .GetQuestions(quizId, cancellationToken);

        var questions = _Mapper
            .Map<QuestionForDisplay[]>(questionResult);
        return questions;
    }
}
