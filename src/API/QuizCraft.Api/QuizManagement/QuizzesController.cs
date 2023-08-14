// Copyright (c)  Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.AspNetCore.Mvc;
using QuizCraft.Api.Helpers;
using QuizCraft.Application.QuizManagement;
using QuizCraft.Application.QuizManagement.QuestionManagement;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Api.QuizManagement;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class QuizzesController : ControllerBase
{
    private const string _GetQuizByIdEndpointName = "GetQuiz";

    private readonly IQuizHandler _quizRepository;
    private readonly IUpsertQuestionRepository<MultipleOptionQuestionDTO> _multipleQuestionRepository;
    private readonly IUpsertQuestionRepository<FillInBlankQuestionDTO> _fillInBlankQuestionRepository;
    private readonly IQuestionHandler _questionRepository;

    public QuizzesController(
        IQuizHandler quizRepository,
        IUpsertQuestionRepository<MultipleOptionQuestionDTO> multipleQuestionRepository,
        IUpsertQuestionRepository<FillInBlankQuestionDTO> fillInBlankQuestionRepository,
        IQuestionHandler questionRepository)
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
    [ProducesResponseType(typeof(IEnumerable<QuizDTO>), 200)]
    public async Task<ActionResult<IEnumerable<QuizDTO>>> GetQuizzes(CancellationToken cancellationToken)
    {
        return Ok(await _quizRepository.RetrieveQuizzes(cancellationToken));
    }

    [HttpGet("{id}", Name=_GetQuizByIdEndpointName)]
    [ProducesResponseType(typeof(QuizDTO), 200)]
    public async Task<ActionResult<QuizDTO>> GetQuiz(
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
    public async Task<ActionResult<QuizDTO>> DeleteQuiz(
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
    [ProducesResponseType(typeof(IEnumerable<QuestionDTO>), 200)]
    public async Task<ActionResult<IEnumerable<QuestionDTO>>> GetQuestions(
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
    [ProducesResponseType(typeof(QuestionDTO), 200)]
    public async Task<ActionResult<QuestionDTO>> GetQuestionById(
        int quizId, int questionId, CancellationToken cancellationToken)
    {
        var result = await _questionRepository.RetrieveQuestion(quizId, questionId, cancellationToken);

        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return result.HandleError(this);
    }

    [HttpPost("{quizId}/multipleOptionQuestions")]
    [ProducesResponseType(typeof(MultipleOptionQuestionDTO), 201)]
    [ProducesResponseType(422)]
    public async Task<ActionResult<MultipleOptionQuestionDTO>> PostQuestion(
        int quizId, [FromBody] MultipleOptionQuestionDTO question, CancellationToken cancellationToken)
    {
        return await CreateMultipleOptionQuestion(quizId, question, cancellationToken);
    }

    [HttpPost("{quizId}/fillInBlankQuestions")]
    [ProducesResponseType(typeof(FillInBlankQuestionDTO), 201)]
    [ProducesResponseType(422)]
    public async Task<ActionResult<FillInBlankQuestionDTO>> PostQuestion(
        int quizId, [FromBody] FillInBlankQuestionDTO question, CancellationToken cancellationToken)
    {
        return await CreateFillInBlankOptionQuestion(quizId, question, cancellationToken);
    }

    private async Task<ActionResult<MultipleOptionQuestionDTO>> CreateMultipleOptionQuestion(
        int quizId, MultipleOptionQuestionDTO question, CancellationToken cancellationToken)
    {
        var result = await _multipleQuestionRepository
            .CreateQuestion(quizId, question, cancellationToken);

        if (result.IsT0)
        {
            var resourceUrl = Url.Action(
                _GetQuizByIdEndpointName,
                ControllerContext.ActionDescriptor.ControllerName,
                new { result.AsT0.Id, cancellationToken }, Request.Scheme);
            return Created(resourceUrl!, result.AsT0);
        }

        return result.HandleError(this);
    }

    private async Task<ActionResult<FillInBlankQuestionDTO>> CreateFillInBlankOptionQuestion(
        int quizId, FillInBlankQuestionDTO question, CancellationToken cancellationToken)
    {
        var result = await _fillInBlankQuestionRepository
            .CreateQuestion(quizId, question, cancellationToken);

        if (result.IsT0)
        {
            var resourceUrl = Url.Action(
                _GetQuizByIdEndpointName,
                ControllerContext.ActionDescriptor.ControllerName,
                new { result.AsT0.Id, cancellationToken }, Request.Scheme);
            return Created(resourceUrl!, result.AsT0);
        }

        return result.HandleError(this);
    }
}