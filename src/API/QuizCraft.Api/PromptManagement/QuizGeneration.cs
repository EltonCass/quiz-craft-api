// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using QuizCraft.Api.QuizManagement;
using QuizCraft.Models;

namespace QuizCraft.Api.PromptManagement;

public class QuizGeneration : IQuizGeneration
{
    private readonly OpenAIAPI _openApi;

    public QuizGeneration(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        var key = configuration["OpenAPIKey"];
        _openApi = new OpenAIAPI(key);
    }

    public async Task<string> GenerateMultipleOptionQuizQuestion(
        MultipleOptionRequestPrompt prompt, CancellationToken token)
    {
        var chat = await _openApi.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1,
            MaxTokens = 50,
            Messages = new ChatMessage[]
            {
                new ChatMessage(ChatMessageRole.User, prompt.PromptText),
            },
        });

        var reply = chat.Choices[0].Message;
        return reply.Content;
    }
}
