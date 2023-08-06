// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Models.Entities;

namespace QuizCraft.Application;

public static class QuizzesStub
{
    public static IEnumerable<Quiz> Records = new List<Quiz>
    {
        new Quiz(
            id: 1,
            categories: new List<Category>() { new(1, "Geography", "") } ,
            description:"Test your knowledge of world capitals!",
            questions:new List<BaseQuestion>
            {
                new MultipleOptionQuestion(
                    id: 1,
                    text: "What is the capital of France?",
                    options: new List<string> {
                        "London", "Paris", "Berlin", "Madrid" },
                    correctAnswer: "Paris"
                ),
                new MultipleOptionQuestion(
                    id: 2,
                    text: "What is the capital of Japan?",
                    options: new List<string> { "Tokyo", "Beijing", "Seoul", "Bangkok" },
                    correctAnswer: "Tokyo"
                )
            }
        ),
        new Quiz(
            id: 2,
            categories: new List<Category>() { new(1, "Art", "") } ,
            description: "Test your knowledge of famous paintings!",
            questions: new List<BaseQuestion>
            {
                new MultipleOptionQuestion(
                    id: 3,
                    text: "Who painted the Mona Lisa?",
                    options: new List<string> {
                        "Pablo Picasso", "Vincent van Gogh", "Leonardo da Vinci", "Michelangelo" },
                    correctAnswer: "Leonardo da Vinci"
                ),
                new MultipleOptionQuestion(
                    id: 4,
                    text: "Who painted The Starry Night?",
                    options: new List<string> {
                        "Claude Monet", "Vincent van Gogh", "Salvador Dalí", "Pablo Picasso" },
                    correctAnswer: "Vincent van Gogh"
                )
            }
        )
    };
}
