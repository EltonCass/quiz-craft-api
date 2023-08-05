// Copyright (c)  Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.AspNetCore.Mvc;
using QuizCraft.Api.CollectionsManagement;
using QuizCraft.Models;
using QuizCraft.Models.Entities;

namespace QuizCraft.Api.QuizManagement;

[ApiController]
[Route("api/[controller]")]
public class QuizzesController : ControllerBase
{
    private readonly ILogger<QuizzesController> _logger;
    private readonly IQuizGeneration _quizGenerationService;

    public QuizzesController(
        ILogger<QuizzesController> logger,
        IQuizGeneration quizGeneration)
    {
        _logger = logger;
        _quizGenerationService = quizGeneration;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Quiz>> GetQuizzes(CancellationToken cancellationToken)
    {
        if (QuizzesStub.Records.Any())
        {
            return Ok(QuizzesStub.Records);
        }
        return NoContent();
    }

    [HttpGet("{id}")]
    public ActionResult<IEnumerable<Quiz>> GetQuiz(
        int id, CancellationToken cancellationToken)
    {
        var foundedRecord = QuizzesStub.Records.FirstOrDefault(r => r.Id == id);
        if (foundedRecord is not null)
        {
            return Ok(foundedRecord);
        }

        _logger.Log(LogLevel.Information, $"Quiz with Id {id} not found");
        return NotFound();
    }

    [HttpPost("multiple-option/suggestion")]
    public async Task<ActionResult<MultipleOptionResponse>> MakeMultipleOptionQuizSuggestion(
        [FromBody]MultipleOptionRequestPrompt prompt, CancellationToken token)
    {
        var response = await _quizGenerationService
            .GenerateMultipleOptionQuizQuestion(prompt, token);
        return Ok(new MultipleOptionResponse(
            QuestionContent: response,
            Options: new List<string> { "O1" }
        ));
    }

    [HttpGet("{id}/questions")]
    public ActionResult<IEnumerable<BaseQuestion>> GetQuizQuestions(
        int id, CancellationToken cancellationToken)
    {
        var foundedQuiz = QuizzesStub.Records.FirstOrDefault(q => q.Id == id);
        if (foundedQuiz is null)
        {
            return NotFound();
        }

        return Ok(foundedQuiz.Questions);
    }

    [HttpGet("{quizId}/questions/{questionId}")]
    public ActionResult<IEnumerable<BaseQuestion>> GetQuizQuestionById(
        int quizId, int questionId, CancellationToken cancellationToken)
    {
        var foundedQuiz = QuizzesStub.Records
            .FirstOrDefault(q => q.Id == quizId);
        if (foundedQuiz is null)
        {
            return NotFound();
        }

        var foundedQuestion = foundedQuiz.Questions
            .FirstOrDefault(q => q.Id == questionId);
        if (foundedQuestion is null)
        {
            return NotFound();
        }

        return Ok(foundedQuestion);
    }
}