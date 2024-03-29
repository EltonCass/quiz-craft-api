// Copyright (c)  Elton Cassas. All rights reserved.
// See LICENSE.txt

using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using QuizCraft.Api.Helpers;
using QuizCraft.Application.Quizzes;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Api.Quizzes;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class QuizzesController : ControllerBase
{
    private const string _GetQuizByIdEndpointName = "GetQuiz";

    private readonly IQuizHandler _quizHandler;
    private readonly IMapper _mapper;

    public QuizzesController(
        IQuizHandler quizRepository,
        IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(quizRepository);
        ArgumentNullException.ThrowIfNull(mapper);

        _quizHandler = quizRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<QuizForDisplay>), 200)]
    public async Task<ActionResult<IEnumerable<QuizForDisplay>>> GetQuizzes(
        CancellationToken cancellationToken)
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

        return result.IsT0
            ? Ok(result.AsT0)
            : result.HandleError(this);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<QuizForDisplay>> DeleteQuiz(
        int id, CancellationToken cancellationToken)
    {
        var result = await _quizHandler.DeleteQuiz(id, cancellationToken);

        return result.IsT0
            ? NoContent()
            : result.HandleError(this);
    }

    [HttpPost]
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
                new { result.AsT0.Id, cancellationToken },
                Request.Scheme);
        var responseQuiz = _mapper.Map<QuizForUpsert>(result.AsT0);
        return Created(resourceUrl!, responseQuiz);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(QuizForDisplay), 200)]
    [ProducesResponseType(422)]
    public async Task<ActionResult<QuizForDisplay>> PutQuiz(
        int id, [FromBody] QuizForUpsert quiz, CancellationToken cancellationToken)
    {
        var result = await _quizHandler
            .UpdateQuiz(id, quiz, cancellationToken);

        return result.IsT1
            ? result.HandleError(this)
            : Ok(result.AsT0);
    }
}