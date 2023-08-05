// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using QuizCraft.Models;

namespace QuizCraft.Api.QuizManagement;

public class QuizGeneration : IQuizGeneration
{
    private readonly string _key;
    private readonly OpenAIAPI _openApi;

    public QuizGeneration(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
        _key = configuration["OpenAPIKey"]!;
        _openApi = new OpenAIAPI(_key);
    }

    public async Task<string> GenerateMultipleOptionQuizQuestion(
        MultipleOptionRequestPrompt prompt, CancellationToken token)
    {
        var chat = await _openApi.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1,
            MaxTokens = 50,
            Messages = new ChatMessage[] {
                new ChatMessage(ChatMessageRole.User, prompt.PromptText)
            }
        });

        var reply = chat.Choices[0].Message;
        return reply.Content;
    }
}
