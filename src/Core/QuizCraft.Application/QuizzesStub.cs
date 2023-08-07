// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Models.Entities;

namespace QuizCraft.Application;

public static class Stubs
{
    public static List<Category> Categories = new List<Category>()
    {
        new(1, "Geography", ""),
        new(2, "Art", "")
    };

    public static List<Quiz> Quizzes = new List<Quiz>
    {
        new Quiz(
            id: 1,
            categories: Categories.Take(1),
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
            categories: Categories.Skip(1).Take(1) ,
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
