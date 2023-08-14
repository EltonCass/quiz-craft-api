// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentValidation;
using OneOf;
using QuizCraft.Application.QuizManagement.QuestionManagement;
using QuizCraft.Models.DTOs;
using System.Net;

namespace QuizCraft.Application.QuizManagement;

public class QuizHandler : IQuizHandler
{
    private readonly IValidator<QuizDTO> _validator;
    private readonly IUpsertQuestionRepository<MultipleOptionQuestionDTO> _multipleOptionRepository;
    private readonly IUpsertQuestionRepository<FillInBlankQuestionDTO> _fillInBlankRepository;

    public QuizHandler(
        IValidator<QuizDTO> validator,
        IUpsertQuestionRepository<MultipleOptionQuestionDTO> multipleOptionRepository,
        IUpsertQuestionRepository<FillInBlankQuestionDTO> fillInBlankRepository)
    {
        ArgumentNullException.ThrowIfNull(validator);
        ArgumentNullException.ThrowIfNull(multipleOptionRepository);
        ArgumentNullException.ThrowIfNull(fillInBlankRepository);
        _validator = validator;
        _multipleOptionRepository = multipleOptionRepository;
        _fillInBlankRepository = fillInBlankRepository;
    }

    public async Task<OneOf<QuizDTO, RequestError>> CreateQuiz(QuizDTO newQuiz, CancellationToken cancellationToken)
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
            if (question.Type is QuestionType.MultipleOption)
            {
                await _multipleOptionRepository.CreateQuestion(newQuiz.Id, (MultipleOptionQuestionDTO)question, cancellationToken);
            }
            else if (question.Type is QuestionType.FillInBlank)
            {
                await _fillInBlankRepository.CreateQuestion(newQuiz.Id, (FillInBlankQuestionDTO)question, cancellationToken);
            }
        }

        Stubs.Quizzes.Add(newQuiz);
        return newQuiz;
    }

    public async Task<OneOf<QuizDTO, RequestError>> DeleteQuiz(int id, CancellationToken cancellationToken)
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
    
    public async Task<OneOf<QuizDTO, RequestError>> RetrieveQuiz(int id, CancellationToken cancellationToken)
    {
        var foundedQuiz = Stubs.Quizzes.FirstOrDefault(q => q.Id == id);
        await Task.Delay(100, cancellationToken);
        if (foundedQuiz is null) 
        {
            return new RequestError(HttpStatusCode.NotFound, Constants.RequestErrorMessages.QuizNotFound);
        }
        
        return foundedQuiz;
    }

    public async Task<IEnumerable<QuizDTO>> RetrieveQuizzes(CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);
        return Stubs.Quizzes;
    }

    public Task<OneOf<QuizDTO, RequestError>> UpdateQuiz(QuizDTO quiz, CancellationToken cancellationToken) => throw new NotImplementedException();
}
