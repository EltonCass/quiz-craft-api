// Copyright (c)  Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.AspNetCore.Mvc;
using QuizCraft.Application.QuizManagement;
using QuizCraft.Application.QuizManagement.QuestionManagement;
using QuizCraft.Models;
using QuizCraft.Models.Entities;

namespace QuizCraft.Api.QuizManagement;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class QuizzesController : ControllerBase
{
    private readonly ILogger<QuizzesController> _logger;
    private readonly IQuizRepository _quizRepository;
    private readonly IQuestionRepository _questionRepository;

    public QuizzesController(
        ILogger<QuizzesController> logger,
        IQuizRepository quizRepository,
        IQuestionRepository questionRepository)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(quizRepository, nameof(quizRepository));
        ArgumentNullException.ThrowIfNull(questionRepository, nameof(questionRepository));
        _logger = logger;
        _quizRepository = quizRepository;
        _questionRepository = questionRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes(CancellationToken cancellationToken)
    {
        return Ok(await _quizRepository.RetrieveQuizzes(cancellationToken));
    }

    [HttpGet("{id}")]
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
    public async Task<ActionResult<IEnumerable<BaseQuestion>>> GetQuizQuestions(
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
    public async Task<ActionResult<BaseQuestion>> GetQuizQuestionById(
        int quizId, int questionId, CancellationToken cancellationToken)
    {
        var result = await _questionRepository.RetrieveQuestion(quizId, questionId, cancellationToken);

        if (result.IsT0)
        {
            return Ok(result.AsT0);
        }

        return result.HandleError(this);
    }
}