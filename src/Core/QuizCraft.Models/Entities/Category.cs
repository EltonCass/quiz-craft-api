// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.Entities;

public class Category : IEntity
{
    public Category()
    {
        Quizzes = new List<Quiz>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<Quiz> Quizzes { get; set; }
}