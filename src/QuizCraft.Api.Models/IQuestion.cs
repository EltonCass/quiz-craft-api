// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Api.Models
{
    public interface IQuestion
    {
        string CorrectAnswer { get; }
        int Score { get; }
        string Text { get; }
    }
}