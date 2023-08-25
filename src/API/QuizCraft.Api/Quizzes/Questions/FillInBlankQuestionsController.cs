// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.AspNetCore.Mvc;
using QuizCraft.Api.Helpers;
using QuizCraft.Application.Quizzes.Questions;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Api.Quizzes.Questions;

[ApiController]
[Route("api/v{version:apiVersion}/quizzes/{quizId}/fillInBlankQuestions")]
[ApiVersion("1.0")]
public class FillInBlankQuestionsController : ControllerBase
{
    private const string _GetQuizByIdEndpointName = "GetQuiz";
    private readonly ISpecificQuestionHandler<FillInBlankQuestionDTO> _fillInBlankQuestionHandler;

    public FillInBlankQuestionsController(ISpecificQuestionHandler<FillInBlankQuestionDTO> fillInBlankQuestionRepository)
    {
        ArgumentNullException.ThrowIfNull(fillInBlankQuestionRepository, nameof(fillInBlankQuestionRepository));
        _fillInBlankQuestionHandler = fillInBlankQuestionRepository;
    }

    [HttpPost()]
    [ProducesResponseType(typeof(FillInBlankQuestionDTO), 201)]
    [ProducesResponseType(422)]
    public async Task<ActionResult<FillInBlankQuestionDTO>> PostQuestion(
        [FromRoute] int quizId,
        [FromBody] FillInBlankQuestionDTO question,
        CancellationToken cancellationToken)
    {
        return await CreateFillInBlankOptionQuestion(quizId, question, cancellationToken);
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
