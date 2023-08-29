// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using MapsterMapper;
using OneOf;
using QuizCraft.Models;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Application.Quizzes.Questions;

public class QuestionHandler : IQuestionHandler
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IMapper _mapper;

    public QuestionHandler(
        IQuestionRepository questionRepository,
        IMapper mapper)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
    }

    public async Task<OneOf<QuestionForDisplay, RequestError>> DeleteQuestion(
        int quizId, int id, CancellationToken cancellationToken)
    {
        var questionResult = await _questionRepository
            .DeleteQuestion(quizId, id, cancellationToken);

        if (questionResult.IsT1)
        {
            return questionResult.AsT1;
        }

        var question = _mapper
            .Map<QuestionForDisplay>(questionResult.AsT0);
        return question;
    }

    public async Task<OneOf<QuestionForDisplay, RequestError>> RetrieveQuestion(
        int quizId, int id, CancellationToken cancellationToken)
    {
        var questionResult = await _questionRepository
            .GetQuestion(quizId, id, cancellationToken);

        if (questionResult.IsT1)
        {
            return questionResult.AsT1;
        }

        var question = _mapper
            .Map<QuestionForDisplay>(questionResult.AsT0);
        return question;
    }

    public async Task<ICollection<QuestionForDisplay>> RetrieveQuestions(
        int quizId, CancellationToken cancellationToken)
    {
        var questionResult = await _questionRepository
            .GetQuestions(quizId, cancellationToken);

        var questions = _mapper
            .Map<QuestionForDisplay[]>(questionResult);
        return questions;
    }
}
