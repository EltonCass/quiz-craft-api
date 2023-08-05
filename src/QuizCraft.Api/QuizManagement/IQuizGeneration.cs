// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Models;

namespace QuizCraft.Api.QuizManagement;

public interface IQuizGeneration
{
    Task<string> GenerateMultipleOptionQuizQuestion(
        MultipleOptionRequestPrompt prompt, CancellationToken token);
}
