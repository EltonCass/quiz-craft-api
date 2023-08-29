// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.AspNetCore.Mvc;
using QuizCraft.Api.Helpers;
using QuizCraft.Application.Quizzes.Questions;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Api.Quizzes.Questions;

[ApiController]
[Route("api/v{version:apiVersion}/quizzes/{quizId}/questions")]
[ApiVersion("1.0")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionHandler _questionHandler;

    public QuestionsController(
        IQuestionHandler questionHandler)
    {
        ArgumentNullException.ThrowIfNull(questionHandler, nameof(questionHandler));
        _questionHandler = questionHandler;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<QuestionForDisplay>), 200)]
    public async Task<ActionResult<IEnumerable<QuestionForDisplay>>> GetQuestions(
        [FromRoute] int quizId, CancellationToken cancellationToken)
    {
        var result = await _questionHandler
            .RetrieveQuestions(quizId, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{questionId}")]
    [ProducesResponseType(typeof(QuestionForDisplay), 200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<QuestionForDisplay>> GetQuestionById(
        [FromRoute] int quizId, [FromRoute] int questionId, CancellationToken cancellationToken)
    {
        var result = await _questionHandler
            .RetrieveQuestion(quizId, questionId, cancellationToken);

        return result.IsT0
            ? Ok(result.AsT0)
            : result.HandleError(this);
    }
}
