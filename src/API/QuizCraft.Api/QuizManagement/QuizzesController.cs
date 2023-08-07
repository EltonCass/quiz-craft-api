// Copyright (c)  Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.AspNetCore.Mvc;
using OneOf;
using QuizCraft.Api.Helpers;
using QuizCraft.Application.QuizManagement;
using QuizCraft.Application.QuizManagement.QuestionManagement;
using QuizCraft.Models;
using QuizCraft.Models.Constants;
using QuizCraft.Models.Entities;

namespace QuizCraft.Api.QuizManagement;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class QuizzesController : ControllerBase
{
    private readonly IQuizRepository _quizRepository;
    private readonly IUpsertQuestionRepository<MultipleOptionQuestion> _multipleQuestionRepository;
    private readonly IUpsertQuestionRepository<FillInBlankQuestion> _fillInBlankQuestionRepository;
    private readonly IQuestionRepository _questionRepository;

    public QuizzesController(
        IQuizRepository quizRepository,
        IUpsertQuestionRepository<MultipleOptionQuestion> multipleQuestionRepository,
        IUpsertQuestionRepository<FillInBlankQuestion> fillInBlankQuestionRepository,
        IQuestionRepository questionRepository)
    {
        ArgumentNullException.ThrowIfNull(quizRepository, nameof(quizRepository));
        ArgumentNullException.ThrowIfNull(multipleQuestionRepository, nameof(multipleQuestionRepository));
        ArgumentNullException.ThrowIfNull(fillInBlankQuestionRepository, nameof(fillInBlankQuestionRepository));
        ArgumentNullException.ThrowIfNull(questionRepository, nameof(questionRepository));
        _quizRepository = quizRepository;
        _multipleQuestionRepository = multipleQuestionRepository;
        _fillInBlankQuestionRepository = fillInBlankQuestionRepository;
        _questionRepository = questionRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Quiz>), 200)]
    public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes(CancellationToken cancellationToken)
    {
        return Ok(await _quizRepository.RetrieveQuizzes(cancellationToken));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Quiz), 200)]
    public async Task<ActionResult<Quiz>> GetQuiz(
        int id, CancellationToken cancellationToken)
    {
        var result = await _quizRepository.RetrieveQuiz(id, cancellationToken);

        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return result.HandleError(this);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    public async Task<ActionResult<Quiz>> DeleteQuiz(
        int id, CancellationToken cancellationToken)
    {
        var result = await _quizRepository.DeleteQuiz(id, cancellationToken);

        if (result.IsT0)
        {
            return NoContent();
        }

        return result.HandleError(this);
    }
    

    [HttpGet("{id}/questions")]
    [ProducesResponseType(typeof(IEnumerable<BaseQuestion>), 200)]
    public async Task<ActionResult<IEnumerable<BaseQuestion>>> GetQuestions(
        int id, CancellationToken cancellationToken)
    {
        var result = await _questionRepository.RetrieveQuestions(id, cancellationToken);
        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return result.HandleError(this);
    }

    [HttpGet("{quizId}/questions/{questionId}")]
    [ProducesResponseType(typeof(BaseQuestion), 200)]
    public async Task<ActionResult<BaseQuestion>> GetQuestionById(
        int quizId, int questionId, CancellationToken cancellationToken)
    {
        var result = await _questionRepository.RetrieveQuestion(quizId, questionId, cancellationToken);

        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return result.HandleError(this);
    }

    [HttpPost("{quizId}/questions")]
    [ProducesResponseType(typeof(BaseQuestion), 200)]
    [ProducesResponseType(422)]
    public async Task<ActionResult<BaseQuestion>> PostQuestion(
        int quizId, [FromBody]BaseQuestion question, CancellationToken cancellationToken)
    {
        var result = question.Type switch
        {
            QuestionType.MultipleOption =>
                await CreateMultipleOptionQuestion(quizId, question, cancellationToken),
            QuestionType.FillInBlank =>
                await CreateFillInBlankOptionQuestion(quizId, question, cancellationToken),
            _ => BadRequest("Invalid question type.")
        };

        return result;
    }

    private async Task<ActionResult<BaseQuestion>> CreateMultipleOptionQuestion(
        int quizId, BaseQuestion question, CancellationToken cancellationToken)
    {
        var result = await _multipleQuestionRepository
            .CreateQuestion(quizId, (MultipleOptionQuestion)question, cancellationToken);

        return result.IsT0 
            ? (ActionResult<BaseQuestion>)Created("", result.AsT0)
            : (ActionResult<BaseQuestion>)result.HandleError(this);
    }

    private async Task<ActionResult<BaseQuestion>> CreateFillInBlankOptionQuestion(
        int quizId, BaseQuestion question, CancellationToken cancellationToken)
    {
        var result = await _fillInBlankQuestionRepository
            .CreateQuestion(quizId, (FillInBlankQuestion)question, cancellationToken);

        return result.IsT0
            ? (ActionResult<BaseQuestion>)Created("", result.AsT0)
            : (ActionResult<BaseQuestion>)result.HandleError(this);
    }
}