// Copyright (c)  Elton Cassas. All rights reserved.
// See LICENSE.txt

using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using QuizCraft.Api.Helpers;
using QuizCraft.Application.Quizzes;
using QuizCraft.Application.Quizzes.Questions;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Api.Quizzes;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class QuizzesController : ControllerBase
{
    private const string _GetQuizByIdEndpointName = "GetQuiz";

    private readonly IQuizHandler _quizHandler;
    private readonly IQuestionHandler _questionHandler;
    private readonly IMapper _Mapper;
    private readonly ISpecificQuestionHandler<MultipleOptionQuestionDTO> _multipleQuestionHandler;
    private readonly ISpecificQuestionHandler<FillInBlankQuestionDTO> _fillInBlankQuestionHandler;

    public QuizzesController(
        IQuizHandler quizRepository,
        ISpecificQuestionHandler<MultipleOptionQuestionDTO> multipleQuestionRepository,
        ISpecificQuestionHandler<FillInBlankQuestionDTO> fillInBlankQuestionRepository,
        IQuestionHandler questionRepository,
        IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(quizRepository, nameof(quizRepository));
        ArgumentNullException.ThrowIfNull(multipleQuestionRepository, nameof(multipleQuestionRepository));
        ArgumentNullException.ThrowIfNull(fillInBlankQuestionRepository, nameof(fillInBlankQuestionRepository));
        ArgumentNullException.ThrowIfNull(questionRepository, nameof(questionRepository));
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        _quizHandler = quizRepository;
        _multipleQuestionHandler = multipleQuestionRepository;
        _fillInBlankQuestionHandler = fillInBlankQuestionRepository;
        _questionHandler = questionRepository;
        _Mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<QuizForDisplay>), 200)]
    public async Task<ActionResult<IEnumerable<QuizForDisplay>>> GetQuizzes(CancellationToken cancellationToken)
    {
        return Ok(await _quizHandler.RetrieveQuizzes(cancellationToken));
    }

    [HttpGet("{id}", Name = _GetQuizByIdEndpointName)]
    [ProducesResponseType(typeof(QuizForDisplay), 200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<QuizForDisplay>> GetQuiz(
        int id, CancellationToken cancellationToken)
    {
        var result = await _quizHandler.RetrieveQuiz(id, cancellationToken);

        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return result.HandleError(this);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<QuizForDisplay>> DeleteQuiz(
        int id, CancellationToken cancellationToken)
    {
        var result = await _quizHandler.DeleteQuiz(id, cancellationToken);

        if (result.IsT0)
        {
            return NoContent();
        }

        return result.HandleError(this);
    }

    [HttpPost()]
    [ProducesResponseType(201)]
    [ProducesResponseType(422)]
    public async Task<ActionResult<QuizForUpsert>> PostQuiz(
        [FromBody]QuizForUpsert quiz, CancellationToken cancellationToken)
    {
        var result = await _quizHandler
            .CreateQuiz(quiz, cancellationToken);

        if (result.IsT1)
        {
            return result.HandleError(this);
        }

        var resourceUrl = Url.Action(
                _GetQuizByIdEndpointName,
                "Quizzes",
                new { result.AsT0.Id, cancellationToken }, Request.Scheme);
        var responseQuiz = _Mapper.Map<QuizForUpsert>(result.AsT0);
        return Created(resourceUrl!, responseQuiz);
    }

    [HttpGet("{id}/questions")]
    [ProducesResponseType(typeof(IEnumerable<QuestionDTO>), 200)]
    public async Task<ActionResult<IEnumerable<QuestionDTO>>> GetQuestions(
        int id, CancellationToken cancellationToken)
    {
        var result = await _questionHandler.RetrieveQuestions(id, cancellationToken);
        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return result.HandleError(this);
    }

    [HttpGet("{quizId}/questions/{questionId}")]
    [ProducesResponseType(typeof(QuestionDTO), 200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<QuestionDTO>> GetQuestionById(
        int quizId, int questionId, CancellationToken cancellationToken)
    {
        var result = await _questionHandler.RetrieveQuestion(quizId, questionId, cancellationToken);

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
        var result = await _multipleQuestionHandler
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
        var result = await _fillInBlankQuestionHandler
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