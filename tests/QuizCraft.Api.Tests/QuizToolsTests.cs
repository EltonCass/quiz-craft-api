// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using FluentAssertions;
using ModelContextProtocol;
using NSubstitute;
using OneOf;
using QuizCraft.Api.Mcp;
using QuizCraft.Application.Quizzes;
using QuizCraft.Models;
using QuizCraft.Models.DTOs;
using System.Net;

namespace QuizCraft.Api.Tests;

public class QuizToolsTests
{
    private readonly IQuizHandler _handler = Substitute.For<IQuizHandler>();

    [Fact]
    public async Task GetQuiz_WhenQuizExists_ReturnsQuiz()
    {
        var quiz = new QuizForDisplay(
            1, new List<CategoryForDisplay>(), "desc", "title", new List<QuestionForDisplay>(), (DateTime?)null);
        _handler.RetrieveQuiz(1, default)
            .Returns(OneOf<QuizForDisplay, RequestError>.FromT0(quiz));

        var result = await QuizTools.GetQuiz(_handler, 1, default);

        result.Should().Be(quiz);
    }

    [Fact]
    public async Task GetQuiz_WhenQuizDoesNotExist_ThrowsMcpExceptionWithStatus()
    {
        _handler.RetrieveQuiz(7, default)
            .Returns(OneOf<QuizForDisplay, RequestError>.FromT1(
                new RequestError(HttpStatusCode.NotFound, "Quiz not found")));

        var act = () => QuizTools.GetQuiz(_handler, 7, default);

        (await act.Should().ThrowAsync<McpException>())
            .WithMessage("*NotFound*Quiz not found*");
    }
}
