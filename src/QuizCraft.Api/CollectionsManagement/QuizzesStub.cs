// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Api.Models;

namespace QuizCraft.Api.Collections
{
    public static class QuizzesStub
    {
        public static IEnumerable<Quiz> Records = new List<Quiz>
        {
            new Quiz(
                id: 1,
                category:"Geography",
                description:"Test your knowledge of world capitals!",
                questions:new List<IQuestion>
                {
                    new MultipleOptionQuestion(
                        text: "What is the capital of France?",
                        options: new List<string> {
                            "London", "Paris", "Berlin", "Madrid" },
                        correctAnswer: "Paris"
                    ),
                    new MultipleOptionQuestion(
                        text: "What is the capital of Japan?",
                        options: new List<string> { "Tokyo", "Beijing", "Seoul", "Bangkok" },
                        correctAnswer: "Tokyo"
                    )
                }
            ),
            new Quiz(
                id: 2,
                category: "Art",
                description: "Test your knowledge of famous paintings!",
                questions: new List<IQuestion>
                {
                    new MultipleOptionQuestion(
                        text: "Who painted the Mona Lisa?",
                        options: new List<string> {
                            "Pablo Picasso", "Vincent van Gogh", "Leonardo da Vinci", "Michelangelo" },
                        correctAnswer: "Leonardo da Vinci"
                    ),
                    new MultipleOptionQuestion(
                        text: "Who painted The Starry Night?",
                        options: new List<string> {
                            "Claude Monet", "Vincent van Gogh", "Salvador Dalí", "Pablo Picasso" },
                        correctAnswer: "Vincent van Gogh"
                    )
                }
            )
        };
    }
}
