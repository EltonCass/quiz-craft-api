// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Api.Models
{
    public struct MultipleOptionQuestion : IQuestion
    {
        public string Text { get; }
        public IReadOnlyList<string> Options { get; }
        public string CorrectAnswer { get; }
        public int Score { get; }

        public MultipleOptionQuestion(string text, IEnumerable<string> options, string correctAnswer, int score = 10)
        {
            Text = text;
            Options = new List<string>(options);
            CorrectAnswer = correctAnswer;
            Score = score;
        }
    }
}
