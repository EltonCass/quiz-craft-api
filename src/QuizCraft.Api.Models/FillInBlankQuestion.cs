// Copyright (c)  Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Api.Models
{
    public class FillInBlankQuestion : IQuestion
    {
        public string Text { get; }
        public string CorrectAnswer { get; }
        public int Score { get; }

        public FillInBlankQuestion(
            string text, string correctAnswer, int score = 10)
        {
            Text = text;
            CorrectAnswer = correctAnswer;
            Score = score;
        }
    }
}
