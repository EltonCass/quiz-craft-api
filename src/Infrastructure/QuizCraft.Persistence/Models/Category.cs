using System;
using System.Collections.Generic;

namespace QuizCraft.Persistence.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<CategoriesQuiz> CategoriesQuizzes { get; set; } = new List<CategoriesQuiz>();
}
