// Copyright (c)  Elton Cassas. All rights reserved.
// See LICENSE.txt

using Microsoft.AspNetCore.Mvc;
using QuizCraft.Api.Collections;
using QuizCraft.Api.Models;

namespace QuizCraft.Api.QuizManagement
{
    [ApiController]
    [Route("[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly ILogger<QuizController> _logger;
        private readonly IQuizGeneration _quizGenerationService;

        public QuizController(
            ILogger<QuizController> logger,
            IQuizGeneration quizGeneration)
        {
            _logger = logger;
            _quizGenerationService = quizGeneration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Quiz>> GetQuizzes()
        {
            return Ok(QuizzesStub.Records);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Quiz>> GetQuiz(int id)
        {
            var foundedRecord = QuizzesStub.Records.FirstOrDefault(r => r.Id == id);
            if (foundedRecord is not null)
            {
                return Ok(foundedRecord);
            }

            _logger.Log(LogLevel.Information, $"Quiz with Id {id} not found");
            return NoContent();
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
    }
}