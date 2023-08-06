// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.AspNetCore.Mvc;
using QuizCraft.Api.QuizManagement;
using QuizCraft.Models;

namespace QuizCraft.Api.PromptManagement;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class QuizPromptSuggestionsController : ControllerBase
{
    private readonly IQuizGeneration _quizGenerationService;

    public QuizPromptSuggestionsController(IQuizGeneration quizGeneration)
    {
        ArgumentNullException.ThrowIfNull(quizGeneration, nameof(quizGeneration));
        _quizGenerationService = quizGeneration;
    }

    [HttpPost("multiple-option/suggestion")]
    public async Task<ActionResult<MultipleOptionResponse>> MakeMultipleOptionQuizSuggestion(
    [FromBody] MultipleOptionRequestPrompt prompt, CancellationToken token)
    {
        var response = await _quizGenerationService
            .GenerateMultipleOptionQuizQuestion(prompt, token);
        return Ok(new MultipleOptionResponse(
            QuestionContent: response,
            Options: new List<string> { "O1" }
        ));
    }
}
