// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using ModelContextProtocol;
using ModelContextProtocol.Server;
using OneOf;
using QuizCraft.Application.Categories;
using QuizCraft.Application.Quizzes;
using QuizCraft.Models;
using QuizCraft.Models.DTOs;
using System.ComponentModel;

namespace QuizCraft.Api.Mcp;

[McpServerToolType]
public static class QuizTools
{
    [McpServerTool(Name = "list_categories", ReadOnly = true)]
    [Description("Lists all quiz categories with their ids, names and descriptions.")]
    public static async Task<IEnumerable<CategoryForDisplay>> ListCategories(
        ICategoryHandler categoryHandler, CancellationToken cancellationToken)
    {
        return await categoryHandler.RetrieveCategories(cancellationToken);
    }

    [McpServerTool(Name = "list_quizzes", ReadOnly = true)]
    [Description("Lists all quizzes, including their categories and questions.")]
    public static async Task<IEnumerable<QuizForDisplay>> ListQuizzes(
        IQuizHandler quizHandler, CancellationToken cancellationToken)
    {
        return await quizHandler.RetrieveQuizzes(cancellationToken);
    }

    [McpServerTool(Name = "get_quiz", ReadOnly = true)]
    [Description("Gets one quiz by id, including its categories and questions.")]
    public static async Task<QuizForDisplay> GetQuiz(
        IQuizHandler quizHandler,
        [Description("The id of the quiz.")] int id,
        CancellationToken cancellationToken)
    {
        return Unwrap(await quizHandler.RetrieveQuiz(id, cancellationToken));
    }

    // Surfaces a RequestError as a tool error the model can read and recover from.
    private static T Unwrap<T>(OneOf<T, RequestError> result)
    {
        return result.IsT0
            ? result.AsT0
            : throw new McpException(
                $"{result.AsT1.StatusCode}: {result.AsT1.Message}");
    }
}
