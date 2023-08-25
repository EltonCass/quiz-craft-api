// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

using QuizCraft.Models.DTOs;

namespace QuizCraft.Application;

public static class Stubs
{
    public static List<CategoryForDisplay> Categories = new List<CategoryForDisplay>()
    {
        new(1, "Geography", ""),
        new(2, "Art", "")
    };

    public static List<QuizForDisplay> Quizzes = new List<QuizForDisplay>
    {
        new QuizForDisplay(
            id: 1,
            categories: Categories.Take(1),
            title: "World Capitals",
            description:"Test your knowledge of world capitals!",
            createdAt: DateTime.Now,
            questions:new List<QuestionForDisplay>
            {
                new MultipleOptionQuestionDTO(
                    Id: 1,
                    QuizId: 1,
                    Text: "What is the capital of France?",
                    Options: new List<string> {
                        "London", "Paris", "Berlin", "Madrid" },
                    CorrectAnswer: "Paris"
                ),
                new MultipleOptionQuestionDTO(
                    Id: 2,
                    QuizId: 1,
                    Text: "What is the capital of Japan?",
                    Options: new List<string> { "Tokyo", "Beijing", "Seoul", "Bangkok" },
                    CorrectAnswer: "Tokyo"
                ),  
                new FillInBlankQuestionDTO(
                    Id: 5,
                    QuizId: 1,
                    Text: "The capital of Japan is _____",
                    CorrectAnswer: "Tokyo",
                    Position: 24
                )
            }
        ),
        new QuizForDisplay(
            id: 2,
            categories: Categories.Skip(1).Take(1) ,
            title: "Famous paintings",
            description: "Test your knowledge of famous paintings!",
            createdAt: DateTime.Now,
            questions: new List<QuestionForDisplay>
            {
                new MultipleOptionQuestionDTO(
                    Id: 3,
                    QuizId: 2,
                    Text: "Who painted the Mona Lisa?",
                    Options: new List<string> {
                        "Pablo Picasso", "Vincent van Gogh", "Leonardo da Vinci", "Michelangelo" },
                    CorrectAnswer: "Leonardo da Vinci"
                ),
                new MultipleOptionQuestionDTO(
                    Id: 4,
                    QuizId: 2,
                    Text: "Who painted The Starry Night?",
                    Options: new List<string> {
                        "Claude Monet", "Vincent van Gogh", "Salvador Dalí", "Pablo Picasso" },
                    CorrectAnswer: "Vincent van Gogh"
                )
            }
        )
    };
}
