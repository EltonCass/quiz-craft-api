// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using QuizCraft.Api.Helpers;
using QuizCraft.Application.Quizzes.Questions;
using QuizCraft.Models.DTOs;

namespace QuizCraft.Api.Quizzes.Questions;

[ApiController]
[Route("api/v{version:apiVersion}/quizzes/{quizId:int}/multipleOptionQuestions")]
[ApiVersion("1.0")]
public class MultipleOptionQuestionsController : ControllerBase
{
    private const string _GetQuizByIdEndpointName = "GetQuiz";
    private readonly ISpecificQuestionHandler<MultipleOptionQuestionDTO> _multipleQuestionHandler;
    private readonly IMapper _Mapper;

    public MultipleOptionQuestionsController(
        ISpecificQuestionHandler<MultipleOptionQuestionDTO> multipleQuestionRepository,
        IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));
        ArgumentNullException.ThrowIfNull(multipleQuestionRepository, nameof(multipleQuestionRepository));

        _multipleQuestionHandler = multipleQuestionRepository;
        _Mapper = mapper;
    }

    [HttpPost()]
    [ProducesResponseType(typeof(MultipleOptionQuestionDTO), 201)]
    [ProducesResponseType(422)]
    public async Task<ActionResult<MultipleOptionQuestionDTO>> PostQuestion(
        [FromRoute]int quizId,
        [FromBody] MultipleOptionQuestionDTO question,
        CancellationToken cancellationToken)
    {
        return await CreateMultipleOptionQuestion(quizId, question, cancellationToken);
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
}
