// Copyright (c) 2023 Elton Cassas. All rights reserved.
// See LICENSE.txt

namespace QuizCraft.Models.Entities;

public class Category
{
    public Category()
    {
        CategoryQuizzes = new List<CategoriesQuiz>();
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<CategoriesQuiz> CategoryQuizzes { get; set; }
}